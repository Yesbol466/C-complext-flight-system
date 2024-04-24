using FlightTrackerGUI;
using OOD_first_project.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OOD_first_project
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //part1
            //FileReader FR = new FileReader();
            //var result = FR.ReadFromFile("example_data.ftr");
            //Serializer SR = new Serializer(result);
            //SR.Serializ("app.json");
            //part2
            //Server server = new Server("example_data.ftr");
            //server.ReadFile();
            //par3

            //adapter.UpdateGUIPeriodicallyInStream();
            //GUIUpdater.UpdateGUIPeriodically();
            //GUIUpdater.UpdateGUIPeriodicallyInStream();
            //Console.WriteLine("GUI is running. Press any key to exit...");

            //Console.ReadKey();
            //if (thread.IsAlive)
            //{
            //    thread.Interrupt();
            //    thread.Join();
            //}
            //part4
            //string filePath = "example_data.ftr";
            //var reportables = NewsGenerator.LoadReportablesFromFTR(filePath);

            //// Instantiate your news providers
            //var newsProviders = new List<INewsVisitor>
            //{
            //    new Television("TV News Channel"),
            //    new Radio("Radio News Channel"),
            //    new Newspaper("The Daily News")
            //};
            //NewsGenerator.NewsCommands(newsProviders, reportables);
            //part5
            FileReader fileReader = new FileReader();
            List<Data> initialData = fileReader.ReadFromFile("example_data.ftr");
            if (initialData == null || initialData.Count == 0)
            {
                Console.WriteLine("No initial data found. Exiting program.");
                return;
            }
            DataManager dataManager = new DataManager(initialData);// Ensure you provide the correct file path

            Thread thread = new Thread(new ThreadStart(Runner.Run));
            thread.Start();
            GUIAdapter adapter = new GUIAdapter();
            Thread thread1 = new Thread(()=> adapter.UpdateGUIPeriodically(initialData));
            thread1.Start();
            Thread.Sleep(5000);

            // Setup the server with the path to simulation data and the DataSource
            string simulatorDataPath = "example.ftre";
            Server server = new Server(simulatorDataPath);
            server.AddObserver(dataManager);
            server.StartServer();

            Console.WriteLine("Simulation is running. Press any key to exit...");
            Console.ReadKey();


        }
    }
}
