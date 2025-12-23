using WebApplication2.Models.DTO;

namespace WebApplication2.DAL
{
    public interface IDonorDal
    {
        List<donorDTO> GetAll();
        // שיטת הסינון החדשה
        List<donorDTO> GetByFilter(string? name, string? email, string? giftName);
        void Add(donorDTO newDonor);
        void Update(donorDTO donorDto);
        void Delete(int id);
    }
}