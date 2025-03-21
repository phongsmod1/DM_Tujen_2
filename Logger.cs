using System;
using System.IO;

public class Logger
{

private static readonly string logDir = "logs";
private static readonly string logPath = $"{logDir}/log_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.txt";

public static void WriteLog(string message)
{
    // Tạo thư mục logs nếu chưa tồn tại
    if (!Directory.Exists(logDir))
    {
        Directory.CreateDirectory(logDir);
    }

    // Ghi log vào file log.txt
    string logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}\n";
    File.AppendAllText(logPath, logMessage);
}
}
