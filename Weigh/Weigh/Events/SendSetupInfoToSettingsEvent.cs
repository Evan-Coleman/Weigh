using Prism.Events;
using Weigh.Models;

namespace Weigh.Events
{
    internal class SendSetupInfoToSettingsEvent : PubSubEvent<SettingVals>
    {
    }
}