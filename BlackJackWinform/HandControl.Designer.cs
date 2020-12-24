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
            this.lblActive = new System.Windows.Forms.Label();
            this.pnlMoney = new System.Windows.Forms.Panel();
            this.lblOutcome = new System.Windows.Forms.Label();
            this.pnlHand = new System.Windows.Forms.Panel();
            this.pnlMoney.SuspendLayout();
            this.pnlHand.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblActive
            // 
            this.lblActive.AutoSize = true;
            this.lblActive.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActive.ForeColor = System.Drawing.Color.Yellow;
            this.lblActive.Location = new System.Drawing.Point(78, -4);
            this.lblActive.Name = "lblActive";
            this.lblActive.Size = new System.Drawing.Size(25, 31);
            this.lblActive.TabIndex = 0;
            this.lblActive.Text = "*";
            this.lblActive.Visible = false;
            // 
            // pnlMoney
            // 
            this.pnlMoney.Controls.Add(this.lblOutcome);
            this.pnlMoney.Location = new System.Drawing.Point(0, 0);
            this.pnlMoney.Name = "pnlMoney";
            this.pnlMoney.Size = new System.Drawing.Size(242, 54);
            this.pnlMoney.TabIndex = 1;
            // 
            // lblOutcome
            // 
            this.lblOutcome.AutoSize = true;
            this.lblOutcome.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOutcome.ForeColor = System.Drawing.Color.Yellow;
            this.lblOutcome.Location = new System.Drawing.Point(3, 35);
            this.lblOutcome.Name = "lblOutcome";
            this.lblOutcome.Size = new System.Drawing.Size(90, 20);
            this.lblOutcome.TabIndex = 2;
            this.lblOutcome.Text = "{outcome}";
            this.lblOutcome.Visible = false;
            // 
            // pnlHand
            // 
            this.pnlHand.Controls.Add(this.lblActive);
            this.pnlHand.Location = new System.Drawing.Point(0, 60);
            this.pnlHand.Name = "pnlHand";
            this.pnlHand.Size = new System.Drawing.Size(242, 245);
            this.pnlHand.TabIndex = 2;
            // 
            // HandControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlHand);
            this.Controls.Add(this.pnlMoney);
            this.Name = "HandControl";
            this.Size = new System.Drawing.Size(245, 308);
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
    }
}
