using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ImageSorter2._0.Model;
using ImageSorter2._0.ViewModel;

namespace ImageSorter2._0.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly InputBindingCollection _bindings = new InputBindingCollection();
        public MainWindow()
        {
            InitializeComponent();
            var logic = new MainLogic();
            var viewModel = new MainViewModel(logic);

            foreach (var dir in viewModel.Directories)
            {
                var shortcut = dir.Shortcut.Split(' ').Last();
                if(shortcut == "📂") continue;
                
                var shortcutArray = shortcut.Split('+');

                Enum.TryParse(shortcutArray.Last(), true, out Key key);
                var modifier = ModifierKeys.None;
                if (shortcutArray.Length == 2)
                {
                    Enum.TryParse(shortcutArray.First().Replace("Ctrl", "Control"), true, out modifier);
                }

                if (modifier != ModifierKeys.None)
                {
                    InputBindings.Add(new KeyBinding
                    {
                        Command = viewModel.MoveCommand,
                        CommandParameter = dir.Index,
                        Key = key,
                        Modifiers = modifier
                    });
                }
                else
                {
                    InputBindings.Add(new KeyBinding
                    {
                        Command = viewModel.MoveCommand,
                        CommandParameter = dir.Index,
                        Key = key
                    });
                }
            }
            
            DataContext = viewModel;
        }

        private void WindowMouseDown(object sender, MouseButtonEventArgs e)
        {
            Grid.Focus();
        }
        
        private void OpenSettings(object sender, RoutedEventArgs e)
        {
            var modalWindow = new Settings {Owner = this};
            modalWindow.ShowDialog();
        }

        private void NameBox_OnGotFocus(object sender, RoutedEventArgs e)
        {
            _bindings.AddRange(InputBindings);
            InputBindings.Clear();
        }
        
        private void NameBox_OnLostFocus(object sender, RoutedEventArgs e)
        {
            InputBindings.AddRange(_bindings);
            _bindings.Clear();
        }
    }
}