using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Windows;

namespace FileArchiver.Models.Compressor
{
    public static class Compressor
    {
        public static void CreateZipFromDirectory(string pathToDir, string zipName = null,
            CompressionSpeed mode = CompressionSpeed.Optimal)
        {
            if (!Directory.Exists(pathToDir))
                MessageBox.Show($"{pathToDir} doesn't exist!", "Folder",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            if (string.IsNullOrEmpty(zipName)) zipName = new DirectoryInfo(pathToDir).Name;

            try
            {
                ZipFile.CreateFromDirectory(
                    pathToDir,
                    $@"{Path.GetDirectoryName(pathToDir)}\{zipName}.zip",
                    (CompressionLevel) mode,
                    false);
            }
            catch (IOException e)
            {
                Debug.WriteLine($"Source - {e.Source}\nMessage - {e.Message}");
            }
            catch (Exception e)
            {
                MessageBox.Show($@"{e.Message}", @"Compression failed",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}