using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_first_project
{
    public abstract class NewsProvider
    {
        public string Name { get; }

        protected NewsProvider(string name) => Name = name;

    }

    public class Television : NewsProvider, INewsVisitor
    {
        public Television(string name) : base(name) { }

        public string Visit(AirPort airPort)
        {
            return $"<An image of {airPort.Name} airport>";
        }
        public string Visit(PassengerPlane passengerPlane)
        {
            return $"<An image of {passengerPlane.Name} passenger plane>";
        }
        public string Visit(CargoPlane cargoPlane)
        {
            return $"<An image of {cargoPlane.Name} cargo plane>";
        }
      
        
    }

    public class Radio : NewsProvider, INewsVisitor
    {
        public Radio(string name) : base(name) { }
        public string Visit(AirPort airPort)
        {
            return $"Reporting for {Name}, Ladies and Gentlemen, we are at the {airPort.Name} airport.";
        }
        public string Visit(PassengerPlane passengerPlane)
        {
            return $"Reporting for {Name}, Ladies and Gentlemen, we’ve just witnessed {passengerPlane.Serial} take off.";
        }
        public string Visit(CargoPlane cargoPlane)
        {
            return $"Reporting for {Name}, Ladies and Gentlemen, we are seeing the {cargoPlane.Serial} aircraft fly above us.";
        }

        
    }

    public class Newspaper : NewsProvider, INewsVisitor
    {
        public Newspaper(string name) : base(name) { }
        public string Visit(AirPort airPort)
        {
            return $"{Name} - A report from the {airPort.Name} airport, {airPort.Country}.";
        }
        public string Visit(PassengerPlane passengerPlane)
        {
            return $"{Name} - Breaking news! {passengerPlane.Model} aircraft loses EASA certification after inspection of {passengerPlane.Serial}.";
        }
        public string Visit(CargoPlane cargoPlane)
        {
            return $"{Name} - An interview with the crew of {cargoPlane.Serial}.";
        }
        
    }

}
