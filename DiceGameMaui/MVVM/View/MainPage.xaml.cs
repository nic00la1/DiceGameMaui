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
            // Tworzymy kopię kolekcji Player1Dice
            var player1DiceCopy = ((DiceGameViewModel)BindingContext).Player1Dice.ToList();
            foreach (var dice in player1DiceCopy)
            {
                dice.Value = new Random().Next(1, 7); // Symulacja rzutu
                await Task.Delay(300); // Opóźnienie między rzutami
            }

            // Tworzymy kopię kolekcji Player2Dice
            var player2DiceCopy = ((DiceGameViewModel)BindingContext).Player2Dice.ToList();
            foreach (var dice in player2DiceCopy)
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
