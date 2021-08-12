
namespace yu_gi_oh
{
    partial class Graveyard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Graveyard));
            this.lbGraveyardCards = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pbGrobishta = new System.Windows.Forms.PictureBox();
            this.btnToDeck = new System.Windows.Forms.Button();
            this.btnToHand = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbGrobishta)).BeginInit();
            this.SuspendLayout();
            // 
            // lbGraveyardCards
            // 
            this.lbGraveyardCards.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbGraveyardCards.FormattingEnabled = true;
            this.lbGraveyardCards.ItemHeight = 15;
            this.lbGraveyardCards.Location = new System.Drawing.Point(24, 30);
            this.lbGraveyardCards.Name = "lbGraveyardCards";
            this.lbGraveyardCards.Size = new System.Drawing.Size(209, 289);
            this.lbGraveyardCards.TabIndex = 0;
            this.lbGraveyardCards.SelectedIndexChanged += new System.EventHandler(this.lbGraveyardCards_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.BackgroundImage = global::yu_gi_oh.Properties.Resources.ReaperoftheCards_TF04_JP_VG;
            this.groupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.groupBox1.Controls.Add(this.pbGrobishta);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI Black", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.groupBox1.Location = new System.Drawing.Point(365, 30);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(228, 289);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Card ";
            // 
            // pbGrobishta
            // 
            this.pbGrobishta.BackgroundImage = global::yu_gi_oh.Properties.Resources.wp2866512;
            this.pbGrobishta.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbGrobishta.Location = new System.Drawing.Point(24, 27);
            this.pbGrobishta.Name = "pbGrobishta";
            this.pbGrobishta.Size = new System.Drawing.Size(186, 237);
            this.pbGrobishta.TabIndex = 0;
            this.pbGrobishta.TabStop = false;
            // 
            // btnToDeck
            // 
            this.btnToDeck.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnToDeck.Font = new System.Drawing.Font("Segoe UI Black", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnToDeck.Location = new System.Drawing.Point(24, 356);
            this.btnToDeck.Name = "btnToDeck";
            this.btnToDeck.Size = new System.Drawing.Size(89, 44);
            this.btnToDeck.TabIndex = 2;
            this.btnToDeck.Text = "To Deck";
            this.btnToDeck.UseVisualStyleBackColor = false;
            this.btnToDeck.Click += new System.EventHandler(this.btnToDeck_Click);
            // 
            // btnToHand
            // 
            this.btnToHand.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnToHand.Font = new System.Drawing.Font("Segoe UI Black", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnToHand.Location = new System.Drawing.Point(144, 356);
            this.btnToHand.Name = "btnToHand";
            this.btnToHand.Size = new System.Drawing.Size(89, 44);
            this.btnToHand.TabIndex = 3;
            this.btnToHand.Text = "To Hand";
            this.btnToHand.UseVisualStyleBackColor = false;
            this.btnToHand.Click += new System.EventHandler(this.btnToHand_Click);
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnBack.Font = new System.Drawing.Font("Segoe UI Black", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnBack.Location = new System.Drawing.Point(504, 356);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(89, 44);
            this.btnBack.TabIndex = 4;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // Graveyard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::yu_gi_oh.Properties.Resources.Grobishta;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(605, 424);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnToHand);
            this.Controls.Add(this.btnToDeck);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lbGraveyardCards);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Graveyard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Graveyard";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbGrobishta)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbGraveyardCards;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pbGrobishta;
        private System.Windows.Forms.Button btnToDeck;
        private System.Windows.Forms.Button btnToHand;
        private System.Windows.Forms.Button btnBack;
    }
}