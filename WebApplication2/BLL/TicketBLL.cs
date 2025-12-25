
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication2.DAL;
using WebApplication2.Models.DTO;

namespace WebApplication2.BLL;
public class TicketServiceBLL : ITicketBLL
{
    private readonly ITicketDAL _ticketDal;

    public TicketServiceBLL(ITicketDAL ticketDal)
    {
        _ticketDal = ticketDal;
    }

    public List<TicketDTO> GetAllTickets() => _ticketDal.GetAllTickets();

    public void AddTicket(TicketDTO ticket) => _ticketDal.Create(ticket);

    public void UpdateTicket(TicketDTO ticket) => _ticketDal.Update(ticket);

    public void DeleteTicket(int id) => _ticketDal.Delete(id);

    public Task<TicketDTO> GetTicketId(int id)
    {
        return _ticketDal.GetById(id);
    } 
    }

