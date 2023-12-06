using System;
using System.IO;
using System.Xml;				// permet d'utiliser les classes XML
using System.Windows.Forms;

namespace TraceGPS
{
    /**
     * Cette classe fournit les outils permettant de "parser" un fichier GPX pour mettre à jour un objet Trace.
     * @author dP
     *
     */
    public class PasserelleGPX : Passerelle
    {
	    /**
	     * méthode publique pour mettre à jour un objet Trace (vide) à partir n'un fichier GPX
	     * @param nomFichier : le nom du fichier contenant la trace
	     * @param laTraceAcreer : l'objet Trace à mettre à jour
	     * @return : un message d'erreur de traitement (ou un message vide si pas d'erreur)
	     */
	    public override String creerTrace (String nomFichier, Trace laTraceAcreer) {
		    try {
                // création d'un flux en lecture (StreamReader) à partir du fichier
                StreamReader unFluxEnLecture = getFluxEnLecture(nomFichier);

                // création d'un objet XmlReader à partir du flux ; il servira à parcourir le flux XML
                XmlReader leDocument = getDocumentXML(unFluxEnLecture);

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

                // vide la liste actuelle des points de trace
			    laTraceAcreer.viderListePoints();

                // démarrer le parcours au premier noeud de type <trkpt>
				leDocument.ReadToFollowing("trkpt");
				do
			    {	
                    // lecture de l'attribut "lat"
                    String valeurAttribut = leDocument.GetAttribute("lat").Replace(".", ",");
				    double latitude = Convert.ToDouble(valeurAttribut);
				
				    // lecture de l'attribut "lon"
                    valeurAttribut = leDocument.GetAttribute("lon").Replace(".", ",");
                    double longitude = Convert.ToDouble(valeurAttribut);
				
                    // lecture de la balise <ele>
                    leDocument.ReadToFollowing("ele");
                    leDocument.Read();
                    double altitude = Convert.ToDouble(leDocument.Value.Replace(".", ","));

                    // lecture de la balise <time>
                    leDocument.ReadToFollowing("time");
                    leDocument.Read();
                    String valeurNoeud = leDocument.Value;
                    // passage du format "yyyy-MM-ddThh:mm:ssZ" au format "dd/MM/yyyy hh:mm:ss"
                    String annee = valeurNoeud.Substring(0, 4);
                    String mois = valeurNoeud.Substring(5, 2);
                    String jour = valeurNoeud.Substring(8, 2);
                    String horaire = valeurNoeud.Substring(11, 8);
                    String chaineDateHeure = jour + "/" + mois + "/" + annee + " " + horaire;
                    DateTime dateHeure = Convert.ToDateTime(chaineDateHeure);

                    // recherche du rythme cardiaque
                    // avance jusqu'à la prochaine balise <gpxtpx:hr> (si elle est présente dans le schéma), 
                    // ou jusqu'à la prochaine balise <trkpt> (si elle n'est pas présente dans le schéma)
                    while (leDocument.Name != "gpxtpx:hr" && leDocument.Name != "trkpt") leDocument.Read();

                    // le rythme cardiaque est mis à 0 si la balise <gpxtpx:hr> n'est pas présente dans le schéma
                    int rythmeCardio = 0;
                    if (leDocument.Name == "gpxtpx:hr")
                    {
                        leDocument.Read();
                        rythmeCardio = Convert.ToInt32(leDocument.Value);
                    }

                    // création d'un point de trace
                    PointDeTrace unNouveauPoint = new PointDeTrace(latitude, longitude, altitude, dateHeure, rythmeCardio);

				    // ajoute le point à l'objet laTraceAcreer
				    laTraceAcreer.ajouterPoint(unNouveauPoint);

                } while (leDocument.ReadToFollowing("trkpt"));	// continue au noeud suivant de type <trkpt>

                // ferme le flux  en lecture
                unFluxEnLecture.Close();
			
			    return "";						    // il n'y a pas de problème
		    }
		    catch (Exception ex)
		    {	return "Erreur : " + ex.Message;	// il y a un problème
		    }
	    }

    } // fin de la classe
} // fin du namespace
