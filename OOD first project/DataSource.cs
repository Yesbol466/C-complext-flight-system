using NetworkSourceSimulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_first_project
{
   public interface IObservable
    {
        void AddObserver(iObserver observer);
        void RemoveObserver(iObserver observer);
        void NotifyIDchange(object sender,IDUpdateArgs iDUpdateArgs);
        void NotifyPositionChange(object sender,PositionUpdateArgs positionUpdateArgs);
        void NotifyContactInfoChange(object sender, ContactInfoUpdateArgs contactInfoUpdateArgs);
    }
    public interface iObserver
    {
        void UpdateID(IDUpdateArgs iDUpdateArgs);
        void UpdatePosition(PositionUpdateArgs positionUpdateArgs);
        void UpdateContactInfo(ContactInfoUpdateArgs contactInfoUpdateArgs);
    }
}
