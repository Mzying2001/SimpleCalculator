using System.ComponentModel;

namespace SimpleCalculator.ViewModels
{
    class NotificationObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName, object sender = null)
        {
            PropertyChanged?.Invoke(sender ?? this, new PropertyChangedEventArgs(propertyName));
        }
    }
}