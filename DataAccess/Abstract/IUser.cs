using Core.DataAccess;
using Core.Entities.Concrete;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IUser : IEntityRepository<User>
    {
        List<OperationClaim> GetClaims(User user);

        List<User> GetListByUserId(int id);
    }
}
