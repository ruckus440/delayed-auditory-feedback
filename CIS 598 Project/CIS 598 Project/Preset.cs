using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIS_598_Project
{
    [Serializable]
    public class Preset : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public int Delay { get; set; }
        public int Frequency { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        
        protected void NotifyPropertyChange(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
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
