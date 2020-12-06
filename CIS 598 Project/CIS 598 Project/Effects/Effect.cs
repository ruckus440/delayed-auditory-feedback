using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIS_598_Project.Effects
{
    public abstract class Effect
    {
        private List<Slider> sliders;
        public float SampleRate { get; set; }

        protected float slider1 { get { return sliders[0].Value; } }

        public Effect()
        {
            SampleRate = 44100;
        }



        public virtual void Init()
        {

        }

        protected abstract void Slider();

        public virtual void Block(int samplesblock)
        {

        }

    }
}
