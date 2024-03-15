using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TraceGPS
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            
            //// test des passerelles
            //// les fichiers de données sont placés dans le dossier d'exécution
            //String nomFichier;
            ////nomFichier = "2015-09-13_11-07-37.gpx";
            //nomFichier = "fit-20161203T102115.gpx";
            ////nomFichier = "fit-20161203T102115.pwx";
            ////nomFichier = "fit-20161203T102115.tcx";

            //Trace laTrace = new Trace();
            //Passerelle laPasserelle = null;

            //// création de la passerelle en fonction du type de fichier
            //if (nomFichier.ToLower().EndsWith(".gpx")) laPasserelle = new PasserelleGPX();
            //if (nomFichier.ToLower().EndsWith(".pwx")) laPasserelle = new PasserellePWX();
            //if (nomFichier.ToLower().EndsWith(".tcx")) laPasserelle = new PasserelleTCX();

            //String msg = laPasserelle.creerTrace(nomFichier, laTrace);

            //if (msg != "")
            //    MessageBox.Show(msg, "Problème", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);      // si erreur retournée par la passerelle					
            //else
            //    MessageBox.Show(laTrace.toString(), "Résultat", MessageBoxButtons.OK, MessageBoxIcon.Information);       // si aucune erreur	
        }
    }
}
