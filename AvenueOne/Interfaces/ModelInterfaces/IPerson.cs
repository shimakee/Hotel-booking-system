using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Interfaces
{
    public enum GenderType
    {
        Male = 1,
        Female = 2
    }

    public enum CivilStatusType
    {
        Single = 1,
        Married = 2,
        Widowed = 3,
        Annuled = 4,
        Divorced = 5
    }

    public interface IPerson
    {
        string Id { get; set; }
        string FirstName { get; set; }
        string MiddleName { get; set; }
        string LastName { get; set; }
        string MaidenName { get; set; }
        GenderType Gender { get; set; }
        CivilStatusType CivilStatus { get; set; }
        string Nationality { get; set; }
        DateTime BirthDate { get; set; }

        string FullName { get; set; }
    }
}
