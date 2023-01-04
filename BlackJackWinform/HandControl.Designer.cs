namespace BlackJackWinform
{
    partial class HandControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblActive = new System.Windows.Forms.Label();
            this.pnlMoney = new System.Windows.Forms.Panel();
            this.lblOutcome = new System.Windows.Forms.Label();
            this.pnlHand = new System.Windows.Forms.Panel();
            this.btnStand = new System.Windows.Forms.Button();
            this.btnSplit = new System.Windows.Forms.Button();
            this.btnHit = new System.Windows.Forms.Button();
            this.btnDoubleDown = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pnlMoney.SuspendLayout();
            this.pnlHand.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblActive
            // 
            this.lblActive.AutoSize = true;
            this.lblActive.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActive.ForeColor = System.Drawing.Color.Yellow;
            this.lblActive.Location = new System.Drawing.Point(117, -6);
            this.lblActive.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblActive.Name = "lblActive";
            this.lblActive.Size = new System.Drawing.Size(41, 54);
            this.lblActive.TabIndex = 0;
            this.lblActive.Text = "*";
            this.lblActive.Visible = false;
            // 
            // pnlMoney
            // 
            this.pnlMoney.Controls.Add(this.lblOutcome);
            this.pnlMoney.Location = new System.Drawing.Point(21, 5);
            this.pnlMoney.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlMoney.Name = "pnlMoney";
            this.pnlMoney.Size = new System.Drawing.Size(363, 83);
            this.pnlMoney.TabIndex = 1;
            // 
            // lblOutcome
            // 
            this.lblOutcome.AutoSize = true;
            this.lblOutcome.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOutcome.ForeColor = System.Drawing.Color.Yellow;
            this.lblOutcome.Location = new System.Drawing.Point(4, 54);
            this.lblOutcome.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOutcome.Name = "lblOutcome";
            this.lblOutcome.Size = new System.Drawing.Size(150, 32);
            this.lblOutcome.TabIndex = 2;
            this.lblOutcome.Text = "{outcome}";
            this.lblOutcome.Visible = false;
            // 
            // pnlHand
            // 
            this.pnlHand.Controls.Add(this.lblActive);
            this.pnlHand.Location = new System.Drawing.Point(21, 92);
            this.pnlHand.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlHand.Name = "pnlHand";
            this.pnlHand.Size = new System.Drawing.Size(363, 320);
            this.pnlHand.TabIndex = 2;
            // 
            // btnStand
            // 
            this.btnStand.Location = new System.Drawing.Point(112, 422);
            this.btnStand.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnStand.Name = "btnStand";
            this.btnStand.Size = new System.Drawing.Size(64, 48);
            this.btnStand.TabIndex = 4;
            this.btnStand.Text = "Stand";
            this.btnStand.UseVisualStyleBackColor = true;
            // 
            // btnSplit
            // 
            this.btnSplit.Location = new System.Drawing.Point(268, 422);
            this.btnSplit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSplit.Name = "btnSplit";
            this.btnSplit.Size = new System.Drawing.Size(64, 48);
            this.btnSplit.TabIndex = 6;
            this.btnSplit.Text = "Split";
            this.btnSplit.UseVisualStyleBackColor = true;
            // 
            // btnHit
            // 
            this.btnHit.Location = new System.Drawing.Point(44, 422);
            this.btnHit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnHit.Name = "btnHit";
            this.btnHit.Size = new System.Drawing.Size(60, 48);
            this.btnHit.TabIndex = 3;
            this.btnHit.Text = "Hit";
            this.btnHit.UseVisualStyleBackColor = true;
            // 
            // btnDoubleDown
            // 
            this.btnDoubleDown.Location = new System.Drawing.Point(186, 422);
            this.btnDoubleDown.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnDoubleDown.Name = "btnDoubleDown";
            this.btnDoubleDown.Size = new System.Drawing.Size(74, 48);
            this.btnDoubleDown.TabIndex = 5;
            this.btnDoubleDown.Text = "Double";
            this.btnDoubleDown.UseVisualStyleBackColor = true;
            // 
            // HandControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnStand);
            this.Controls.Add(this.btnHit);
            this.Controls.Add(this.btnDoubleDown);
            this.Controls.Add(this.btnSplit);
            this.Controls.Add(this.pnlHand);
            this.Controls.Add(this.pnlMoney);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "HandControl";
            this.Size = new System.Drawing.Size(416, 478);
            this.pnlMoney.ResumeLayout(false);
            this.pnlMoney.PerformLayout();
            this.pnlHand.ResumeLayout(false);
            this.pnlHand.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.Label lblActive;
        protected System.Windows.Forms.Panel pnlMoney;
        protected System.Windows.Forms.Label lblOutcome;
        protected System.Windows.Forms.Panel pnlHand;
        public System.Windows.Forms.Button btnStand;
        public System.Windows.Forms.Button btnSplit;
        public System.Windows.Forms.Button btnHit;
        public System.Windows.Forms.Button btnDoubleDown;
        private System.Windows.Forms.Timer timer1;
    }
}
