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

    public static ItemDetails ExtractBasicItemDetails(string text)
    {
        string[] lines = text.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

        string itemClass = lines.Length > 0 ? lines[0].Replace("Item Class: ", "").Trim() : "N/A Class";
        string rarity = lines.Length > 1 ? lines[1].Replace("Rarity: ", "").Trim() : "N/A Rarity";

        string itemName = "N/A Name";
        string itemSubName = "";

        if ((rarity == "Rare" || rarity == "Unique") && lines.Length > 3)
        {
            itemName = lines[3].Trim();       // Dòng 4
            itemSubName = lines[2].Trim();    // Dòng 3
        }
        else if (lines.Length > 2)
        {
            itemName = lines[2].Trim();       // Dòng 3
        }

        bool isGem = (itemClass == "Skill Gems" || itemClass == "Support Gems");

        return new ItemDetails
        {
            ItemClass = itemClass,
            Rarity = rarity,
            ItemName = itemName,
            ItemSubName = itemSubName,
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

}

// ✅ Class lưu trữ dữ liệu item
public class ItemDetails
{
    public string ItemClass { get; set; }
    public string ItemName { get; set; }
    public string GemLevel { get; set; }
    public string GemQuality { get; set; }
    public bool IsGem { get; set; }
    public string Rarity { get; set; }
    public string ItemSubName { get; set; }
}
