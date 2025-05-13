using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DiceGameMaui.MVVM.ViewModel
{
    public class DiceGameViewModel : INotifyPropertyChanged // Opisuje całą gre i jej logikę
    {
        /// <summary>
        /// Powiadamia o zmianach w UI
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged; 
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name)); 

        const int DiceCount = 5; // Ilość kostek
        Random random = new(); // Losowanie kostek

        public ObservableCollection<DiceViewModel> Player1Dice { get; set; } // Kostki gracza 1
        public ObservableCollection<DiceViewModel> Player2Dice { get; set; } // Kostki gracza 2

        public ICommand RerollCommand { get; set; } 
        public ICommand RestartCommand { get; set; }
        public ICommand ToggleDiceCommand { get; set; }

        private string _result;
        public string Result
        {
            get => _result;
            set
            {
                _result = value;
                OnPropertyChanged();
            }
        }

        private bool _rerollUsed;
        public bool RerollUsed
        {
            get => _rerollUsed;
            set
            {
                _rerollUsed = value;
                OnPropertyChanged();
            }
        }

       public DiceGameViewModel()
        {
            Player1Dice = new ObservableCollection<DiceViewModel>();
            Player2Dice = new ObservableCollection<DiceViewModel>();
            RerollCommand = new Command(RerollSelectedDice, () => !RerollUsed);
            RestartCommand = new Command(StartNewGame);
            ToggleDiceCommand = new Command<DiceViewModel>(dice => dice.Toggle());

            StartNewGame();
        }

        public void StartNewGame()
        {
            Player1Dice.Clear();
            Player2Dice.Clear();
            for (int i = 0; i < DiceCount; i++)
            {
                Player1Dice.Add(new DiceViewModel { Value = random.Next(1, 7) });
                Player2Dice.Add(new DiceViewModel { Value = random.Next(1, 7) });
            }
            Result = string.Empty;
            RerollUsed = false;
            ((Command)RerollCommand).ChangeCanExecute();
        }

        public void RerollSelectedDice()
        {
            foreach (var dice in Player1Dice.Where(d => d.IsSelected))
            {
                dice.Value = random.Next(1, 7);
                dice.IsSelected = false;
            }
            foreach (var dice in Player2Dice.Where(d => d.IsSelected))
            {
                dice.Value = random.Next(1, 7);
                dice.IsSelected = false;
            }
            OnPropertyChanged(nameof(Player1Dice));
            OnPropertyChanged(nameof(Player2Dice));
            RerollUsed = true;
            ((Command)RerollCommand).ChangeCanExecute();
            ShowResults();
        }

        void ShowResults()
        {
            int p1 = CountPoints(Player1Dice.Select(d => d.Value).ToArray());
            int p2 = CountPoints(Player2Dice.Select(d => d.Value).ToArray());

            if (p1 > p2)
                Result = $"Gracz 1: {p1} pkt, Gracz 2: {p2} pkt\nWygrywa Gracz 1!";
            else if (p2 > p1)
                Result = $"Gracz 1: {p1} pkt, Gracz 2: {p2} pkt\nWygrywa Gracz 2!";
            else
                Result = $"Gracz 1: {p1} pkt, Gracz 2: {p2} pkt\nRemis!";
        }

        int CountPoints(int[] dice)
        {
            var counts = dice.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());
            int points = 0;
            bool[] used = new bool[DiceCount];

            // Piątka
            if (counts.Values.Contains(5))
                return 50;

            // Full
            if (dice.OrderBy(x => x).SequenceEqual(new[] { 1, 2, 3, 4, 5 }) ||
                dice.OrderBy(x => x).SequenceEqual(new[] { 2, 3, 4, 5, 6 }))
                return 50;

            // Czwórka
            var four = counts.FirstOrDefault(kv => kv.Value == 4);
            if (four.Value == 4)
            {
                points += four.Key * 5;
                MarkUsed(dice, used, four.Key, 4);
            }

            // Trójka
            var three = counts.FirstOrDefault(kv => kv.Value == 3);
            if (three.Value == 3)
            {
                points += three.Key * 4;
                MarkUsed(dice, used, three.Key, 3);
            }

            // Para
            var pairs = counts.Where(kv => kv.Value == 2).ToList();
            foreach (var pair in pairs)
            {
                points += pair.Key * 3;
                MarkUsed(dice, used, pair.Key, 2);
            }

            // Pozostałe
            for (int i = 0; i < DiceCount; i++)
                if (!used[i])
                    points += dice[i];

            return points;
        }

        void MarkUsed(int[] dice, bool[] used, int value, int count)
        {
            int found = 0;
            for (int i = 0; i < dice.Length && found < count; i++)
            {
                if (!used[i] && dice[i] == value)
                {
                    used[i] = true;
                    found++;
                }
            }
        }
    }
}
