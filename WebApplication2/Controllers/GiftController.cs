using Microsoft.AspNetCore.Authorization; // מייבא Authorize attribute
using Microsoft.AspNetCore.Mvc; // מייבא MVC attributes
using WebApplication2.Models.DTO; // מייבא DTOs של מתנות

[ApiController] // מציין Controller מסוג API
[Route("api/[controller]")] // נתיב ניתוב
public class GiftsController : ControllerBase // בקר לטיפול במתנות
{ // התחלת מחלקה
    private readonly IGiftBLL _giftBll; // שדה ל-BLL של מתנות

    public GiftsController(IGiftBLL giftBll) // בנאי המקבל את ה-BLL
    { // התחלת בנאי
        _giftBll = giftBll; // שמירת ה-BLL בשדה
    } // סיום בנאי

    [HttpGet]
    public ActionResult<List<GiftDTO>> Get([FromQuery] string? name, [FromQuery] string? donorName, [FromQuery] int? minPurchasers)
    {
        // אם לא נשלחו פרמטרים, זה יחזיר את כל המתנות.
        // אם נשלחו, ה-BLL יבצע את הסינון שכתבנו ב-DAL.
        var gifts = _giftBll.GetGiftsByFilter(name, donorName, minPurchasers);
        return Ok(gifts);
    }

    [Authorize(Roles = "Manager")] // רק למנהל מחובר
    [HttpPost] // פעולה להוספה
    public IActionResult Add([FromBody] GiftDTO gift) // הוספת מתנה חדשה
    { // התחלת שיטה Add
        _giftBll.addGift(gift); // קריאה ל-BLL להוספת המתנה
        return Ok("המתנה נוספה בהצלחה"); // החזרת הצלחה
    } // סיום שיטה Add

    [Authorize(Roles = "Manager")] // רק למנהל
    [HttpPut] // עדכון מתנה
    public IActionResult Update([FromBody] GiftDTO gift) // עדכון מתנה
    { // התחלת שיטה Update
        _giftBll.updateGift(gift); // קריאה ל-BLL לעדכון
        return Ok("המתנה עודכנה בהצלחה"); // החזרת הצלחה
    } // סיום שיטה Update

    [Authorize(Roles = "Manager")] // רק למנהל
    [HttpDelete("{id}")] // מחיקה לפי Id
    public IActionResult Delete(int id) // מחיקת מתנה
    { // התחלת שיטה Delete
        _giftBll.deleteGift(id); // קריאה ל-BLL למחיקה
        return Ok("המתנה נמחקה מהמערכת"); // החזרת הצלחה
    } // סיום שיטה Delete
      // 1. נתיב למיון לפי המחיר הגבוה ביותר
    [HttpGet("sorted-by-price")]
    [Authorize(Roles = "manager")] // רק מנהל יכול לראות מיונים ניהוליים
    public IActionResult GetGiftsByPrice()
    {
        var gifts = _giftBll.GetGiftsSortedByPrice();
        return Ok(gifts);
    }

    // 2. נתיב למיון לפי המתנה הנרכשת ביותר
    [HttpGet("most-purchased")]
    [Authorize(Roles = "manager")]
    public IActionResult GetMostPurchased()
    {
        var gifts = _giftBll.GetMostPurchasedGifts();
        return Ok(gifts);
    }

    // 3. עדכון ה-Get הקיים כדי לתמוך בסינון המלא (כולל minPurchasers)
    [HttpGet("filter")]
    public IActionResult GetFiltered([FromQuery] string? name, [FromQuery] string? donorName, [FromQuery] int? minPurchasers)
    {
        var results = _giftBll.GetGiftsByFilter(name, donorName, minPurchasers);
        return Ok(results);
    }
} // סיום מחלקה