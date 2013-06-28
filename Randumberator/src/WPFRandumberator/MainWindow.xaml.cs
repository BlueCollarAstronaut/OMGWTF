using System;
using System.Configuration;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using Randumberator.Core;

namespace WPFRandumberator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private delegate void ShowStoryboardHandler(Storyboard storyboard);
        private readonly ShowStoryboardHandler _onShowStoryboard;

        private readonly Storyboard _coinFlipHeadsToHeads;
        private readonly Storyboard _coinFlipHeadsToTails;
        private readonly Storyboard _coinFlipTailsToHeads;
        private readonly Storyboard _coinFlipTailsToTails;
        private Storyboard _selectedStoreboard;

        private string[] _backgroundPool;

        bool previousResult = true;         // Start on Heads
        bool isFlipping = false;
        public MainWindow()
        {
            InitializeComponent();

            new SplashWindow().ShowDialog();
            
            // Build the collection of background images:
            string backgrounImagePath = MethodsAndStuff.GetPath(ConfigurationManager.AppSettings.Get("BackgroundImagePath"));
            
            
            // Figure out if the debug info should be hidden.
            // NOTE: 
            bool hideDebugInfo = false;
            try
            {
                hideDebugInfo = Boolean.Parse(ConfigurationManager.AppSettings.Get("HideDebugInfo").ToString());
            }
            catch (Exception ex) { 
                // Ignore;
            }
            if (hideDebugInfo == true)
            {
                listBox1.Visibility = (hideDebugInfo) ? Visibility.Hidden : Visibility.Visible;
            }
            else if (hideDebugInfo != true)
            {
                listBox1.Visibility = Visibility.Visible;
            }




            try
            {
                _backgroundPool = Directory.GetFiles(backgrounImagePath);
            }
            catch (Exception ex) { _backgroundPool = null; }
            // And load a random one
            UpdateBackground();

            // Get storyboards
            _coinFlipHeadsToHeads = Resources["CoinFlipHeadsToHeads"] as Storyboard;
            _coinFlipHeadsToTails = Resources["CoinFlipHeadsToTails"] as Storyboard;
            _coinFlipTailsToHeads = Resources["CoinFlipTailsToHeads"] as Storyboard;
            _coinFlipTailsToTails = Resources["CoinFlipTailsToTails"] as Storyboard;

            // Individual null checks might be nice, but that takes too long
            if (_coinFlipHeadsToHeads != null && _coinFlipHeadsToTails != null &&
                 _coinFlipTailsToHeads != null && _coinFlipTailsToTails != null)
            {
                _coinFlipHeadsToHeads.Completed += new EventHandler(_storeboard_Completed);
                _coinFlipHeadsToTails.Completed += new EventHandler(_storeboard_Completed);
                _coinFlipTailsToHeads.Completed += new EventHandler(_storeboard_Completed);
                _coinFlipTailsToTails.Completed += new EventHandler(_storeboard_Completed);
            }


            // Define delegate
            _onShowStoryboard = OnShowStoryboard;
        }

        private void OnShowStoryboard(Storyboard storyboard)
        {
            BeginStoryboard(storyboard);
        }

        private void UpdateBackground()
        {
            // If there is a pool of background images, then pick one at random to display
            // with random transformations
            if (_backgroundPool != null || _backgroundPool.Length > 0)
            {
                // Pick a random pic
                int index = Engine.Randomizer.Randomize(_backgroundPool.Length - 1);

                // Assign random opacity (between .25 and .85)
                var brush = new ImageBrush();
                brush.ImageSource = new BitmapImage(new Uri(_backgroundPool[index]));
                brush.Opacity = 0.25 + (0.01d * (double)Engine.Randomizer.Randomize(60));

                // Rotate randomly
                RotateTransform rotation = new RotateTransform();
                rotation.CenterX = 0.5;
                rotation.CenterY = 0.5;
                rotation.Angle = Engine.Randomizer.Randomize(360);
                brush.RelativeTransform = rotation;

                // Randomize Tile mode
                int tileMode = Engine.Randomizer.Randomize(4);
                brush.TileMode =
                    (tileMode == 0) ? TileMode.Tile :
                    (tileMode == 1) ? TileMode.FlipXY :
                    (tileMode == 2) ? TileMode.FlipY :
                    TileMode.FlipX;


                MainGrid.Background = brush;
            }
        }

        private void DisplayResult(bool result)
        {
            if (result == true)
            {
               new HeadsWindow().ShowDialog();
            }
            else if (result == false)
            {
                new TailsWindow().ShowDialog();
            }
            else
            {
                new FileNotFoundWindow().ShowDialog();
            }

        }
        private void button1_Click_1(object sender, RoutedEventArgs e)
        {
            // Don't flip is already flipping
            if (!isFlipping)
            {
                try
                {
                    isFlipping = true;
                    UpdateBackground();

                    // Determine outcome:
                    var plugin = Engine.PluginPicker.PickPlugin(Engine.PluginRepository);
                    var randomResult = plugin.Randomize(1);
                    var newResult = (randomResult == 1);

                    // Pick the appropriate storyboard
                    _selectedStoreboard =
                        (previousResult) ?
                        (newResult) ? _coinFlipHeadsToHeads : _coinFlipHeadsToTails :
                        (newResult) ? _coinFlipTailsToHeads : _coinFlipTailsToTails;
                    previousResult = newResult;

                    string message = string.Format("Plugin [{0}] was selected and gave result of {1}",
                            plugin.ToString(),
                            randomResult.ToString());
                    listBox1.Items.Add(message);
                    
                    // And flip the coin
                    Dispatcher.Invoke(_onShowStoryboard, _selectedStoreboard);
                    Thread.Sleep(1040);
                }
                finally
                {
                    //isFlipping = false;
                }
            }
        }

        void _storeboard_Completed(object sender, EventArgs e)
        {
            DisplayResult(previousResult);
            isFlipping = false;
        }
    }
}