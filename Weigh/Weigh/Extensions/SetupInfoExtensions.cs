using System;
using System.Collections.Generic;
using System.Text;
using Acr.UserDialogs;
using Weigh.Localization;
using Weigh.Models;

namespace Weigh.Extensions
{
    public static class SetupInfoExtensions
    {
        public static void CalculateBMIBMRRDCI(this SettingValsValidated _setupInfo)
        {
            double Weight = Convert.ToDouble(_setupInfo.Weight);
            double Feet = Convert.ToDouble(_setupInfo.HeightMajor);
            int Inches = Convert.ToInt32(_setupInfo.HeightMinor);
            double HeightMajor = Convert.ToDouble(_setupInfo.HeightMajor);
            double GoalWeight = Convert.ToDouble(_setupInfo.GoalWeight);
            int Age = Convert.ToInt32(_setupInfo.Age);

            // Units are metric if false, so do conversion here
            if (SettingVals.Units == false)
            {
                (Feet, Inches) = HeightMajor.CentimetersToFeetInches();
                Weight = Weight.KilogramsToPounds();
            }

            SettingVals.BMI = (Weight / Math.Pow(((Feet * 12) + Inches), 2)) * 703;

            // Categories based on site here: https://www.nhlbi.nih.gov/health/educational/lose_wt/BMI/bmicalc.htm
            if (SettingVals.BMI < 18.5)
            {
                SettingVals.BMICategory = AppResources.UnderweightBMICategory;
            }

            if (SettingVals.BMI >= 18.5 && SettingVals.BMI <= 24.9)
            {
                SettingVals.BMICategory = AppResources.NormalWeightBMICategory;
            }

            if (SettingVals.BMI >= 25 && SettingVals.BMI <= 29.9)
            {
                SettingVals.BMICategory = AppResources.OverweightBMICategory;
            }

            if (SettingVals.BMI >= 30)
            {
                SettingVals.BMICategory = AppResources.ObeseWeightBMICategory;
            }

            if (SettingVals.Sex == false)
            {
                SettingVals.BMR = 66 + (6.2 * Weight) + (12.7 * ((Feet * 12) + Inches)) - (6.76 * Age);
            }
            else
            {
                SettingVals.BMR = 655.1 + (4.35 * Weight) + (4.7 * ((Feet * 12) + Inches)) - (4.7 * Age);
            }
            if (SettingVals.PickerSelectedItem == AppResources.LowActivityPickItem)
            {
                SettingVals.BMR *= 1.2;
            }
            if (SettingVals.PickerSelectedItem == AppResources.LightActivityPickItem)
            {
                SettingVals.BMR *= 1.375;
            }
            if (SettingVals.PickerSelectedItem == AppResources.MediumActivityPickItem)
            {
                SettingVals.BMR *= 1.55;
            }
            if (SettingVals.PickerSelectedItem == AppResources.HeavyActivityPickItem)
            {
                SettingVals.BMR *= 1.725;
            }

            SettingVals.WeightPerDayToMeetGoal = (Weight - GoalWeight) / (SettingVals.GoalDate - DateTime.UtcNow).TotalDays;
            SettingVals.WeightPerWeekToMeetGoal = SettingVals.WeightPerDayToMeetGoal * 7;
            SettingVals.RequiredCaloricDefecit = 500 * SettingVals.WeightPerWeekToMeetGoal;
            SettingVals.RecommendedDailyCaloricIntake = (int)SettingVals.BMR - SettingVals.RequiredCaloricDefecit;
        }

        public static bool  ValidateGoal(this SettingValsValidated _setupInfo)
        {
            double Weight = Convert.ToDouble(_setupInfo.Weight);
            double GoalWeight = Convert.ToDouble(_setupInfo.GoalWeight);

            _setupInfo.CalculateBMIBMRRDCI();

            if (SettingVals.Sex == true && SettingVals.RecommendedDailyCaloricIntake < 1200)
            {
                // Min calories/day for women is 1200
                SettingVals.RequiredCaloricDefecit = SettingVals.BMR - 1300;
                SettingVals.WeightPerWeekToMeetGoal = SettingVals.RequiredCaloricDefecit / 500;
                SettingVals.DaysToAddToMeetMinimum = (int)((Weight - GoalWeight) / (SettingVals.WeightPerWeekToMeetGoal / 7));
                SettingVals.GoalDate = DateTime.Now.ToLocalTime().AddDays(SettingVals.DaysToAddToMeetMinimum);
                UserDialogs.Instance.Alert(string.Format(AppResources.GoalTooSoonPopup, SettingVals.GoalDate));
                return false;
                //Create(async token => await this.Dialogs.AlertAsync("Test alert", "Alert Title", null, token));
            }
            if (SettingVals.Sex == false && SettingVals.RecommendedDailyCaloricIntake < 1800)
            {
                // Min calories/day for men is 1800
                SettingVals.RequiredCaloricDefecit = SettingVals.BMR - 1900;
                SettingVals.WeightPerWeekToMeetGoal = SettingVals.RequiredCaloricDefecit / 500;
                SettingVals.DaysToAddToMeetMinimum = (int)((Weight - GoalWeight) / (SettingVals.WeightPerWeekToMeetGoal / 7));
                SettingVals.GoalDate = DateTime.Now.ToLocalTime().AddDays(SettingVals.DaysToAddToMeetMinimum);
                UserDialogs.Instance.Alert(string.Format(AppResources.GoalTooSoonPopup, SettingVals.GoalDate));
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
