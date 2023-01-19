using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserCreditDal : EfEntityRepositoryBase<UserCredit, PatronContextDb>, IUserCreditDal
    {
        //public List<UserCredit> GetInvoiceUpdate(UserCreditAddDto userCreditAddDto)
        //{
        //    using (PatronContextDb context = new PatronContextDb())
        //    {
        //        Expression<Func<PatronContextDb, bool>> query = q =>
        //              (userCreditAddDto.Credit != null ? q.UserCredits.Equals(userCreditAddDto.Credit) : true);
                
        //        return context.Set<UserCredit>().Where(query).ToList();
        //    }
        //}
    }
}
