using WebApplication2.DAL;
using WebApplication2.Models.DTO;

namespace WebApplication2.BLL
{
    public interface IOrderBLL
    {
        // יצירת הזמנה חדשה והחזרת ה-ID שלה
        int PlaceOrder(OrderDTO orderDto);
        List<PurchaserDetailsDto> GetPurchasersForGift(int giftId);// קבלת פרטי רוכשים עבור מתנה מסוימת

        // קבלת היסטוריית הזמנות של משתמש
        List<OrderDTO> GetUserHistory(int userId);
    }
}