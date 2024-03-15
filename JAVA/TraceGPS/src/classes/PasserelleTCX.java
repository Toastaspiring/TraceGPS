// Projet TraceGPS - API Java
// Fichier : PasserelleTCX.java
// Cette classe fournit les outils permettant de "parser" un fichier TCX pour mettre à jour un objet Trace.<br>
// Dernière mise à jour : 26/3/2018 par Jim

package classes;

import java.io.InputStream;
import java.util.Date;

import org.w3c.dom.Document;
import org.w3c.dom.Element;
import org.w3c.dom.NodeList;

public class PasserelleTCX extends PasserelleFichierXML {

	// méthode pour mettre à jour un objet Trace (vide) à partir n'un fichier TCX
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
	
			/* Exemple de données obtenues pour un point de trace :
						<Trackpoint>
							<Time>2016-12-03T09:21:15Z</Time>
							<Position>
								<LatitudeDegrees>48.150052</LatitudeDegrees>
								<LongitudeDegrees>-1.680224</LongitudeDegrees>
							</Position>
							<AltitudeMeters>31.6</AltitudeMeters>
							<DistanceMeters>0.0</DistanceMeters>
							<HeartRateBpm>
								<Value>112</Value>
							</HeartRateBpm>
							<Extensions>
								<x:TPX>
									<Speed>0.0</Speed>
								</x:TPX>
							</Extensions>
						</Trackpoint>
			 */
		
			// création d'une liste contenant tous les noeuds <Trackpoint> de l'élément racine
			NodeList lesNoeuds = leDocument.getElementsByTagName("Trackpoint");

			// vide la liste actuelle des points de trace
			laTraceAmaj.viderListePoints();
			
			// mémoriser l'id de la trace
			int idTrace = laTraceAmaj.getId();
			// initialiser l'id des points
			int idPoint = 0;

			// parcours de la liste des noeuds <Trackpoint>
			for (int i = 0 ; i <= lesNoeuds.getLength()-1 ; i++)
			{	// création de l'élément courant à chaque tour de boucle
				Element unNoeud = (Element) lesNoeuds.item(i);
				
				// on vérifie que le noeud possède toutes les balises
				if (unNoeud.getElementsByTagName("Time").getLength() > 0 &&
				unNoeud.getElementsByTagName("LatitudeDegrees").getLength() > 0 &&
				unNoeud.getElementsByTagName("LongitudeDegrees").getLength() > 0 &&
				unNoeud.getElementsByTagName("AltitudeMeters").getLength() > 0 &&
				unNoeud.getElementsByTagName("HeartRateBpm").getLength() > 0 ) 
				{
					// lecture de la balise <time>
					String valeurNoeud = unNoeud.getElementsByTagName("Time").item(0).getTextContent();
					// passage du format "yyyy-MM-ddThh:mm:ssZ" au format "dd/MM/yyyy hh:mm:ss"
					String annee = valeurNoeud.substring(0, 4);
					String mois = valeurNoeud.substring(5, 7);
					String jour = valeurNoeud.substring(8, 10);
					String horaire = valeurNoeud.substring(11, 19);
					String chaineDateHeure = jour + "/" + mois + "/" + annee + " " + horaire;
					Date dateHeure = Outils.convertirEnDateHeure(chaineDateHeure);
					
					// lecture de la balise <LatitudeDegrees>
					valeurNoeud = unNoeud.getElementsByTagName("LatitudeDegrees").item(0).getTextContent();
					double latitude = Double.parseDouble(valeurNoeud);
					
					// lecture de la balise <LongitudeDegrees>
					valeurNoeud = unNoeud.getElementsByTagName("LongitudeDegrees").item(0).getTextContent();
					double longitude = Double.parseDouble(valeurNoeud);
					
					// lecture de la balise <AltitudeMeters>
					valeurNoeud = unNoeud.getElementsByTagName("AltitudeMeters").item(0).getTextContent();
					double altitude = Double.parseDouble(valeurNoeud);
					
					// lecture de la balise <HeartRateBpm>
					unNoeud = (Element) unNoeud.getElementsByTagName("HeartRateBpm").item(0);
					// lecture de la balise <Value>
					valeurNoeud = unNoeud.getElementsByTagName("Value").item(0).getTextContent();
					int rythmeCardio = Integer.parseInt(valeurNoeud);
					
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
