using KumaEngine.OpenAL.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KumaEngine.OpenAL.Source
{
    public enum ALError
    {
        NoError = ALNative.AL_NO_ERROR,
        InvalidName = ALNative.AL_INVALID_NAME,
        InvalidEnum = ALNative.AL_INVALID_ENUM,
        InvalidValue = ALNative.AL_INVALID_VALUE,
        InvalidOperation = ALNative.AL_INVALID_OPERATION,
        OutOfMemory = ALNative.AL_OUT_OF_MEMORY
    }

    public enum ALGetString
    {
        Vendor = ALNative.AL_VENDOR,
        Version = ALNative.AL_VERSION,
        Renderer = ALNative.AL_RENDERER,
        Extensions = ALNative.AL_EXTENSIONS
    }

    public enum ALGetInteger
    {
        DistanceModel = ALNative.AL_DISTANCE_MODEL,
        DopplerFactor = ALNative.AL_DOPPLER_FACTOR,
        SpeedOfSound = ALNative.AL_SPEED_OF_SOUND
    }

    public enum ALGetFloat
    {
        DopplerFactor = ALNative.AL_DOPPLER_FACTOR,
        SpeedOfSound = ALNative.AL_SPEED_OF_SOUND
    }

    public enum ALDistanceModel
    {
        None = ALNative.AL_NONE,
        InverseDistance = ALNative.AL_INVERSE_DISTANCE,
        InverseDistanceClamped = ALNative.AL_INVERSE_DISTANCE_CLAMPED,
        LinearDistance = ALNative.AL_LINEAR_DISTANCE,
        LinearDistanceClamped = ALNative.AL_LINEAR_DISTANCE_CLAMPED,
        ExponentDistance = ALNative.AL_EXPONENT_DISTANCE,
        ExponentDistanceClamped = ALNative.AL_EXPONENT_DISTANCE_CLAMPED
    }

    public enum ALFormat
    {
        Mono8 = ALNative.AL_FORMAT_MONO8,
        Mono16 = ALNative.AL_FORMAT_MONO16,
        Stereo8 = ALNative.AL_FORMAT_STEREO8,
        Stereo16 = ALNative.AL_FORMAT_STEREO16
    }

    public enum ALSourceState
    {
        Initial = ALNative.AL_INITIAL,
        Playing = ALNative.AL_PLAYING,
        Paused = ALNative.AL_PAUSED,
        Stopped = ALNative.AL_STOPPED
    }

    public enum ALSourceType
    {
        Static = ALNative.AL_STATIC,
        Streaming = ALNative.AL_STREAMING,
        Undetermined = ALNative.AL_UNDETERMINED
    }

    public enum ALSourceFloat
    {
        Pitch = ALNative.AL_PITCH,
        Gain = ALNative.AL_GAIN,
        MinGain = ALNative.AL_MIN_GAIN,
        MaxGain = ALNative.AL_MAX_GAIN,
        MaxDistance = ALNative.AL_MAX_DISTANCE,
        RolloffFactor = ALNative.AL_ROLLOFF_FACTOR,
        ConeOuterGain = ALNative.AL_CONE_OUTER_GAIN,
        ConeInnerAngle = ALNative.AL_CONE_INNER_ANGLE,
        ConeOuterAngle = ALNative.AL_CONE_OUTER_ANGLE,
        ReferenceDistance = ALNative.AL_REFERENCE_DISTANCE,
        SecOffset = ALNative.AL_SEC_OFFSET
    }

    public enum ALSourceVector3
    {
        Position = ALNative.AL_POSITION,
        Velocity = ALNative.AL_VELOCITY,
        Direction = ALNative.AL_DIRECTION
    }

    public enum ALSourceInteger
    {
        Buffer = ALNative.AL_BUFFER,
        SourceRelative = ALNative.AL_SOURCE_RELATIVE,
        Looping = ALNative.AL_LOOPING,
        BuffersQueued = ALNative.AL_BUFFERS_QUEUED,
        BuffersProcessed = ALNative.AL_BUFFERS_PROCESSED,
        SourceType = ALNative.AL_SOURCE_TYPE,
        ByteOffset = ALNative.AL_BYTE_OFFSET,
        SampleOffset = ALNative.AL_SAMPLE_OFFSET
    }

    public enum ALListenerFloat
    {
        Gain = ALNative.AL_GAIN
    }

    public enum ALListenerVector3
    {
        Position = ALNative.AL_POSITION,
        Velocity = ALNative.AL_VELOCITY
    }

    public enum ALListenerFloatArray
    {
        Orientation = ALNative.AL_ORIENTATION
    }

    public enum ALBufferInteger
    {
        Frequency = ALNative.AL_FREQUENCY,
        Bits = ALNative.AL_BITS,
        Channels = ALNative.AL_CHANNELS,
        Size = ALNative.AL_SIZE
    }
}
