using WebApplication2.DAL;
using WebApplication2.Models.DTO;

namespace WebApplication2.BLL
{
    public class DonorServiceBLL : IDonorBLL
    {
        private readonly IDonorDal _donorDal;

        public DonorServiceBLL(IDonorDal donorDal) => _donorDal = donorDal;

        public List<donorDTO> GetAllDonors() => _donorDal.GetAll();
        // קריאה ל-DAL לביצוע הסינון
        public List<donorDTO> GetDonorsByFilter(string? name, string? email, string? giftName)
            => _donorDal.GetByFilter(name, email, giftName);
        public void AddDonor(donorDTO donor) => _donorDal.Add(donor);
        public void UpdateDonor(donorDTO donor) => _donorDal.Update(donor);
        public void DeleteDonor(int id) => _donorDal.Delete(id);
    }
}