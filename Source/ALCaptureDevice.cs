using KumaEngine.OpenAL.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KumaEngine.OpenAL.Source
{
    public unsafe class ALCaptureDevice(string name, uint frequency, ALFormat format, int bufferSize) : IDisposable
    {
        ALCdevice* _devHandle = ALNative.alcCaptureOpenDevice(
            (sbyte*)Marshal.StringToHGlobalAnsi(name),
            frequency, (int)format, bufferSize
        );

        public ALCdevice* Handle => _devHandle;
        public string Name => name;

        public int AvaliableSamples => GetAvailSamples(_devHandle);

        static int GetAvailSamples(ALCdevice* dev)
        {
            int result;
            ALNative.alcGetIntegerv(dev, ALNative.ALC_CAPTURE_SAMPLES, 1, &result);
            return result;
        }

        public void StartCapture() => ALNative.alcCaptureStart(_devHandle);
        public void StopCapture() => ALNative.alcCaptureStop(_devHandle);

        public void CaptureSamples<T>(Span<T> destination, int sampleCount) where T : unmanaged
        {
            fixed (void* pDest = destination)
                ALNative.alcCaptureSamples(_devHandle, pDest, sampleCount);
        }

        public void Dispose() => ALNative.alcCaptureCloseDevice(_devHandle);
    }
}
