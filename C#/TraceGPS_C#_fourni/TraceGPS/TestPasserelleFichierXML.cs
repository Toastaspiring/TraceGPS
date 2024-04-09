using System;
using System.Windows.Forms;

namespace TraceGPS
{
    public class TestPasserelleFichierXML
    {
        static void Main()
        {
            // test des passerelles
            // les fichiers de données sont placés dans le dossier d'exécution
            String nomFichier;
            nomFichier = "fit-20161203T102115.gpx";
            //nomFichier = "fit-20161203T102115.pwx";
            //nomFichier = "fit-20161203T102115.tcx";

            Trace laTrace = new Trace();
            PasserelleFichierXML laPasserelle = null;

            // création de la passerelle en fonction du type de fichier
            if (nomFichier.ToLower().EndsWith(".gpx")) laPasserelle = new PasserelleGPX();
            if (nomFichier.ToLower().EndsWith(".pwx")) laPasserelle = new PasserellePWX();
            if (nomFichier.ToLower().EndsWith(".tcx")) laPasserelle = new PasserelleTCX();

            String msg = laPasserelle.creerTrace(nomFichier, laTrace);

            if (msg != "")
            {   // si erreur retournée par la passerelle	
                MessageBox.Show(msg, "Problème", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {   // si aucune erreur
                MessageBox.Show(laTrace.toString(), nomFichier, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
