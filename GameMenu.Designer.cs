namespace NotVision
{
    partial class GameMenu
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.buttonDis = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.listBoxPlayers = new System.Windows.Forms.ListBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.numericPortCl = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.labelStatusCl = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numericMaxPlayers = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonOn = new System.Windows.Forms.Button();
            this.buttonOff = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.labelStatusSe = new System.Windows.Forms.Label();
            this.listBoxMaps = new System.Windows.Forms.ListBox();
            this.numericPortSe = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.labelip = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericPortCl)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericMaxPlayers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericPortSe)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonConnect);
            this.groupBox1.Controls.Add(this.buttonDis);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.listBoxPlayers);
            this.groupBox1.Controls.Add(this.textBoxName);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBoxIP);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.numericPortCl);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.labelStatusCl);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(375, 500);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Клиент";
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(236, 250);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(133, 40);
            this.buttonConnect.TabIndex = 11;
            this.buttonConnect.Text = "Подключиться";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // buttonDis
            // 
            this.buttonDis.Location = new System.Drawing.Point(236, 296);
            this.buttonDis.Name = "buttonDis";
            this.buttonDis.Size = new System.Drawing.Size(133, 40);
            this.buttonDis.TabIndex = 12;
            this.buttonDis.Text = "Отключиться";
            this.buttonDis.UseVisualStyleBackColor = true;
            this.buttonDis.Click += new System.EventHandler(this.buttonDis_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 227);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(127, 20);
            this.label5.TabIndex = 10;
            this.label5.Text = "Список игроков";
            // 
            // listBoxPlayers
            // 
            this.listBoxPlayers.FormattingEnabled = true;
            this.listBoxPlayers.ItemHeight = 20;
            this.listBoxPlayers.Location = new System.Drawing.Point(10, 250);
            this.listBoxPlayers.Name = "listBoxPlayers";
            this.listBoxPlayers.Size = new System.Drawing.Size(220, 244);
            this.listBoxPlayers.TabIndex = 9;
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(60, 182);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(246, 26);
            this.textBoxName.TabIndex = 8;
            this.textBoxName.Text = "noname";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 182);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "Ник";
            // 
            // textBoxIP
            // 
            this.textBoxIP.Location = new System.Drawing.Point(60, 81);
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.Size = new System.Drawing.Size(246, 26);
            this.textBoxIP.TabIndex = 6;
            this.textBoxIP.Text = "127.0.0.1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(24, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "IP";
            // 
            // numericPortCl
            // 
            this.numericPortCl.Location = new System.Drawing.Point(60, 120);
            this.numericPortCl.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericPortCl.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericPortCl.Name = "numericPortCl";
            this.numericPortCl.Size = new System.Drawing.Size(120, 26);
            this.numericPortCl.TabIndex = 4;
            this.numericPortCl.Value = new decimal(new int[] {
            7777,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Порт";
            // 
            // labelStatusCl
            // 
            this.labelStatusCl.AutoSize = true;
            this.labelStatusCl.Location = new System.Drawing.Point(6, 37);
            this.labelStatusCl.Name = "labelStatusCl";
            this.labelStatusCl.Size = new System.Drawing.Size(51, 20);
            this.labelStatusCl.TabIndex = 1;
            this.labelStatusCl.Text = "label1";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.numericMaxPlayers);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.buttonStart);
            this.groupBox2.Controls.Add(this.buttonOn);
            this.groupBox2.Controls.Add(this.buttonOff);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.labelStatusSe);
            this.groupBox2.Controls.Add(this.listBoxMaps);
            this.groupBox2.Controls.Add(this.numericPortSe);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.labelip);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox2.Location = new System.Drawing.Point(397, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(375, 500);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Сервер";
            // 
            // numericMaxPlayers
            // 
            this.numericMaxPlayers.Location = new System.Drawing.Point(127, 180);
            this.numericMaxPlayers.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericMaxPlayers.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericMaxPlayers.Name = "numericMaxPlayers";
            this.numericMaxPlayers.Size = new System.Drawing.Size(120, 26);
            this.numericMaxPlayers.TabIndex = 18;
            this.numericMaxPlayers.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 182);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(111, 20);
            this.label8.TabIndex = 17;
            this.label8.Text = "Макс игроков";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 81);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 20);
            this.label7.TabIndex = 16;
            this.label7.Text = "Ваш IP:";
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(236, 450);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(133, 44);
            this.buttonStart.TabIndex = 15;
            this.buttonStart.Text = "Начать игру";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonOn
            // 
            this.buttonOn.Location = new System.Drawing.Point(236, 250);
            this.buttonOn.Name = "buttonOn";
            this.buttonOn.Size = new System.Drawing.Size(133, 40);
            this.buttonOn.TabIndex = 13;
            this.buttonOn.Text = "Включить";
            this.buttonOn.UseVisualStyleBackColor = true;
            this.buttonOn.Click += new System.EventHandler(this.buttonOn_Click);
            // 
            // buttonOff
            // 
            this.buttonOff.Location = new System.Drawing.Point(236, 296);
            this.buttonOff.Name = "buttonOff";
            this.buttonOff.Size = new System.Drawing.Size(133, 40);
            this.buttonOff.TabIndex = 14;
            this.buttonOff.Text = "Выключить";
            this.buttonOff.UseVisualStyleBackColor = true;
            this.buttonOff.Click += new System.EventHandler(this.buttonOff_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 227);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(108, 20);
            this.label6.TabIndex = 5;
            this.label6.Text = "Выбор карты";
            // 
            // labelStatusSe
            // 
            this.labelStatusSe.AutoSize = true;
            this.labelStatusSe.Location = new System.Drawing.Point(10, 37);
            this.labelStatusSe.Name = "labelStatusSe";
            this.labelStatusSe.Size = new System.Drawing.Size(51, 20);
            this.labelStatusSe.TabIndex = 4;
            this.labelStatusSe.Text = "label6";
            // 
            // listBoxMaps
            // 
            this.listBoxMaps.FormattingEnabled = true;
            this.listBoxMaps.ItemHeight = 20;
            this.listBoxMaps.Location = new System.Drawing.Point(10, 250);
            this.listBoxMaps.Name = "listBoxMaps";
            this.listBoxMaps.Size = new System.Drawing.Size(220, 244);
            this.listBoxMaps.TabIndex = 3;
            // 
            // numericPortSe
            // 
            this.numericPortSe.Location = new System.Drawing.Point(60, 122);
            this.numericPortSe.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericPortSe.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericPortSe.Name = "numericPortSe";
            this.numericPortSe.Size = new System.Drawing.Size(120, 26);
            this.numericPortSe.TabIndex = 2;
            this.numericPortSe.Value = new decimal(new int[] {
            7777,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 122);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Порт";
            // 
            // labelip
            // 
            this.labelip.AutoSize = true;
            this.labelip.Location = new System.Drawing.Point(77, 81);
            this.labelip.Name = "labelip";
            this.labelip.Size = new System.Drawing.Size(51, 20);
            this.labelip.TabIndex = 0;
            this.labelip.Text = "label1";
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(12, 518);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(760, 36);
            this.buttonClose.TabIndex = 0;
            this.buttonClose.Text = "Вернуться в меню";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // GameMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "GameMenu";
            this.Text = "GameMenu";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GameMenu_FormClosing);
            this.Load += new System.EventHandler(this.GameMenu_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericPortCl)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericMaxPlayers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericPortSe)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Label labelip;
        private System.Windows.Forms.Label labelStatusCl;
        private System.Windows.Forms.NumericUpDown numericPortSe;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericPortCl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxIP;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Button buttonDis;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox listBoxPlayers;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelStatusSe;
        private System.Windows.Forms.ListBox listBoxMaps;
        private System.Windows.Forms.Button buttonOn;
        private System.Windows.Forms.Button buttonOff;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numericMaxPlayers;
        private System.Windows.Forms.Label label8;
    }
}