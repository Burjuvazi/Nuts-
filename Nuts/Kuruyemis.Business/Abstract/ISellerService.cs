using AutoMapper;
using FluentValidation.Results;
using Kuruyemis.DataAccess.Abstract;
using Kuruyemis.Entities.Concrete;
using Kuruyemis.Entities.Dtos;
using Nuts.Business.ValidationRules.FluentValidation;



namespace Kuruyemis.Business.Abstract
{
    public interface ISellerService
    {
        ISellerDal _sellerDal { get; set; }
        IMapper _mapper { get; set; }
        SellerValidator rules { get; set; }
        public ValidationResult Validator(Seller seller)
        {
            return rules.Validate(seller);
        }

        public async Task<Seller> GetSellerById(int sellerId)
        {
            return await _sellerDal.GetAsync(x => x.Id == sellerId);
        }

        public async Task SellerAdd(SellerDto sellerDto)
        {
            Seller seller = _mapper.Map<Seller>(sellerDto);
            var result = Validator(seller);
            if (result.IsValid)
            {
                await _sellerDal.AddAsync(seller);
            }

        }
        public async Task SellerDeleteById(int sellerId)
        {
            Seller seller = await GetSellerById(sellerId);
            await _sellerDal.RemoveAsync(seller);
        }

        public async Task<IList<Seller>> SellerList()
        {
            return await _sellerDal.GetAllAsync();
        }

        public async Task<IList<Seller>> SellerListByName(string sellerName)
        {
            return await _sellerDal.GetAllAsync(x => x.SellerName == sellerName);
        }

        public async Task SellerUpdate(Seller seller)
        {
            var result = Validator(seller);
            if (result.IsValid)
            {
                await _sellerDal.UpdateAsync(seller);
            }
        }
    }
}

