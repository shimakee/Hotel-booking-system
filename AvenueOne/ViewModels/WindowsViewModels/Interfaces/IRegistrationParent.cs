﻿using AvenueOne.Core.Models;
using AvenueOne.Interfaces;
using AvenueOne.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.ViewModels.WindowsViewModels.Interfaces
{
    public interface IRegistrationParent : IWindowViewModel
    {
        void OnUserAdded(object source, UserEventArgs e);
    }
}
