using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class PermitImageManager:IPermitImageService
    {
        private readonly IPermitImage _permitImage;
        public PermitImageManager(IPermitImage permitImage)
        {
            _permitImage = permitImage;
        }

        public void Add(PermitImageTbl permitImageTbl)
        {
            _permitImage.Add(permitImageTbl);
        }

        public void Update(PermitImageTbl permitImageTbl)
        {
           _permitImage.Update(permitImageTbl);
        }

        public void Delete(PermitImageTbl permitImageTbl)
        {
            _permitImage.Delete(permitImageTbl);
        }

        public List<PermitImageTbl> GetListByCarId(int carId)
        {
            return _permitImage.GetList(p => p.CarID == carId);
        }
    }
}
