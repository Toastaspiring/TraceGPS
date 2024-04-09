namespace TraceGPS
{
    partial class FormIdentification
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormIdentification));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnAccesComplet = new System.Windows.Forms.Button();
            this.txtPseudo = new System.Windows.Forms.TextBox();
            this.txtMotDePasse = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnAccesReduit = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnAccesComplet);
            this.groupBox1.Controls.Add(this.txtPseudo);
            this.groupBox1.Controls.Add(this.txtMotDePasse);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(22, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(681, 158);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Accès avec identification";
            // 
            // btnAccesComplet
            // 
            this.btnAccesComplet.Location = new System.Drawing.Point(246, 109);
            this.btnAccesComplet.Name = "btnAccesComplet";
            this.btnAccesComplet.Size = new System.Drawing.Size(209, 26);
            this.btnAccesComplet.TabIndex = 3;
            this.btnAccesComplet.Text = "Connexion";
            this.btnAccesComplet.UseVisualStyleBackColor = true;
            this.btnAccesComplet.Click += new System.EventHandler(this.btnAccesComplet_Click);
            // 
            // txtPseudo
            // 
            this.txtPseudo.Location = new System.Drawing.Point(112, 70);
            this.txtPseudo.Name = "txtPseudo";
            this.txtPseudo.Size = new System.Drawing.Size(208, 20);
            this.txtPseudo.TabIndex = 1;
            // 
            // txtMotDePasse
            // 
            this.txtMotDePasse.Location = new System.Drawing.Point(434, 70);
            this.txtMotDePasse.Name = "txtMotDePasse";
            this.txtMotDePasse.PasswordChar = '*';
            this.txtMotDePasse.Size = new System.Drawing.Size(173, 20);
            this.txtMotDePasse.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(351, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Mot de passe :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(57, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Pseudo :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(499, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ce type d\'accès vous permet de consulter les traces des utilisateurs qui vous ont" +
    " donné leur autorisation.\r\nIl permet également de consulter des fichiers enregis" +
    "trés sur cet ordinateur.";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnAccesReduit);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(22, 208);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(681, 105);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Accès sans identification";
            // 
            // btnAccesReduit
            // 
            this.btnAccesReduit.Location = new System.Drawing.Point(246, 59);
            this.btnAccesReduit.Name = "btnAccesReduit";
            this.btnAccesReduit.Size = new System.Drawing.Size(209, 26);
            this.btnAccesReduit.TabIndex = 4;
            this.btnAccesReduit.Text = "Accès aux fichiers uniquement";
            this.btnAccesReduit.UseVisualStyleBackColor = true;
            this.btnAccesReduit.Click += new System.EventHandler(this.btnAccesReduit_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(461, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Ce type d\'accès vous permet uniquement de consulter des fichiers enregistrés sur " +
    "cet ordinateur.";
            // 
            // FormIdentification
            // 
            this.AcceptButton = this.btnAccesComplet;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(732, 332);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormIdentification";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Trace GPS 1.1 - Connexion";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAccesComplet;
        private System.Windows.Forms.TextBox txtPseudo;
        private System.Windows.Forms.TextBox txtMotDePasse;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnAccesReduit;
    }
}