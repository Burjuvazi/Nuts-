using AutoMapper;
using Kuruyemis.Business.Abstract;
using Kuruyemis.DataAccess.Abstract;
using Nuts.Business.ValidationRules.FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuruyemis.Business.Concrete
{
    public class SellerManager : ISellerService
    {
        public ISellerDal _sellerDal { get; set; }
        public IMapper _mapper { get; set; }
       

        public SellerManager(ISellerDal sellerDal, IMapper mapper)
        {
            _sellerDal = sellerDal;
            _mapper = mapper;
           
        }

        public SellerValidator rules { get; set; }
    }
}
