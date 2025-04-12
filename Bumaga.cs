using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Курсовая_2._0
{
    class Bumaga
    {
        private string name = "";
        private double sum = 0;
        private double percent = 0.0;
        public string NAME {  get { return name; } }
        public double SUM { get { return sum; } set { sum = value; } }
        public double PERCENT {  get { return percent; } }
        public Bumaga(string name, double sum, double percent)
        {
            this.name = name;
            this.sum = sum;
            this.percent = percent;
        }
    }
}
