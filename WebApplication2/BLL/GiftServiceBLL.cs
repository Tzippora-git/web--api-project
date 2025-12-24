using WebApplication2.DAL; // מייבא את ה-DAL
using WebApplication2.Models.DTO; // מייבא DTO של מתנה

public class GiftServiceBLL : IGiftBLL // מימוש שירותי ה-BLL למתנות
{ // פתיחת מחלקה
    private readonly IGiftDal _giftDal; // שדה ל-DAL

    public GiftServiceBLL(IGiftDal giftDal) => _giftDal = giftDal; // בנאי מקבל תלותיות

    public List<GiftDTO> getAllGifts() => _giftDal.getAll(); // החזרת כל המתנות מה-DAL
    public List<GiftDTO> GetGiftsByFilter(string? name, string? donorName, int? minPurchasers)
        => _giftDal.GetByFilter(name, donorName, minPurchasers); // החזרת מתנות לפי סינון מה-DAL
    // עדכון הפונקציה הקיימת להשתמש בסינון החדש
    public List<GiftDTO> GetFilteredGifts(string? name, string? donorName, int? minPurchasers)
    {
        return _giftDal.GetByFilter(name, donorName, minPurchasers);
    }

    // מימוש הפונקציות החדשות של המיון
    public List<GiftDTO> GetGiftsSortedByPrice()
    {
        return _giftDal.GetGiftsSortedByPrice();
    }
    public List<GiftDTO> GetMostPurchasedGifts()
    {
        return _giftDal.GetMostPurchasedGifts();
    }
    public void addGift(GiftDTO gift) => _giftDal.add(gift); // הוספת מתנה דרך DAL
    public void updateGift(GiftDTO gift) => _giftDal.update(gift); // עדכון מתנה דרך DAL
    public void deleteGift(int id) => _giftDal.delete(id); // מחיקת מתנה דרך DAL
} // סגירת מחלקה