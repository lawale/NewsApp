using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace NewsApp.Core.ViewModel
{
    public interface IViewModel : INotifyPropertyChanged
    {
        string Title { get; }

        void SetState<T>(Action<T> action) where T : class, IViewModel;
    }
}
