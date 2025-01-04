using AudioLibrary.Interfaces;
using NAudio.Lame;
using NAudio.Wave;

namespace AudioLibrary.Classes;

public class ToMP3Converter : ISamplesConverter<byte[]>
{
    public async Task ConvertAsync(byte[] from, string path, WaveFormat waveFormat)
    {
        using var memStream = new MemoryStream(from);
        using var writer = new LameMP3FileWriter(path, waveFormat, LAMEPreset.INSANE);
        await memStream.CopyToAsync(writer);
    }
}