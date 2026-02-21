USE [RaffleDB];
GO

-- 1. ניקוי יסודי של הטבלאות
DELETE FROM Winners; DELETE FROM OrderTicket; DELETE FROM Orders;
DELETE FROM Gifts; DELETE FROM Categories; DELETE FROM Donors; DELETE FROM Users;

-- 2. הכנסת משתמשים ותורמים
INSERT INTO Users (Name, Email, Password, Role, IsDeleted) VALUES
(N'אדמין המערכת', 'admin@example.com', '123456', N'Admin', 0),
(N'משתמשת לדוגמה', 'user@example.com', '123456', N'Customer', 0);

INSERT INTO Donors (Name, Email, Address, IsDeleted) VALUES
(N'טק-סולושנס', 'tech@company.com', N'קרית המדע 5', 0),
(N'קרן החיוך', 'smile@foundation.org', N'רחוב האושר 10', 0);

INSERT INTO Categories (Name, IsDeleted) VALUES (N'אלקטרוניקה', 0), (N'פנאי ונופש', 0);

-- 3. שליפת מזהים (IDs) למשתנים
DECLARE @Cat1 INT = (SELECT TOP 1 Id FROM Categories WHERE Name = N'אלקטרוניקה');
DECLARE @Cat2 INT = (SELECT TOP 1 Id FROM Categories WHERE Name = N'פנאי ונופש');
DECLARE @D1 INT = (SELECT TOP 1 Id FROM Donors WHERE Name = N'טק-סולושנס');
DECLARE @D2 INT = (SELECT TOP 1 Id FROM Donors WHERE Name = N'קרן החיוך');

-- 4. הכנסת המתנות עם העדכון לאופניים חשמליים
INSERT INTO Gifts (Name, Description, ImageUrl, TicketPrice, CategoryId, DonorId, IsDeleted) VALUES
-- קטגוריית אלקטרוניקה
(N'הפתעה!', N'אף אחד לא יודע מה זה, גם לא המתכנתת שכתבה את הקוד.', 'https://images.pexels.com/photos/264787/pexels-photo-264787.jpeg?auto=compress&cs=tinysrgb&w=400', 250, @Cat1, @D1, 0),
(N'מחשב נייד חזק', N'מגיע עם מאוורר כל כך חזק שהוא כמעט ממריא מהשולחן.', 'https://images.unsplash.com/photo-1496181133206-80ce9b88a853?q=80&w=400', 150, @Cat1, @D1, 0),
(N'מצלמת 4K', N'כדי שתוכלי לראות את הבאגים בחדות מקסימלית.', 'https://images.unsplash.com/photo-1516035069371-29a1b244cc32?q=80&w=400', 80, @Cat1, @D1, 0),
(N'כספת מזומנים', N'זהירות, השכנים עלולים לבקש הלוואה.', 'https://images.pexels.com/photos/4386431/pexels-photo-4386431.jpeg?auto=compress&cs=tinysrgb&w=400', 999, @Cat1, @D1, 0),

-- קטגוריית פנאי ונופש
(N'מטבח מפואר', N'כדי שיהיה לך איפה להכין קפה בזמן שהקוד רץ.', 'https://images.pexels.com/photos/1080721/pexels-photo-1080721.jpeg?auto=compress&cs=tinysrgb&w=400', 120, @Cat2, @D2, 0),
(N'חופשה חלומית', N'הזמן היחיד שבו מותר לך לכבות את המחשב בלי רגשות אשם.', 'https://images.unsplash.com/photo-1464822759023-fed622ff2c3b?q=80&w=400', 500, @Cat2, @D2, 0),
(N'ספה מפנקת', N'המקום האידיאלי לבהות במסך ולתהות למה ה-CSS לא עובד.', 'https://images.unsplash.com/photo-1555041469-a586c61ea9bc?q=80&w=400', 120, @Cat2, @D2, 0),
(N'מכונת אספרסו', N'כי הדרך לציון 100 עוברת בהרבה קפאין.', 'https://images.unsplash.com/photo-1517668808822-9ebb02f2a0e6?q=80&w=400', 180, @Cat2, @D2, 0),
(N'רכב חשמלי', N'בניגוד לקוד שלך - הוא באמת יודע לאן הוא נוסע.', 'https://images.unsplash.com/photo-1593941707882-a5bba14938c7?q=80&w=400', 2000, @Cat2, @D2, 0),
(N'סט גיימינג', N'מקלדת מוארת שתאיר לך את הלילה כששוב תתקעי על באג.', 'https://images.unsplash.com/photo-1542751371-adc38448a05e?q=80&w=400', 60, @Cat2, @D2, 0),
(N'טיסה ל-Success', N'הדרך הכי מהירה להטיס את הפרויקט הזה לסוף הסמסטר.', 'https://images.pexels.com/photos/46148/aircraft-jet-landing-cloud-46148.jpeg?auto=compress&cs=tinysrgb&w=400', 20, @Cat2, @D1, 0),

-- שינוי שם ל"אופניים חשמליים":
(N'אופניים חשמליים', N'יותר מהיר מלהסביר לאמא מה את לומדת.', 'https://images.unsplash.com/photo-1553105659-d918b253f0f2?q=80&w=400', 350, @Cat2, @D2, 0);

PRINT 'Database updated: Name changed to Electric Bicycle!';