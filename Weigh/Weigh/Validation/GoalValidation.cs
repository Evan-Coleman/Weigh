using System;
using System.Collections.Generic;
using System.Text;
using Acr.UserDialogs;
using Weigh.Events;
using Weigh.Extensions;
using Weigh.Helpers;
using Weigh.Models;

namespace Weigh.Validation
{
    public static class GoalValidation
    {
        public static bool ValidateGoala(ref SettingValsValidated _setupInfo)
        {
            double Feet = Convert.ToDouble(_setupInfo.HeightMajor);
            int Inches = Convert.ToInt32(_setupInfo.HeightMinor);
            double Weight = Convert.ToDouble(_setupInfo.Weight);
            double GoalWeight = Convert.ToDouble(_setupInfo.GoalWeight);
            double HeightMajor = Convert.ToDouble(_setupInfo.HeightMajor);
            int Age = Convert.ToInt32(_setupInfo.Age);

            _setupInfo.BMI = (Weight / Math.Pow(((Feet * 12) + Inches), 2)) * 703;

            // Units are metric if false, so do conversion here
            if (_setupInfo.Units == false)
            {
                (Feet, Inches) = HeightMajor.CentimetersToFeetInches();
                Weight = Weight.KilogramsToPounds();
            }

            if (_setupInfo.Sex == false)
            {
                _setupInfo.BMR = 66 + (6.2 * Weight) + (12.7 * ((Feet * 12) + Inches)) - (6.76 * Age);
            }
            else
            {
                _setupInfo.BMR = 655.1 + (4.35 * Weight) + (4.7 * ((Feet * 12) + Inches)) - (4.7 * Age);
            }
            if (_setupInfo.PickerSelectedItem == "No Exercise")
            {
                _setupInfo.BMR *= 1.2;
            }
            if (_setupInfo.PickerSelectedItem == "Light Exercise")
            {
                _setupInfo.BMR *= 1.375;
            }
            if (_setupInfo.PickerSelectedItem == "Moderate Exercise")
            {
                _setupInfo.BMR *= 1.55;
            }
            if (_setupInfo.PickerSelectedItem == "Heavy Exercise")
            {
                _setupInfo.BMR *= 1.725;
            }

            int daysToAddToMeetMinimum;
            double weightPerDayToMeetGoal = (Weight - GoalWeight) / (_setupInfo.GoalDate - DateTime.UtcNow).TotalDays;
            double weightPerWeekToMeetGoal = weightPerDayToMeetGoal * 7;
            double RequiredCaloricDefecit = 500 * weightPerWeekToMeetGoal;
            _setupInfo.RecommendedDailyCaloricIntake = (int)_setupInfo.BMR - RequiredCaloricDefecit;
            if (_setupInfo.Sex == true && _setupInfo.RecommendedDailyCaloricIntake < 1200)
            {
                // Min calories/day for women is 1200
                // TODO: Implement something to handle this case
                RequiredCaloricDefecit = _setupInfo.BMR - 1300;
                weightPerWeekToMeetGoal = RequiredCaloricDefecit / 500;
                daysToAddToMeetMinimum = (int)((Weight - GoalWeight) / (weightPerWeekToMeetGoal / 7));
                _setupInfo.GoalDate = DateTime.Now.ToLocalTime().AddDays(daysToAddToMeetMinimum);
                UserDialogs.Instance.Alert(string.Format("Goal date was too soon, and has been set to: {0:MM/dd/yy}", _setupInfo.GoalDate));
                return false;
                //Create(async token => await this.Dialogs.AlertAsync("Test alert", "Alert Title", null, token));
            }
            if (_setupInfo.Sex == false && _setupInfo.RecommendedDailyCaloricIntake < 1800)
            {
                // Min calories/day for men is 1800
                // TODO: Implement something to handle this case
                RequiredCaloricDefecit = _setupInfo.BMR - 1900;
                weightPerWeekToMeetGoal = RequiredCaloricDefecit / 500;
                daysToAddToMeetMinimum = (int)((Weight - GoalWeight) / (weightPerWeekToMeetGoal / 7));
                _setupInfo.GoalDate = DateTime.Now.ToLocalTime().AddDays(daysToAddToMeetMinimum);
                UserDialogs.Instance.Alert(string.Format("Goal date was too soon, and has been set to: {0:MM/dd/yy}", _setupInfo.GoalDate));
                return false;
                // Keeping for future use maybe
                /*
                UserDialogs.Instance.Toast(new ToastConfig(string.Format("Goal date has been set to: {0:MM/dd/yy}", AppState.GoalDate))
                    .SetDuration(TimeSpan.FromSeconds(3))
                    .SetPosition(ToastPosition.Bottom));
                    */               
            }
            return true;
        }
    }
}
