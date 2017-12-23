using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;
using Weigh.Models;

namespace Weigh.Events
{
    public class AddWeightEvent : PubSubEvent<WeightEntry>
    {
    }
}
