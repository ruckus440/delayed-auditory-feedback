using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIS_598_Project.Audio
{
    interface IAudioProcessor
    {
        void ProcessOutgoing(byte[] buffer, int count);
        void ProcessIncoming(byte[] buffer, int count);
        void QueueForPlayback(string path);
    }
}
