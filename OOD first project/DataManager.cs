using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace OOD_first_project
{
    public class DataManager
    {
        List<Data> dataObjects;

        public DataManager(List<Data> initialData)
        {
            dataObjects = initialData ?? new List<Data>(); 
        }

        public void SubscribeToDataSourceEvents(DataSource dataSource)
        {
            dataSource.OnIDUpdate += HandleIDUpdate;
            dataSource.OnPositionUpdate += HandlePositionUpdate;
            dataSource.OnContactInfoUpdate += HandleContactInfoUpdate;
        }

        private void HandleIDUpdate(object sender, IDUpdateArgs e)
        {
            var item = dataObjects.FirstOrDefault(x => x.ID == e.ObjectID);
            if (item != null)
            {
                LogBeforeChange(item);
                item.ID = e.NewObjectID;
                LogAfterChange(item);
            }
            else
            {
                LogError($"No object found with ID {e.ObjectID}");
            }
        }

        private void HandlePositionUpdate(object sender, PositionUpdateArgs e)
        {
            var item = dataObjects.OfType<AirPort>().FirstOrDefault(x => x.ID == e.ObjectID);
            if (item != null)
            {
                LogBeforeChange(item);
                item.Longitude = e.Longitude;
                item.Latitude = e.Latitude;
                item.AMSL = e.AMSL;
                LogAfterChange(item);
            }
            else
            {
                LogError($"No AirPort found with ID {e.ObjectID}");
            }
        }

        private void HandleContactInfoUpdate(object sender, ContactInfoUpdateArgs e)
        {
            var item = dataObjects.OfType<Passenger>().FirstOrDefault(x => x.ID == e.ObjectID);
            if (item != null)
            {
                LogBeforeChange(item);
                item.Phone = e.PhoneNumber;
                item.Email = e.EmailAddress;
                LogAfterChange(item);
            }
            else
            {
                LogError($"No Passenger found with ID {e.ObjectID}");
            }
        }

        private void LogBeforeChange(Data item)
        {
            string logMessage = $"Before Update: {item}";
            Console.WriteLine(logMessage);
            File.AppendAllText(GetLogFileName(), logMessage + Environment.NewLine);
        }

        private void LogAfterChange(Data item)
        {
            string logMessage = $"After Update: {item}";
            Console.WriteLine(logMessage);
            File.AppendAllText(GetLogFileName(), logMessage + Environment.NewLine);
        }

        private void LogError(string message)
        {
            File.AppendAllText(GetLogFileName(), $"Error: {message}" + Environment.NewLine);
        }

        private string GetLogFileName()
        {
            return $"Log_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
        }
    }
}
