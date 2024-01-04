namespace HotelManager.GUI
{
    partial class MainGUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainGUI));
            this.label1 = new System.Windows.Forms.Label();
            this.btnHotel = new System.Windows.Forms.Button();
            this.btnService = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.manageMyAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rechargeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lOGOUTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.menuStrip1.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("MV Boli", 27.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label1.Location = new System.Drawing.Point(172, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(435, 49);
            this.label1.TabIndex = 31;
            this.label1.Text = "Welcome to the Hotel";
            // 
            // btnHotel
            // 
            this.btnHotel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnHotel.BackgroundImage")));
            this.btnHotel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnHotel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnHotel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnHotel.Location = new System.Drawing.Point(216, 61);
            this.btnHotel.Name = "btnHotel";
            this.btnHotel.Size = new System.Drawing.Size(115, 30);
            this.btnHotel.TabIndex = 36;
            this.btnHotel.Text = "Hotel";
            this.btnHotel.UseVisualStyleBackColor = true;
            this.btnHotel.Click += new System.EventHandler(this.btnHotel_Click);
            // 
            // btnService
            // 
            this.btnService.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnService.BackgroundImage")));
            this.btnService.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnService.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnService.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnService.Location = new System.Drawing.Point(357, 61);
            this.btnService.Name = "btnService";
            this.btnService.Size = new System.Drawing.Size(115, 30);
            this.btnService.TabIndex = 37;
            this.btnService.Text = "Service";
            this.btnService.UseVisualStyleBackColor = true;
            this.btnService.Click += new System.EventHandler(this.btnService_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.manageMyAccountToolStripMenuItem,
            this.rechargeToolStripMenuItem,
            this.lOGOUTToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(899, 9);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(335, 28);
            this.menuStrip1.TabIndex = 39;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // manageMyAccountToolStripMenuItem
            // 
            this.manageMyAccountToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.manageMyAccountToolStripMenuItem.ForeColor = System.Drawing.Color.Blue;
            this.manageMyAccountToolStripMenuItem.Name = "manageMyAccountToolStripMenuItem";
            this.manageMyAccountToolStripMenuItem.Size = new System.Drawing.Size(162, 24);
            this.manageMyAccountToolStripMenuItem.Text = "Manage My Account";
            this.manageMyAccountToolStripMenuItem.Click += new System.EventHandler(this.manageMyAccountToolStripMenuItem_Click);
            // 
            // rechargeToolStripMenuItem
            // 
            this.rechargeToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.rechargeToolStripMenuItem.ForeColor = System.Drawing.Color.Blue;
            this.rechargeToolStripMenuItem.Name = "rechargeToolStripMenuItem";
            this.rechargeToolStripMenuItem.Size = new System.Drawing.Size(86, 24);
            this.rechargeToolStripMenuItem.Text = "Recharge";
            this.rechargeToolStripMenuItem.Click += new System.EventHandler(this.rechargeToolStripMenuItem_Click);
            // 
            // lOGOUTToolStripMenuItem
            // 
            this.lOGOUTToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.lOGOUTToolStripMenuItem.ForeColor = System.Drawing.Color.Red;
            this.lOGOUTToolStripMenuItem.Name = "lOGOUTToolStripMenuItem";
            this.lOGOUTToolStripMenuItem.Size = new System.Drawing.Size(79, 24);
            this.lOGOUTToolStripMenuItem.Text = "LOGOUT";
            this.lOGOUTToolStripMenuItem.Click += new System.EventHandler(this.lOGOUTToolStripMenuItem_Click);
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(976, 361);
            this.toolStripContainer1.Location = new System.Drawing.Point(129, 117);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(976, 386);
            this.toolStripContainer1.TabIndex = 40;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // MainGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1242, 609);
            this.ControlBox = false;
            this.Controls.Add(this.toolStripContainer1);
            this.Controls.Add(this.btnService);
            this.Controls.Add(this.btnHotel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.Color.Fuchsia;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "MainGUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Label label1;
        private Button btnHotel;
        private Button btnService;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem manageMyAccountToolStripMenuItem;
        private ToolStripMenuItem rechargeToolStripMenuItem;
        private ToolStripMenuItem lOGOUTToolStripMenuItem;
        private ToolStripContainer toolStripContainer1;
    }
}