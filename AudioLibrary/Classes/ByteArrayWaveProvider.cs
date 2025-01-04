using NAudio.Wave;

namespace AudioLibrary.Classes;

public class ByteArrayWaveProvider(byte[] audioData, WaveFormat waveFormat) : IWaveProvider
{
    private readonly byte[] _audioData = audioData;
    private readonly WaveFormat _waveFormat = waveFormat;
    private int _position = 0;

    public int Read(byte[] buffer, int offset, int count)
    {
        int bytesAvailable = _audioData.Length - _position;
        int bytesToCopy = Math.Min(count, bytesAvailable);

        Array.Copy(_audioData, _position, buffer, offset, bytesToCopy);
        _position += bytesToCopy;

        return bytesToCopy;
    }

    public WaveFormat WaveFormat => _waveFormat;
}