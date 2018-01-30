using System;

namespace Weigh.Helpers
{
    public static class AppConstants
    {
        // Put constants here that are not of a sensitive nature

        public static string AppCenterStart
        {
            get
            {
                string startup = string.Empty;

                if(Guid.TryParse(Secrets.AppCenter_iOS_Secret, out Guid iOSSecret))
                {
                    startup += $"ios={iOSSecret};";
                }

                if(Guid.TryParse(Secrets.AppCenter_Android_Secret, out Guid AndroidSecret))
                {
                    startup += $"android={AndroidSecret};";
                }

                if(Guid.TryParse(Secrets.AppCenter_UWP_Secret, out Guid UWPSecret))
                {
                    startup += $"uwp={UWPSecret};";
                }

                return startup;
            }
        }
    }
}
