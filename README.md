# 🏀 Basketball Calendar

Desktopová aplikace pro Windows Forms sloužící ke správě basketbalového kalendáře, včetně zápasů NBA a dalších událostí. Uživatelé mohou prohlížet události, nastavovat upozornění a upravovat vzhled podle preferovaného týmu.

## 📦 Funkce

- ✅ Přehled všech událostí ve formě kalendáře
- ✅ Podpora upozornění na nadcházející zápasy
- ✅ Možnost nastavení oblíbeného týmu a jeho barev (primární, sekundární)
- ✅ Trvalé uložení uživatelského nastavení
- ✅ Možnost přidávání vlastních událostí

## 📁 Struktura projektu

```
basketball_calendar/
├── Forms/
│   └── MainForm.cs
│       └── MainForm.Designer.cs
│   └── SettingsForm.cs
│       └── SettingsForm.Designer.cs
│   └── EventForm.cs
│       └── EventForm.Designer.cs
├── Models/
│   ├── Event.cs
│   └── NbaGame.cs
├── Services/
│   └── UserSettings.cs
│   └── NbaGameService.cs
├── Program.cs
└── README.md
```

## ⚙️ Nastavení

Ve formuláři `SettingsForm` si uživatel vybírá tým NBA ze seznamu. Na základě výběru se zobrazí primární a sekundární barva týmu. Výběr se ukládá do třídy `UserSettings`:

```csharp
public class UserSettings
{
    public string? TeamName { get; set; }
    public int PrimaryColorArgb { get; set; }
    public int SecondaryColorArgb { get; set; }
}
```

Barevná data se předávají jako ARGB celá čísla.

## 📚 Modely

### Event.cs

Model jedné události v kalendáři:

```csharp
public class Event
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public string Tag { get; set; }
    public TimeSpan? ReminderOffset { get; set; }
    public bool ReminderSent { get; set; }
}
```

### NbaGame.cs

Model jednoho zápasu NBA:

```csharp
public class NbaGame
{
    public DateTime Date { get; set; }
    public string Home { get; set; }
    public string Away { get; set; }
    public string? Note { get; set; }

    public override string ToString()
    {
        return $"{Date:yyyy-MM-dd} {Away} @ {Home}";
    }
}
```

## 🎨 Barevná témata

Každý tým má definovanou dvojici barev:

```csharp
new Dictionary<string, (Color Primary, Color Secondary)>
{
    ["Los Angeles Lakers"] = (Color.FromArgb(85, 37, 130), Color.FromArgb(253, 185, 39)),
    ["Golden State Warriors"] = (Color.FromArgb(29, 66, 138), Color.FromArgb(255, 199, 44)),
    // …
}
```

## 💾 Ukládání nastavení

Uživatelská nastavení se ukládají např. pomocí serializace nebo do konfiguračního souboru (není-li uvedeno jinak).

## 🏀 Balldontlie API

Data o NBA zápasech se získávají pomocí balldontlie API.  
[Hlavní stránka](https://www.balldontlie.io) a [dokumentace](https://docs.balldontlie.io/#nba-api)

## 🚀 Spuštění

Pro jednodušší spuštění použijte [release](https://github.com/Boootyshaker9000/basketball-calendar/releases/tag/c%23)
Projekt můžeš otevřít v [Visual Studio](https://visualstudio.microsoft.com/) nebo [JetBrains Rider](https://www.jetbrains.com/rider/). Při spuštění aplikace se otevře hlavní okno, odkud lze prohlížet a upravovat události.

## 📝 Licence

MIT License (volně použitelný projekt)
