﻿using Core.Utilities.Result.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface INDataService
    {
        IResult Add(NDataTbl nDataTbl);
    }
}
