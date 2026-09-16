using KumaEngine.OpenAL.Native;
using KumaEngine.OpenAL.Source;
using System;
using System.Runtime.InteropServices;

namespace KumaEngine.OpenAL
{
    public static class AL
    {
        #region Capabilities

        public static void EnableCapability(int capability) => ALNative.alEnable((int)capability);
        public static void DisableCapability(int capability) => ALNative.alDisable((int)capability);
        public static bool IsCapabilityEnabled(int capability) => ALNative.alIsEnabled((int)capability) != 0;

        public static void EnableCapability(string capability) => EnableCapability(capability.GetHashCode());
        public static void DisableCapability(string capability) => DisableCapability(capability.GetHashCode());
        public static bool IsCapabilityEnabled(string capability) => IsCapabilityEnabled(capability.GetHashCode());

        #endregion

        #region State

        public static void SetDopplerFactor(float factor)
            => ALNative.alDopplerFactor(factor);

        public static void SetDopplerVelocity(float velocity)
            => ALNative.alDopplerVelocity(velocity);

        public static void SetSpeedOfSound(float speed)
            => ALNative.alSpeedOfSound(speed);

        public static void SetDistanceModel(ALDistanceModel model)
            => ALNative.alDistanceModel((int)model);

        public static ALError GetError()
            => (ALError)ALNative.alGetError();

        public static unsafe string GetString(ALGetString param)
        {
            sbyte* strPtr = ALNative.alGetString((int)param);
            return strPtr == null ? string.Empty : Marshal.PtrToStringAnsi((IntPtr)strPtr)!;
        }

