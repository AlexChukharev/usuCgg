using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace CGG
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public List<TextBox> ArgsBoxs
        {
            get { return _argsBoxs; }
        }
        #region

        private void FirstResize(object sender, RoutedEventArgs e)
        {
            FirstTaskClick(this, new RoutedEventArgs());
            mainWindow.SizeChanged -= FirstResize;
        }
        private void SecondResize(object sender, RoutedEventArgs e)
        {
            SecondTaskClick(this, new RoutedEventArgs());
            mainWindow.SizeChanged -= SecondResize;
        }
		private void ThirdResize(object sender, RoutedEventArgs e)
		{
			ThirdTaskClick(this, new RoutedEventArgs());
			mainWindow.SizeChanged -= ThirdResize;
		}

        #endregion //RESIZES

        private List<TextBox> _argsBoxs = new List<TextBox>();

        private void AddButton(Panel panel, RoutedEventHandler f)
        {
            var button = new Button
            {
                Margin = new Thickness(35, 10, 0, 0),
                Height = 23,
                Width = 63,
                Content = "Drow!!!"
            };
            button.Click += f;
            panel.Children.Add(button);
        }

        private void MenuFirstTaskClick(object sender, RoutedEventArgs e)
        {
            _argsBoxs = ArgsPanel.CreateTextBoxs(ArgsForm, 5);
            AddButton(ArgsForm, FirstTaskClick);
        }

        private void MenuSecondTaskClick(object sender, RoutedEventArgs e)
        {
            _argsBoxs = ArgsPanel.CreateTextBoxs(ArgsForm, 6);
            AddButton(ArgsForm, SecondTaskClick);
        }

		private void MenuThirdTaskClick(object sender, RoutedEventArgs e)
		{
			_argsBoxs = ArgsPanel.CreateTextBoxs(ArgsForm, 2);
			AddButton(ArgsForm, ThirdTaskClick);
		}

	    private void MenuFourthTaskClick(object sender, RoutedEventArgs e)
	    {
		    var f = new PolyCordForm();
			f.Show();
	    }

	    private void ThirdTaskClick(object sender, RoutedEventArgs e)
	    {
			mainWindow.SizeChanged += ThirdResize;
			var args = new ThirdArgs(_argsBoxs);
			canvas.Height = Height - 90;
			canvas.Width = Width - 150;
			MainFunction.Function(args, canvas, ThirdTaskPainter.Drow);
	    }

	    private void SecondTaskClick(Object sender, EventArgs e)
        {
            mainWindow.SizeChanged += SecondResize;
            var args = new SecondArgs(_argsBoxs);
            canvas.Height = Height - 90;
            canvas.Width = Width - 150;
            MainFunction.Function(args, canvas, SecondTaskPainter.Drow);
        }

        private void FirstTaskClick(Object sender, EventArgs e)
        {
            mainWindow.SizeChanged += FirstResize;
            var args = new FirstArgs(_argsBoxs);
            canvas.Height = Height - 90;
            canvas.Width = Width - 150;
            MainFunction.Function(args, canvas, FirstTaskPainter.Drow);
        }

        private void AboutClick(object sender, RoutedEventArgs e)
        {
            const string mes = "Hi! It is my app for CGG!";
            const MessageBoxButton button = MessageBoxButton.OK;
            const MessageBoxImage icon = MessageBoxImage.Information;
            const string title = "What is it?";
            MessageBox.Show(mes, title, button, icon);
        }

	    
    }
}
