using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

class ClipboardListener : Form
{
    private static AutoResetEvent clipboardEvent = new AutoResetEvent(false);
    private static string clipboardText = "";

    public static string GetClipboardTextFast()
    {
        clipboardEvent.WaitOne(); // Đợi clipboard cập nhật
        return clipboardText;
    }

    protected override void WndProc(ref Message m)
    {
        const int WM_CLIPBOARDUPDATE = 0x031D;
        if (m.Msg == WM_CLIPBOARDUPDATE)
        {
            clipboardText = GetTextFromClipboard();
            clipboardEvent.Set(); // Báo hiệu clipboard đã cập nhật
        }
        base.WndProc(ref m);
    }

    private static string GetTextFromClipboard()
    {
        if (Clipboard.ContainsText())
            return Clipboard.GetText();
        return string.Empty;
    }

    public static void Start()
    {
        Application.Run(new ClipboardListener());
    }
}