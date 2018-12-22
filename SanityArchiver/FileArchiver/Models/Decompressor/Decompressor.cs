using System;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;

namespace FileArchiver.Models.Decompressor
{
    public static class Decompressor
    {
        public static void ExtractZip(string pathToZip, string pathToExtract = null)
        {
            pathToExtract = pathToExtract ?? new FileInfo(pathToZip).DirectoryName;

            if (!Directory.Exists(pathToExtract))
            {
                try
                {
                    Directory.CreateDirectory(pathToExtract);
                }
                catch (Exception e)
                {
                    MessageBox.Show($@"Couldn't create a directory in {pathToExtract}. {e.Message}", @"Compression failed",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            try
            {
                ZipFile.ExtractToDirectory(pathToZip, pathToExtract);
            }
            catch (Exception e)
            {
                MessageBox.Show($@"{e.Message}", @"Extraction failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}