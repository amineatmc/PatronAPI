using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUser : EfEntityRepositoryBase<User, PatronContextDb>, IUser
    {
        public List<OperationClaim> GetClaims(User user)
        {
            using (var context = new PatronContextDb())
            {
                //var result = from x in context.UserOperationClaims.Where(p => p.UserId == user.UserID)
                //             join y in context.OperationClaims on x.OperationClaimId equals y.Id
                //             select new OperationClaim
                //             {
                //                 Id = x.OperationClaimId,
                //                 Name = y.Name
                //             };
                //return result.ToList();

                var result1 = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.UserOperationClaims
                                 on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == user.UserID
                             select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
                return result1.ToList();
            }

        }

        public List<User> GetListByUserId(int id)
        {
            using (var context = new PatronContextDb())
            {
                var data = context.User.Where(x => x.IsActive == true && x.UserID == id).ToList();
                return data;
            }
        }
    }
}
