﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Timers;

namespace DM_Tujen
{
    public partial class Form1 : Form
    {
        public static Form1 Instance { get; private set; }

        public static Thread mainThread;
        public static Thread hotkeyThread;
        //public System.Windows.Forms.Timer timer1;
        private DateTime startTime;

        HashSet<string> items = Whitelist.list;
        public Form1()
        {
            InitializeComponent();
            Instance = this; // Lưu instance của Form1 để gọi từ static method
            TrackBarValue = trackBar1.Value; // Gán giá trị ban đầu
            // Tạo Timer
            timer1.Interval = 1000; // Cập nhật mỗi giây
            timer1.Tick += timer1_Tick;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string item = textBox1.Text.Trim();

            if (!string.IsNullOrEmpty(item) && !Whitelist.list.Contains(item))
            {
                Whitelist.list.Add(item);
                Whitelist.SaveToFile(); // 🔥 Lưu danh sách vào file ngay lập tức
                UpdateListBox(); // Cập nhật ListBox
                textBox1.Clear();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            // Đăng ký phím V (Virtual-Key: 0x56)
            KeyStop.RegisterHotKey(IntPtr.Zero, 1, KeyStop.MOD_NOREPEAT, 0x56);

            KeyStop.stopRequested = false;

            Program.FocusPOE();

            hotkeyThread = new Thread(KeyStop.HotKeyListener);
            hotkeyThread.IsBackground = true;
            hotkeyThread.Start(); // Chạy luồng lắng nghe phím V

            startTime = DateTime.Now; // Lưu thời gian bắt đầu
            timer1.Start(); // Bắt đầu bộ đếm thời gian

            // Tạo luồng chính
            mainThread = new Thread(Program.MainLoop);
            mainThread.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            Whitelist.LoadFromFile(); // Tải dữ liệu từ file
            listBox1.Items.AddRange(Whitelist.list.ToArray()); // Cập nhật ListBox
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete) // Kiểm tra xem có nhấn phím Delete không
            {
                if (listBox1.SelectedItem != null) // Kiểm tra xem có item nào được chọn không
                {
                    Whitelist.list.Remove(listBox1.SelectedItem.ToString()); // Xóa item khỏi danh sách
                    listBox1.Items.Remove(listBox1.SelectedItem); // Xóa item được chọn
                    Whitelist.SaveToFile(); // 🔥 Lưu danh sách vào file ngay lập tức
                    UpdateListBox(); // Cập nhật ListBox
                }
            }
        }

        private void UpdateListBox()
        {
            listBox1.Items.Clear(); // Xóa toàn bộ danh sách cũ
            listBox1.Items.AddRange(Whitelist.list.ToArray()); // Cập nhật lại danh sách từ HashSet
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public static void BoxWrite(string message)
        {
            if (Instance?.listBox2.InvokeRequired == true)
            {
                Instance.listBox2.Invoke(new Action(() =>
                {
                    Instance.listBox2.Items.Add(message);
                    Instance.listBox2.TopIndex = Instance.listBox2.Items.Count - 1; // ✅ Đảm bảo cuộn xuống
                }));
            }

            else
            {
                //Instance.listBox2.SelectedIndex = Instance.listBox2.Items.Count - 1;
                Instance.listBox2.Items.Add(message);
                Instance.listBox2.TopIndex = Instance.listBox2.Items.Count - 1;
            }
        }

        public void timer1_Tick(object sender, EventArgs e)
        {
            TimeSpan elapsed = DateTime.Now - startTime;
            label2.Text = $"🕛 {elapsed.Minutes:D2}:{elapsed.Seconds:D2}";
        }

        public void label2_Click_1(object sender, EventArgs e)
        {

        }

        public void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }


        public int TrackBarValue { get; private set; }
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label3.Text = $"Speed: {trackBar1.Value}";
            TrackBarValue = trackBar1.Value;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
