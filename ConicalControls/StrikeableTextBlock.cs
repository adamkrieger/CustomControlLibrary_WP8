using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Data;
using GridExtensions;

namespace ConicalControls
{
	public class StrikeableTextBlock : ViewBase
	{
		private TextBlock _textBlock;
		private Grid _grid;

		public StrikeableTextBlock()
		{
			InitializeGrid();
			InitializeTextBlock();
			InitializeStrikeThrough();
		}

		private void InitializeGrid()
		{
			_grid = new Grid();
			_grid.AddAutoHeightRow();
			_grid.AddAutoWidthColumn();

			var backgroundBinding = new Binding
			{
				Source = this,
				Path = new PropertyPath("Background")
			};

			_grid.SetBinding(Grid.BackgroundProperty, backgroundBinding);
		}

		private void InitializeTextBlock()
		{
			_textBlock = new TextBlock
			{
				VerticalAlignment = VerticalAlignment.Center,
				Margin = new Thickness(2, 0, 2, 2)
			};

			var textBinding = new Binding
			{
				Source = this,
				Path = new PropertyPath("Text")
			};

			var foregroundBinding = new Binding
			{
				Source = this,
				Path = new PropertyPath("Foreground")
			};

			_textBlock.SetBinding(TextBlock.TextProperty, textBinding);
			_textBlock.SetBinding(TextBlock.ForegroundProperty, foregroundBinding);
			
			_grid.AddChild(_textBlock, 0, 0);
		}

		private void InitializeStrikeThrough()
		{
			var strikeLine = new Rectangle
			{
				Height = 2,
				//Fill = new SolidColorBrush(Colors.Black),
				VerticalAlignment = VerticalAlignment.Center,
				HorizontalAlignment = HorizontalAlignment.Stretch
			};

			var visibilityBinding = new Binding
			{
				Source = this,
				Path = new PropertyPath("StrikeThrough"),
				Converter = new BoolToVisibilityConverter()
			};

			var fillBinding = new Binding
			{
				Source = this,
				Path = new PropertyPath("StrikeThroughBrush")
			};

			strikeLine.SetBinding(Rectangle.VisibilityProperty, visibilityBinding);
			strikeLine.SetBinding(Rectangle.FillProperty, fillBinding);

			_grid.AddChild(strikeLine, 0, 0);
		}

		public string Text { 
			get { return GetValue(TextProperty).ToString(); } 
			set { SetValue(TextProperty,value); } 
		}

		private DependencyProperty TextProperty = 
			DependencyProperty.Register("Text",typeof(string),typeof(StrikeableTextBlock),new PropertyMetadata(null));

		public bool StrikeThrough
		{
			get { return (bool)(GetValue(StrikeThroughProperty) ?? false); }
			set { SetValue(StrikeThroughProperty, value); }
		}

		private DependencyProperty StrikeThroughProperty =
			DependencyProperty.Register("StrikeThrough", typeof(bool), typeof(StrikeableTextBlock), new PropertyMetadata(false));

		public Brush StrikeThroughBrush
		{
			get { return (Brush)(GetValue(StrikeThroughBrushProperty) ?? false); }
			set { SetValue(StrikeThroughBrushProperty, value); }
		}

		private DependencyProperty StrikeThroughBrushProperty =
			DependencyProperty.Register("StrikeThroughBrush", typeof(Brush), typeof(StrikeableTextBlock), new PropertyMetadata(new SolidColorBrush(Colors.Black)));

		public Brush Foreground
		{
			get { return (Brush)(GetValue(ForegroundProperty) ?? false); }
			set { SetValue(ForegroundProperty, value); }
		}

		private DependencyProperty ForegroundProperty =
			DependencyProperty.Register("Foreground", typeof(Brush), typeof(StrikeableTextBlock), new PropertyMetadata(new SolidColorBrush(Colors.Black)));

		public Brush Background
		{
			get { return (Brush)(GetValue(BackgroundProperty) ?? false); }
			set { SetValue(BackgroundProperty, value); }
		}

		private DependencyProperty BackgroundProperty =
			DependencyProperty.Register("Background", typeof(Brush), typeof(StrikeableTextBlock), new PropertyMetadata(null));

		public UIElement GetView()
		{
			return _grid;
		}
	}
}
