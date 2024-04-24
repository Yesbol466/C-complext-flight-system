using FlightTrackerGUI;
using System.Timers;

namespace OOD_first_project
{

    public class GUIAdapter : IGUIUpdater
    {


        public void UpdateGUIPeriodically(List<Data> datas)
        {
            var allData = datas;


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
            var currentPosition = CalculateCurrentPosition(flight,origin, destination, flight.TakeoffTime, flight.LandingTime);

            flight.Latitute = currentPosition.X;
            flight.Longitude = currentPosition.Y;
        }
        private static (float Y, float X) CalculateCurrentPosition(Flight flight,AirPort origin, AirPort destination, string takeoffTime, string landingTime)
        {
            DateTime takeoff = DateTime.Parse(takeoffTime);
            DateTime landing = DateTime.Parse(landingTime);
            DateTime now = DateTime.UtcNow;
            if (landing < takeoff)
            {
                landing.AddDays(1);
            }

            if (now < takeoff) return (origin.Longitude, origin.Latitude);
            if (now > landing) return (destination.Longitude, destination.Latitude);
            
            double totalFlightDuration = (landing - takeoff).TotalSeconds;
            double elapsedFlightDuration = (now - takeoff).TotalSeconds;
            double progress = elapsedFlightDuration / totalFlightDuration;
            var remainingTime = (landing - now).TotalSeconds;
            double currentLongitude = flight.Longitude + (destination.Longitude - flight.Longitude) / remainingTime;
            double currentLatitude = flight.Latitute + (destination.Latitude - flight.Latitute) /remainingTime;

            return ((float)currentLongitude, (float)currentLatitude);
        }



    }
}