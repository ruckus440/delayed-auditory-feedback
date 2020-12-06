using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIS_598_Project.Effects
{
    class EffectStream : ISampleProvider
    {
        private readonly List<Effect> effects;
        private readonly ISampleProvider sourceProvider;
        private readonly object effectLock = new object();

        public EffectStream(ISampleProvider sourceProvider)
        {
            this.effects = new List<Effect>();
            this.sourceProvider = sourceProvider;
            foreach (Effect e in effects)
            {
                InitialiseEffect(e);
            }
        }

        public WaveFormat WaveFormat
        {
            get { return sourceProvider.WaveFormat; }
        }

        public int Read(float[] buffer, int offset, int count)
        {
            int read = sourceProvider.Read(buffer, offset, count);

            lock (effectLock)
            {
                Process(buffer, offset, read);
            }
            return read;
        }

        private void Process(float[] buffer, int offset, int count)
        {
            int sample = count;
            foreach (var effect in effects)
            {
                effect.Block(samples);
            }
        }


    }
}
