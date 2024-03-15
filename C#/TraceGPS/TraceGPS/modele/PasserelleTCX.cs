﻿using System;
using System.IO;
using System.Xml;				// permet d'utiliser les classes XML
using System.Windows.Forms;

namespace TraceGPS
{
	/**
	 * Cette classe fournit les outils permettant de "parser" un fichier TCX pour mettre à jour un objet Trace.
	 * @author dP
	 *
	 */
	public class PasserelleTCX : Passerelle
	{
		/**
		 * méthode publique pour mettre à jour un objet Trace (vide) à partir n'un fichier TCX
		 * @param nomFichier : le nom du fichier contenant la trace
		 * @param laTraceAcreer : l'objet Trace à mettre à jour
		 * @return : un message d'erreur de traitement (ou un message vide si pas d'erreur)
		 */
		public override String creerTrace(String nomFichier, Trace laTraceAcreer)
		{
			try
			{
                // création d'un flux en lecture (StreamReader) à partir du fichier
                StreamReader unFluxEnLecture = getFluxEnLecture(nomFichier);

                // création d'un objet XmlReader à partir du flux ; il servira à parcourir le flux XML
                XmlReader leDocument = getDocumentXML(unFluxEnLecture);

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

				// vide la liste actuelle des points de trace
				laTraceAcreer.viderListePoints();

				// création d'un flux en écriture (StreamWriter) pour enregistrer tous les points
				//StreamWriter unFluxEnSortie = File.CreateText("listePoints.txt");

				// démarrer le parcours au premier noeud de type <Trackpoint>
				leDocument.ReadToFollowing("Trackpoint");
				do
				{
					// lecture de la balise <Time>
					leDocument.ReadToFollowing("Time");
					leDocument.Read();
					String valeurNoeud = leDocument.Value;
					// passage du format "yyyy-MM-ddThh:mm:ssZ" au format "dd/MM/yyyy hh:mm:ss"
					String annee = valeurNoeud.Substring(0, 4);
					String mois = valeurNoeud.Substring(5, 2);
					String jour = valeurNoeud.Substring(8, 2);
					String horaire = valeurNoeud.Substring(11, 8);
					String chaineDateHeure = jour + "/" + mois + "/" + annee + " " + horaire;
					DateTime dateHeure = Convert.ToDateTime(chaineDateHeure);

					// lecture de la balise <LatitudeDegrees>
					leDocument.ReadToFollowing("LatitudeDegrees");
					leDocument.Read();
					double latitude = Convert.ToDouble(leDocument.Value.Replace(".", ","));

					// lecture de la balise <LongitudeDegrees>
					leDocument.ReadToFollowing("LongitudeDegrees");
					leDocument.Read();
					double longitude = Convert.ToDouble(leDocument.Value.Replace(".", ","));

					// lecture de la balise <AltitudeMeters>
					leDocument.ReadToFollowing("AltitudeMeters");
					leDocument.Read();
					double altitude = Convert.ToDouble(leDocument.Value.Replace(".", ","));

                    // lecture des balises <HeartRateBpm> et <Value>
                    leDocument.ReadToFollowing("HeartRateBpm");
                    leDocument.ReadToFollowing("Value");
                    leDocument.Read();
                    int rythmeCardio = Convert.ToInt32(leDocument.Value);

					// création d'un point de trace
                    PointDeTrace unNouveauPoint = new PointDeTrace(latitude, longitude, altitude, dateHeure, rythmeCardio);

					// ajoute le point à l'objet laTraceAcreer
					laTraceAcreer.ajouterPoint(unNouveauPoint);

				} while (leDocument.ReadToFollowing("Trackpoint"));	// continue au noeud suivant de type <Trackpoint>

                // ferme le flux  en lecture
                unFluxEnLecture.Close();

				return "";						    // il n'y a pas de problème
			}
			catch (Exception ex)
			{
				return "Erreur : " + ex.Message;	// il y a un problème
			}
		}

    } // fin de la classe
} // fin du namespace
