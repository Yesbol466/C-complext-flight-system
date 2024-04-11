using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_first_project
{
    public static class HelperMethods
    {
        public static List<Flight> LoadFlights(List<Data> datas)
        {

            var flights = new List<Flight>();
            foreach (var data in datas)
            {
                if (data is Flight flight)
                {
                    flights.Add(flight);
                }
            }
            return flights;
        }

        public static Dictionary<ulong, AirPort> LoadAirports(List<Data> datas)
        {

            var airports = new Dictionary<ulong, AirPort>();
            foreach (var data in datas)
            {
                if (data is AirPort airport)
                {
                    airports[airport.ID] = airport;
                }
            }
            return airports;
        }
    }
}