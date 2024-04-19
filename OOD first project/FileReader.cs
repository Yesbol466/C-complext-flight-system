using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_first_project
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Security.Cryptography.X509Certificates;

    public class FileReader
    {
        public Dictionary<string, Func<string[], Data>> objectFactory;

        public FileReader()
        {
            objectFactory = new Dictionary<string, Func<string[], Data>>()
        {
            { "C", parts => new Crew ( ulong.Parse(parts[1]),parts[2],ulong.Parse(parts[3]),parts[4],parts[5],ushort.Parse(parts[6]),parts[7]) },
            {"AI",parts=>new AirPort(ulong.Parse(parts[1]),parts[2],parts[3],float.Parse(parts[4]),float.Parse(parts[5]),float.Parse(parts[6]),parts[7]) },
                {"CA",parts=>new Cargo(ulong.Parse(parts[1]),float.Parse(parts[2]),parts[3],parts[4]) },
                {"CP",parts=>new CargoPlane(ulong.Parse(parts[1]),parts[2],parts[3],parts[4],float.Parse(parts[5])) },
                {"P",parts=> new Passenger(ulong.Parse(parts[1]),parts[2],ulong.Parse(parts[3]),parts[4],parts[5],parts[6],ulong.Parse(parts[7])) },
                {"PP",parts=>new PassengerPlane(ulong.Parse(parts[1]),parts[2],parts[3],parts[4],ushort.Parse(parts[5]),ushort.Parse(parts[6]),ushort.Parse(parts[7])) },
                {"FL",parts=>new Flight(ulong.Parse(parts[1]),ulong.Parse(parts[2]),ulong.Parse(parts[3]),parts[4],parts[5],float.Parse(parts[6]),float.Parse(parts[7]),float.Parse(parts[8]),ulong.Parse(parts[9]),StringConvert(parts[10]),StringConvert(parts[11]) )}
        };
        }
            public List<ulong> StringConvert( string str)
            {
                var numberStrings = str.Split(';','[',']'); 
                List<ulong> numberList = new List<ulong>();

                foreach (var numberString in numberStrings)
                {
                    if (ulong.TryParse(numberString, out ulong number))
                    {
                        numberList.Add(number); 
                    }
                }
                    return numberList;
            }

        public List<Data> ReadFromFile(string filePath)
        {
            var datas = new List<Data>();
            var lines = File.ReadAllLines(filePath);

            foreach (var line in lines)
            {
                var parts = line.Split(',');
                if (objectFactory.TryGetValue(parts[0], out var createObject))
                {
                    datas.Add(createObject(parts));
                }
            }

            return datas;
        }
        public List<Data> FileReadUpdate(string filePath)
        {
            List<Data> dataList = new List<Data>();
            try
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    string[] parts = line.Split(',');
                    Data data = ParseData(parts);
                    if (data != null)
                    {
                        dataList.Add(data);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file {filePath}: {ex.Message}");
            }
            return dataList;
        }
        private Data ParseData(string[] parts)
        {
            if (parts == null || parts.Length == 0)
            {
                Console.WriteLine("Empty or null line provided to ParseData");
                return null; 
            }

            string typeKey = parts[0]; 

            if (objectFactory.TryGetValue(typeKey, out Func<string[], Data> factory))
            {
                try
                {
                    return factory(parts);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error parsing data for type {typeKey}: {ex.Message}");
                    Console.WriteLine($"Data: {string.Join(", ", parts)}");
                    return null; 
                }
            }
            else
            {
                Console.WriteLine($"No factory found for type {typeKey}");
                return null;
            }
        }


    }

}
