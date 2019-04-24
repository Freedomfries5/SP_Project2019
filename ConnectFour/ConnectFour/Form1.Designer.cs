namespace ConnectFour
{
    partial class ConnectFour
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
            this.PlayChoiceLabel = new System.Windows.Forms.Label();
            this.PlayerButton = new System.Windows.Forms.Button();
            this.PCButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.easyButt = new System.Windows.Forms.Button();
            this.medButt = new System.Windows.Forms.Button();
            this.hardButt = new System.Windows.Forms.Button();
            this.turnLabel = new System.Windows.Forms.Label();
            this.turnDisplay = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // PlayChoiceLabel
            // 
            this.PlayChoiceLabel.AutoSize = true;
            this.PlayChoiceLabel.Location = new System.Drawing.Point(447, 11);
            this.PlayChoiceLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.PlayChoiceLabel.Name = "PlayChoiceLabel";
            this.PlayChoiceLabel.Size = new System.Drawing.Size(81, 13);
            this.PlayChoiceLabel.TabIndex = 0;
            this.PlayChoiceLabel.Text = "Who goes first?";
            // 
            // PlayerButton
            // 
            this.PlayerButton.Location = new System.Drawing.Point(449, 28);
            this.PlayerButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.PlayerButton.Name = "PlayerButton";
            this.PlayerButton.Size = new System.Drawing.Size(68, 19);
            this.PlayerButton.TabIndex = 1;
            this.PlayerButton.Text = "Player";
            this.PlayerButton.UseVisualStyleBackColor = true;
            this.PlayerButton.Click += new System.EventHandler(this.PlayerButton_Click);
            // 
            // PCButton
            // 
            this.PCButton.Location = new System.Drawing.Point(449, 51);
            this.PCButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.PCButton.Name = "PCButton";
            this.PCButton.Size = new System.Drawing.Size(68, 19);
            this.PCButton.TabIndex = 2;
            this.PCButton.Text = "Computer";
            this.PCButton.UseVisualStyleBackColor = true;
            this.PCButton.Click += new System.EventHandler(this.PCButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(447, 116);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Difficulty (Medium as Default)";
            // 
            // easyButt
            // 
            this.easyButt.Location = new System.Drawing.Point(449, 141);
            this.easyButt.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.easyButt.Name = "easyButt";
            this.easyButt.Size = new System.Drawing.Size(95, 19);
            this.easyButt.TabIndex = 4;
            this.easyButt.Text = "Game Journo";
            this.easyButt.UseVisualStyleBackColor = true;
            this.easyButt.Click += new System.EventHandler(this.easyButt_Click);
            // 
            // medButt
            // 
            this.medButt.Location = new System.Drawing.Point(449, 165);
            this.medButt.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.medButt.Name = "medButt";
            this.medButt.Size = new System.Drawing.Size(95, 19);
            this.medButt.TabIndex = 5;
            this.medButt.Text = "Normal Gamer";
            this.medButt.UseVisualStyleBackColor = true;
            // 
            // hardButt
            // 
            this.hardButt.Location = new System.Drawing.Point(449, 188);
            this.hardButt.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.hardButt.Name = "hardButt";
            this.hardButt.Size = new System.Drawing.Size(95, 24);
            this.hardButt.TabIndex = 6;
            this.hardButt.Text = "Pain a Plenty";
            this.hardButt.UseVisualStyleBackColor = true;
            // 
            // turnLabel
            // 
            this.turnLabel.AutoSize = true;
            this.turnLabel.Location = new System.Drawing.Point(449, 236);
            this.turnLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.turnLabel.Name = "turnLabel";
            this.turnLabel.Size = new System.Drawing.Size(66, 13);
            this.turnLabel.TabIndex = 7;
            this.turnLabel.Text = "Current Turn";
            // 
            // turnDisplay
            // 
            this.turnDisplay.Location = new System.Drawing.Point(449, 253);
            this.turnDisplay.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.turnDisplay.Name = "turnDisplay";
            this.turnDisplay.Size = new System.Drawing.Size(133, 20);
            this.turnDisplay.TabIndex = 0;
            // 
            // ConnectFour
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.turnDisplay);
            this.Controls.Add(this.turnLabel);
            this.Controls.Add(this.hardButt);
            this.Controls.Add(this.medButt);
            this.Controls.Add(this.easyButt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PCButton);
            this.Controls.Add(this.PlayerButton);
            this.Controls.Add(this.PlayChoiceLabel);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "ConnectFour";
            this.Text = "Connectu Fouru";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ConnectFour_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ConnectFour_MouseClick);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label PlayChoiceLabel;
        private System.Windows.Forms.Button PlayerButton;
        private System.Windows.Forms.Button PCButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button easyButt;
        private System.Windows.Forms.Button medButt;
        private System.Windows.Forms.Button hardButt;
        private System.Windows.Forms.Label turnLabel;
        private System.Windows.Forms.TextBox turnDisplay;
    }
}

