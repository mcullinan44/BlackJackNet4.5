namespace BlackJackWinform
{
    partial class BlackJackForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BlackJackForm));
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.splitter1 = new System.Windows.Forms.Splitter();
            this._playerLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this._dealerLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this._btnBet = new System.Windows.Forms.Button();
            this._tbBet = new System.Windows.Forms.MaskedTextBox();
            this._lblBankroll = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 245);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 20);
            this.label2.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(456, 109);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 20);
            this.label4.TabIndex = 14;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "cards.bmp");
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(4, 1214);
            this.splitter1.TabIndex = 38;
            this.splitter1.TabStop = false;
            // 
            // _playerLayoutPanel
            // 
            this._playerLayoutPanel.BackColor = System.Drawing.Color.Transparent;
            this._playerLayoutPanel.Location = new System.Drawing.Point(28, 528);
            this._playerLayoutPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this._playerLayoutPanel.Name = "_playerLayoutPanel";
            this._playerLayoutPanel.Size = new System.Drawing.Size(1230, 477);
            this._playerLayoutPanel.TabIndex = 34;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Wide Latin", 15.85714F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Yellow;
            this.label6.Location = new System.Drawing.Point(147, 487);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1111, 46);
            this.label6.TabIndex = 16;
            this.label6.Text = "Dealer hits on 16, stands on soft 17";
            // 
            // _dealerLayoutPanel
            // 
            this._dealerLayoutPanel.BackColor = System.Drawing.Color.Transparent;
            this._dealerLayoutPanel.Location = new System.Drawing.Point(465, 18);
            this._dealerLayoutPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this._dealerLayoutPanel.Name = "_dealerLayoutPanel";
            this._dealerLayoutPanel.Size = new System.Drawing.Size(744, 442);
            this._dealerLayoutPanel.TabIndex = 33;
            // 
            // _btnBet
            // 
            this._btnBet.Location = new System.Drawing.Point(497, 1030);
            this._btnBet.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this._btnBet.Name = "_btnBet";
            this._btnBet.Size = new System.Drawing.Size(196, 35);
            this._btnBet.TabIndex = 11;
            this._btnBet.Text = "Bet and Deal!";
            this._btnBet.UseVisualStyleBackColor = true;
            this._btnBet.Click += new System.EventHandler(this.btnBet_Click);
            // 
            // _tbBet
            // 
            this._tbBet.BackColor = System.Drawing.Color.Black;
            this._tbBet.Font = new System.Drawing.Font("HP Simplified", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._tbBet.ForeColor = System.Drawing.Color.Yellow;
            this._tbBet.Location = new System.Drawing.Point(540, 1075);
            this._tbBet.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this._tbBet.Mask = "#####";
            this._tbBet.Name = "_tbBet";
            this._tbBet.Size = new System.Drawing.Size(110, 40);
            this._tbBet.TabIndex = 22;
            // 
            // _lblBankroll
            // 
            this._lblBankroll.AutoSize = true;
            this._lblBankroll.BackColor = System.Drawing.Color.Transparent;
            this._lblBankroll.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblBankroll.ForeColor = System.Drawing.Color.Yellow;
            this._lblBankroll.Location = new System.Drawing.Point(577, 1132);
            this._lblBankroll.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._lblBankroll.Name = "_lblBankroll";
            this._lblBankroll.Size = new System.Drawing.Size(43, 51);
            this._lblBankroll.TabIndex = 39;
            this._lblBankroll.Text = "0";
            // 
            // BlackJackForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGreen;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1353, 1214);
            this.Controls.Add(this._lblBankroll);
            this.Controls.Add(this._playerLayoutPanel);
            this.Controls.Add(this._dealerLayoutPanel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this._tbBet);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._btnBet);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "BlackJackForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Blackjack";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.FlowLayoutPanel _playerLayoutPanel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.FlowLayoutPanel _dealerLayoutPanel;
        private System.Windows.Forms.Button _btnBet;
        private System.Windows.Forms.MaskedTextBox _tbBet;
        private System.Windows.Forms.Label _lblBankroll;
    }
}

