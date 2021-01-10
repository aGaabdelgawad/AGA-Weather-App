using System;
using System.Windows.Input;

namespace AGAWeatherApp.ViewModel.Commands
{
    /// <summary>
    /// The search command which controls the search button.
    /// </summary>
    public class SearchCommand : ICommand
    {
        /// <summary>
        /// The constructor of the class.
        /// </summary>
        /// <param name="vm">The weather view model containing this search command.</param>
        public SearchCommand(WeatherVM vm)
        {
            // Assigning the WeatherViewModel object to the class's Weather ViewModel property.
            VM = vm;
        }

        /// <summary>
        /// The WeatherViewModel property.
        /// </summary>
        public WeatherVM VM { get; set; }

        /// <summary>
        /// The event handler which handles the button's event.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// The method for checking the condition for firing the button's event.
        /// </summary>
        /// <param name="parameter">The parameter to be checked</param>
        /// <returns>True if the command can be executed, and false if not.</returns>
        public bool CanExecute(object parameter)
        {
            // Checking that query parameter containing some text.
            return !string.IsNullOrWhiteSpace(parameter as string);
        }

        /// <summary>
        /// The method for executing the command.
        /// </summary>
        /// <param name="parameter">The parameter to be checked</param>
        public void Execute(object parameter)
        {
            // Calling the MakeQuery method of the WeatherViewModel containing this search command.
            VM.MakeQuery();
        }
    }
}
