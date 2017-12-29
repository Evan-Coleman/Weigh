using System;
using System.Collections.Generic;
using System.Text;
using Acr.UserDialogs;
using Weigh.Extensions;

namespace Weigh.Behaviors
{
    public static class GoalValidation
    {
        public static bool ValidateGoal()
        {
            double BMR;

            double Feet = App.HeightMajor;
            int Inches = App.HeightMinor;
            double Weight = App.Weight;

            // Units are metric if false, so do conversion here
            if (App.Units == false)
            {
                (Feet, Inches) = App.HeightMajor.CentimetersToFeetInches();
                Weight = App.Weight.KilogramsToPounds();
            }

            if (App.Sex == false)
            {
                BMR = 66 + (6.2 * Weight) + (12.7 * ((Feet * 12) + Inches)) - (6.76 * App.Age);
            }
            else
            {
                BMR = 655.1 + (4.35 * Weight) + (4.7 * ((Feet * 12) + Inches)) - (4.7 * App.Age);
            }
            if (App.PickerSelectedItem == "No Exercise")
            {
                BMR *= 1.2;
            }
            if (App.PickerSelectedItem == "Light Exercise")
            {
                BMR *= 1.375;
            }
            if (App.PickerSelectedItem == "Moderate Exercise")
            {
                BMR *= 1.55;
            }
            if (App.PickerSelectedItem == "Heavy Exercise")
            {
                BMR *= 1.725;
            }

            double RecommendedDailyCaloricIntake;
            int daysToAddToMeetMinimum;
            double weightPerDayToMeetGoal = (App.Weight - App.GoalWeight) / (App.GoalDate - DateTime.UtcNow).TotalDays;
            double weightPerWeekToMeetGoal = weightPerDayToMeetGoal * 7;
            double RequiredCaloricDefecit = 500 * weightPerWeekToMeetGoal;
            RecommendedDailyCaloricIntake = (int)BMR - RequiredCaloricDefecit;
            if (App.Sex == true && RecommendedDailyCaloricIntake < 1200)
            {
                // Min calories/day for women is 1200
                // TODO: Implement something to handle this case
                RequiredCaloricDefecit = BMR - 1900;
                weightPerWeekToMeetGoal = RequiredCaloricDefecit / 500;
                daysToAddToMeetMinimum = (int)((App.Weight - App.GoalWeight) / (weightPerWeekToMeetGoal / 7));
                App.GoalDate = DateTime.Now.ToLocalTime().AddDays(daysToAddToMeetMinimum);
                UserDialogs.Instance.Alert(string.Format("Goal date was too soon, and has been set to: {0:MM/dd/yy}", App.GoalDate));
                return false;
                //Create(async token => await this.Dialogs.AlertAsync("Test alert", "Alert Title", null, token));
            }
            if (App.Sex == false && RecommendedDailyCaloricIntake < 1800)
            {
                // Min calories/day for men is 1800
                // TODO: Implement something to handle this case
                RequiredCaloricDefecit = BMR - 1900;
                weightPerWeekToMeetGoal = RequiredCaloricDefecit / 500;
                daysToAddToMeetMinimum = (int)((App.Weight - App.GoalWeight) / (weightPerWeekToMeetGoal / 7));
                App.GoalDate = DateTime.Now.ToLocalTime().AddDays(daysToAddToMeetMinimum);
                UserDialogs.Instance.Alert(string.Format("Goal date was too soon, and has been set to: {0:MM/dd/yy}", App.GoalDate));
                return false;
                // Keeping for future use maybe
                /*
                UserDialogs.Instance.Toast(new ToastConfig(string.Format("Goal date has been set to: {0:MM/dd/yy}", App.GoalDate))
                    .SetDuration(TimeSpan.FromSeconds(3))
                    .SetPosition(ToastPosition.Bottom));
                    */               
            }
            return true;
        }
    }
}
