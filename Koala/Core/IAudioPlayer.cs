using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koala.Core
{
    interface IAudioPlayer
    {
        void Play(string url);
        void PlayAsync(string url);
    }
}
