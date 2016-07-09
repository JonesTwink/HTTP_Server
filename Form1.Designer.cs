namespace WebServer
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.gui_port = new System.Windows.Forms.NumericUpDown();
            this.gui_run = new System.Windows.Forms.Button();
            this.gui_stop = new System.Windows.Forms.Button();
            this.gui_log = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.gui_dir = new System.Windows.Forms.TextBox();
            this.gui_choosedir = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gui_port)).BeginInit();
            this.SuspendLayout();
            // 
            // gui_port
            // 
            this.gui_port.Location = new System.Drawing.Point(14, 35);
            this.gui_port.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.gui_port.Name = "gui_port";
            this.gui_port.Size = new System.Drawing.Size(46, 20);
            this.gui_port.TabIndex = 1;
            this.gui_port.Value = new decimal(new int[] {
            81,
            0,
            0,
            0});
            // 
            // gui_run
            // 
            this.gui_run.Location = new System.Drawing.Point(150, 4);
            this.gui_run.Name = "gui_run";
            this.gui_run.Size = new System.Drawing.Size(75, 23);
            this.gui_run.TabIndex = 2;
            this.gui_run.Text = "Запуск";
            this.gui_run.UseVisualStyleBackColor = true;
            this.gui_run.Click += new System.EventHandler(this.gui_run_Click);
            // 
            // gui_stop
            // 
            this.gui_stop.Location = new System.Drawing.Point(150, 4);
            this.gui_stop.Name = "gui_stop";
            this.gui_stop.Size = new System.Drawing.Size(75, 23);
            this.gui_stop.TabIndex = 3;
            this.gui_stop.Text = "Выключить";
            this.gui_stop.UseVisualStyleBackColor = true;
            this.gui_stop.Visible = false;
            this.gui_stop.Click += new System.EventHandler(this.gui_stop_Click);
            // 
            // gui_log
            // 
            this.gui_log.Location = new System.Drawing.Point(3, 160);
            this.gui_log.Multiline = true;
            this.gui_log.Name = "gui_log";
            this.gui_log.ReadOnly = true;
            this.gui_log.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.gui_log.Size = new System.Drawing.Size(222, 98);
            this.gui_log.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Порт:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Корневая директория:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // gui_dir
            // 
            this.gui_dir.Location = new System.Drawing.Point(14, 81);
            this.gui_dir.Name = "gui_dir";
            this.gui_dir.Size = new System.Drawing.Size(211, 20);
            this.gui_dir.TabIndex = 7;
            this.gui_dir.Text = "gui_dir";
            this.gui_dir.TextChanged += new System.EventHandler(this.gui_dir_TextChanged);
            // 
            // gui_choosedir
            // 
            this.gui_choosedir.Location = new System.Drawing.Point(150, 107);
            this.gui_choosedir.Name = "gui_choosedir";
            this.gui_choosedir.Size = new System.Drawing.Size(75, 23);
            this.gui_choosedir.TabIndex = 8;
            this.gui_choosedir.Text = "Обзор";
            this.gui_choosedir.UseVisualStyleBackColor = true;
            this.gui_choosedir.Click += new System.EventHandler(this.gui_choosedir_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(233, 261);
            this.Controls.Add(this.gui_choosedir);
            this.Controls.Add(this.gui_dir);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gui_log);
            this.Controls.Add(this.gui_stop);
            this.Controls.Add(this.gui_run);
            this.Controls.Add(this.gui_port);
            this.Name = "Form1";
            this.Text = "Веб-сервер";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.gui_port)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown gui_port;
        private System.Windows.Forms.Button gui_run;
        private System.Windows.Forms.Button gui_stop;
        private System.Windows.Forms.TextBox gui_log;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox gui_dir;
        private System.Windows.Forms.Button gui_choosedir;
    }
}

