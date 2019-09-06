using AvenueOne.ViewModels.ModelViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Interfaces.ViewModelInterfaces
{
    public interface IPersonViewModel : IPerson, IModelViewModel
    {
        #region Properties
        IPerson Person { get; set; }
        byte[] GenderValues { get; }
        byte[] CivilStatusValues { get; }
        bool IsNotMaiden { get; }
        //string Id { get; set; }
        //string FirstName { get; set; }
        //string MiddleName { get; set; }
        //string LastName { get; set; }
        //string MaidenName { get; set; }
        //string FullName { get; set; }
        //GenderType Gender { get; set; }
        //CivilStatusType CivilStatus { get; set; }
        //string Nationality { get; set; }
        //DateTime? BirthDate { get; set; }
        #endregion

    }
}
