using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace TextReader.Models
{
    public sealed class TextFileReader : INotifyPropertyChanged
    {
        private string text = "";
        private string path = "";

        public string Text
        {
            get => text;
            set
            {
                text = value;
                OnPropertyChanged();
            }
        }
        public string Path
        {
            get => path;
            set
            {
                if (value == null) return;
                if (File.Exists(value))
                {
                    path = value;
                    Text = LoadText();
                    OnPropertyChanged("path");
                }
                else
                {
                    MessageBox.Show($"{value} - file doesn't exist!", "Path set",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public TextFileReader(string path = null)
        {
            Path = path;
            Text = LoadText();
        }

        private string LoadText()
        {
            throw new NotImplementedException();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}