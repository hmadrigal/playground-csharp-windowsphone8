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
        private readonly double cellSize = 60d;

        private List<UIElement> _allChrckers;

        private double expectedRow = double.NaN;
        private double beginRow = double.NaN;
        private double expectedColumnA = double.NaN;
        private double expectedColumnB = double.NaN;
        private double beginColumn = double.NaN;


        // Constructor
        public MainPage()
        {
            InitializeComponent();

            _allChrckers = new List<UIElement>();
            foreach (var contentControl in BoardPanel.Children.OfType<ContentControl>().Where(cc => cc.Template == Resources["WhiteChekers"]))
            {
                _allChrckers.Add(contentControl);
            }
            foreach (var contentControl in BoardPanel.Children.OfType<ContentControl>().Where(cc => cc.Template == Resources["BlackChekers"]))
            {
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
            var contentControl = sender as ContentControl;
            var uiPosition = e.GetPosition(BoardPanel);
            beginColumn = Math.Truncate(uiPosition.X / cellSize);
            beginRow = Math.Truncate(uiPosition.Y / cellSize);
            var isWhiteChecker = contentControl.Template == Resources["WhiteChekers"];

            // Computing possible landing positions
            expectedColumnA = expectedColumnB = expectedRow = double.NaN;
            if (isWhiteChecker)
            {
                expectedRow = beginRow == 7 ? 7 : beginRow + 1;
            }
            else
            {
                expectedRow = beginRow == 0 ? 0 : beginRow - 1;
            }
            expectedColumnA = beginColumn == 0 ? double.NaN : beginColumn - 1;
            expectedColumnB = beginColumn == 7 ? double.NaN : beginColumn + 1;
        }

        private void OnUIElementManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            var contentControl = sender as ContentControl;
            Canvas.SetLeft(contentControl, Canvas.GetLeft(contentControl) + e.DeltaManipulation.Translation.X);
            Canvas.SetTop(contentControl, Canvas.GetTop(contentControl) + e.DeltaManipulation.Translation.Y);
        }

        private void MainPage_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            var contentControl = sender as ContentControl;
            var endColumn = Math.Truncate(Canvas.GetLeft(contentControl) / cellSize);
            var endRow = Math.Truncate(Canvas.GetTop(contentControl) / cellSize);

            
            // Validating the final position
            var isRowValid = endRow == expectedRow;
            var finalRow = isRowValid ? expectedRow : beginRow;
            var isColumnValid = endColumn == expectedColumnA || endColumn == expectedColumnB ;
            var finalColumn = isColumnValid ? endColumn : beginColumn;
            if (!isColumnValid || !isRowValid)
            {
                finalRow = beginRow;
                finalColumn = beginColumn;
            }

            // Correct the final position of the tem
            Canvas.SetTop(contentControl, cellSize * finalRow + 10);
            Canvas.SetLeft(contentControl, cellSize * finalColumn + 10);
        }







    }
}