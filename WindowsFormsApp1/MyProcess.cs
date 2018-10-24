using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class MyProcess
    {
        public int ID { get; set; }
        public string Path { get; set; }
        public string Arguments { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsWorking { get; set; }
    }
}
