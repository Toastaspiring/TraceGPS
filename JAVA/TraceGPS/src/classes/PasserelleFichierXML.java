// Projet TraceGPS - API Java
// Fichier : PasserelleFichier.java
// Cette classe abstraite hérite de la classe Passerelle
// Elle précise la signature de la méthode pour "parser" un fichier GPS afin de mettre à jour un objet Trace fourni en paramètre.
// Dernière mise à jour : 26/3/2018 par Jim

package classes;

public abstract class PasserelleFichierXML extends PasserelleXML {
	
	// méthode abstraite pour mettre à jour un objet Trace (vide) à partir d'un fichier GPS
	// paramètre nomFichier  : le nom du fichier contenant la trace
	// paramètre laTraceAmaj : l'objet Trace à mettre à jour
	public abstract String getUneTrace(String nomFichier, Trace laTraceAmaj);
}
