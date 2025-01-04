namespace AudioLibrary.Extensions;
public static class FloatArrayExtensions
{
    /// <summary>
    /// Converts array of source samples represented as floats to destination byte array represented as 16-bit PCM audio
    /// </summary>
    /// <param name="source">Float array of samples</param>
    /// <returns>Byte array represented as 16-bit PCM audio</returns>
    /// <summary>
    /// 
    /// </summary>
    public static byte[] ToByteSamples(this IList<float> source)
    {
        var samplesCount = source.Count;
        var pcm = new byte[samplesCount * 2];
        int sampleIndex = 0,
            pcmIndex = 0;

        while (sampleIndex < samplesCount)
        {
            var outsample = (short)(source[sampleIndex] * short.MaxValue);
            pcm[pcmIndex] = (byte)(outsample & 0xff);
            pcm[pcmIndex + 1] = (byte)((outsample >> 8) & 0xff);

            sampleIndex++;
            pcmIndex += 2;
        }

        return pcm;
    }
}