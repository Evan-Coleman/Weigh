using System;
using System.Collections.Generic;
using System.Text;

namespace Weigh.Helpers
{
    public static class AppState
    {
        #region App State Variables

        private static bool _units;
        public static bool Units
        {
            set
            {
                Settings.Units = value; _units = value;
            }
            get { return _units; }
        }

        private static int _age;
        public static int Age
        {
            set { Settings.Age = value; _age = value; }
            get { return _age; }
        }

        private static double _heightMajor;
        public static double HeightMajor
        {
            set { Settings.HeightMajor = value; _heightMajor = value; }
            get { return _heightMajor; }
        }

        private static int _heightMinor;
        public static int HeightMinor
        {
            set { Settings.HeightMinor = value; _heightMinor = value; }
            get { return _heightMinor; }
        }

        private static double _weight;
        public static double Weight
        {
            set
            {
                Settings.Weight = value; _weight = value;
            }
            get { return _weight; }
        }

        private static double _waistSize;
        public static double WaistSize
        {
            set
            {
                Settings.WaistSize = value; _waistSize = value;
            }
            get { return _waistSize; }
        }

        private static string _pickerSelectedItem;
        public static string PickerSelectedItem
        {
            set
            {
                Settings.PickerSelectedItem = value; _pickerSelectedItem = value;
            }
            get { return _pickerSelectedItem; }
        }




        private static bool _sex;
        public static bool Sex
        {
            set { Settings.Sex = value; _sex = value; }
            get { return _sex; }
        }

        private static double _goalWeight;
        public static double GoalWeight
        {
            set
            {
                Settings.GoalWeight = value; _goalWeight = value;
            }
            get { return _goalWeight; }
        }

        private static DateTime _goalDate;
        public static DateTime GoalDate
        {
            set
            {
                Settings.GoalDate = value; _goalDate = value;
            }
            get { return _goalDate; }
        }




        private static double _lastWeight;
        public static double LastWeight
        {
            set
            {
                Settings.LastWeight = value; _lastWeight = value;
            }
            get { return _lastWeight; }
        }

        private static double _initialWeight;
        public static double InitialWeight
        {
            set
            {
                Settings.InitialWeight = value; _initialWeight = value;
            }
            get { return _initialWeight; }
        }

        private static DateTime _initialWeightDate;
        public static DateTime InitialWeightDate
        {
            set
            {
                Settings.InitialWeightDate = value; _initialWeightDate = value;
            }
            get { return _initialWeightDate; }
        }



        private static DateTime _lastWeighDate;
        public static DateTime LastWeighDate
        {
            set
            {
                Settings.LastWeighDate = value; _lastWeighDate = value;
            }
            get { return _lastWeighDate; }
        }


        /*
        private static string _name;
        public static string Name
        {
            set
            {
                Settings.Name = value; _name = value;
            }
            get { return _name; }
        }
        */


        #endregion

        #region Initialize
        public static void InitializeApplicationState()
        {
            Sex = Settings.Sex;
            Age = Settings.Age;
            HeightMajor = Settings.HeightMajor;
            HeightMinor = Settings.HeightMinor;
            Weight = Settings.Weight;
            WaistSize = Settings.WaistSize;
            Units = Settings.Units;
            PickerSelectedItem = Settings.PickerSelectedItem;
            LastWeight = Settings.LastWeight;
            InitialWeight = Settings.InitialWeight;
            InitialWeightDate = Settings.InitialWeightDate;
            LastWeighDate = Settings.LastWeighDate;
            GoalWeight = Settings.GoalWeight;
            GoalDate = Settings.GoalDate;
        }
        #endregion
    }
}
