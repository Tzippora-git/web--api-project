using WebApplication2.Models.DTO;

namespace WebApplication2.BLL
{
    
        public interface ITicketBLL
        {
            List<TicketDTO> GetAllTickets();
           Task <TicketDTO> GetTicketId(int id);
            void AddTicket(TicketDTO ticket);
            void UpdateTicket(TicketDTO ticket);
            void DeleteTicket(int id);
        
    }
}
