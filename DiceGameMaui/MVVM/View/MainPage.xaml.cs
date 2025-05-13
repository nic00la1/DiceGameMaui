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

        public async Task AnimateDiceRolls()
        {
            // Animacja dla gracza 1
            foreach (var dice in ((DiceGameViewModel)BindingContext).Player1Dice)
            {
                dice.Value = new Random().Next(1, 7); // Symulacja rzutu
                await Task.Delay(300); // Opóźnienie między rzutami
            }

            // Animacja dla gracza 2
            foreach (var dice in ((DiceGameViewModel)BindingContext).Player2Dice)
            {
                dice.Value = new Random().Next(1, 7); // Symulacja rzutu
                await Task.Delay(300); // Opóźnienie między rzutami
            }
        }

        private async void OnRerollClicked(object sender, EventArgs e)
        {
            await AnimateDiceRolls();
        }
    }

}
