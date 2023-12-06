// Projet TraceGPS - API Java
// Fichier : PasserelleGPX.java
// Cette classe fournit les outils permettant de "parser" un fichier GPX pour mettre à jour un objet Trace.<br>
// Dernière mise à jour : 26/3/2018 par Jim

package classes;

import java.io.InputStream;
import java.util.Date;

import org.w3c.dom.Document;
import org.w3c.dom.Element;
import org.w3c.dom.NodeList;

public class PasserelleGPX extends PasserelleFichierXML {
	
	// méthode pour mettre à jour un objet Trace (vide) à partir n'un fichier GPX
	// paramètre nomFichier  : le nom du fichier contenant la trace
	// paramètre laTraceAmaj : l'objet Trace à mettre à jour
	// retourne              : un message d'erreur de traitement (ou un message vide si pas d'erreur)
	@Override
	public String getUneTrace (String nomFichier, Trace laTraceAmaj) {
		try {
			// création d'un flux en lecture (InputStream) à partir du fichier
			InputStream unFluxEnLecture = getFluxEnLecture(nomFichier);
			
			// création d'un objet org.w3c.dom.Document à partir du flux ; il servira à parcourir le flux XML
			Document leDocument = getDocumentXML(unFluxEnLecture);

            // ferme le flux  en lecture
            unFluxEnLecture.close();
            
			/* Exemple de données obtenues pour un point de trace :
                    <trkpt lat="48.140161" lon="-1.667260">
                        <ele>83.32</ele>
                        <time>2015-09-13T09:08:00Z</time>
                        <pdop>6.00</pdop>
                        <extensions>
                            <gpxtpx:TrackPointExtension>
                                <gpxtpx:course>63.0</gpxtpx:course>
                            </gpxtpx:TrackPointExtension>
                        </extensions>
                    </trkpt>
                 
                 ou bien :
                 
                    <trkpt lat="48.150052" lon="-1.680224">
                        <ele>31.6</ele>
                        <time>2016-12-03T09:21:15.000Z</time>
                        <extensions>
                        <gpxtpx:TrackPointExtension>
                            <gpxtpx:hr>112</gpxtpx:hr>              // rythme cardiaque
                        </gpxtpx:TrackPointExtension>
                        </extensions>
                    </trkpt>
			 */
		
			// création d'une liste contenant tous les noeuds <trkpt> de l'élément racine
			NodeList lesNoeuds = leDocument.getElementsByTagName("trkpt");

			// vide la liste actuelle des points de trace
			laTraceAmaj.viderListePoints();

			// mémoriser l'id de la trace
			int idTrace = laTraceAmaj.getId();
			// initialiser l'id des points
			int idPoint = 0;
			
			// parcours de la liste des noeuds <trkpt>
			for (int i = 0 ; i <= lesNoeuds.getLength()-1 ; i++)
			{	// création de l'élément courant à chaque tour de boucle
				Element unNoeud = (Element) lesNoeuds.item(i);
				
				// on vérifie que le noeud possède toutes les balises
				if (unNoeud.getElementsByTagName("ele").getLength() > 0 &&
				unNoeud.getElementsByTagName("time").getLength() > 0 &&
				unNoeud.getElementsByTagName("gpxtpx:hr").getLength() > 0 ) 
				{
					// lecture de l'attribut "lat"
					String valeurAttribut = unNoeud.getAttribute("lat");
					double latitude = Double.parseDouble(valeurAttribut);
				
					// lecture de l'attribut "lon"
					valeurAttribut = unNoeud.getAttribute("lon");
					double longitude = Double.parseDouble(valeurAttribut);
					
					// lecture de la balise <ele>
					String valeurNoeud = unNoeud.getElementsByTagName("ele").item(0).getTextContent();
					double altitude = Double.parseDouble(valeurNoeud);
					
					// lecture de la balise <time>
					valeurNoeud = unNoeud.getElementsByTagName("time").item(0).getTextContent();
					// passage du format "yyyy-MM-ddThh:mm:ssZ" au format "dd/MM/yyyy hh:mm:ss"
					String annee = valeurNoeud.substring(0, 4);
					String mois = valeurNoeud.substring(5, 7);
					String jour = valeurNoeud.substring(8, 10);
					String horaire = valeurNoeud.substring(11, 19);
					String chaineDateHeure = jour + "/" + mois + "/" + annee + " " + horaire;
					Date dateHeure = Outils.convertirEnDateHeure(chaineDateHeure);
					
	                // recherche du rythme cardiaque
	                // le rythme cardiaque est mis à 0 si la balise <gpxtpx:hr> n'est pas présente dans le schéma
	                int rythmeCardio = 0;
	                if (unNoeud.getElementsByTagName("gpxtpx:hr").getLength() > 0)
	                {  	// lecture de la balise <gpxtpx:hr>
	    				valeurNoeud = unNoeud.getElementsByTagName("gpxtpx:hr").item(0).getTextContent();
	    				rythmeCardio = Integer.parseInt(valeurNoeud);
	                }				
					
					// création d'un point de trace
	                idPoint++;
					PointDeTrace unNouveauPoint = new PointDeTrace(idTrace, idPoint, latitude, longitude, altitude, dateHeure, rythmeCardio);
	
					// ajoute le point à l'objet laTraceAcreer
					laTraceAmaj.ajouterPoint(unNouveauPoint);
				}
			}
            // ferme le flux  en lecture
            unFluxEnLecture.close();
			
			return "";						// il n'y a pas de problème
		}
		catch (Exception ex)
		{	return "Erreur : " + ex.getMessage();	// il y a un problème
		}
	}

}
