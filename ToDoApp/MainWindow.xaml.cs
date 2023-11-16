using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ToDoApp
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void AddToDoButton_Click(object sender, RoutedEventArgs e)
		{
			string toDoText = ToDoInput.Text;

			if (!string.IsNullOrEmpty(toDoText))
			{
				Button toDoItemButton = new Button
				{
					Content = new StackPanel
					{
						Orientation = Orientation.Horizontal,
						Children =
						{
							new Label
							{
								Content = "\u2022", // Unicode character for a square to represent bullet point in the list
								FontSize = 18,
								VerticalContentAlignment = VerticalAlignment.Stretch,
							},
							new TextBlock
							{
								Text = toDoText,
								Margin = new Thickness(10),
								TextDecorations = new TextDecorationCollection(),
								VerticalAlignment = VerticalAlignment.Center,
							},
						},
					},
					HorizontalContentAlignment = HorizontalAlignment.Left, 
					Background = Brushes.Transparent, 
					BorderBrush = Brushes.Transparent, 
					BorderThickness = new Thickness(0), 
					Cursor = Cursors.Hand, 
					ClickMode = ClickMode.Press, 
					Tag = false, 
				};

				toDoItemButton.Click += ToDoItemButton_Click;

				ToDoList.Children.Add(toDoItemButton);

				ToDoInput.Clear();
			}
		}

		private void Window_KeyDown(object sender, KeyEventArgs e) //use click logic when pressing Enter
		{
			if (e.Key == Key.Enter)
			{
				AddToDoButton_Click(sender, e);
			}
		}

		private void ToDoInput_GotFocus(object sender, RoutedEventArgs e)
		{
			TextBox textBox = (TextBox)sender;
			if (textBox.Text == "Type your toDo here and press Enter or click the button below...")
			{
				textBox.Text = string.Empty;
				textBox.Foreground = Brushes.Black;
			}
		}
		private void ToDoItemButton_Click(object sender, RoutedEventArgs e) //Strikethrough finished items
		{
			Button button = (Button)sender;
			StackPanel stackPanel = (StackPanel)button.Content;
			TextBlock textBlock = stackPanel.Children[1] as TextBlock;
			bool isStrikethrough = (bool)button.Tag;

			if (isStrikethrough)
			{
				textBlock.TextDecorations = null;
			}
			else
			{
				textBlock.TextDecorations = TextDecorations.Strikethrough;
			}

			button.Tag = !isStrikethrough;
		}
	}
}
