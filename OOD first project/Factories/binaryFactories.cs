using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace OOD_first_project.Factories
{
    public static class BinaryFactory
    {
        public static Dictionary<string, Func<byte[], Data>> BinaryDic = new Dictionary<string, Func<byte[], Data>>()
        {
            { "NCR", CrewFactory.newCrew },
            { "NPA",PassengerFactory.newPassenger },
            { "NCA",CargoFactory.newCargoFactory },
            { "NCP",CargoPlaneFactory.newCargoPlaneFactory},
            { "NPP",PassengerPlaneFactory.NewPassengerPlane },
            { "NAI",AirProtFactory.newAirPort},
            { "NFL",FlightFactory.NewFlight },
        };


    }
}
