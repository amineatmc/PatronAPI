using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCity:EfEntityRepositoryBase<CityTbl,PatronContextDb>,ICity
    {
        public List<CityTbl> GetCities()
        {
            using (var context=new PatronContextDb())
            {
                var result = context.CityTbl.ToList();
                return result;
            }
        }
    }
}
