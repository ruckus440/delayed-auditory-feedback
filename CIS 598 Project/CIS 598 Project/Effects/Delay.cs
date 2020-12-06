using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIS_598_Project.Effects
{
    public class Delay : Effect
    {
        float delayPosition;
        float odelay;
        float delayLength;
        float rspos;
        int rspos2;
        float drspos;
        int tpos;
        float[] buffer = new float[500000];

        
        



        public Delay()
        {

        }

        public override void Init()
        {
            delayPosition = 0;
        }

        protected override void Slider()
        {
            odelay = delayLength;
            delayLength = Math.Min(slider1 * SampleRate / 1000, 500000);
            if (odelay != delayLength)
            {
                if (odelay > delayLength)
                {
                    // resample down delay buffer, heh
                    rspos = 0; rspos2 = 0;
                    drspos = odelay / delayLength;
                    for (int n = 0; n < delayLength; n++)
                    {
                        tpos = ((int)rspos) * 2;
                        buffer[rspos2 + 0] = buffer[tpos + 0];
                        buffer[rspos2 + 1] = buffer[tpos + 1];
                        rspos2 += 2;
                        rspos += drspos;
                    }
                    delayPosition /= drspos;
                    delayPosition = (int)delayPosition;
                    if (delayPosition < 0) delayPosition = 0;
                }
                else
                {
                    if (odelay < delayLength)
                    {
                        // resample up delay buffer, heh
                        drspos = odelay / delayLength;
                        rspos = odelay;
                        rspos2 = (int)delayLength * 2;
                        for (int n = 0; n < (int)delayLength; n++)
                        {
                            rspos -= drspos;
                            rspos2 -= 2;

                            tpos = ((int)(rspos)) * 2;
                            buffer[rspos2 + 0] = buffer[tpos + 0];
                            buffer[rspos2 + 1] = buffer[tpos + 1];
                        }
                        delayPosition /= drspos;
                        delayPosition = (int)delayPosition;
                        if (delayPosition < 0) delayPosition = 0;
                    }
                    
                }
                //freembuf(delaylen*2);
            }
        }
    }
}
