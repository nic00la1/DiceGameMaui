using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace DiceGameMaui.MVVM.ViewModel
{
    public class DiceViewModel : INotifyPropertyChanged
    {
        public string ImageSource => $"dice{Value}.png";
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
        private bool _canBeSelected = true;
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
        public bool CanBeSelected
        {
            get => _canBeSelected;
            set
            {
                if (_canBeSelected != value)
                {
                    _canBeSelected = value;
                    OnPropertyChanged();
                }
            }
        }

        public DiceViewModel()
        {
            ToggleCommand = new Command(Toggle);
        }

        public void Toggle()
        {
            if (CanBeSelected)
                IsSelected = !IsSelected;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}