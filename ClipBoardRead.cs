using System;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

public class ClipboardRead
{

    [DllImport("user32.dll")]
    private static extern bool OpenClipboard(IntPtr hWndNewOwner);

    [DllImport("user32.dll")]
    private static extern bool CloseClipboard();

    [DllImport("user32.dll")]
    private static extern IntPtr GetClipboardData(uint uFormat);

    [DllImport("user32.dll")]
    private static extern bool IsClipboardFormatAvailable(uint format);

    private const uint CF_UNICODETEXT = 13;

    public static unsafe string GetClipboardTextUltraFast()
	{
        if (!IsClipboardFormatAvailable(CF_UNICODETEXT))
            return string.Empty;

        if (!OpenClipboard(IntPtr.Zero))
            return string.Empty;

        IntPtr handle = GetClipboardData(CF_UNICODETEXT);
        string result = string.Empty;

        if (handle != IntPtr.Zero)
        {
            result = new string((char*)handle);  // Truy xuất trực tiếp mà không cần GlobalLock
        }

        CloseClipboard();
        return result;
    }

    public static List<string> FilterItemInfo2(string itemData)
    {
        List<string> itemInfo = new List<string>();

        string itemClass = GetMatch(itemData, @"Item Class:\s*(.+)");
        string rarity = GetMatch(itemData, @"Rarity:\s*(.+)");
        string itemName;

        if (itemClass.Contains("Skill Gems") || itemClass.Contains("Support Gems"))
        {
            itemName = GetMatch(itemData, @"Rarity:\s*.+\r?\n(.+)");
            string level = GetMatch(itemData, @"Level:\s*(\d+)");
            string quality = GetMatch(itemData, @"Quality:\s*\+?(\d+)%");

            itemInfo.Add($"{itemName}");
            itemInfo.Add($"{level}");
            itemInfo.Add($"{quality}");
        }
        else
        {
            itemName = GetMatch(itemData, @"Rarity:\s*.+\r?\n(.+)");
            itemInfo.Add($"{itemName}");
        }

        return itemInfo;
    }

    // ✅ Lọc Item Class & Item Name
    public static ItemDetails ExtractBasicItemDetails(string text)
    {
        string[] lines = text.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

        string itemClass = lines.Length > 0 ? lines[0].Replace("Item Class: ", "").Trim() : "N/A Class";
        string itemName = lines.Length > 2 ? lines[2].Trim() : "N/A Name";
        bool isGem = (itemClass == "Skill Gems" || itemClass == "Support Gems");

        return new ItemDetails
        {
            ItemClass = itemClass,
            ItemName = itemName,
            IsGem = isGem
        };
    }

    // ✅ Nếu là Gem, lọc Level & Quality
    public static void ExtractGemDetails(string text, ItemDetails item)
    {
        item.GemLevel = ExtractValueByRegex(text, @"Level:\s*(\d+)", "0");
        item.GemQuality = ExtractValueByRegex(text, @"Quality:\s*\+?(\d+)%", "0");
    }

    // ✅ Dùng Regex để tìm giá trị nhanh hơn
    static string ExtractValueByRegex(string text, string pattern, string defaultValue)
    {
        Match match = Regex.Match(text, pattern);
        return match.Success ? match.Groups[1].Value : defaultValue;
    }

    static string GetMatch(string text, string pattern, bool firstLineOnly = false)
    {
        if (firstLineOnly)
        {
            return text.Split('\n').FirstOrDefault()?.Trim() ?? "Unknown";
        }

        Match match = Regex.Match(text, pattern, RegexOptions.Multiline);
        return match.Success ? match.Groups[1].Value.Trim() : "Unknown";
    }
}

// ✅ Class lưu trữ dữ liệu item
public class ItemDetails
{
    public string ItemClass { get; set; }
    public string ItemName { get; set; }
    public string GemLevel { get; set; }
    public string GemQuality { get; set; }
    public bool IsGem { get; set; }
}
