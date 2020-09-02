using System;
using System.Collections.Generic;
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
        private readonly InputBindingCollection _metaShortcuts = new InputBindingCollection();

        public MainWindow()
        {
            InitializeComponent();
            var logic = new MainLogic();
            var viewModel = new MainViewModel(logic);

            foreach (var dir in viewModel.Directories)
            {
                var shortcut = dir.Shortcut.Split(' ').Last();
                if (shortcut == "📂") continue;

                var shortcutArray = shortcut.Split('+');
                GetKeyModifier(shortcutArray, out var key, out var modifier);

                dir.KeyBinding = AddBinding(viewModel.MoveCommand, key, modifier, dir.Index);
            }

            var metaShortcut = SetGetMetaShortcut("Undo", "Ctrl+Z");
            GetKeyModifier(metaShortcut.Split('+'), out var metaKey, out var metaModifier);
            _metaShortcuts.Add(AddBinding(viewModel.UndoCommand, metaKey, metaModifier));

            metaShortcut = SetGetMetaShortcut("Delete", "Delete");
            GetKeyModifier(metaShortcut.Split('+'), out metaKey, out metaModifier);
            _metaShortcuts.Add(AddBinding(viewModel.DeleteFileCommand, metaKey, metaModifier));

            metaShortcut = SetGetMetaShortcut("Left", "Left");
            GetKeyModifier(metaShortcut.Split('+'), out metaKey, out metaModifier);
            _metaShortcuts.Add(AddBinding(viewModel.PrevCommand, metaKey, metaModifier));

            metaShortcut = SetGetMetaShortcut("Right", "Right");
            GetKeyModifier(metaShortcut.Split('+'), out metaKey, out metaModifier);
            _metaShortcuts.Add(AddBinding(viewModel.NextCommand, metaKey, metaModifier));

            DataContext = viewModel;
        }

        private KeyBinding AddBinding(ICommand command, Key key, ModifierKeys modifier, object commandParameter = null)
        {
            var keyBinding = new KeyBinding
            {
                Command = command,
                CommandParameter = commandParameter,
                Key = key
            };
            if (modifier != ModifierKeys.None)
            {
                keyBinding.Modifiers = modifier;
            }
            
            InputBindings.Add(keyBinding);
            return keyBinding;
        }

        private static string SetGetMetaShortcut(string key, string defaultValue)
        {
            var value = IOManager.ReadSetting(key);
            if (!string.IsNullOrEmpty(value)) return value;

            IOManager.AddUpdateAppSettings(key, defaultValue);
            return defaultValue;
        }

        private static void GetKeyModifier(IReadOnlyCollection<string> shortcutArray, out Key key, out ModifierKeys modifier)
        {
            Enum.TryParse(shortcutArray.Last(), true, out key);
            if (shortcutArray.Count == 2)
            {
                Enum.TryParse(shortcutArray.First().Replace("Ctrl", "Control"), true, out modifier);
            }
            else
            {
                modifier = ModifierKeys.None;
            }
        }

        private void WindowMouseDown(object sender, MouseButtonEventArgs e)
        {
            Grid.Focus();
        }

        private void OpenSettings(object sender, RoutedEventArgs e)
        {
            var modalWindow = new Settings {Owner = this};
            modalWindow.ShowDialog();
            var mainViewModel = (MainViewModel) DataContext;
            mainViewModel.AlwaysOverride = IOManager.ReadSetting("AlwaysOverride") == "True";
            foreach (InputBinding metaShortcut in _metaShortcuts)
            {
                InputBindings.Remove(metaShortcut);
            }
            
            _metaShortcuts.Clear();
            
            var viewModel = (MainViewModel) DataContext;
            
            var newMetaShortcut = SetGetMetaShortcut("Undo", "Ctrl+Z");
            GetKeyModifier(newMetaShortcut.Split('+'), out var metaKey, out var metaModifier);
            _metaShortcuts.Add(AddBinding(viewModel.UndoCommand, metaKey, metaModifier));
            
            newMetaShortcut = SetGetMetaShortcut("Delete", "Delete");
            GetKeyModifier(newMetaShortcut.Split('+'), out metaKey, out metaModifier);
            _metaShortcuts.Add(AddBinding(viewModel.DeleteFileCommand, metaKey, metaModifier));
            
            newMetaShortcut = SetGetMetaShortcut("Left", "Left");
            GetKeyModifier(newMetaShortcut.Split('+'), out metaKey, out metaModifier);
            _metaShortcuts.Add(AddBinding(viewModel.PrevCommand, metaKey, metaModifier));
            
            newMetaShortcut = SetGetMetaShortcut("Right", "Right");
            GetKeyModifier(newMetaShortcut.Split('+'), out metaKey, out metaModifier);
            _metaShortcuts.Add(AddBinding(viewModel.NextCommand, metaKey, metaModifier));
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