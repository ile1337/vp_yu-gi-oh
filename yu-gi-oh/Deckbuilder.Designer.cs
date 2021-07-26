
namespace yu_gi_oh
{
    partial class Deckbuilder
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.loadingPB = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgv1 = new System.Windows.Forms.DataGridView();
            this.btnSaveDeck = new System.Windows.Forms.Button();
            this.btnNewDeck = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnAddtoDeck = new System.Windows.Forms.Button();
            this.btnRemoveFromDeck = new System.Windows.Forms.Button();
            this.btnOpenDeck = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.dgvDeck = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loadingPB)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeck)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackgroundImage = global::yu_gi_oh.Properties.Resources.link_wizard__bg__by_alanmac95_dcbum43_250t;
            this.groupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.groupBox1.Controls.Add(this.dgvDeck);
            this.groupBox1.Location = new System.Drawing.Point(37, 45);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(714, 492);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Your Deck";
            // 
            // loadingPB
            // 
            this.loadingPB.AccessibleName = "";
            this.loadingPB.BackColor = System.Drawing.SystemColors.Window;
            this.loadingPB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.loadingPB.Image = global::yu_gi_oh.Properties.Resources.loading;
            this.loadingPB.Location = new System.Drawing.Point(6, 11);
            this.loadingPB.Name = "loadingPB";
            this.loadingPB.Size = new System.Drawing.Size(738, 475);
            this.loadingPB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.loadingPB.TabIndex = 5;
            this.loadingPB.TabStop = false;
            this.loadingPB.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.BackgroundImage = global::yu_gi_oh.Properties.Resources.link_wizard__bg__by_alanmac95_dcbum43_250t;
            this.groupBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.groupBox2.Controls.Add(this.loadingPB);
            this.groupBox2.Controls.Add(this.dgv1);
            this.groupBox2.Location = new System.Drawing.Point(770, 44);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(750, 492);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Available Cards";
            // 
            // dgv1
            // 
            this.dgv1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv1.Location = new System.Drawing.Point(6, 11);
            this.dgv1.Name = "dgv1";
            this.dgv1.ReadOnly = true;
            this.dgv1.RowTemplate.Height = 25;
            this.dgv1.Size = new System.Drawing.Size(738, 475);
            this.dgv1.TabIndex = 15;
            // 
            // btnSaveDeck
            // 
            this.btnSaveDeck.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnSaveDeck.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnSaveDeck.Location = new System.Drawing.Point(37, 549);
            this.btnSaveDeck.Name = "btnSaveDeck";
            this.btnSaveDeck.Size = new System.Drawing.Size(127, 33);
            this.btnSaveDeck.TabIndex = 7;
            this.btnSaveDeck.Text = "Save Deck";
            this.btnSaveDeck.UseVisualStyleBackColor = false;
            this.btnSaveDeck.Click += new System.EventHandler(this.btnSaveDeck_Click);
            // 
            // btnNewDeck
            // 
            this.btnNewDeck.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnNewDeck.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnNewDeck.Location = new System.Drawing.Point(315, 549);
            this.btnNewDeck.Name = "btnNewDeck";
            this.btnNewDeck.Size = new System.Drawing.Size(127, 33);
            this.btnNewDeck.TabIndex = 8;
            this.btnNewDeck.Text = "New Deck";
            this.btnNewDeck.UseVisualStyleBackColor = false;
            this.btnNewDeck.Click += new System.EventHandler(this.btnNewDeck_Click);
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnBack.Location = new System.Drawing.Point(1399, 658);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(127, 33);
            this.btnBack.TabIndex = 9;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnAddtoDeck
            // 
            this.btnAddtoDeck.Location = new System.Drawing.Point(770, 542);
            this.btnAddtoDeck.Name = "btnAddtoDeck";
            this.btnAddtoDeck.Size = new System.Drawing.Size(173, 53);
            this.btnAddtoDeck.TabIndex = 11;
            this.btnAddtoDeck.Text = "Add to deck";
            this.btnAddtoDeck.UseVisualStyleBackColor = true;
            this.btnAddtoDeck.Click += new System.EventHandler(this.btnAddtoDeck_Click);
            // 
            // btnRemoveFromDeck
            // 
            this.btnRemoveFromDeck.Location = new System.Drawing.Point(949, 542);
            this.btnRemoveFromDeck.Name = "btnRemoveFromDeck";
            this.btnRemoveFromDeck.Size = new System.Drawing.Size(173, 53);
            this.btnRemoveFromDeck.TabIndex = 12;
            this.btnRemoveFromDeck.Text = "Remove from deck";
            this.btnRemoveFromDeck.UseVisualStyleBackColor = true;
            this.btnRemoveFromDeck.Click += new System.EventHandler(this.btnRemoveFromDeck_Click);
            // 
            // btnOpenDeck
            // 
            this.btnOpenDeck.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnOpenDeck.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnOpenDeck.Location = new System.Drawing.Point(182, 549);
            this.btnOpenDeck.Name = "btnOpenDeck";
            this.btnOpenDeck.Size = new System.Drawing.Size(127, 33);
            this.btnOpenDeck.TabIndex = 14;
            this.btnOpenDeck.Text = "Open Deck";
            this.btnOpenDeck.UseVisualStyleBackColor = false;
            this.btnOpenDeck.Click += new System.EventHandler(this.btnOpenDeck_Click);
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(1128, 542);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(59, 25);
            this.button1.TabIndex = 16;
            this.button1.Text = "<<";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1193, 542);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(54, 25);
            this.button2.TabIndex = 17;
            this.button2.Text = ">>";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // dgvDeck
            // 
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.dgvDeck.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDeck.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDeck.Location = new System.Drawing.Point(7, 22);
            this.dgvDeck.Name = "dgvDeck";
            this.dgvDeck.RowTemplate.Height = 25;
            this.dgvDeck.Size = new System.Drawing.Size(701, 463);
            this.dgvDeck.TabIndex = 0;
            // 
            // Deckbuilder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::yu_gi_oh.Properties.Resources.d17ad80144ef56adbf58a17a686ea619;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1538, 703);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnOpenDeck);
            this.Controls.Add(this.btnRemoveFromDeck);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnAddtoDeck);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnNewDeck);
            this.Controls.Add(this.btnSaveDeck);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Deckbuilder";
            this.Text = "Deckbuilder";
            this.Load += new System.EventHandler(this.Deckbuilder_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.loadingPB)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeck)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnSaveDeck;
        private System.Windows.Forms.Button btnNewDeck;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnAddtoDeck;
        private System.Windows.Forms.Button btnRemoveFromDeck;
        private System.Windows.Forms.Button btnOpenDeck;
        private System.Windows.Forms.PictureBox loadingPB;
        private System.Windows.Forms.DataGridView dgv1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridView dgvDeck;
    }
}