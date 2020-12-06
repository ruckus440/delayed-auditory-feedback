using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIS_598_Project.Effects
{
    class Slider
    {
        List<string> discreteValueText;

        public string Description { get; set; }
        public float Default { get; set; }
        public float Minimum { get; set; }
        public float Maximum { get; set; }
        public float Increment { get; set; }
        public float Value { get; set; }
        
        
        public IList<string> DiscreteValueText { get { return discreteValueText; } }

        public Slider(float defaultValue, float minimum, float maximum, float increment, string description)
        {
            this.Default = defaultValue;
            this.Value = defaultValue;
            this.Minimum = minimum;
            this.Maximum = maximum;
            this.Increment = increment;
            this.Description = description;
            this.discreteValueText = new List<string>();
        }
    }
}
