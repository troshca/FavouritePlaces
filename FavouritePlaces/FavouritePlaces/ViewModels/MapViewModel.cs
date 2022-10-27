﻿using FavouritePlaces.CustomRenderer;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace FavouritePlaces.ViewModels
{
    public class MapViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand ADDCommand { get; }
        CustomPin pinTest2 = new CustomPin
        {
            Type = PinType.Place,
            Position = new Position(37.798150639335, -122.41183),
            Label = "Label2",
            Address = "Address2",
            Name = "Name2",
            IconType = "Shop",
            Url = "url2"
        };

        CustomPin pinTest = new CustomPin
        {
            Type = PinType.Generic,
            Position = new Position(37.79752, -122.40183),
            Label = "Xamarin San Francisco Office",
            Address = "394 Pacific Ave, San Francisco CA",
            Name = "Xamarin",
            IconType = "Restaurant",
            Url = "http://xamarin.com/about/"
        };
        CustomPin pin2 = new CustomPin
        {
            Type = PinType.Generic,
            Position = new Position(37.798150639335, -122.41183),
            Label = "Label",
            Address = "Address",
            Name = "Name",
            IconType = "Toilet",
            Url = "http://xamarin.com/about/"
        };

        //public ObservableCollection<CustomPin> Places { get; set; }


        CustomMap customMap;
        public MapViewModel()
        {
            customMap = new CustomMap();

            ADDCommand = new Command(ADD);
            ListOfPlacesViewModel listOfPlacesViewModel = new ListOfPlacesViewModel();
            //Places = new ObservableCollection<CustomPin>();
            MessagingCenter.Subscribe<Application, Models.Place>(Application.Current, "AddPlace", (sender, arg) =>
            {
                MessagingCenter.Send(Application.Current, "SelectItem", new CustomPin(arg));
                //Places.Add(new CustomPin(arg));
                App.Database.SaveItem(arg);
            });
            //customMap.CustomPins = Places;

        }


        private void Adds(List<CustomPin> pins)
        {
            MessagingCenter.Send(Application.Current, "SelectItems", pins);
        }

        private void ADD()
        {
            //Places.Add(pinTest2);
            List<CustomPin> sendPins = new List<CustomPin>();
            sendPins.Add(pin2);
            sendPins.Add(pinTest);
            Adds(sendPins);
        }

        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
