using Core.Utilities.Result.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICarService
    {
        IResult CarExists(string numberPlate);
        IResult Add(CarAddDto carAddDto);
        IResult Update(CarUpdateDto carUpdateDto);
        IDataResult<List<CarListDto>> GetList(int userId);
        
        IDataResult<List<CarListDto>> GetCarList();
        IDataResult <List<CarListDto>> GetCarByCarId(int cityId);
    }
}
