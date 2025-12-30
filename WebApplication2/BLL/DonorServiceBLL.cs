using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication2.DAL;
using WebApplication2.Models.DTO;

namespace WebApplication2.BLL
{
    public class DonorServiceBLL : IDonorBLL
    {
        private readonly IDonorDal _donorDal;

        public DonorServiceBLL(IDonorDal donorDal) => _donorDal = donorDal;

        public Task<List<DonorDTO>> GetAllDonorsAsync() => _donorDal.GetAll();

        public Task<List<DonorDTO>> GetDonorsByFilterAsync(string? name, string? email, string? giftName)
            => _donorDal.GetByFilter(name, email, giftName);

        public Task AddDonorAsync(DonorDTO donor) => _donorDal.Add(donor);

        public Task UpdateDonorAsync(DonorDTO donor) => _donorDal.Update(donor);

        public Task DeleteDonorAsync(int id) => _donorDal.Delete(id);
    }
}