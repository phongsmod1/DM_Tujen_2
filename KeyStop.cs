using System;
using System.Runtime.InteropServices;
using DM_Tujen;

public class KeyStop
{
    private const byte VK_S = 0x53; // Mã phím S
    public static volatile bool stopRequested = false;
    public const byte VK_CONTROL = 0x11;
    private const uint KEYEVENTF_KEYUP = 0x0002;
    public const int MOD_NOREPEAT = 0x4000;
    private const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
    private const uint MOUSEEVENTF_LEFTUP = 0x0004;
    private const byte VK_C = 0x43; // Mã phím 'C'
    private const uint MOUSEEVENTF_WHEEL = 0x0800;
    private Form1 mainForm;

    [DllImport("user32.dll")]
    public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

    [DllImport("user32.dll")]
    private static extern short GetAsyncKeyState(int vKey);

    [DllImport("user32.dll")]
    public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);

    [DllImport("user32.dll")]
    public static extern bool SetCursorPos(int X, int Y);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, UIntPtr dwExtraInfo);

    public static void HotKeyListener()
    {
        while (!stopRequested)
        {
            if (GetAsyncKeyState(0x56) < 0) // 0x56 là phím 'V'
            {
                stopAndCtrlUp();
            }
            Thread.Sleep(50);
        }
    }

    public static void stopAndCtrlUp()
    {
        stopRequested = true;
        Form1.Instance.timer1.Stop();
        keybd_event(VK_CONTROL, 0, KEYEVENTF_KEYUP, 0);
    }

    // Hàm cuộn chuột xuống `times` lần
    public static void ScrollMouseDownMultiple( int times)
    {
        for (int i = 0; i < times; i++)
        {
            ScrollMouseDown(120); // Gửi sự kiện cuộn chuột xuống
            Thread.Sleep(5); // Delay nhỏ để cuộn mượt hơn
        }
    }

    static void ScrollMouseDown(int amount)
    {
        mouse_event(MOUSEEVENTF_WHEEL, 0, 0, (uint)-amount, UIntPtr.Zero);
    }

    public static void LClick()
    {
        mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, UIntPtr.Zero);
        Thread.Sleep(100);
        mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, UIntPtr.Zero);
        Thread.Sleep(100);
    }

    public static void LClickMid()
    {
        mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, UIntPtr.Zero);
        Thread.Sleep(50);
        mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, UIntPtr.Zero);
        Thread.Sleep(50);
    }

    public static void LClickFast()
    {
        mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, UIntPtr.Zero);
        Thread.Sleep(10);
        mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, UIntPtr.Zero);
        Thread.Sleep(10);
    }

    public static void PressC()
    {
        keybd_event(VK_C, 0, 0, 0);
        keybd_event(VK_C, 0, KEYEVENTF_KEYUP, 0); 
    }

    public static void PressSTwice()
    {
        for (int i = 0; i < 2; i++)
        {
            keybd_event(VK_S, 0, 0, 0);          // Ấn phím S xuống
            Thread.Sleep(100); // Chờ 50ms
            keybd_event(VK_S, 0, KEYEVENTF_KEYUP, 0); // Nhả phím S
            Thread.Sleep(300); // Chờ 100ms giữa 2 lần nhấn
        }
    }

    public static void BuyItem()
    {
        LClickFast();
        int waitTime = 0;
        while (!Check.isConfirmOn() && waitTime <= 150)
        {
            Thread.Sleep(10);
            waitTime += 10;
            //Console.WriteLine("waiting confirm");
            if (Check.isConfirmOn())
            {
                //Console.WriteLine("confirm on");
                break;
            }
            Thread.Sleep(5);
        }
        if (!Check.isOutOfArtf())
        {
            if (Check.isConfirmOn())
            {
                ScrollMouseDownMultiple(7);
                Thread.Sleep(100);
                SetCursorPos(375, 638);
                Thread.Sleep(50);
                LClick();
                while (Check.isConfirmOn())
                {
                    KeyStop.LClick();
                    if (!Check.isConfirmOn())
                    {
                        break;
                    }
                    Thread.Sleep(5);
                }
            }                  
        }    
        else stopAndCtrlUp();
    }
}
