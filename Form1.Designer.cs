namespace DM_Tujen
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            buttonQuay = new Button();
            listBox1 = new ListBox();
            textBox1 = new TextBox();
            buttonAdd = new Button();
            label1 = new Label();
            listBox2 = new ListBox();
            timer1 = new System.Windows.Forms.Timer(components);
            label2 = new Label();
            checkBox1 = new CheckBox();
            trackBar1 = new TrackBar();
            label3 = new Label();
            ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
            SuspendLayout();
            // 
            // buttonQuay
            // 
            buttonQuay.Location = new Point(50, 25);
            buttonQuay.Margin = new Padding(4, 3, 4, 3);
            buttonQuay.Name = "buttonQuay";
            buttonQuay.Size = new Size(136, 51);
            buttonQuay.TabIndex = 0;
            buttonQuay.Text = "Bắt Đầu";
            buttonQuay.UseVisualStyleBackColor = true;
            buttonQuay.Click += button1_Click;
            // 
            // listBox1
            // 
            listBox1.BackColor = SystemColors.Menu;
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(13, 128);
            listBox1.Margin = new Padding(4, 3, 4, 3);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(134, 244);
            listBox1.TabIndex = 1;
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            listBox1.KeyDown += listBox1_KeyDown;
            // 
            // textBox1
            // 
            textBox1.BackColor = SystemColors.Menu;
            textBox1.Location = new Point(13, 99);
            textBox1.Margin = new Padding(4, 3, 4, 3);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(134, 23);
            textBox1.TabIndex = 2;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // buttonAdd
            // 
            buttonAdd.Location = new Point(155, 99);
            buttonAdd.Margin = new Padding(4, 3, 4, 3);
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Size = new Size(41, 23);
            buttonAdd.TabIndex = 3;
            buttonAdd.Text = "add";
            buttonAdd.UseVisualStyleBackColor = true;
            buttonAdd.Click += button2_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.Red;
            label1.Location = new Point(70, 377);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(98, 15);
            label1.TabIndex = 4;
            label1.Text = "ấn nút V để dừng";
            // 
            // listBox2
            // 
            listBox2.BackColor = SystemColors.ActiveBorder;
            listBox2.BorderStyle = BorderStyle.None;
            listBox2.ForeColor = SystemColors.ButtonHighlight;
            listBox2.FormattingEnabled = true;
            listBox2.ItemHeight = 15;
            listBox2.Location = new Point(-1, 4);
            listBox2.Name = "listBox2";
            listBox2.SelectionMode = SelectionMode.None;
            listBox2.Size = new Size(280, 390);
            listBox2.TabIndex = 5;
            listBox2.SelectedIndexChanged += listBox2_SelectedIndexChanged;
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = SystemColors.Control;
            label2.Location = new Point(173, 358);
            label2.Name = "label2";
            label2.Size = new Size(19, 15);
            label2.TabIndex = 6;
            label2.Text = "🕛";
            label2.Click += label2_Click_1;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Checked = true;
            checkBox1.CheckState = CheckState.Checked;
            checkBox1.Location = new Point(154, 128);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(82, 19);
            checkBox1.TabIndex = 7;
            checkBox1.Text = "gem 21/20";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // trackBar1
            // 
            trackBar1.AutoSize = false;
            trackBar1.LargeChange = 1;
            trackBar1.Location = new Point(150, 321);
            trackBar1.Maximum = 5;
            trackBar1.Minimum = 1;
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new Size(82, 34);
            trackBar1.TabIndex = 8;
            trackBar1.Value = 1;
            trackBar1.Scroll += trackBar1_Scroll;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(154, 303);
            label3.Name = "label3";
            label3.Size = new Size(51, 15);
            label3.TabIndex = 9;
            label3.Text = "Speed: 1";
            label3.Click += label3_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveBorder;
            ClientSize = new Size(240, 400);
            Controls.Add(label3);
            Controls.Add(trackBar1);
            Controls.Add(checkBox1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(buttonAdd);
            Controls.Add(textBox1);
            Controls.Add(listBox1);
            Controls.Add(buttonQuay);
            Controls.Add(listBox2);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            Name = "Form1";
            Text = "DM Tujen";
            TopMost = true;
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonQuay;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Timer timer1;
        public CheckBox checkBox1;
        public Label label2;
        public ListBox listBox2;
        public TrackBar trackBar1;
        private Label label3;
    }
}

