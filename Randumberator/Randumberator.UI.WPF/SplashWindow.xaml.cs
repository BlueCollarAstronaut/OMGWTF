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
using System.Windows.Shapes;
using Randumberator.Core.Abstractions;

namespace Randumberator.UI.WPF
{
    /// <summary>
    /// Interaction logic for SplashWindow.xaml
    /// </summary>
    public partial class SplashWindow : Window
    {

        private delegate void ExplodeTitleHandler();
        
        private Storyboard _TitleSplosion;
        private ExplodeTitleHandler _onExplodeTitle;

        private Thread _LoadingThread;

        public SplashWindow()
        {
            InitializeComponent();

            _TitleSplosion = Resources["TitleSplosion"] as Storyboard;
            _onExplodeTitle = ExplodeTitle;
        }

        private void Load()
        {
            Thread.Sleep(2000);
            Dispatcher.Invoke(_onExplodeTitle);
            Thread.Sleep(2000);

        }

        private void ExplodeTitle()
        {
            BeginStoryboard(_TitleSplosion);
        }

        private void SplashWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            _LoadingThread = new Thread(Load);
            _LoadingThread.Start();
        }

        public void ReceiveMessage(string message)
        {
            Dispatcher.Invoke(_onExplodeTitle);
            Thread.Sleep(2000);
        }
    }
}
