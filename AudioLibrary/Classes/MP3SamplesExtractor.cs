using AudioLibrary.Interfaces;
using NAudio.Wave;

namespace AudioLibrary.Classes;

public class MP3SamplesExtractor : ISamplesExtractor
{
    public (float[] samples, WaveFormat waveFormat) ExtractSamplesAsFloatArray(string path)
    {
        if (!File.Exists(path))
            throw new FileNotFoundException("File not found or not enough permissions", path);

        if (Path.GetExtension(path) != ".mp3")
            throw new ArgumentException("File is not mp3 file");

        using Mp3FileReader reader = new(path);
        WaveFormat waveFormat = reader.WaveFormat;

        ISampleProvider sampleProvider = reader.ToSampleProvider();

        float[] sampleBuffer = new float[waveFormat.SampleRate * waveFormat.Channels]; // For one second
        List<float> samples = [];
        int samplesRead;

        while ((samplesRead = sampleProvider.Read(sampleBuffer, 0, sampleBuffer.Length)) > 0)
        {
            samples.AddRange(sampleBuffer);
        }
        
        return (samples: samples.ToArray(), waveFormat);
    }

    public (byte[] samples, WaveFormat waveFormat) ExtractSamplesAsByteArray(string path)
    {
        var (samples, waveFormat) = ExtractSamplesAsFloatArray(path);
        return (GetSamplesWaveData(samples, samples.Length), waveFormat);
    }

    private static byte[] GetSamplesWaveData(float[] samples, int samplesCount)
    {
        var pcm = new byte[samplesCount * 2];
        int sampleIndex = 0,
            pcmIndex = 0;

        while (sampleIndex < samplesCount)
        {
            var outsample = (short)(samples[sampleIndex] * short.MaxValue);
            pcm[pcmIndex] = (byte)(outsample & 0xff);
            pcm[pcmIndex + 1] = (byte)((outsample >> 8) & 0xff);

            sampleIndex++;
            pcmIndex += 2;
        }

        return pcm;
    }
}