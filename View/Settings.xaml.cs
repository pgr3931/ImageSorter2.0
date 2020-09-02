using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ImageSorter2._0.ViewModel;

namespace ImageSorter2._0.View
{
    public partial class Settings : Window
    {
        private readonly List<string> _metaShortcuts;

        public Settings()
        {
            DataContext = new SettingsViewModel();
            InitializeComponent();

            _metaShortcuts = new List<string>()
            {
                SetGetMetaShortcut("Undo", "Ctrl+Z"),
                SetGetMetaShortcut("Delete", "Delete"),
                SetGetMetaShortcut("Left", "Left"),
                SetGetMetaShortcut("Right", "Right")
            };
        }

        private static string SetGetMetaShortcut(string key, string defaultValue)
        {
            var value = IOManager.ReadSetting(key);
            if (!string.IsNullOrEmpty(value)) return value;

            IOManager.AddUpdateAppSettings(key, defaultValue);
            return defaultValue;
        }

        private void Shortcut(object sender, KeyEventArgs e)
        {
            // The text box grabs all input.
            e.Handled = true;

            // Fetch the actual shortcut key.
            var key = (e.Key == Key.System ? e.SystemKey : e.Key);

            // Ignore modifier keys.
            if (key == Key.LeftShift || key == Key.RightShift
                                     || key == Key.LeftCtrl || key == Key.RightCtrl
                                     || key == Key.LeftAlt || key == Key.RightAlt
                                     || key == Key.LWin || key == Key.RWin)
            {
                return;
            }

            // Build the shortcut key name.
            var shortcutText = new StringBuilder();
            if ((Keyboard.Modifiers & ModifierKeys.Control) != 0)
            {
                shortcutText.Append("Ctrl+");
            }

            if ((Keyboard.Modifiers & ModifierKeys.Shift) != 0)
            {
                shortcutText.Append("Shift+");
            }

            if ((Keyboard.Modifiers & ModifierKeys.Alt) != 0)
            {
                shortcutText.Append("Alt+");
            }

            shortcutText.Append(key.ToString());

            var viewModel = (MainViewModel) Owner.DataContext;
            if (viewModel.Directories.Any(dir => dir.Shortcut.Split(' ').Last() == shortcutText.ToString()))
            {
                return;
            }

            if (viewModel.Directories.Any(dir => _metaShortcuts.Contains(dir.Shortcut.Split(' ').Last())))
            {
                return;
            }

            // Update the text box.
            ((TextBox) sender).Text = shortcutText.ToString();
        }

        private void WindowMouseDown(object sender, MouseButtonEventArgs e) => Grid.Focus();
    }
}