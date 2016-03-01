namespace Client
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
            this.button_Get = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.textBox_Create = new System.Windows.Forms.TextBox();
            this.button_Kill = new System.Windows.Forms.Button();
            this.button_Create = new System.Windows.Forms.Button();
            this.textBox_IP = new System.Windows.Forms.TextBox();
            this.button_Connect = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button_Get
            // 
            this.button_Get.Location = new System.Drawing.Point(7, 75);
            this.button_Get.Margin = new System.Windows.Forms.Padding(4);
            this.button_Get.Name = "button_Get";
            this.button_Get.Size = new System.Drawing.Size(316, 43);
            this.button_Get.TabIndex = 0;
            this.button_Get.Text = "Get processes";
            this.button_Get.UseVisualStyleBackColor = true;
            this.button_Get.Click += new System.EventHandler(this.button_Get_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(7, 249);
            this.listBox1.Margin = new System.Windows.Forms.Padding(4);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(315, 260);
            this.listBox1.Sorted = true;
            this.listBox1.TabIndex = 1;
            // 
            // textBox_Create
            // 
            this.textBox_Create.Location = new System.Drawing.Point(31, 217);
            this.textBox_Create.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_Create.Name = "textBox_Create";
            this.textBox_Create.Size = new System.Drawing.Size(263, 22);
            this.textBox_Create.TabIndex = 2;
            // 
            // button_Kill
            // 
            this.button_Kill.Location = new System.Drawing.Point(7, 121);
            this.button_Kill.Margin = new System.Windows.Forms.Padding(4);
            this.button_Kill.Name = "button_Kill";
            this.button_Kill.Size = new System.Drawing.Size(316, 43);
            this.button_Kill.TabIndex = 0;
            this.button_Kill.Text = "Kill process";
            this.button_Kill.UseVisualStyleBackColor = true;
            this.button_Kill.Click += new System.EventHandler(this.button_Kill_Click);
            // 
            // button_Create
            // 
            this.button_Create.Location = new System.Drawing.Point(7, 166);
            this.button_Create.Margin = new System.Windows.Forms.Padding(4);
            this.button_Create.Name = "button_Create";
            this.button_Create.Size = new System.Drawing.Size(316, 43);
            this.button_Create.TabIndex = 3;
            this.button_Create.Text = "Create process";
            this.button_Create.UseVisualStyleBackColor = true;
            this.button_Create.Click += new System.EventHandler(this.button_Create_Click);
            // 
            // textBox_IP
            // 
            this.textBox_IP.Location = new System.Drawing.Point(201, 36);
            this.textBox_IP.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_IP.Name = "textBox_IP";
            this.textBox_IP.Size = new System.Drawing.Size(97, 22);
            this.textBox_IP.TabIndex = 2;
            // 
            // button_Connect
            // 
            this.button_Connect.Location = new System.Drawing.Point(16, 18);
            this.button_Connect.Margin = new System.Windows.Forms.Padding(4);
            this.button_Connect.Name = "button_Connect";
            this.button_Connect.Size = new System.Drawing.Size(164, 43);
            this.button_Connect.TabIndex = 0;
            this.button_Connect.Text = "Connect to server";
            this.button_Connect.UseVisualStyleBackColor = true;
            this.button_Connect.Click += new System.EventHandler(this.button_Connect_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(188, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "IP-address of server";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(331, 516);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_Create);
            this.Controls.Add(this.textBox_IP);
            this.Controls.Add(this.textBox_Create);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.button_Kill);
            this.Controls.Add(this.button_Connect);
            this.Controls.Add(this.button_Get);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_Get;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TextBox textBox_Create;
        private System.Windows.Forms.Button button_Kill;
        private System.Windows.Forms.Button button_Create;
        private System.Windows.Forms.TextBox textBox_IP;
        private System.Windows.Forms.Button button_Connect;
        private System.Windows.Forms.Label label1;
    }
}

