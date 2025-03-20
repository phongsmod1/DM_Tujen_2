using System;
using System.Runtime.InteropServices;

public class POEhwd
{

    // Windows API: Tìm cửa sổ game
    [DllImport("user32.dll", SetLastError = true)]
    public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

    [DllImport("user32.dll")]
    public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

    [DllImport("user32.dll")]
    private static extern bool SetForegroundWindow(IntPtr hWnd);
    public static bool FocusPoEWindow()
    {
        IntPtr hWnd = FindWindow(null, "Path of Exile");
        if (hWnd != IntPtr.Zero)
        {
            return SetForegroundWindow(hWnd);
        }
        return false;
    }
}
