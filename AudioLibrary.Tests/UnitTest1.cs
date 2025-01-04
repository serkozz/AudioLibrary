using AudioLibrary.Interfaces;
using AudioLibrary.Classes;
using NAudio.Wave;

namespace AudioLibrary.Tests;

public class UnitTest1
{
    public const string AUDIO_PATH_INPUT = $@"D:\Repositories\dotnet\AudioLibrary\AudioLibrary.Tests\samples\input\last-cxntury.mp3";
    public const string AUDIO_PATH_OUTPUT = $@"D:\Repositories\dotnet\AudioLibrary\AudioLibrary.Tests\samples\output";
    public const string MP3 = $@".mp3";
    [Fact]
    public void MP3SamplesExtractor_ReturnsNotZeroArrayOfSamples()
    {
        var (samples, waveFormat) = new MP3SamplesExtractor().ExtractSamplesAsFloatArray(AUDIO_PATH_INPUT);
        Assert.False(samples.All(s => s == 0));
    }

    [Fact]
    public void AudioPlaybackTest()
    {
        var (samples, waveFormat) = new MP3SamplesExtractor().ExtractSamplesAsByteArray(AUDIO_PATH_INPUT);
        WaveOutEvent waveOutEvent = new();
        IWaveProvider waveProvider = new ByteArrayWaveProvider(samples, waveFormat);
        waveOutEvent.Init(waveProvider);

        waveOutEvent.Play();

        /// For full track playback
        // while (waveOutEvent.PlaybackState == PlaybackState.Playing)
        // {
        //     Thread.Sleep(2000);
        // }

        Assert.True(true);
    }

    [Fact]
    public async Task AudioConvertersTest()
    {
        var (samples, waveFormat) = new MP3SamplesExtractor().ExtractSamplesAsByteArray(AUDIO_PATH_INPUT);
        IList<ISamplesConverter<byte[]>> converters = [new ToMP3Converter()];
        Dictionary<ISamplesConverter<byte[]>, bool> conversionsResults = [];

        foreach (var converter in converters)
        {
            try
            {
                var converterName = converter.GetType().ToString();
                await converter.ConvertAsync(samples, Path.Combine(AUDIO_PATH_OUTPUT, converterName
                    + "__"
                    + $@"test.{(converterName.Contains("mp3", StringComparison.InvariantCultureIgnoreCase) ? "mp3" : "unknown")}"), waveFormat);
                conversionsResults.Add(converter, true);
            }
            catch
            {
                conversionsResults.Add(converter, false);
            }
        }
        /// Assert that conversionResults doesn't contain failures
        Assert.DoesNotContain(conversionsResults, convRes => !convRes.Value);
    }
}
