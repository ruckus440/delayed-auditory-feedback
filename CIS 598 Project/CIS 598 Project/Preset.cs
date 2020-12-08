using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIS_598_Project
{
    [Serializable]
    public class Preset
    {
        public string Name { get; set; }
        public int Delay { get; set; }
        public int Frequency { get; set; }

        public override string ToString()
        {
            return Name;
        }

        //public Preset (string s, int d, int f)
        //{
        //    this.Name = s;
        //    this.Delay = d;
        //    this.Frequency = f;
        //}

        //public Preset()
        //{

        //}
    }
}
