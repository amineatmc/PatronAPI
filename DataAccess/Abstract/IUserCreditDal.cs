using Core.DataAccess;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IUserCreditDal : IEntityRepository<UserCredit>
    {
        //List<UserCredit> GetAllBy(InvoiceDetailDto invoiceDetailDto);
        //public int GetInvoiceCount(InvoiceCountDto invoiceCountDto);
       // public List<UserCredit> GetInvoiceUpdate(UserCreditAddDto userCreditAddDto);

    }
}
