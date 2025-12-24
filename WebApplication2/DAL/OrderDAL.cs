using WebApplication2.Models;
using WebApplication2.Models.DTO;

namespace WebApplication2.DAL
{
    public class OrderDAL : IOrderDal
    {
        private readonly StoreContext _context;

        public OrderDAL(StoreContext context)
        {
            _context = context;
        }


        public int AddOrder(OrderModel order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges(); // שומר את ההזמנה ואת כל ה-OrderItems שקשורים אליה
            return order.Id;
        }

        public List<PurchaserDetailsDto> GetPurchasersByGiftId(int giftId)//שולף את כל הרוכשים של מתנה מסוימת
        {
            return _context.OrderTicket
                .Where(t => t.GiftId == giftId)
                .Select(t => new PurchaserDetailsDto
                {
                    CustomerName = t.Order.User.Name, // Use Name property instead of FirstName + LastName
                    Email = t.Order.User.Email,
                    TicketsCount = t.Quantity
                })
                .ToList();
        }

        public List<OrderModel> GetUserOrders(int userId)//שולף את כל הרכישות של משתמש מסוים
        {
            return _context.Orders
                .Where(o => o.UserId == userId)

                .ToList();
        }
    }
}