using FlightTrackerGUI;
using System;
using System.Collections.Generic;

namespace OOD_first_project
{
    public static class FlightDataConverter
    {
        public static FlightGUI ConvertToFlightGUI(Flight flight, Dictionary<ulong, AirPort> airports)
        {
            var origin = airports[flight.OriginID];
            var destination = airports[flight.TargetID];

            var rotation = CalculateRotation(flight, destination);
            return new FlightGUI
            {
                ID = flight.ID,
                WorldPosition = new WorldPosition(flight.Latitute, flight.Longitude),
                MapCoordRotation = rotation
            };
        }




        private static double CalculateRotation(Flight flight, AirPort destination)
        {
            double deltaY = destination.Latitude - flight.Latitute;
            double deltaX = destination.Longitude - flight.Longitude;

            double angleInRadians = Math.Atan2(deltaX, deltaY);
            return angleInRadians;
        }



    }
}