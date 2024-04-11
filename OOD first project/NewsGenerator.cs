using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_first_project
{
    public class NewsGenerator
    {
        private readonly List<INewsVisitor> _providers;
        private readonly List<IReportable> _reportables;
        

        public NewsGenerator(List<INewsVisitor> providers, List<IReportable> reportables)
        {
            _providers = providers ?? throw new ArgumentNullException(nameof(providers));
            _reportables = reportables ?? throw new ArgumentNullException(nameof(reportables));
        }
        public static List<IReportable> LoadReportablesFromFTR(string filePath)
        {
            var fileReader = new FileReader();
            var datas = fileReader.ReadFromFile(filePath);
            var reportables = new List<IReportable>();

            foreach (var data in datas)
            {
                if (data is IReportable reportable)
                {
                    reportables.Add(reportable);
                }
            }
            return reportables;
        }

        public IEnumerable<string> GenerateNextNews()
        {
           foreach (var provider in _providers)
            {
                foreach(var reportable in _reportables)
                {
                    yield return reportable.Accept(provider);
                }
            }
        }
        public static void NewsCommands(List<INewsVisitor> providers,List<IReportable> reportables)
        {
            var newsGenerator = new NewsGenerator(providers, reportables);

            string command;
            while ((command = Console.ReadLine()) != "exit")
            {
                if (command == "report")
                {
                    
                    foreach(var newsReport in newsGenerator.GenerateNextNews())
                    {
                        Console.WriteLine(newsReport);
                    }
                }
            }

            Console.WriteLine("All news reports have been generated.");

        }
    }
}

