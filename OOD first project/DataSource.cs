using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_first_project
{
    public class DataSource
    {
        public event EventHandler<IDUpdateArgs> OnIDUpdate;
        public event EventHandler<PositionUpdateArgs> OnPositionUpdate;
        public event EventHandler<ContactInfoUpdateArgs> OnContactInfoUpdate;

      
        public void SimulateUpdates()
        {
            OnIDUpdate?.Invoke(this, new IDUpdateArgs { ObjectID = 1, NewObjectID = 2 });
            OnPositionUpdate?.Invoke(this, new PositionUpdateArgs { ObjectID = 1, Longitude = -122.4194f, Latitude = 37.7749f, AMSL = 150 });
            OnContactInfoUpdate?.Invoke(this, new ContactInfoUpdateArgs { ObjectID = 1, PhoneNumber = "123-456-7890", EmailAddress = "update@example.com" });
        }
    }
}
