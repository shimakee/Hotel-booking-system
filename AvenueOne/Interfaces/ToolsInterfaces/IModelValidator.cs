using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Interfaces.ToolsInterfaces
{
    public interface IModelValidator<TEntity> where TEntity : class
    {
        bool Validate(TEntity entity);
        //HasContent
    }
}
