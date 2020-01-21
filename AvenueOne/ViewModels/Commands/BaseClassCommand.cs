using AvenueOne.Interfaces.RepositoryInterfaces;
using AvenueOne.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.ViewModels.Commands
{
    public class BaseClassCommand
    {
        protected IUnitOfWork _unitOfWork;
        protected IDisplayService _displayService;

        public BaseClassCommand(IUnitOfWork unitOfWork, IDisplayService displayService)
        {
            this._unitOfWork = unitOfWork;
            this._displayService = displayService;
        }
    }
}
