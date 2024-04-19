using FlightTrackerGUI;
using System.Timers;

namespace OOD_first_project
{

    public class GUIAdapter : IGUIUpdater
    {


        public void UpdateGUIPeriodically()
        {
            var allData = new FileReader().ReadFromFile("example_data.ftr");


            var airports = HelperMethods.LoadAirports(allData);
            var flights = HelperMethods.LoadFlights(allData);

            while (true)
            {
                FlightsGUIData flightsGUIData = new FlightsGUIData();
                var list = new List<FlightGUI>();
                foreach (var flight in flights)
                {
                    if (DateTime.UtcNow >= DateTime.Parse(flight.LandingTime))
                        continue;
                    if (DateTime.UtcNow < DateTime.Parse(flight.TakeoffTime))
                        continue;
                    updateFlightOnMap(flight, airports);
                    var flightGUI = FlightDataConverter.ConvertToFlightGUI(flight, airports);


                    list.Add(flightGUI);
                }

                flightsGUIData.UpdateFlights(list);
                Runner.UpdateGUI(flightsGUIData);
                list.Clear();
                Thread.Sleep(1000);
            }
        }
        public void UpdateGUIPeriodicallyInStream()
        {
            var allDatabin = new Server("example_data.ftr").ReadFile();
            var airportsinStream = HelperMethods.LoadAirports(allDatabin);
            var flightsinStream = HelperMethods.LoadFlights(allDatabin);
            while (true)
            {
                FlightsGUIData flightsGUIData = new FlightsGUIData();
                var list = new List<FlightGUI>();
                foreach (var flight in flightsinStream)
                {
                    if (DateTime.UtcNow >= DateTime.Parse(flight.LandingTime))
                        continue;
                    if (DateTime.UtcNow < DateTime.Parse(flight.TakeoffTime))
                        continue;
                    updateFlightOnMap(flight, airportsinStream);
                    var flightGUI = FlightDataConverter.ConvertToFlightGUI(flight, airportsinStream);


                    list.Add(flightGUI);
                }
                flightsGUIData.UpdateFlights(list);
                Runner.UpdateGUI(flightsGUIData);

                Thread.Sleep(1000);
            }
        }

        public void updateFlightOnMap(Flight flight, Dictionary<ulong, AirPort> airports)
        {
            var origin = airports[flight.OriginID];
            var destination = airports[flight.TargetID];
            var currentPosition = CalculateCurrentPosition(origin, destination, flight.TakeoffTime, flight.LandingTime);

            flight.Latitute = currentPosition.X;
            flight.Longitude = currentPosition.Y;
        }
        private static (float Y, float X) CalculateCurrentPosition(AirPort origin, AirPort destination, string takeoffTime, string landingTime)
        {
            DateTime takeoff = DateTime.Parse(takeoffTime);
            DateTime landing = DateTime.Parse(landingTime);
            DateTime now = DateTime.UtcNow;

            if (now < takeoff) return (origin.Longitude, origin.Latitude);
            if (now > landing) return (destination.Longitude, destination.Latitude);

            double totalFlightDuration = (landing - takeoff).TotalSeconds;
            double elapsedFlightDuration = (now - takeoff).TotalSeconds;
            double progress = elapsedFlightDuration / totalFlightDuration;

            double currentLongitude = origin.Longitude + (destination.Longitude - origin.Longitude) * progress;
            double currentLatitude = origin.Latitude + (destination.Latitude - origin.Latitude) * progress;

            return ((float)currentLongitude, (float)currentLatitude);
        }



    }
}