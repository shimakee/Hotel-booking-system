using AvenueOne.Models;
using AvenueOne.ViewModels.Commands.ClassCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.ViewModels.Commands.UserCommands
{
    public class ClearUserCommand : ClearClassCommand<User>
    {
        public ClearUserCommand()
            :base()
        {

        }

        //public override void Execute(object parameter)
        //{
        //    try
        //    {
        //        if (this.ViewModel == null)
        //            throw new NullReferenceException("Viewmodel cannot be null.");
        //        //if (this.ViewModel.Model == null || this.ViewModel.ModelSelected == null)
        //        //    throw new NullReferenceException("Model or Selection cannot be null.");
                
        //    }
        //    catch (Exception exception)
        //    {
        //        //TODO: create logger
        //        throw;
        //    }
        //}

        protected override void Clear()
        {
            User user = new User();
            user.Person = new Person();
            ViewModel.ModelSelected = user;
        }
    }
}
