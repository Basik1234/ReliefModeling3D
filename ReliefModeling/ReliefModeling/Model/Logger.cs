using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ReliefModeling.Model
{
    public class Logger : INotifyPropertyChanged
    {
        private static Logger _instance;
        private string _log;
        private Logger()
        {}
 
        public static Logger Instance => _instance ?? (_instance = new Logger());

        public string Log
        {
            get => _log;
            set
            {
                _log = value;
                OnPropertyChanged();
            }
        }

        public void WriteLine(string line)
        {
            Log += line + "\n";
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}