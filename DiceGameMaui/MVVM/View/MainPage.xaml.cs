namespace DiceGameMaui
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new DiceGameMaui.MVVM.ViewModel.DiceGameViewModel();
        }
    }

}
