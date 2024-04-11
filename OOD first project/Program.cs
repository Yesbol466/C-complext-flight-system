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
           
            //FileReader FR = new FileReader();
            //var result = FR.ReadFromFile("example_data.ftr");
            //Serializer SR = new Serializer(result);
            //SR.Serializ("app.json");
            //Server server = new Server("example_data.ftr");
            //server.ReadFile();
            //Thread thread = new Thread(new ThreadStart(Runner.Run));
            //thread.Start();
            //GUIAdapter adapter = new GUIAdapter();
            //adapter.UpdateGUIPeriodically();

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
            string filePath = "example_data.ftr";
            var reportables = NewsGenerator.LoadReportablesFromFTR(filePath);

            // Instantiate your news providers
            var newsProviders = new List<INewsVisitor>
            {
                new Television("TV News Channel"),
                new Radio("Radio News Channel"),
                new Newspaper("The Daily News")
            };
            NewsGenerator.NewsCommands(newsProviders, reportables);




        }
    }
}
