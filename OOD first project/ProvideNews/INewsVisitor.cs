using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_first_project
{
    public interface INewsVisitor
    {
        string Visit(AirPort airPort);
        string Visit(PassengerPlane passengerPlane);
        string Visit(CargoPlane cargoPlane);
    }
}
