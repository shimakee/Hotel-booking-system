using AvenueOne.Core.Models;
using AvenueOne.ViewModels.Commands.ClassCommands;
using AvenueOne.ViewModels.WindowsViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.ViewModels.Commands.TransactionCommands
{
    public class ClearTransactionCommand : ClearClassCommand<Transaction>
    {
        public ClearTransactionCommand()
            :base()
        {
        }

        protected override void Clear()
        {
            var viewModel = ViewModel as ITransactionViewModel;
            viewModel.ModelSelected = new Transaction();
            viewModel.BookingViewModel.ClearClassCommand.Execute(null);
        }
    }
}
