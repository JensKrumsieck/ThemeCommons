using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ThemeCommons.Controls
{
    public class Titlebar : ContentControl
    {
        public Titlebar()
        {
            CommandBindings.Add(new CommandBinding(SystemCommands.MinimizeWindowCommand, MinimizeCommand_Executed));
            CommandBindings.Add(new CommandBinding(SystemCommands.MaximizeWindowCommand, MaximizeCommand_Executed));
            CommandBindings.Add(new CommandBinding(SystemCommands.CloseWindowCommand, CloseCommand_Executed));
            CommandBindings.Add(new CommandBinding(SystemCommands.RestoreWindowCommand, RestoreCommand_Executed));
        }

        private void MinimizeCommand_Executed(object sender, ExecutedRoutedEventArgs e) => WindowCommand(SystemCommands.MinimizeWindow);
        private void CloseCommand_Executed(object sender, ExecutedRoutedEventArgs e) => WindowCommand(SystemCommands.CloseWindow);
        private void MaximizeCommand_Executed(object sender, ExecutedRoutedEventArgs e) => WindowCommand(SystemCommands.MaximizeWindow);
        private void RestoreCommand_Executed(object sender, ExecutedRoutedEventArgs e) => WindowCommand(SystemCommands.RestoreWindow);

        internal void WindowCommand(Action<Window> action) => action(Window.GetWindow(this));
    }

}
