using System.IO.Compression;

namespace FileArchiver.Models.Compressor
{
    public enum CompressionSpeed
    {
        Fast = CompressionLevel.Fastest,
        Optimal = CompressionLevel.Optimal,
        NoCompression = CompressionLevel.NoCompression
    }
}