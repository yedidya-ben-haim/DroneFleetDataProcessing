# Drone Fleet Data Processing System

A system for processing and analyzing drone fleet data

---

## Main requirements



- Load data from a bad JSON file
- Check field validity
- Save valid objects to a dedicated file
- Load objects from a valid JSON file
- Run queries on the objects
- Return a report

---

## חוקים עסקיים

כתוב את הכללים שהמערכת חייבת לאכוף.

**דוגמה:**

- מזהה חייב להיות מספר חיובי
- קטגוריה לא יכולה להיות ריקה
- עדיפות חייבת להיות בין 1 ל־5
- דוח לא תקין לא נשמר ברשימת הדוחות התקינים

---

## Project structure

```text
DroneFleetDataProcessing/
?
??? input/
?   ??? raw/
?   ?   ??? drones_raw.json
?   ?
?   ??? test_scenarios/
?       ??? drones_malformed.json
?       ??? drones_empty.json
?       ??? drones_null.json
?       ??? drones_all_invalid.json
?
??? output/
?   ??? drones_clean.json
?   ??? analysis_report.txt
?
??? src/
?   ??? Pipeline/
?   ?   ??? ProcessPipeline.cs
?   ?
?   ??? FileHandling/
?   ?   ??? JsonHandle.cs
?   ?
?   ??? Models/
?   ?   ??? Enums/
?   ?   ??? Sensors/
?   ?       ??? Drone.cs
?   ?
?   ??? Validators/
?   ?   ??? SensorsValidator.cs
?   ?   ??? DroneFieldValidation.cs
?   ?   ??? DroneBusinessValidation.cs
?   ?   ??? ValidationResult.cs
?   ?   ??? IValidator.cs
?   ?
?   ??? Exceptions/
?   ?   ??? ExceptionsMessage.cs
?   ?
?   ??? Storage/
?       ??? DroneRepository.cs
?
??? Program.cs
??? README.md

```


---

## Clasess

| מחלקה | אחריות |
|---------|------|
| `Drone`   | Represents a drone and includes the report data |
| `JsonHandle` | Manages loading and saving to JSON |
|  |  |
|  |  |

---

## Workflow

```text
Reading a raw file
↓
Converting JSON to objects
↓
Checking the integrity of records
↓
Separating into valid and invalid records
↓
Saving the valid records in a clean file
↓
Rereading the clean file
↓
Performing LINQ analyses
↓
Generating a text report
```

**דוגמה:**

1. המשתמש מזין מזהה, קטגוריה ועדיפות.
2. המערכת יוצרת אובייקט מסוג `Report`.
3. ה־Validator בודק את הנתונים.
4. אם הדוח תקין, הוא נשמר.
5. אם הדוח אינו תקין, מוצגת הודעת שגיאה.

---

## דוגמת שימוש

```csharp
var report = new Report(
    id: 1,
    category: "SIGNAL",
    priority: 4
);

reportService.AddReport(report);
```

**פלט אפשרי:**

```text
Report added successfully.
```

---

## טכנולוגיות

- C#
- .NET
- System.Text.Json
- Console Application

מחק טכנולוגיות שאינן בשימוש והוסף את מה שרלוונטי לפרויקט.

---

## איך מריצים את הפרויקט

1. הורד או שכפל את הפרויקט.
2. פתח את התיקייה ב־Visual Studio או ב־VS Code.
3. ודא ש־.NET מותקן.
4. הרץ:

```bash
dotnet run
```

---

## בדיקות שבוצעו

| תרחיש | תוצאה צפויה |
|---|---|
| הוספת דוח תקין | הדוח נשמר |
| מזהה שלילי | מתקבלת שגיאה |
| קטגוריה ריקה | מתקבלת שגיאה |
| עדיפות מעל 5 | מתקבלת שגיאה |
| קובץ לא קיים | מוצגת הודעה מתאימה |

---

## החלטות תכנון

כתוב בקצרה החלטות חשובות שקיבלת.

**דוגמה:**

- הפרדתי את האימות למחלקה נפרדת כדי שהמחלקה `Report` לא תהיה אחראית גם על בדיקות.
- השתמשתי ב־Repository כדי להפריד בין הלוגיקה העסקית לשמירת הנתונים.
- בפרויקט הנוכחי השתמשתי ברשימה בזיכרון במקום במסד נתונים.

---

## דברים לשיפור בהמשך

- הוספת ממשק משתמש
- חיבור למסד נתונים
- הוספת Unit Tests
- תמיכה בסוגי דוחות נוספים
- שיפור הטיפול בשגיאות

---



---

## Division of responsibility

### Pesach:

### Yedidya:

</div>
