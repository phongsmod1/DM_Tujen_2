using System;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using DM_Tujen;

class Program
{
    //[System.Runtime.InteropServices.DllImport("kernel32.dll")]
    //private static extern bool AllocConsole();

    static void Main()
    {
        //AllocConsole(); // 🔥 Mở cửa sổ console
        //Debug.WriteLine("Debug Console Started!"); // Xuất log ra console

        ApplicationConfiguration.Initialize();
        Application.Run(new Form1());       
    }

    public static void FocusPOE()
    {
        IntPtr hWnd = POEhwd.FindWindow(null, "Path of Exile");
        if (hWnd == IntPtr.Zero)
        {
            Form1.BoxWrite("KHONG TIM THAY CUA SO GAME!");
            return;
        }

        POEhwd.MoveWindow(hWnd, 0, 0, 1280, 800, true);
        POEhwd.FocusPoEWindow();
    }

    public static void MainLoop()
    {
        while (!KeyStop.stopRequested)
        {
            KeyStop.keybd_event(KeyStop.VK_CONTROL, 0, 0, 0);

            if (!Check.IsTujenOpen())
            {
                KeyStop.PressSTwice();
                // Tìm Tujen cho đến khi có vị trí hợp lệ
                Point? TujenPos;

                do
                {
                    TujenPos = Check.FindTujen();
                    Thread.Sleep(200); // Tránh spam CPU
                } while (TujenPos == null);

                // Di chuyển đến Tujen và click
                KeyStop.SetCursorPos(TujenPos.Value.X, TujenPos.Value.Y);
                Thread.Sleep(50);
                KeyStop.LClick();
                KeyStop.SetCursorPos(TujenPos.Value.X-500, TujenPos.Value.Y-500);
                //KeyStop.SetCursorPos(0, 0);

                // Chờ Tujen mở với thời gian tối đa 5 giây
                int waitTime = 0;
                while (!Check.IsTujenOpen() && waitTime < 2000)
                {
                    Thread.Sleep(250);
                    waitTime += 250;
                }
            }
            
            Span<Point> inventoryCount = Check.Inventory3();
            //Console.WriteLine("itemcount: " + itemCount.Count);

            if (inventoryCount.Length >= 50)
            {
                
                //Console.WriteLine($"isStashopen: {Check.IsStashOpen()}");

                Point? stashPos = null;
                int waitTime = 0;
                do
                {
                    KeyStop.PressSTwice();
                    stashPos = Check.FindStash();
                    waitTime += 100;
                    Thread.Sleep(100); // Chờ một chút trước khi thử lại
                } while (stashPos == null && waitTime <= 500);

                if (stashPos == null)
                {
                    KeyStop.SetCursorPos(632, 173);
                    Thread.Sleep(100);
                    KeyStop.LClick();
                    Thread.Sleep(500);
                    stashPos = Check.FindStash();
                }

                KeyStop.SetCursorPos(stashPos.Value.X, stashPos.Value.Y);
                Thread.Sleep(50);
                KeyStop.LClick();
                KeyStop.SetCursorPos(stashPos.Value.X-500, stashPos.Value.Y-500);
                //Console.WriteLine("Đã click vào stash.");

                // Chờ stash mở hoàn toàn
                waitTime = 0;
                while (!Check.IsStashOpen() && waitTime < 2000) // Chờ tối đa 5 giây
                {
                    Thread.Sleep(250);
                    waitTime += 250;
                    //Console.WriteLine("Đang chờ stash mở...");
                }

                //if (!Check.IsStashOpen())
                //{
                    //Console.WriteLine("Stash không mở được! Thử lại lần sau.");
                //    return;
                //}

                //Console.WriteLine("Stash đã mở!");

                // Di chuyển từng item vào stash
                foreach (var pos in inventoryCount)
                {
                    if (KeyStop.stopRequested) break;
                    if (Check.IsStashOpen())
                    {
                        // Đợi 100ms giữa các lần click
                        KeyStop.SetCursorPos(pos.X, pos.Y);
                        Thread.Sleep(20);
                        KeyStop.LClickMid();
                    }
                    else break;
                    //Console.WriteLine($"Di chuyển item tại {pos.X}, {pos.Y} vào stash.");
                }
                KeyStop.PressSTwice();
            }

            if (Check.IsTujenOpen())
            {
                //Console.WriteLine("bat dau kiem tra item tujen");
                List<Point> tujenItemCount = Check.TujenItemPos();
                //Console.WriteLine("tujenItemcount: " + tujenItemCount.Count);


                foreach (var pos in tujenItemCount)
                {
                    if (KeyStop.stopRequested) break;
                    KeyStop.SetCursorPos(pos.X, pos.Y);
                    Thread.Sleep(30);
                    KeyStop.PressC();
                    Thread.Sleep(25);

                    string itemData = ClipboardRead.GetClipboardTextUltraFast();

                    // 🔥 Lọc thông tin item từ Clipboard
                    ItemDetails item = ClipboardRead.ExtractBasicItemDetails(itemData);
                    //stopwatch.Stop();
                    if (item.IsGem)
                    {
                        ClipboardRead.ExtractGemDetails(itemData, item);
                    }

                    //List<string> itemDetails = ClipboardRead.FilterItemInfo2(itemData);
                    //Form1.BoxWrite($"{item.ItemName}" +
                     //     (item.IsGem ? $" | L: {item.GemLevel} | Q: {item.GemQuality}%" : ""));

                    string itemInfo = $"{item.ItemName}" +
                          (item.IsGem ? $" | L: {item.GemLevel} | Q: {item.GemQuality}%" : "");

                    Form1.BoxWrite(itemInfo);
                    Logger.WriteLog(itemInfo);

                    if ((item.ItemClass == "Skill Gems" || item.ItemClass == "Support Gems") &&
                                            item.GemLevel == "21" && item.GemQuality == "20")
                    {
                        //Console.WriteLine("Gem 21/20, mua ngay!");
                        Logger.WriteLog(" -> BUY");
                        KeyStop.BuyItem();
                    }
                    else if (Check.IsItemWhitelisted(item.ItemName))
                    {
                        //Console.WriteLine("Gem trong whitelist, mua ngay!");
                        Logger.WriteLog(" -> BUY");
                        KeyStop.BuyItem();
                    };
                }

                if (Check.isOutOfCoin()) KeyStop.stopAndCtrlUp();

                if (!KeyStop.stopRequested)
                {
                    Form1.BoxWrite("--------------------REFRESH-------------------");
                    Logger.WriteLog("--------------------REFRESH-------------------");
                    KeyStop.SetCursorPos(630, 645);
                    Thread.Sleep(20);
                    KeyStop.LClickMid();
                }
            }
            

        }
    }
}