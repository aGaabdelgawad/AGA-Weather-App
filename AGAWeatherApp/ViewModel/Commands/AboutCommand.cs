using System;
using System.Windows.Input;

namespace AGAWeatherApp.ViewModel.Commands
{
    /// <summary>
    /// The search command which controls about menu item.
    /// </summary>
    public class AboutCommand : ICommand
    {
        /// <summary>
        /// The constructor of the class.
        /// </summary>
        /// <param name="vm">The weather view model containing this search command.</param>
        public AboutCommand(WeatherVM vm)
        {
            VM = vm;
        }

        /// <summary>
        /// The WeatherViewModel property.
        /// </summary>
        public WeatherVM VM { get; set; }

        /// <summary>
        /// The event handler which handles the menu item's event.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// The method for checking the condition for firing the menu item's event.
        /// </summary>
        /// <param name="parameter">The parameter to be checked</param>
        /// <returns>True if the command can be executed, and false if not.</returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// The method for executing the command.
        /// </summary>
        /// <param name="parameter">The parameter to be checked</param>
        public void Execute(object parameter)
        {
            // Calling the About method of the WeatherViewModel containing this about command.
            VM.About();
        }
    }
}
