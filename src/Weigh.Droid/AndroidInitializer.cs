﻿using System;
using System.Collections.Generic;
using System.Linq;
using Prism;
using Prism.Ioc;
using DryIoc;
using Prism.DryIoc;

namespace Weigh.Droid
{
    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register Any Platform Specific Implementations that you cannot 
            // access from Shared Code
        }
    }
}
