namespace FrontEnd
{
    partial class MainForm
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
            this.pbMainFrame = new System.Windows.Forms.PictureBox();
            this.gameTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pbMainFrame)).BeginInit();
            this.SuspendLayout();
            // 
            // pbMainFrame
            // 
            this.pbMainFrame.Location = new System.Drawing.Point(12, 12);
            this.pbMainFrame.Name = "pbMainFrame";
            this.pbMainFrame.Size = new System.Drawing.Size(564, 313);
            this.pbMainFrame.TabIndex = 0;
            this.pbMainFrame.TabStop = false;
            this.pbMainFrame.Paint += new System.Windows.Forms.PaintEventHandler(this.pbMainFrame_Paint);
            // 
            // gameTimer
            // 
            this.gameTimer.Tick += new System.EventHandler(this.gameTimer_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(588, 337);
            this.Controls.Add(this.pbMainFrame);
            this.Name = "MainForm";
            this.Text = "GameEngine";
            ((System.ComponentModel.ISupportInitialize)(this.pbMainFrame)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbMainFrame;
        private System.Windows.Forms.Timer gameTimer;
    }
}

