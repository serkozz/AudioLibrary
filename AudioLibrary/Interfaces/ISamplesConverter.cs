using NAudio.Wave;

namespace AudioLibrary.Interfaces;

public interface ISamplesConverter<From>
{
    public Task ConvertAsync(From from, string path, WaveFormat waveFormat);
}