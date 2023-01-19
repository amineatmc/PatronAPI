using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete;
using Entities.Dtos;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCar : EfEntityRepositoryBase<CarTbl, PatronContextDb>, ICar
    {
        public List<CarListDto> GetCarList()
        {
            using (var context = new PatronContextDb())
            {
                var result = from car in context.CarTbl
                             join recipe in context.Recipe on car.RecipesID equals recipe.Id
                             join user in context.User on car.UserID equals user.UserID
                             join city in context.CityTbl on car.CityID equals city.ID
                             select new CarListDto
                             {
                                 ID = car.ID,
                                 AddedAt = car.AddedAt,
                                 CarImages = context.PermitImageTbl.Where(p => p.CarID == car.ID).ToList(),
                                 Recipes=context.Recipe.Where(x=>x.Id==car.RecipesID).ToList(),
                                 CityID = car.CityID,
                                 CityName = city.CityName,
                                 IsActive = car.IsActive,
                                 NumberPlate = car.NumberPlate,
                                 UserID = car.UserID,
                                 UserName = user.UserName
                             };
                return result.Where(x=>x.IsActive==true).ToList();

            }
        }

        public List<CarTbl> GetListByUserId(int userId)
        {
            using (var context = new PatronContextDb())
            {
                var result = context.CarTbl.Where(p => p.UserID == userId && p.IsActive == true).ToList();
                return result;
            }
        }
    }
}
