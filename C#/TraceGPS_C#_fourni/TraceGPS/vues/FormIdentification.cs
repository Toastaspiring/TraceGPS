// TP C# réalisé sous Visual Studio 2013
// Thème : affichage de trace GPS - Formulaire d'identification
// Auteur : dp
// Dernière mise à jour : 1/11/2021

using System;
using System.Windows.Forms;

namespace TraceGPS
{
    public partial class FormIdentification : Form
    {
        public FormIdentification()
        {
            InitializeComponent();

            this.Text = Global.NOM_APPLICATION + " - Connexion...";
        }

        private void btnAccesReduit_Click(object sender, EventArgs e)
        {
            Global.pseudo = "";
            Global.mdpSha1 = "";

            this.Hide();
            Form1 formulairePrincipal = new Form1();
            formulairePrincipal.ShowDialog();
            this.Close();
        }

        private void btnAccesComplet_Click(object sender, EventArgs e)
        {
            String msg = "";
            String pseudo = txtPseudo.Text.Trim();
            String mdp = txtMotDePasse.Text.Trim();
            if (pseudo == "" || mdp == "")
            {
                msg = "Données non saisies !";
                MessageBox.Show(msg, Global.NOM_APPLICATION, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                String mdpSha1 = Outils.sha1(mdp);
                // appel du service web Connecter
                msg = PasserelleServicesWebXML.connecter(pseudo, mdpSha1);
                if (msg.StartsWith("Erreur"))
                {
                    // l'authentification n'est pas valide
                    msg = "Erreur : authentification incorrecte.";
                    MessageBox.Show(msg, Global.NOM_APPLICATION, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    // l'authentification est valide
                    Global.pseudo = pseudo;
                    Global.mdpSha1 = mdpSha1;

                    this.Hide();
                    Form1 formulairePrincipal = new Form1();
                    formulairePrincipal.ShowDialog();
                    this.Close();
                }
            }
        }
    }
}
