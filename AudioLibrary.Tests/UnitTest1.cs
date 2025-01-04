using AudioLibrary.Classes;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace AudioLibrary.Tests;

public class UnitTest1
{
    public const string AUDIO_PATH = $@"D:\Repositories\dotnet\AudioLibrary\AudioLibrary.Tests\samples\last-cxntury.mp3";
    [Fact]
    public void MP3SamplesExtractor_ReturnsNotZeroArrayOfSamples()
    {
        MP3SamplesExtractor extractor = new();
        var (samples, waveFormat) = extractor.ExtractSamplesAsFloatArray(AUDIO_PATH);
        Assert.False(samples.All(s => s == 0));
    }

    [Fact]
    public void Test1()
    {
        var (samples, waveFormat) = new MP3SamplesExtractor().ExtractSamplesAsByteArray(AUDIO_PATH);
        WaveOutEvent waveOutEvent = new();
        IWaveProvider waveProvider = new ByteArrayWaveProvider(samples, waveFormat);
        waveOutEvent.Init(waveProvider);
        waveOutEvent.Play();

        while (waveOutEvent.PlaybackState == PlaybackState.Playing)
        {
            Thread.Sleep(2000);
        }
        Assert.True(true);
    }
}
