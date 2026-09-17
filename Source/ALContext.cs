using KumaEngine.OpenAL.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace KumaEngine.OpenAL.Source
{
    public unsafe class ALContext(ALDevice device, ALCcontext* native) : IDisposable
    {
        public static ALContext Current { get; private set; } = null!;

        ALCcontext* _devHandle = native;

        public ALCcontext* Handle => _devHandle;
        public ALDevice Device => device;

        public void MakeCurrent()
        {
            Current = this;
            ALNative.alcMakeContextCurrent(_devHandle);
        }

        [Obsolete("OpenALSoft defines this as a no-op.")]
        public void Suspend() => ALNative.alcSuspendContext(_devHandle);

        [Obsolete("OpenALSoft defines this as a no-op.")]
        public void Resume() => ALNative.alcProcessContext(_devHandle);

        public void Dispose() => ALNative.alcDestroyContext(_devHandle);
    }
}
