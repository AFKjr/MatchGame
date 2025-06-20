using System.ComponentModel.Design;

namespace AnimalMatchingGame
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void PlayAgainButton_Clicked(object sender, EventArgs e)
        {
            AnimalButtons.IsVisible = true;
            PlayAgainButton.IsVisible = false;
            List<string> animalEmoji = [
                    "❤", "❤",
                    "🍕", "🍕",
                    "🍣", "🍣",
                    "🥞", "🥞",
                    "🛴", "🛴",
                    "🛹", "🛹",
                    "💙", "💙",
                    "🎊", "🎊",
                ];
            foreach (var button in AnimalButtons.Children.OfType<Button>())
            {
                int index = Random.Shared.Next(animalEmoji.Count);
                string nextEmoji = animalEmoji[index];
                button.Text = nextEmoji;
                animalEmoji.RemoveAt(index);
                
            }
            Dispatcher.StartTimer(TimeSpan.FromSeconds(.1), TimerTick);
        }
        private bool TimerTick()
        {
            if (!this.IsLoaded) return false;

            tenthsOfSecondsElapsed++;

            TimeElapsed.Text = $"Time Elapsed: {tenthsOfSecondsElapsed / 10F:0.0s}";

            if (PlayAgainButton.IsVisible)
            {
                tenthsOfSecondsElapsed = 0;
                return false;
            }

            return true;
        }

        private Button? lastClicked = null;
        bool findingMatch = false;
        int matchesFound;
        private int tenthsOfSecondsElapsed;

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (sender is Button buttonClicked)
            {
                // First tap (start of a match attempt)
                if (!string.IsNullOrWhiteSpace(buttonClicked.Text) && !findingMatch)
                {
                    buttonClicked.BackgroundColor = Colors.Red;
                    lastClicked = buttonClicked;
                    findingMatch = true;
                }
                else if (lastClicked != null) // Second tap, compare with lastClicked
                {
                    if (buttonClicked != lastClicked && buttonClicked.Text == lastClicked.Text 
                        && !string.IsNullOrWhiteSpace(buttonClicked.Text))
                    {
                        // It's a match
                        matchesFound++;
                        lastClicked.Text = " ";
                        buttonClicked.Text = " ";
                    }

                    // Reset visuals
                    lastClicked.BackgroundColor = Colors.LightBlue;
                    buttonClicked.BackgroundColor = Colors.LightBlue;

                    findingMatch = false;
                }
            }

            if (matchesFound == 8)
            {
                matchesFound = 0;
                AnimalButtons.IsVisible = false;
                PlayAgainButton.IsVisible = true;
            }
        }
    }
}

/* Three elements to make this game better: 
 * Maybe add different kinds of animals for every new game.
 * Have the game save the player's best time so they can beat their record.
 * Maybe make it a true matching game where the player has to choose two invisible buttons to find a match. 
 */