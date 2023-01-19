using Core.Entities.Concrete;
using Core.Utilities.Result.Abstract;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IUserService
    {
        //IResult Add(UserTbl user);
        //IDataResult<List<UserTbl>> GetList();
        //List<OperationClaim> GetClaims(UserTbl user);
        //UserTbl GetUser(UserTbl user);
        IDataResult<List<User>> GetList(int id);
        List<OperationClaim> GetClaims(User user);
        void Add(User user);
        IResult Update(UserUpdateDto userUpdate);
        User GetUserPhone(string phone);
        IDataResult<User> GetbyId(int id);
    }
}
