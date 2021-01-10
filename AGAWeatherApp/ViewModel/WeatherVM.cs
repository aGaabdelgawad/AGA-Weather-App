using AGAWeatherApp.Model;
using AGAWeatherApp.ViewModel.Commands;
using AGAWeatherApp.ViewModel.Helpers;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace AGAWeatherApp.ViewModel
{
    /// <summary>
    /// The Weather ViewModel which controls the Weather View.
    /// </summary>
    public class WeatherVM : INotifyPropertyChanged
    {
        private string query;

        /// <summary>
        /// Query property "the query to be used by the user to search for a city".
        /// </summary>
        public string Query
        {
            get { return query; }
            set
            {
                query = value;
                OnPropertyChanged("Query");
            }
        }
        
        private City selectedCity;

        /// <summary>
        /// SelectedCity property "The selected city by the user".
        /// </summary>
        public City SelectedCity
        {
            get { return selectedCity; }
            set
            {
                if (value != null)
                {
                    selectedCity = value;
                    OnPropertyChanged("SelectedCity");
                    GetWeather();
                }
            }
        }

        private CurrentConditions currentConditions;

        /// <summary>
        /// CurrentConditions property "The weather conditions for the selected city".
        /// </summary>
        public CurrentConditions CurrentConditions
        {
            get { return currentConditions; }
            set
            {
                currentConditions = value;
                OnPropertyChanged("CurrentConditions");
            }
        }

        /// <summary>
        /// The Cities Observable Collection "List" property.
        /// </summary>
        public ObservableCollection<City> Cities { get; set; }

        /// <summary>
        /// The Search Command property.
        /// </summary>
        public SearchCommand SearchCommand { get; set; }

        /// <summary>
        /// The Exit Command property.
        /// </summary>
        public ExitCommand ExitCommand { get; set; }

        /// <summary>
        /// The About Command property.
        /// </summary>
        public AboutCommand AboutCommand { get; set; }

        /// <summary>
        /// The constructor of the WeatherVM.
        /// </summary>
        public WeatherVM()
        {
            // Initializing the SearchCommand property.
            SearchCommand = new SearchCommand(this);

            // Initializing the ExitCommand property.
            ExitCommand = new ExitCommand(this);

            // Initializing the AboutCommand property.
            AboutCommand = new AboutCommand(this);

            // Initializing the Cities property.
            Cities = new ObservableCollection<City>();

            // Viewing some information in the UI in case of Design Mode.
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
            {
                selectedCity = new City
                {
                    LocalizedName = "Cairo",
                    Country = new Area
                    {
                        LocalizedName = "Egypt"
                    },
                    AdministrativeArea = new Area
                    {
                        LocalizedName = "Cairo"
                    }
                };

                Cities.Add(SelectedCity);
                Cities.Add(SelectedCity);
                Cities.Add(SelectedCity);
                Cities.Add(SelectedCity);
                Cities.Add(SelectedCity);
                Cities.Add(SelectedCity);
                Cities.Add(SelectedCity);
                Cities.Add(SelectedCity);

                CurrentConditions = new CurrentConditions
                {
                    WeatherText = "Overcast",
                    HasPrecipitation = false,
                    Temperature = new Temperature
                    {
                        Metric = new UnitSystem
                        {
                            Value = 21
                        }
                    }
                };
            }
        }

        /// <summary>
        /// The event handler which handles the changing in a property value.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The method which invokes the event handler if a property changed.
        /// </summary>
        /// <param name="propertyName">The name of the property that has been changed.</param>
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// The method that makes the query search.
        /// </summary>
        public async void MakeQuery()
        {
            // Revieving the the list of cities from the API.
            var returnedCities = await AccuWeatherHelper.GetCities(Query);

            // Clearing the old cities from the Cities property.
            Cities.Clear();

            // Re-filling the Cities property with the new cities.
            if (returnedCities != null && returnedCities.Count > 0)
            {
                foreach (var c in returnedCities)
                {
                    Cities.Add(c);
                }
            }
            else
                // Showing the a message to the user in case of no results found.
                MessageBox.Show("No results found.", "Search Result", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// The method that gets the weather status.
        /// </summary>
        private async void GetWeather()
        {
            CurrentConditions = await AccuWeatherHelper.GetCurrentConditions(SelectedCity.Key);
        }

        /// <summary>
        /// The method for closing the weather window.
        /// </summary>
        public void Exit()
        {
            if (MessageBox.Show("Are you sure you want to close?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                Application.Current.Shutdown();
        }

        /// <summary>
        /// The method for displaying info about the app.
        /// </summary>
        public void About()
        {
            string aboutMessage = $"AGA Weather App 2021{Environment.NewLine}Version 1.0.0{Environment.NewLine}© 2021 Ahmed Gamal Abdel Gawad.{Environment.NewLine}All rights reserved.";
            MessageBox.Show(aboutMessage, "About AGA Weather App", MessageBoxButton.OK, MessageBoxImage.Information);
        }

    }
}
