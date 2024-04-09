// TP C# réalisé sous Visual Studio 2010
// Thème : affichage de trace GPS - Feuille A propos de...
// Auteur : dp
// Dernière mise à jour : 1/11/2021

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TraceGPS
{
    public partial class FormAPropos : Form
    {
        double t;                           // le temps qui passe (en secondes)
        double yDepart;                     // position verticale de l'image au départ
        double rebond;                      // valeur du rebond de la feuille (positif ou négatif)        
        const double amortissement = 0.9;   // amortissement de 90 % de chaque rebond

        public FormAPropos()
        {
            InitializeComponent();
        }

        private void FormAPropos_Load(object sender, EventArgs e)
        {
            this.Text = Global.NOM_APPLICATION + " - A propos du logiciel...";
            yDepart = this.pictureBox1.Top;  // stocker la position verticale de l'image au départ
            t = 0.0;
            rebond = 30.0;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            t = t + Timer1.Interval / 1000.0;
            this.pictureBox1.Top = (int) (yDepart + t * t * this.ClientSize.Height);
            if (this.pictureBox1.Top + this.pictureBox1.Height > this.ClientSize.Height)
            {
                this.pictureBox1.Top = this.ClientSize.Height - this.pictureBox1.Height;
                t = 0.0;
                Timer1.Enabled = false;                 // la chute est finie...
                Timer2.Enabled = true;                  // mais la feuille va être secouée
                                                        // émet un bip sonore
            }
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            this.Timer1.Enabled = true;                 // la chute démarre
        }

        private void Timer2_Tick(object sender, EventArgs e)
        {
            this.Top =  this.Top + (int)rebond;       // vibration verticale
            rebond = -rebond * amortissement;           // amortissement et inversion du rebond
            t = t + Timer2.Interval / 1000.0;
            if (t > 2.0) this.Close();                    // au bout de 2 secondes, ça suffit !
        }

    }
}
