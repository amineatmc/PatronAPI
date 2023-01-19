using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IPermitImageService
    {
        void Add(PermitImageTbl permitImageTbl);
        void Update(PermitImageTbl permitImageTbl);
        void Delete(PermitImageTbl permitImageTbl);
        List<PermitImageTbl> GetListByCarId(int carId);
    }
}
