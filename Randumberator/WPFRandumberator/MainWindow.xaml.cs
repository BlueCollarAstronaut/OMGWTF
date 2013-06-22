using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
        private readonly Storyboard _coinFlip;
        

        public MainWindow()
        {
            InitializeComponent();
            new SplashWindow().ShowDialog();

            _coinFlip = Resources["CoinFlip"] as Storyboard;
            _onShowStoryboard = OnShowStoryboard;
            

        }

        private void OnShowStoryboard(Storyboard storyboard)
        {
            BeginStoryboard(storyboard);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            var plugin = Engine.PluginPicker.PickPlugin(Engine.PluginRepository);
            var result = plugin.Randomize(1);
            var message = string.Format("According to {0}: your answer is {1}",
                                        plugin.ToString(),
                                        result.ToString());
            MessageBox.Show(message);
        }

        private void button1_Click_1(object sender, RoutedEventArgs e)
        {
            Dispatcher.Invoke(_onShowStoryboard, _coinFlip);
            Thread.Sleep(1000);
        }
    }
}
