using Business.Abstract;
using Business.Aspects;
using Business.Constans;
using Core.Entities.Concrete;
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
    public class OperationClaimManager : IOperationClaimService
    {
        private readonly IOperationClaim _operationClaim;
        public OperationClaimManager(IOperationClaim operationClaim)
        {
            _operationClaim = operationClaim;
        }
        [SecuredOperation("Admin")]
        public IResult Add(OperationClaim operationClaim)
        {
            _operationClaim.Add(operationClaim);
            return new SuccessResult(Messages.AddedOperationClaim);
        }
        [SecuredOperation("Admin")]
        public IResult Delete(OperationClaim operationClaim)
        {
            _operationClaim.Delete(operationClaim);
            return new SuccessResult(Messages.DeletedOperationClaim);
        }
        [SecuredOperation("Admin")]
        public IDataResult<OperationClaim> GetById(int Id)
        {
            return new SuccessDataResult<OperationClaim>(_operationClaim.Get(x => x.Id == Id));
        }
        [SecuredOperation("Admin")]
        public IDataResult<List<OperationClaim>> GetList()
        {
            return new SuccessDataResult<List<OperationClaim>>(_operationClaim.GetList());
        }
        [SecuredOperation("Admin")]
        public IResult Update(OperationClaim operationClaim)
        {
            _operationClaim.Update(operationClaim);
            return new SuccessResult(Messages.UpdateSuccessOperationClaim);
        }
    }
}
