﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Interfaces
{
    public interface IAddUserProcessor
    {
        IUserModel AddUser(IUserModel userModel);
    }
}
