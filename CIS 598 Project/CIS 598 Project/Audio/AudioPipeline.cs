using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIS_598_Project.Audio
{
    class AudioPipeline : IAudioProcessor
    {
        private readonly BufferedWaveProvider bufferedWaveProvider;
        private readonly IWaveProvider outputProvider;
        private readonly MixingSampleProvider mixer;


        public AudioPipeline(WaveIn recorder)
        {
            this.bufferedWaveProvider = new BufferedWaveProvider(recorder.WaveFormat);
            var bufferStream32 = new Pcm16BitToSampleProvider(bufferedWaveProvider);
            var effectStream = new EffectStream(bufferStream32);
            mixer = new MixingSampleProvider(effectStream);
        }
    }
}
