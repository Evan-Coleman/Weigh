using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Weigh.Events;
using Weigh.Localization;
using Weigh.Models;

namespace Weigh.ViewModels
{
	public class AddEntryPageViewModel : ViewModelBase
    {
        public AddEntryPageViewModel(INavigationService navigationService, IEventAggregator ea)
            : base(navigationService)
        {
            Title = AppResources.AddEntryPageTitle;
        }





        public async void AddWeightToList()
        {
            // URGENT TODO: Issue with validation (Fresh install -> Enter info (235lb) -> Enter weight (234lb) = validation fail CHECK INTO IT
            ButtonEnabled = false;
            // Disabling the 12hr restriction for now
            if ((SettingValsValidated.LastWeighDate - DateTime.UtcNow).TotalHours > 11231232313232)
            {
                ButtonEnabled = true;
                // TODO: Add an error message, less than 12 hours since last entry
                return;
            }
            else
            {
                _newWeight = new WeightEntry();
                SettingValsValidated.LastWeight = Convert.ToDouble(SettingValsValidated.Weight);
                _newWeight.WeightDelta = NewWeightEntry - SettingValsValidated.LastWeight;
                _newWeight.Weight = NewWeightEntry;
                _newWeight.WaistSize = NewWaistSizeEntry;
                SettingValsValidated.WaistSize = _newWeight.WaistSize.ToString();
                SettingValsValidated.Weight = _newWeight.Weight.ToString();
                SettingValsValidated.Weight = _newWeight.Weight.ToString();
                SettingValsValidated.WaistSize = _newWeight.WaistSize.ToString();
                SettingValsValidated.LastWeighDate = DateTime.UtcNow;
                SettingValsValidated.DistanceToGoalWeight = Convert.ToDouble(SettingValsValidated.Weight) - Convert.ToDouble(SettingValsValidated.GoalWeight);

                SettingValsValidated.ValidateGoal();
                SettingValsValidated.SaveSettingValsToDevice();
                _ea.GetEvent<AddWeightEvent>().Publish(_newWeight);
                _ea.GetEvent<SendSetupInfoToSettingsEvent>().Publish(SettingValsValidated);
                await App.Database.SaveWeightAsync(_newWeight);
            }
            ButtonEnabled = true;
        }
    }
}
