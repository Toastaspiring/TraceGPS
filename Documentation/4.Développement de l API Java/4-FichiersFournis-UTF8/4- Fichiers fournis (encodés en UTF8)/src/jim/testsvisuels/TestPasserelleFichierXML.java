package jim.testsvisuels;

import jim.classes.PasserelleFichierXML;
import jim.classes.PasserelleGPX;
import jim.classes.PasserellePWX;
import jim.classes.PasserelleTCX;
import jim.classes.Trace;

public class TestPasserelleFichierXML {

	public static void main(String[] args) {
        // test des passerelles
        // les fichiers de données sont placés dans le dossier d'exécution
        String nomFichier = "";

        nomFichier = "fit-20161203T102115.gpx";
        //nomFichier = "fit-20161203T102115.pwx";
        //nomFichier = "fit-20161203T102115.tcx";

        Trace laTrace = new Trace();
        PasserelleFichierXML laPasserelle = null;

        // création de la passerelle en fonction du type de fichier
        if (nomFichier.toLowerCase().endsWith(".gpx")) laPasserelle = new PasserelleGPX();
        if (nomFichier.toLowerCase().endsWith(".pwx")) laPasserelle = new PasserellePWX();
        if (nomFichier.toLowerCase().endsWith(".tcx")) laPasserelle = new PasserelleTCX();

        String msg = laPasserelle.getUneTrace(nomFichier, laTrace);
		
		if ( msg.equals("") )
			System.out.println(laTrace.toString());		// si aucune erreur
		else
			System.out.println(msg);					// si erreur retournée par la passerelle
	}
}
