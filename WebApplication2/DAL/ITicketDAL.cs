using WebApplication2.Models.DTO;

namespace WebApplication2.DAL
{
    public interface ITicketDAL
    {
       
        public Task<TicketDTO> GetById(int id);
        public Task<bool> Update(TicketDTO ticketDto);
        public Task<TicketDTO> Create(TicketDTO ticketDto);
        public Task<bool> Delete(int id);
        List<TicketDTO> GetAllTickets();
    }
}
