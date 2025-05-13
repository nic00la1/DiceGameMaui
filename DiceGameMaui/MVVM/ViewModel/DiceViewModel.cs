using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DiceGameMaui.MVVM.ViewModel
{
    public class DiceViewModel : INotifyPropertyChanged // Opisuje jedną kość
    {
        public int Value { get; set; }
        public bool IsSelected { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;

        public void Toggle()
        {
            IsSelected = !IsSelected;
            OnPropertyChanged(nameof(IsSelected));
        }
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
             => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
