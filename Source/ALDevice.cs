using KumaEngine.OpenAL.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KumaEngine.OpenAL.Source
{
    public unsafe class ALDevice(string name) : IDisposable
    {
        ALCdevice* _devHandle = ALNative.alcOpenDevice((sbyte*)Marshal.StringToHGlobalAnsi(name));

        public ALCdevice* Handle => _devHandle;
        public string Name => name;

        public ALContext CreateContext(Dictionary<ALContextAttribute,int> attributes)
        {
            if (attributes.Count > 0)
            {
                List<int> attrcompute = new();

                foreach (var item in attributes) attrcompute.AddRange((int)item.Key, item.Value);

                attrcompute.Add(0);

                var attrarray = attrcompute.ToArray();

                fixed (int* atmp = attrarray)
                    return new(this, ALNative.alcCreateContext(_devHandle, atmp));
            }

            return new(this, ALNative.alcCreateContext(_devHandle, null));
        }

        public void Dispose() => ALNative.alcCloseDevice(_devHandle);
    }
}
