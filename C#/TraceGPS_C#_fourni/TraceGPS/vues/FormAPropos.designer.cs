namespace TraceGPS
{
    partial class FormAPropos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAPropos));
            this.Timer1 = new System.Windows.Forms.Timer(this.components);
            this.Timer2 = new System.Windows.Forms.Timer(this.components);
            this.LinkLabel1 = new System.Windows.Forms.LinkLabel();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.BtnOK = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Timer1
            // 
            this.Timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // Timer2
            // 
            this.Timer2.Interval = 40;
            this.Timer2.Tick += new System.EventHandler(this.Timer2_Tick);
            // 
            // LinkLabel1
            // 
            this.LinkLabel1.AutoSize = true;
            this.LinkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LinkLabel1.Location = new System.Drawing.Point(488, 426);
            this.LinkLabel1.Name = "LinkLabel1";
            this.LinkLabel1.Size = new System.Drawing.Size(193, 13);
            this.LinkLabel1.TabIndex = 13;
            this.LinkLabel1.TabStop = true;
            this.LinkLabel1.Text = "http://www.lycee-delasalle.com/";
            // 
            // Label2
            // 
            this.Label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(440, 329);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(289, 22);
            this.Label2.TabIndex = 12;
            this.Label2.Text = "( Version 1.1 du 1/12/2017 )";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label4
            // 
            this.Label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label4.Location = new System.Drawing.Point(452, 395);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(264, 23);
            this.Label4.TabIndex = 11;
            this.Label4.Text = "Copyright © 2017 Jean-Michel CARTRON";
            this.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label3
            // 
            this.Label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.Location = new System.Drawing.Point(418, 268);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(333, 44);
            this.Label3.TabIndex = 10;
            this.Label3.Text = "Logiciel créé par Jean-Michel CARTRON (professeur d\'informatique au Lycée De La S" +
    "alle à Rennes)";
            this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BtnOK
            // 
            this.BtnOK.BackColor = System.Drawing.SystemColors.Control;
            this.BtnOK.Location = new System.Drawing.Point(548, 467);
            this.BtnOK.Name = "BtnOK";
            this.BtnOK.Size = new System.Drawing.Size(72, 23);
            this.BtnOK.TabIndex = 8;
            this.BtnOK.Text = "OK";
            this.BtnOK.UseVisualStyleBackColor = false;
            this.BtnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(42, 57);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(312, 124);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 38;
            this.pictureBox1.TabStop = false;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(418, 124);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(333, 48);
            this.label14.TabIndex = 41;
            this.label14.Text = "Le survol des points de parcours avec la souris affiche \r\nles données du point su" +
    "rvolé (temps cumulé, distance \r\ncumulée, vitesse, altitude et rythme cardiaque)";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(418, 82);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(304, 32);
            this.label12.TabIndex = 40;
            this.label12.Text = "Les points de départ et d\'arrivée sont repérés par \r\ndes marqueurs particuliers.";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(418, 37);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(316, 32);
            this.label13.TabIndex = 39;
            this.label13.Text = "Ce logiciel vous permet de visionner vos tracés aux \r\nformats gpx, pwx, tcx.";
            // 
            // FormAPropos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 566);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.LinkLabel1);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.BtnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAPropos";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "A propos du logiciel...";
            this.Load += new System.EventHandler(this.FormAPropos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Timer Timer1;
        internal System.Windows.Forms.Timer Timer2;
        internal System.Windows.Forms.LinkLabel LinkLabel1;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Button BtnOK;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
    }
}