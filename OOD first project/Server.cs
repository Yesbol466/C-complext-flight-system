using OOD_first_project.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_first_project
{
    public class Server//observer that handles all handlers neededd
        //multiple interface
        //use of network source simulator
    {
        NetworkSourceSimulator.NetworkSourceSimulator Simulator;
        public void StartServer()
        {
            try
            {
                Simulator.Run();
            }
            catch (ThreadInterruptedException)
            {
                Console.WriteLine("exiting");
            }
        }
        public Server(string path)
        {
            Simulator = new NetworkSourceSimulator.NetworkSourceSimulator(path,0,0);

        }
        public List<Data> ReadFile()
        {

            List<Data> list = new List<Data>();
            
            Simulator.OnNewDataReady += (sender, e) =>
            {
                var data = Simulator.GetMessageAt(e.MessageIndex);
                var id = Encoding.UTF8.GetString(data.MessageBytes, 0, 3);
                list.Add(BinaryFactory.BinaryDic[id](data.MessageBytes));
            };
            
            Thread thread = new Thread(new ThreadStart(StartServer));
            thread.Start();
            while (thread.IsAlive)
            {
                string check = Console.ReadLine();
                if (check == "print")
                {
                    Serializer serializer = new Serializer(list);
                    var hour = DateTime.Now.Hour;
                    var minute = DateTime.Now.Minute;
                    var second = DateTime.Now.Second;
                    serializer.Serializ($"snapshot{hour}_{minute}_{second}.json");
                    Console.WriteLine("wrote_to_file");
                }
                else if (check == "exit")
                {
                    thread.Interrupt();
                    break;
                }
            }
            thread.Join();
            return list;
        }
        
    }
}
