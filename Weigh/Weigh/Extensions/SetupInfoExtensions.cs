using System;
using System.Collections.Generic;
using System.Text;
using Acr.UserDialogs;
using Weigh.Models;

namespace Weigh.Extensions
{
    public static class SetupInfoExtensions
    {
        public static void CalculateBMIBMRRDCI(this SetupInfo _setupInfo)
        {
            double Weight = Convert.ToDouble(_setupInfo.Weight);
            double Feet = Convert.ToDouble(_setupInfo.HeightMajor);
            int Inches = Convert.ToInt32(_setupInfo.HeightMinor);
            double HeightMajor = Convert.ToDouble(_setupInfo.HeightMajor);
            double GoalWeight = Convert.ToDouble(_setupInfo.GoalWeight);
            int Age = Convert.ToInt32(_setupInfo.Age);

            // Units are metric if false, so do conversion here
            if (_setupInfo.Units == false)
            {
                (Feet, Inches) = HeightMajor.CentimetersToFeetInches();
                Weight = Weight.KilogramsToPounds();
            }

            _setupInfo.BMI = (Weight / Math.Pow(((Feet * 12) + Inches), 2)) * 703;

            // Categories based on site here: https://www.nhlbi.nih.gov/health/educational/lose_wt/BMI/bmicalc.htm
            if (_setupInfo.BMI < 18.5)
            {
                _setupInfo.BMICategory = "Underweight";
            }

            if (_setupInfo.BMI >= 18.5 && _setupInfo.BMI <= 24.9)
            {
                _setupInfo.BMICategory = "Normal Weight";
            }

            if (_setupInfo.BMI >= 25 && _setupInfo.BMI <= 29.9)
            {
                _setupInfo.BMICategory = "Overweight";
            }

            if (_setupInfo.BMI >= 30)
            {
                _setupInfo.BMICategory = "Obese";
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

            _setupInfo.WeightPerDayToMeetGoal = (Weight - GoalWeight) / (_setupInfo.GoalDate - DateTime.UtcNow).TotalDays;
            _setupInfo.WeightPerWeekToMeetGoal = _setupInfo.WeightPerDayToMeetGoal * 7;
            _setupInfo.RequiredCaloricDefecit = 500 * _setupInfo.WeightPerWeekToMeetGoal;
            _setupInfo.RecommendedDailyCaloricIntake = (int)_setupInfo.BMR - _setupInfo.RequiredCaloricDefecit;
        }

        public static bool  ValidateGoal(this SetupInfo _setupInfo)
        {
            double Weight = Convert.ToDouble(_setupInfo.Weight);
            double GoalWeight = Convert.ToDouble(_setupInfo.GoalWeight);

            _setupInfo.CalculateBMIBMRRDCI();

            if (_setupInfo.Sex == true && _setupInfo.RecommendedDailyCaloricIntake < 1200)
            {
                // Min calories/day for women is 1200
                _setupInfo.RequiredCaloricDefecit = _setupInfo.BMR - 1300;
                _setupInfo.WeightPerWeekToMeetGoal = _setupInfo.RequiredCaloricDefecit / 500;
                _setupInfo.DaysToAddToMeetMinimum = (int)((Weight - GoalWeight) / (_setupInfo.WeightPerWeekToMeetGoal / 7));
                _setupInfo.GoalDate = DateTime.Now.ToLocalTime().AddDays(_setupInfo.DaysToAddToMeetMinimum);
                UserDialogs.Instance.Alert(string.Format("Goal date was too soon, and has been set to: {0:MM/dd/yy}", _setupInfo.GoalDate));
                return false;
                //Create(async token => await this.Dialogs.AlertAsync("Test alert", "Alert Title", null, token));
            }
            if (_setupInfo.Sex == false && _setupInfo.RecommendedDailyCaloricIntake < 1800)
            {
                // Min calories/day for men is 1800
                _setupInfo.RequiredCaloricDefecit = _setupInfo.BMR - 1900;
                _setupInfo.WeightPerWeekToMeetGoal = _setupInfo.RequiredCaloricDefecit / 500;
                _setupInfo.DaysToAddToMeetMinimum = (int)((Weight - GoalWeight) / (_setupInfo.WeightPerWeekToMeetGoal / 7));
                _setupInfo.GoalDate = DateTime.Now.ToLocalTime().AddDays(_setupInfo.DaysToAddToMeetMinimum);
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
