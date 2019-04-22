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
            this.SuspendLayout();
            // 
            // PlayChoiceLabel
            // 
            this.PlayChoiceLabel.AutoSize = true;
            this.PlayChoiceLabel.Location = new System.Drawing.Point(640, 13);
            this.PlayChoiceLabel.Name = "PlayChoiceLabel";
            this.PlayChoiceLabel.Size = new System.Drawing.Size(107, 17);
            this.PlayChoiceLabel.TabIndex = 0;
            this.PlayChoiceLabel.Text = "Who goes first?";
            // 
            // PlayerButton
            // 
            this.PlayerButton.Location = new System.Drawing.Point(643, 34);
            this.PlayerButton.Name = "PlayerButton";
            this.PlayerButton.Size = new System.Drawing.Size(75, 23);
            this.PlayerButton.TabIndex = 1;
            this.PlayerButton.Text = "Player";
            this.PlayerButton.UseVisualStyleBackColor = true;
            // 
            // PCButton
            // 
            this.PCButton.Location = new System.Drawing.Point(643, 64);
            this.PCButton.Name = "PCButton";
            this.PCButton.Size = new System.Drawing.Size(75, 23);
            this.PCButton.TabIndex = 2;
            this.PCButton.Text = "Computer";
            this.PCButton.UseVisualStyleBackColor = true;
            // 
            // ConnectFour
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.PCButton);
            this.Controls.Add(this.PlayerButton);
            this.Controls.Add(this.PlayChoiceLabel);
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
    }
}

