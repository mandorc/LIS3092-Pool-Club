namespace Pool_Club
{
    partial class MenuFrame
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.startButtom = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // startButtom
            // 
            this.startButtom.Location = new System.Drawing.Point(301, 262);
            this.startButtom.Name = "startButtom";
            this.startButtom.Size = new System.Drawing.Size(238, 97);
            this.startButtom.TabIndex = 0;
            this.startButtom.Text = "button1";
            this.startButtom.UseVisualStyleBackColor = true;
            this.startButtom.Click += new System.EventHandler(this.startButtom_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.startButtom);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private Button startButtom;
    }
}