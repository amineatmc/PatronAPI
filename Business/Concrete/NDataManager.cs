using Business.Abstract;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class NDataManager : INDataService
    {
        private readonly INData _nData;
        public NDataManager(INData nData)
        {
            _nData = nData;
        }

        public IResult Add(NDataTbl nDataTbl)
        {
           _nData.Add(nDataTbl);
            return new SuccessResult("",200);
        }
    }
}
