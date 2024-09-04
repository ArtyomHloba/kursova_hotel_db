namespace Hotel_db
{
    partial class delete
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
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            button1 = new Button();
            comboBox1 = new ComboBox();
            label1 = new Label();
            tabPage2 = new TabPage();
            comboBox2 = new ComboBox();
            label3 = new Label();
            button2 = new Button();
            tabPage3 = new TabPage();
            comboBox3 = new ComboBox();
            label4 = new Label();
            button3 = new Button();
            tabPage4 = new TabPage();
            comboBox4 = new ComboBox();
            label2 = new Label();
            button4 = new Button();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            tabPage3.SuspendLayout();
            tabPage4.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Controls.Add(tabPage4);
            tabControl1.Location = new Point(0, 1);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(517, 234);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(button1);
            tabPage1.Controls.Add(comboBox1);
            tabPage1.Controls.Add(label1);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(509, 206);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Кімната";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new Point(185, 107);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 2;
            button1.Text = "Видалити";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(185, 78);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(263, 23);
            comboBox1.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(81, 81);
            label1.Name = "label1";
            label1.Size = new Size(98, 15);
            label1.TabIndex = 0;
            label1.Text = "Оберіть кімнату:";
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(comboBox2);
            tabPage2.Controls.Add(label3);
            tabPage2.Controls.Add(button2);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(509, 206);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Клієнт";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(130, 64);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(270, 23);
            comboBox2.TabIndex = 2;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(28, 67);
            label3.Name = "label3";
            label3.Size = new Size(96, 15);
            label3.TabIndex = 1;
            label3.Text = "Оберіть клієнта:";
            // 
            // button2
            // 
            button2.Location = new Point(130, 93);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 0;
            button2.Text = "Видалити";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(comboBox3);
            tabPage3.Controls.Add(label4);
            tabPage3.Controls.Add(button3);
            tabPage3.Location = new Point(4, 24);
            tabPage3.Name = "tabPage3";
            tabPage3.Size = new Size(509, 206);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Телефон";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // comboBox3
            // 
            comboBox3.FormattingEnabled = true;
            comboBox3.Location = new Point(201, 70);
            comboBox3.Name = "comboBox3";
            comboBox3.Size = new Size(121, 23);
            comboBox3.TabIndex = 2;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(92, 73);
            label4.Name = "label4";
            label4.Size = new Size(103, 15);
            label4.TabIndex = 1;
            label4.Text = "Оберіть телефон:";
            // 
            // button3
            // 
            button3.Location = new Point(201, 99);
            button3.Name = "button3";
            button3.Size = new Size(75, 23);
            button3.TabIndex = 0;
            button3.Text = "Видалити";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // tabPage4
            // 
            tabPage4.Controls.Add(comboBox4);
            tabPage4.Controls.Add(label2);
            tabPage4.Controls.Add(button4);
            tabPage4.Location = new Point(4, 24);
            tabPage4.Name = "tabPage4";
            tabPage4.Size = new Size(509, 206);
            tabPage4.TabIndex = 3;
            tabPage4.Text = "Бронь";
            tabPage4.UseVisualStyleBackColor = true;
            // 
            // comboBox4
            // 
            comboBox4.FormattingEnabled = true;
            comboBox4.Location = new Point(50, 67);
            comboBox4.Name = "comboBox4";
            comboBox4.Size = new Size(430, 23);
            comboBox4.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(50, 49);
            label2.Name = "label2";
            label2.Size = new Size(173, 15);
            label2.TabIndex = 1;
            label2.Text = "Оберіть бронь для видалення:";
            // 
            // button4
            // 
            button4.Location = new Point(50, 96);
            button4.Name = "button4";
            button4.Size = new Size(75, 23);
            button4.TabIndex = 0;
            button4.Text = "Видалити";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // delete
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(517, 236);
            Controls.Add(tabControl1);
            Name = "delete";
            Text = "Видалення";
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            tabPage3.ResumeLayout(false);
            tabPage3.PerformLayout();
            tabPage4.ResumeLayout(false);
            tabPage4.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Button button1;
        private ComboBox comboBox1;
        private Label label1;
        private ComboBox comboBox2;
        private Label label3;
        private Button button2;
        private TabPage tabPage3;
        private ComboBox comboBox3;
        private Label label4;
        private Button button3;
        private TabPage tabPage4;
        private ComboBox comboBox4;
        private Label label2;
        private Button button4;
    }
}