using System;
using System.Text.Json;

public class Whitelist
{
    private static readonly string filePath = "whitelist.json"; // Lưu danh sách vào file JSON
    public static HashSet<string> list = new HashSet<string>();

    // Tải danh sách từ file khi chương trình khởi động
    public static void LoadFromFile()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            list = JsonSerializer.Deserialize<HashSet<string>>(json) ?? new HashSet<string>();
        }
    }

    // Lưu danh sách vào file khi có thay đổi
    public static void SaveToFile()
    {
        string json = JsonSerializer.Serialize(list, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filePath, json);
    }

    /*
    {
        "Chaos Orb",
        "Exalted Orb",
        "Mirror of Kalandra",
        "Mirror Shard",
        "Divine Orb",
        "Enlighten Support",
        "Orb of Alteration"
    };
    */
}