namespace HotelManager.GUI
{
    partial class AllOrderGUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AllOrderGUI));
            this.dtgBooking = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtFrom = new System.Windows.Forms.TextBox();
            this.txtTo = new System.Windows.Forms.TextBox();
            this.dtgDetails = new System.Windows.Forms.DataGridView();
            this.lblTotals = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dtgBooking)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // dtgBooking
            // 
            this.dtgBooking.AllowUserToAddRows = false;
            this.dtgBooking.AllowUserToDeleteRows = false;
            this.dtgBooking.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dtgBooking.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dtgBooking.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgBooking.Location = new System.Drawing.Point(379, 79);
            this.dtgBooking.Name = "dtgBooking";
            this.dtgBooking.ReadOnly = true;
            this.dtgBooking.RowTemplate.Height = 25;
            this.dtgBooking.Size = new System.Drawing.Size(473, 187);
            this.dtgBooking.TabIndex = 21;
            this.dtgBooking.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgBooking_CellClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("MV Boli", 21.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label1.Location = new System.Drawing.Point(666, -3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 39);
            this.label1.TabIndex = 26;
            this.label1.Text = "All Order";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.BackColor = System.Drawing.Color.Transparent;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblTotal.Location = new System.Drawing.Point(578, 45);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(52, 21);
            this.lblTotal.TabIndex = 28;
            this.lblTotal.Text = "Total:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label4.Location = new System.Drawing.Point(163, 208);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 21);
            this.label4.TabIndex = 31;
            this.label4.Text = "From:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label5.Location = new System.Drawing.Point(163, 265);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 21);
            this.label5.TabIndex = 32;
            this.label5.Text = "To:";
            // 
            // btnSearch
            // 
            this.btnSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.BackgroundImage")));
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnSearch.ForeColor = System.Drawing.Color.Yellow;
            this.btnSearch.Location = new System.Drawing.Point(163, 327);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(116, 31);
            this.btnSearch.TabIndex = 33;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtFrom
            // 
            this.txtFrom.Location = new System.Drawing.Point(163, 232);
            this.txtFrom.Name = "txtFrom";
            this.txtFrom.Size = new System.Drawing.Size(116, 23);
            this.txtFrom.TabIndex = 34;
            // 
            // txtTo
            // 
            this.txtTo.Location = new System.Drawing.Point(163, 289);
            this.txtTo.Name = "txtTo";
            this.txtTo.Size = new System.Drawing.Size(116, 23);
            this.txtTo.TabIndex = 35;
            // 
            // dtgDetails
            // 
            this.dtgDetails.AllowUserToAddRows = false;
            this.dtgDetails.AllowUserToDeleteRows = false;
            this.dtgDetails.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dtgDetails.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dtgDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgDetails.Location = new System.Drawing.Point(379, 279);
            this.dtgDetails.Name = "dtgDetails";
            this.dtgDetails.ReadOnly = true;
            this.dtgDetails.RowTemplate.Height = 25;
            this.dtgDetails.Size = new System.Drawing.Size(473, 156);
            this.dtgDetails.TabIndex = 36;
            // 
            // lblTotals
            // 
            this.lblTotals.AutoSize = true;
            this.lblTotals.BackColor = System.Drawing.Color.Transparent;
            this.lblTotals.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTotals.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblTotals.Location = new System.Drawing.Point(636, 45);
            this.lblTotals.Name = "lblTotals";
            this.lblTotals.Size = new System.Drawing.Size(48, 21);
            this.lblTotals.TabIndex = 37;
            this.lblTotals.Text = "Total";
            // 
            // AllOrderGUI
            // 
            this.AcceptButton = this.btnSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(966, 483);
            this.ControlBox = false;
            this.Controls.Add(this.lblTotals);
            this.Controls.Add(this.dtgDetails);
            this.Controls.Add(this.txtTo);
            this.Controls.Add(this.txtFrom);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtgBooking);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "AllOrderGUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AllBokingGUI";
            ((System.ComponentModel.ISupportInitialize)(this.dtgBooking)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgDetails)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DataGridView dtgBooking;
        private Label label1;
        private Label lblTotal;
        private Label label4;
        private Label label5;
        private Button btnSearch;
        private TextBox txtFrom;
        private TextBox txtTo;
        private DataGridView dtgDetails;
        private Label lblTotals;
    }
}