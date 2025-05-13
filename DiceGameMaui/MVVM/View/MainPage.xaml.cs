using DiceGameMaui.MVVM.ViewModel;
using System.ComponentModel;

namespace DiceGameMaui
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnDiceTapped(object sender, TappedEventArgs e)
        {
            if (sender is Frame frame)
            {
                await frame.ScaleTo(1.2, 100, Easing.CubicIn);
                await frame.ScaleTo(1.0, 100, Easing.CubicOut);
            }
        }
    }

}
