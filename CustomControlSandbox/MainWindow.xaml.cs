using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ConicalControls;
using GridExtensions;

namespace CustomControlSandbox
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		TextBlock _regularTextBlock;
		StrikeableTextBlock _strikeableTextBlock;

		public MainWindow()
		{
			InitializeComponent();

			contentGrid.AddMaxWidthColumn();
			contentGrid.AddMaxHeightRows(3);

			_regularTextBlock = new TextBlock { Text = "regular" };
			_strikeableTextBlock = new StrikeableTextBlock { Text = "strikeable" };

			AddToGridWithinBorder(_regularTextBlock, 0, 0);

			AddToGridWithinBorder(_strikeableTextBlock.GetView(), 1, 0);

			_regularTextBlock.MouseLeftButtonUp += new MouseButtonEventHandler(RegularTextBlock_Click);
			_strikeableTextBlock.GetView().MouseLeftButtonUp += StrikeThroughTextBlock_Click;

			var button = new Button { Content = "ChangeColor" };
			button.Click += new RoutedEventHandler(button_Click);

			AddToGridWithinBorder(button, 2, 0);
		}

		private void button_Click(object sender, RoutedEventArgs e)
		{
			_strikeableTextBlock.StrikeThroughBrush = new SolidColorBrush(Colors.Pink);
			_strikeableTextBlock.Foreground = new SolidColorBrush(Colors.Red);
			_strikeableTextBlock.Background = new SolidColorBrush(Colors.Black);
		}

		private void RegularTextBlock_Click(object sender, MouseButtonEventArgs e)
		{
			if (_regularTextBlock.TextDecorations == TextDecorations.Strikethrough)
			{
				_regularTextBlock.TextDecorations = null;
			}
			else
			{
				_regularTextBlock.TextDecorations = TextDecorations.Strikethrough;
			}
		}

		private void StrikeThroughTextBlock_Click(object sender, MouseButtonEventArgs e)
		{
			if (_strikeableTextBlock.StrikeThrough)
			{
				_strikeableTextBlock.StrikeThrough = false;
			}
			else
			{
				_strikeableTextBlock.StrikeThrough = true;
			}
		}

		private void AddToGridWithinBorder(UIElement control, int row, int column)
		{
			var border = new Border
			{
				BorderBrush = Brushes.Black,
				BorderThickness = new Thickness(2)
			};

			border.Child = control;

			contentGrid.AddChild(border, row, column);
		}
	}
}
