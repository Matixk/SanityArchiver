using System.ComponentModel;

namespace SanityArchiver.Application.Models
{
    public abstract class Entity : INotifyPropertyChanged
    {
        private string name;
        private string path;
        
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged("name");
            }
        }
        public string Path
        {
            get => path;
            set
            {
                path = value;
                OnPropertyChanged("path");
            }
        }
        public long Size => GetSize();

        public Entity(string path)
        {
            Path = path;
        }
        
        /// <summary> Returns the occupied space on disk in bytes </summary>
        /// <returns> long </returns>
        public abstract long GetSize();
        
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}