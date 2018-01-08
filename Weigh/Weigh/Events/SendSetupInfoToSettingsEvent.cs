using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;
using Weigh.Models;

namespace Weigh.Events
{
    class SendSetupInfoToSettingsEvent : PubSubEvent<SettingValsValidated>
    {
    }
}
