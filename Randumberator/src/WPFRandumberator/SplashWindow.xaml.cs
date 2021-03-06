﻿using System;
using System.Configuration;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using Randumberator.Core;
using Randumberator.Core.Abstractions;
using Randumberator.Core.FailSafe;

namespace WPFRandumberator
{
    /// <summary>
    /// Interaction logic for SplashWindow.xaml
    /// </summary>
    public partial class SplashWindow : Window, ITheUserExperience
    {
        private delegate void ShowStoryboardHandler(Storyboard storyboard);
        private delegate void ShowTextHandler(string text);
        private delegate void HideTextHandler();

        private readonly ShowStoryboardHandler _onShowStoryboard;
        private readonly ShowTextHandler _onShowText;
        private readonly HideTextHandler _onHideText;
        
        private readonly Storyboard _titleSplosion;
        private readonly Storyboard _showSubtitle;
        private readonly Storyboard _showMessage;
        private readonly Storyboard _hideMessage;


        private Thread _loadingThread;

        public SplashWindow()
        {
            InitializeComponent();

            _titleSplosion = Resources["TitleSplosion"] as Storyboard;
            _showSubtitle = Resources["ShowSubTitle"] as Storyboard;
            _showMessage = Resources["ShowLoadingMessage"] as Storyboard;
            _hideMessage = Resources["HideLoadingMessage"] as Storyboard;

            txtLoading.Text = "";

            _onShowStoryboard = OnShowStoryboard;
            _onShowText = OnShowText;
            _onHideText = OnHideText;
        }

        private void OnHideText()
        {
            BeginStoryboard(_hideMessage);
        }

        private void OnShowText(string text)
        {
            txtLoading.Text = text;
            BeginStoryboard(_showMessage);
        }

        private void OnShowStoryboard(Storyboard storyboard)
        {
            BeginStoryboard(storyboard);
        }

        private void Load()
        {
            // Show Title (give time for it to all sink in)
            Dispatcher.Invoke(_onShowStoryboard, _titleSplosion);
            Thread.Sleep(1340);
            Dispatcher.Invoke(_onShowStoryboard, _showSubtitle);
            
            // Figure stuff out...
            ReceiveMessage("Delving into user settings");

            var args = System.Environment.GetCommandLineArgs();
            var rootPath = Path.GetDirectoryName(args[0]);
            if (rootPath == null)
            {

                // Not really sure what to do here, but these steps usually work for me
                ReceiveMessage(
                    "Woops! Uknown Error: Using the list below, proceed to the next item if the previus item does not correct the problem\n" +
                    "1) Restart application\n" +
                    "2) Reinstall application\n" +
                    "3) Reboot computer\n" +
                    "4) Reinstall Windows\n" +
                    "5) Purchase new computer :( \n", 1005);
            }
            else
            {
                var pluginPath = MethodsAndStuff.GetPath(ConfigurationManager.AppSettings.Get("PlgDir"));
                var pluginRepositoryPath = MethodsAndStuff.GetPath(ConfigurationManager.AppSettings.Get("PginRepoPath"));
                var pluginPickerPath = MethodsAndStuff.GetPath(ConfigurationManager.AppSettings.Get("PluginPickerDirectory"));

                // Find Repository factories available...
                ReceiveMessage("Finding Repository Factories...");
                var repositoryFactories = Utilities.GetPluginRepositoryFactories(pluginRepositoryPath);


                // ... if there are none, then use the fail safe, otherwise pick a random one to use
                ReceiveMessage("Building Repository...");
                if (repositoryFactories.Length == 0)
                    repositoryFactories = new IPluginRepositoryFactory[] {new FailsafePluginRepositoryFactory()};

                if (repositoryFactories.Length == 1)
                    Engine.PluginRepositoryFactory = repositoryFactories[0];
                else
                {
                    // For multiple repository factories, randomly choose one to be the
                    // one we use for this go-around
                    var selectedIndex = Engine.Randomizer.Randomize(repositoryFactories.Length);
                    Engine.PluginRepositoryFactory = repositoryFactories[selectedIndex];
                }

                // Calculate the number of plugins so that the message can be "smart"
                int pluginCount = Engine.PluginRepository.Plugins.Length;
                ReceiveMessage(string.Format("Retrieving {0} Plugins...", pluginCount));

                // This is a clever trick I discovered on the internet:  the plugin collection
                // can be preloaded here simply by being accessed for the first time!  By doing 
                // this here during the loading step, we can save time in the longrun 
                var deleteMe = Engine.PluginRepository.Plugins;
            }


            //close the window
            this.Dispatcher.Invoke(DispatcherPriority.Normal, (Action)delegate() { Close(); });
        }

        private void SplashWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            _loadingThread = new Thread(Load);
            _loadingThread.Start();
        }

        public void ReceiveMessage(string message)
        {
            ReceiveMessage(message, 208);
        }

        /// <summary>
        /// Displays a message for the specified amount of time
        /// </summary>
        /// <param name="message">The message to be displayed</param>
        /// <param name="waitTimeInCentiseconds">The time (cs) the application should wait before clearing the message </param>
        /// <remarks>
        /// The unit for the wait time here in in 1/100 of a second to a high level of granular control over time
        /// </remarks>
        public void ReceiveMessage(string message, int waitTimeInCentiseconds)
        {
            Thread.Sleep(1270);
            this.Dispatcher.Invoke(_onShowText, message);

            // Hmmm... .Net one-ups me here, and does things in milliseconds.   I think that's probably too much
            // granularity, though.  There's no real difference between 1 ms and 10ms, so centiseconds
            // is probably  much more practical
            Thread.Sleep(MethodsAndStuff.ConvertCentisecondsToMilliseconds(waitTimeInCentiseconds));

            this.Dispatcher.Invoke(_onHideText);
        }
    }
}
