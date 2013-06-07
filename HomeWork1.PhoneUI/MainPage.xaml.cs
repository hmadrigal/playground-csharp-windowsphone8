using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using HomeWork1.PhoneUI.Resources;
using Microsoft.Xna.Framework;
using System.Windows.Input;
using System.Windows.Media;

namespace HomeWork1.PhoneUI
{
    public partial class MainPage : PhoneApplicationPage
    {
        private List<UIElement> _whiteCheckers;
        private List<UIElement> _blackCheckers;
        private List<UIElement> _allChrckers;
        private readonly double cellSize = 60d;
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            _whiteCheckers = new List<UIElement>();
            _blackCheckers = new List<UIElement>();
            _allChrckers = new List<UIElement>();

            foreach (var contentControl in BoardPanel.Children.OfType<ContentControl>().Where(cc => cc.Template == Resources["WhiteChekers"]))
            {
                _whiteCheckers.Add(contentControl);
                _allChrckers.Add(contentControl);
            }
            foreach (var contentControl in BoardPanel.Children.OfType<ContentControl>().Where(cc => cc.Template == Resources["BlackChekers"]))
            {
                _blackCheckers.Add(contentControl);
                _allChrckers.Add(contentControl);
            }

            var i = 0;
            foreach (var row in new int[] { 0, 1, 2, 5, 6, 7 })
            {
                var columns = (row % 2 == 0) ? new int[] { 1, 3, 5, 7 } : new int[] { 0, 2, 4, 6 };
                foreach (var column in columns)
                {
                    Canvas.SetTop(_allChrckers[i], cellSize * row + 10);
                    Canvas.SetLeft(_allChrckers[i], cellSize * column + 10);

                    _allChrckers[i].ManipulationDelta += OnUIElementManipulationDelta;
                    _allChrckers[i].MouseLeftButtonDown += OnUIElementMouseLeftButtonDown;
                    _allChrckers[i].ManipulationCompleted += MainPage_ManipulationCompleted;
                    i = i + 1;

                }
            }
        }

        

        private void OnUIElementMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var uiElementSender = sender as UIElement;
            var uiPosition = e.GetPosition(BoardPanel);
            var beginColumn = Math.Truncate(uiPosition.X / cellSize);
            var beginRow = Math.Truncate(uiPosition.Y / cellSize);
        }

        private void OnUIElementManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            var uiElementSender = sender as UIElement;
            Canvas.SetLeft(uiElementSender, Canvas.GetLeft(uiElementSender) + e.DeltaManipulation.Translation.X);
            Canvas.SetTop(uiElementSender, Canvas.GetTop(uiElementSender) + e.DeltaManipulation.Translation.Y);
        }

        private void MainPage_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            var uiElementSender = sender as UIElement;
            var endColumn = Math.Truncate(Canvas.GetLeft(uiElementSender) / cellSize);
            var endRow = Math.Truncate(Canvas.GetTop(uiElementSender) / cellSize);
        }




        


    }
}