        public static unsafe bool IsExtensionPresent(string extensionName)
        {
            IntPtr ptr = Marshal.StringToHGlobalAnsi(extensionName);
            try
            {
                return ALNative.alIsExtensionPresent((sbyte*)ptr) != 0;
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
        }

        public static unsafe IntPtr GetProcAddress(string functionName)
        {
            IntPtr ptr = Marshal.StringToHGlobalAnsi(functionName);
            try
            {
                return (IntPtr)ALNative.alGetProcAddress((sbyte*)ptr);
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
        }

        public static unsafe int GetEnumValue(string enumName)
        {
            IntPtr ptr = Marshal.StringToHGlobalAnsi(enumName);
            try
            {
                return ALNative.alGetEnumValue((sbyte*)ptr);
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
        }

        public static float GetFloat(ALGetFloat param)
            => ALNative.alGetFloat((int)param);

        public static int GetInteger(ALGetInteger param)
            => ALNative.alGetInteger((int)param);

        public static bool GetBoolean(int param)
            => ALNative.alGetBoolean(param) != 0;

        #endregion

        #region Listener

        public static void SetListener(ALListenerFloat param, float value)
            => ALNative.alListenerf((int)param, value);

        public static void SetListener(ALListenerVector3 param, float x, float y, float z)
            => ALNative.alListener3f((int)param, x, y, z);

        public static unsafe void SetListener(ALListenerFloatArray param, ReadOnlySpan<float> values)
        {
            fixed (float* pValues = values)
            {
                ALNative.alListenerfv((int)param, pValues);
            }
        }

        public static unsafe float GetListener(ALListenerFloat param)
        {
            float result;
            ALNative.alGetListenerf((int)param, &result);
            return result;
        }

        public static unsafe (float X, float Y, float Z) GetListener(ALListenerVector3 param)
        {
            float x, y, z;
            ALNative.alGetListener3f((int)param, &x, &y, &z);
            return (x, y, z);
        }

        public static unsafe void GetListener(ALListenerFloatArray param, Span<float> values)
        {
            fixed (float* pValues = values)
            {
                ALNative.alGetListenerfv((int)param, pValues);
            }
        }

        #endregion

        #region Sources

        public static unsafe uint GenSource()
        {
            uint source = 0;
            ALNative.alGenSources(1, &source);
            return source;
        }

        public static unsafe void GenSources(Span<uint> sources)
        {
            fixed (uint* pSources = sources)
            {
                ALNative.alGenSources(sources.Length, pSources);
            }
        }

        public static unsafe void DeleteSource(uint source)
            => ALNative.alDeleteSources(1, &source);

        public static unsafe void DeleteSources(ReadOnlySpan<uint> sources)
        {
            fixed (uint* pSources = sources)
            {
                ALNative.alDeleteSources(sources.Length, pSources);
            }
        }

        public static bool IsSource(uint source)
            => ALNative.alIsSource(source) != 0;

        public static void SetSource(uint source, ALSourceFloat param, float value)
            => ALNative.alSourcef(source, (int)param, value);

        public static void SetSource(uint source, ALSourceVector3 param, float x, float y, float z)
            => ALNative.alSource3f(source, (int)param, x, y, z);

        public static void SetSource(uint source, ALSourceInteger param, int value)
            => ALNative.alSourcei(source, (int)param, value);

        public static void SetSource(uint source, ALSourceInteger param, bool value)
            => ALNative.alSourcei(source, (int)param, value ? 1 : 0);

        public static unsafe float GetSourceFloat(uint source, ALSourceFloat param)
        {
            float val;
            ALNative.alGetSourcef(source, (int)param, &val);
            return val;
        }

        public static unsafe (float X, float Y, float Z) GetSourceVector3(uint source, ALSourceVector3 param)
        {
            float x, y, z;
            ALNative.alGetSource3f(source, (int)param, &x, &y, &z);
            return (x, y, z);
        }

        public static unsafe int GetSourceInteger(uint source, ALSourceInteger param)
        {
            int val;
            ALNative.alGetSourcei(source, (int)param, &val);
            return val;
        }

        public static ALSourceState GetSourceState(uint source)
            => (ALSourceState)GetSourceInteger(source, ALSourceInteger.Buffer);

        #region Source Controls

        public static void SourcePlay(uint source) => ALNative.alSourcePlay(source);
        public static void SourceStop(uint source) => ALNative.alSourceStop(source);
        public static void SourcePause(uint source) => ALNative.alSourcePause(source);
        public static void SourceRewind(uint source) => ALNative.alSourceRewind(source);

        public static unsafe void SourcePlay(ReadOnlySpan<uint> sources)
        {
            fixed (uint* pSources = sources)
            {
                ALNative.alSourcePlayv(sources.Length, pSources);
            }
        }

        public static unsafe void SourceStop(ReadOnlySpan<uint> sources)
        {
            fixed (uint* pSources = sources)
            {
                ALNative.alSourceStopv(sources.Length, pSources);
            }
        }

        public static unsafe void SourcePause(ReadOnlySpan<uint> sources)
        {
            fixed (uint* pSources = sources)
            {
                ALNative.alSourcePausev(sources.Length, pSources);
            }
        }

        public static unsafe void SourceRewind(ReadOnlySpan<uint> sources)
        {
            fixed (uint* pSources = sources)
            {
                ALNative.alSourceRewindv(sources.Length, pSources);
            }
        }

        #endregion

        #region Queueing

        public static unsafe void SourceQueueBuffers(uint source, ReadOnlySpan<uint> buffers)
        {
            fixed (uint* pBuffers = buffers)
            {
                ALNative.alSourceQueueBuffers(source, buffers.Length, pBuffers);
            }
        }

        public static unsafe void SourceUnqueueBuffers(uint source, Span<uint> buffers)
        {
            fixed (uint* pBuffers = buffers)
            {
                ALNative.alSourceUnqueueBuffers(source, buffers.Length, pBuffers);
            }
        }

        #endregion

        #endregion

        #region Buffers

        public static unsafe uint GenBuffer()
        {
            uint buffer = 0;
            ALNative.alGenBuffers(1, &buffer);
            return buffer;
        }

        public static unsafe void GenBuffers(Span<uint> buffers)
        {
            fixed (uint* pBuffers = buffers)
            {
                ALNative.alGenBuffers(buffers.Length, pBuffers);
            }
        }

        public static unsafe void DeleteBuffer(uint buffer)
            => ALNative.alDeleteBuffers(1, &buffer);

        public static unsafe void DeleteBuffers(ReadOnlySpan<uint> buffers)
        {
            fixed (uint* pBuffers = buffers)
            {
                ALNative.alDeleteBuffers(buffers.Length, pBuffers);
            }
        }

        public static bool IsBuffer(uint buffer)
            => ALNative.alIsBuffer(buffer) != 0;

        public static unsafe void BufferData<T>(uint buffer, ALFormat format, ReadOnlySpan<T> data, int sampleRate) where T : unmanaged
        {
            fixed (void* pData = data)
            {
                int sizeInBytes = data.Length * sizeof(T);
                ALNative.alBufferData(buffer, (int)format, pData, sizeInBytes, sampleRate);
            }
        }

        public static unsafe int GetBufferInteger(uint buffer, ALBufferInteger param)
        {
            int val;
            ALNative.alGetBufferi(buffer, (int)param, &val);
            return val;
        }

        #endregion
    }
}