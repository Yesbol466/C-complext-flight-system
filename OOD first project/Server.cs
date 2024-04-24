using OOD_first_project.Factories;
using NetworkSourceSimulator;
using System.Text;
using System.Threading.Tasks;

namespace OOD_first_project
{
    public class Server :IObservable //observer that handles all handlers neededd
        //multiple interface
        //use of network source simulator
    {
        NetworkSourceSimulator.NetworkSourceSimulator Simulator;
        List<iObserver> Observers;
        public void StartServer()
        {
            Thread serverThread = new Thread(() =>
            {
                try
                {
                    Simulator.Run();
                }
                catch (ThreadInterruptedException)
                {
                    Console.WriteLine("Server exiting cleanly.");
                }
            });
            serverThread.Start();
        }
        public Server(string path)
        {
          this.Observers = new List<iObserver>();
          Simulator = new NetworkSourceSimulator.NetworkSourceSimulator(path,0,0);
            SubcribeToDataSourceEvents();
           
            
        }
        public void SubcribeToDataSourceEvents()
        {
         Simulator.OnContactInfoUpdate += NotifyContactInfoChange;
            Simulator.OnIDUpdate += NotifyIDchange;
            Simulator.OnPositionUpdate += NotifyPositionChange;
        }
        public void AddObserver(iObserver observer)
        {
            Observers.Add(observer);
        }
        public void RemoveObserver(iObserver observer)
        {
            Observers.Remove(observer);
        }
        public void NotifyIDchange(object sender,IDUpdateArgs iDUpdateArgs)
        {
            foreach (var observer in Observers)
            {
                observer.UpdateID(iDUpdateArgs);
            }
        }
        public void NotifyPositionChange(object sender,PositionUpdateArgs positionUpdateArgs)
        {
            foreach (var observer in Observers)
            {
                observer.UpdatePosition(positionUpdateArgs);
            }
        }
        public void NotifyContactInfoChange(object sender,ContactInfoUpdateArgs contactInfoUpdateArgs)
        {
            foreach (var observer in Observers)
            {
                observer.UpdateContactInfo(contactInfoUpdateArgs);
            }
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
