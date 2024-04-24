using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using NetworkSourceSimulator;

namespace OOD_first_project
{
    public class DataManager:iObserver
    {
        List<Data> dataObjects;

        public DataManager(List<Data> initialData)
        {
            dataObjects = initialData;
            File.AppendAllText(GetLogFileName(), "app started" + Environment.NewLine);
        }

       

        public void UpdateID(IDUpdateArgs e)
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

        public void UpdatePosition(PositionUpdateArgs e)
        {
            var item = dataObjects.OfType<Flight>().FirstOrDefault(x => x.ID == e.ObjectID);
            if (item != null)
            {
                LogBeforeChange(item);
                item.Longitude = e.Longitude;
                item.Latitute = e.Latitude;
                item.AMSL = e.AMSL;
                Console.WriteLine($"No AirPort found with ID {e.ObjectID}");
                LogAfterChange(item);
            }
            else
            {
                LogError($"No AirPort found with ID {e.ObjectID}");
            }
        }

        public void UpdateContactInfo(ContactInfoUpdateArgs e)
        {
            var item = dataObjects.OfType<Crew>().FirstOrDefault(x => x.ID == e.ObjectID);
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
            string logMessage = $"Before Update: {item.ID}";
            Console.WriteLine(logMessage);
            File.AppendAllText(GetLogFileName(), logMessage + Environment.NewLine);
        }
        private void LogBeforeChange(Crew item)
        {
            string logMessage = $"Before Update {item.ID}: Phone = {item.Phone}, email = {item.Email}";
            Console.WriteLine(logMessage);
            File.AppendAllText(GetLogFileName(), logMessage + Environment.NewLine);
        }
        private void LogBeforeChange(Flight item)
        {
            string logMessage = $"Before Update {item.ID} :item latitude =  {item.Latitute},longtude = {item.Longitude}";
            Console.WriteLine(logMessage);
            File.AppendAllText(GetLogFileName(), logMessage + Environment.NewLine);
        }

        private void LogAfterChange(Data item)
        {
            string logMessage = $"After Update: {item.ID}";
            Console.WriteLine(logMessage);
            File.AppendAllText(GetLogFileName(), logMessage + Environment.NewLine);
        }
        private void LogAfterChange(Crew item)
        {
            string logMessage = $"after Update {item.ID}: Phone = {item.Phone}, email = {item.Email}";
            Console.WriteLine(logMessage);
            File.AppendAllText(GetLogFileName(), logMessage + Environment.NewLine);
        }
        private void LogAfterChange(Flight item)
        {
            string logMessage = $"after Update {item.ID} :item latitude =  {item.Latitute},longtude = {item.Longitude}";
            Console.WriteLine(logMessage);
            File.AppendAllText(GetLogFileName(), logMessage + Environment.NewLine);
        }


        private void LogError(string message)
        {
            File.AppendAllText(GetLogFileName(), $"Error: {message}" + Environment.NewLine);
        }

        private string GetLogFileName()
        {
            return $"LogSomething{DateTime.Now:yyyyMMdd}.txt";
        }
    }
}
