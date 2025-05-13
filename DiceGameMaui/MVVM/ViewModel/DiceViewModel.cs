using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace DiceGameMaui.MVVM.ViewModel
{
    public class DiceViewModel : INotifyPropertyChanged
    {
        private int _value;
        public int Value
        {
            get => _value;
            set
            {
                if (_value != value)
                {
                    _value = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand ToggleCommand { get; }

        public DiceViewModel()
        {
            ToggleCommand = new Command(Toggle);
        }

        public void Toggle()
        {
            IsSelected = !IsSelected;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}