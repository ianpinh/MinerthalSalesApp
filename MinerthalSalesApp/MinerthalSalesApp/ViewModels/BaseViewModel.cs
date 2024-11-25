using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MinerthalSalesApp.ViewModels
{
    public partial class BaseViewModel : INotifyPropertyChanged
    {

        private bool isBusy;
        public bool IsBusy
        {
            get => isBusy;
            set
            {
                isBusy = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsBusy));
            }
        }

        private string busyText;
        public string BusyText
        {
            get => busyText;
            set
            {
                busyText = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(BusyText));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public async Task ExecuteAsync(Func<Task> operation, string? busyText = null)
        {
            IsBusy = true;
            BusyText = busyText ?? "Processing...";
            try
            {
                await operation?.Invoke();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                IsBusy = false;
                BusyText = "Processing...";
            }
        }
    }

    //public abstract class ObservableViewModelBase : ObservableObject, INotifyPropertyChanged
    //{
    //    public event PropertyChangedEventHandler PropertyChanged;

    //    protected void RaisePropertyChanged(string propertyName)
    //        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    //    /// <summary>
    //    /// Set a property and raise a property changed event if it has changed
    //    /// </summary>
    //    protected bool SetProperty<T>(ref T property, T value, [CallerMemberName] string propertyName = null)
    //    {
    //        if (EqualityComparer<T>.Default.Equals(property, value))
    //        {
    //            return false;
    //        }

    //        property = value;
    //        RaisePropertyChanged(propertyName);
    //        return true;
    //    }
    //}
}
