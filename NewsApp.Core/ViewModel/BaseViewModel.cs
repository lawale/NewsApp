using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using NewsApp.Core.ViewModel;

[assembly: Xamarin.Forms.Dependency(typeof(BaseViewModel))]
namespace NewsApp.Core.ViewModel
{
    public class BaseViewModel : IViewModel
    {
        public string Title { get; protected set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        protected void SetValue<T>(ref T backingField, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingField, value))
                return;
            backingField = value;
            OnPropertyChanged(propertyName);
        }

        void IViewModel.SetState<T>(Action<T> action)
        {
            action(this as T);
        }
    }
}
