using NAudio.Wave;

namespace AudioLibrary.Interfaces;

public interface ISamplesExtractor
{
    public (float[] samples, WaveFormat waveFormat) ExtractSamplesAsFloatArray(string path);
    public (byte[] samples, WaveFormat waveFormat) ExtractSamplesAsByteArray(string path);
}