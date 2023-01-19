using Core.Utilities.Result.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IRecipeService
    {
        IResult Add(Recipe recipe);
        IResult Update(Recipe recipe);
        IDataResult<List<Recipe>> GetAllList();
    }
}
