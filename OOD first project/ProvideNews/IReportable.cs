using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_first_project
{
    public interface IReportable
    {
        string Name { get; }
        string ReportIdentifier { get; }
        string Accept(INewsVisitor visitor);
    }
}
