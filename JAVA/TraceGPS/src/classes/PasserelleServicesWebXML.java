// Projet TraceGPS - API Java
// Fichier : PasserelleServicesWeb.java
// Cette classe hérite de la classe Passerelle
// Elle fournit des méthodes pour appeler les différents services web
// Elle utilise le modèle Jaxp pour parcourir le document XML
// Le modèle Jaxp fait partie du JDK (et également du SDK Android)
// Dernière mise à jour : 16/04/2021 par Jim

package classes;

import java.util.ArrayList;
import java.util.Date;

import java.io.InputStream;

import org.w3c.dom.Document;
import org.w3c.dom.Element;
import org.w3c.dom.NodeList;

public class PasserelleServicesWebXML extends PasserelleXML {

	// attributs privés
	private static String formatDateUS = "yyyy-MM-dd HH:mm:ss";

	// Adresse de l'hébergeur Internet
	// private static String _adresseHebergeur =
	// "http://sio.lyceedelasalle.fr/tracegps/api/";
	// Adresse du localhost en cas d'exécution sur le poste de développement (projet
	// de tests des classes)

	// J'ai remplacer par celui du lycee pour etre sur que c'est bon le vrais est :
	// "http://sio.lyceedelasalle.fr/tracegps/api/"
	private static String _adresseHebergeur = "http://sio.lyceedelasalle.fr/tracegps/api/";

	// Noms des services web déjà traités par la passerelle
	private static String _urlArreterEnregistrementParcours = "ArreterEnregistrementParcours";
	private static String _urlChangerDeMdp = "ChangerDeMdp";
	private static String _urlConnecter = "Connecter";
	private static String _urlCreerUnUtilisateur = "CreerUnUtilisateur";
	private static String _urlDemanderMdp = "DemanderMdp";
	private static String _urlDemanderUneAutorisation = "DemanderUneAutorisation";
	private static String _urlDemarrerEnregistrementParcours = "DemarrerEnregistrementParcours";
	private static String _urlEnvoyerPosition = "EnvoyerPosition";
	private static String _urlGetLesParcoursDunUtilisateur = "GetLesParcoursDunUtilisateur";
	private static String _urlGetLesUtilisateursQueJautorise = "GetLesUtilisateursQueJautorise";
	private static String _urlGetLesUtilisateursQuiMautorisent = "GetLesUtilisateursQuiMautorisent";
	private static String _urlGetTousLesUtilisateurs = "GetTousLesUtilisateurs";
	private static String _urlGetUnParcoursEtSesPoints = "GetUnParcoursEtSesPoints";
	private static String _urlRetirerUneAutorisation = "RetirerUneAutorisation";
	private static String _urlSupprimerUnUtilisateur = "SupprimerUnUtilisateur";
	private static String _urlSupprimerUnParcours = "SupprimerUnParcours";

	// -------------------------------------------------------------------------------------------------
	// ------------------------------------- méthodes déjà développées
	// -------------------------------------------------------------------------------------------------

	// Méthode statique pour se connecter (service Connecter)
	// La méthode doit recevoir 2 paramètres :
	// pseudo : le pseudo de l'utilisateur qui fait appel au service web
	// mdpSha1 : le mot de passe hashé en sha1
	public static String connecter(String pseudo, String mdpSha1) {
		String reponse = "";
		try { // création d'un nouveau document XML à partir de l'URL du service web et des
				// paramètres
			String urlDuServiceWeb = _adresseHebergeur + _urlConnecter;
			urlDuServiceWeb += "?pseudo=" + pseudo;
			urlDuServiceWeb += "&mdp=" + mdpSha1;

			// création d'un flux en lecture (InputStream) à partir du service
			InputStream unFluxEnLecture = getFluxEnLecture(urlDuServiceWeb);

			// création d'un objet org.w3c.dom.Document à partir du flux ; il servira à
			// parcourir le flux XML
			Document leDocument = getDocumentXML(unFluxEnLecture);

			// parsing du flux XML
			Element racine = (Element) leDocument.getElementsByTagName("data").item(0);
			reponse = racine.getElementsByTagName("reponse").item(0).getTextContent();

			// retour de la réponse du service web
			return reponse;
		} catch (Exception ex) {
			String msg = "Erreur : " + ex.getMessage();
			return msg;
		}
	}

	// Méthode statique pour obtenir la liste de tous les utilisateurs de niveau 1
	// (service GetTousLesUtilisateurs)
	// La méthode doit recevoir 3 paramètres :
	// pseudo : le pseudo de l'utilisateur qui fait appel au service web
	// mdpSha1 : le mot de passe hashé en sha1
	// lesUtilisateurs : collection (vide) à remplir à partir des données fournies
	// par le service web
	public static String getTousLesUtilisateurs(String pseudo, String mdpSha1, ArrayList<Utilisateur> lesUtilisateurs) {
		String reponse = "";
		try { // création d'un nouveau document XML à partir de l'URL du service web et des
				// paramètres
			String urlDuServiceWeb = _adresseHebergeur + _urlGetTousLesUtilisateurs;
			urlDuServiceWeb += "?pseudo=" + pseudo;
			urlDuServiceWeb += "&mdp=" + mdpSha1;

			// création d'un flux en lecture (InputStream) à partir du service
			InputStream unFluxEnLecture = getFluxEnLecture(urlDuServiceWeb);

			// création d'un objet org.w3c.dom.Document à partir du flux ; il servira à
			// parcourir le flux XML
			Document leDocument = getDocumentXML(unFluxEnLecture);

			// parsing du flux XML
			Element racine = (Element) leDocument.getElementsByTagName("data").item(0);
			reponse = racine.getElementsByTagName("reponse").item(0).getTextContent();

			NodeList listeNoeudsUtilisateurs = leDocument.getElementsByTagName("utilisateur");
			/*
			 * Exemple de données obtenues pour un utilisateur :
			 * <utilisateur>
			 * <id>2</id>
			 * <pseudo>callisto</pseudo>
			 * <adrMail>delasalle.sio.eleves@gmail.com</adrMail>
			 * <numTel>22.33.44.55.66</numTel>
			 * <niveau>1</niveau>
			 * <dateCreation>2018-01-19 20:11:24</dateCreation>
			 * <nbTraces>2</nbTraces>
			 * <dateDerniereTrace>2018-01-19 13:08:48</dateDerniereTrace>
			 * </utilisateur>
			 */

			// vider d'abord la collection avant de la remplir
			lesUtilisateurs.clear();

			// parcours de la liste des noeuds <utilisateur> et ajout dans la collection
			// lesUtilisateurs
			for (int i = 0; i <= listeNoeudsUtilisateurs.getLength() - 1; i++) { // création de l'élément courant à
																					// chaque tour de boucle
				Element courant = (Element) listeNoeudsUtilisateurs.item(i);

				// lecture des balises intérieures
				int unId = Integer.parseInt(courant.getElementsByTagName("id").item(0).getTextContent());
				String unPseudo = courant.getElementsByTagName("pseudo").item(0).getTextContent();
				String unMdpSha1 = ""; // par sécurité, on ne récupère pas le mot de passe
				String uneAdrMail = courant.getElementsByTagName("adrMail").item(0).getTextContent();
				String unNumTel = courant.getElementsByTagName("numTel").item(0).getTextContent();
				int unNiveau = Integer.parseInt(courant.getElementsByTagName("niveau").item(0).getTextContent());
				Date uneDateCreation = Outils.convertirEnDate(
						courant.getElementsByTagName("dateCreation").item(0).getTextContent(), formatDateUS);
				int unNbTraces = Integer.parseInt(courant.getElementsByTagName("nbTraces").item(0).getTextContent());
				Date uneDateDerniereTrace = null;
				if (unNbTraces > 0)
					uneDateDerniereTrace = Outils.convertirEnDate(
							courant.getElementsByTagName("dateDerniereTrace").item(0).getTextContent(), formatDateUS);

				// crée un objet Utilisateur
				Utilisateur unUtilisateur = new Utilisateur(unId, unPseudo, unMdpSha1, uneAdrMail, unNumTel, unNiveau,
						uneDateCreation, unNbTraces, uneDateDerniereTrace);

				// ajoute l'utilisateur à la collection lesUtilisateurs
				lesUtilisateurs.add(unUtilisateur);
			}

			// retour de la réponse du service web
			return reponse;
		} catch (Exception ex) {
			String msg = "Erreur : " + ex.getMessage();
			return msg;
		}
	}

	// Méthode statique pour créer un utilisateur (service CreerUnUtilisateur)
	// La méthode doit recevoir 3 paramètres :
	// pseudo : le pseudo de l'utilisateur qui fait appel au service web
	// adrMail : son adresse mail
	// numTel : son numéro de téléphone
	public static String creerUnUtilisateur(String pseudo, String adrMail, String numTel) {
		String reponse = "";
		try { // création d'un nouveau document XML à partir de l'URL du service web et des
				// paramètres
			String urlDuServiceWeb = _adresseHebergeur + _urlCreerUnUtilisateur;
			urlDuServiceWeb += "?pseudo=" + pseudo;
			urlDuServiceWeb += "&adrMail=" + adrMail;
			urlDuServiceWeb += "&numTel=" + numTel;

			// création d'un flux en lecture (InputStream) à partir du service
			InputStream unFluxEnLecture = getFluxEnLecture(urlDuServiceWeb);

			// création d'un objet org.w3c.dom.Document à partir du flux ; il servira à
			// parcourir le flux XML
			Document leDocument = getDocumentXML(unFluxEnLecture);

			// parsing du flux XML
			Element racine = (Element) leDocument.getElementsByTagName("data").item(0);
			reponse = racine.getElementsByTagName("reponse").item(0).getTextContent();

			// retour de la réponse du service web
			return reponse;
		} catch (Exception ex) {
			String msg = "Erreur : " + ex.getMessage();
			return msg;
		}
	}

	// Méthode statique pour supprimer un utilisateur (service
	// SupprimerUnUtilisateur)
	// Ce service permet à un administrateur de supprimer un utilisateur (à
	// condition qu'il ne possède aucune trace enregistrée)
	// La méthode doit recevoir 3 paramètres :
	// pseudo : le pseudo de l'administrateur qui fait appel au service web
	// mdpSha1 : le mot de passe hashé en sha1
	// pseudoAsupprimer : le pseudo de l'utilisateur à supprimer
	public static String supprimerUnUtilisateur(String pseudo, String mdpSha1, String pseudoAsupprimer) {
		String reponse = "";
		try { // création d'un nouveau document XML à partir de l'URL du service web et des
				// paramètres
			String urlDuServiceWeb = _adresseHebergeur + _urlSupprimerUnUtilisateur;
			urlDuServiceWeb += "?pseudo=" + pseudo;
			urlDuServiceWeb += "&mdp=" + mdpSha1;
			urlDuServiceWeb += "&pseudoAsupprimer=" + pseudoAsupprimer;

			// création d'un flux en lecture (InputStream) à partir du service
			InputStream unFluxEnLecture = getFluxEnLecture(urlDuServiceWeb);

			// création d'un objet org.w3c.dom.Document à partir du flux ; il servira à
			// parcourir le flux XML
			Document leDocument = getDocumentXML(unFluxEnLecture);

			// parsing du flux XML
			Element racine = (Element) leDocument.getElementsByTagName("data").item(0);
			reponse = racine.getElementsByTagName("reponse").item(0).getTextContent();

			// retour de la réponse du service web
			return reponse;
		} catch (Exception ex) {
			String msg = "Erreur : " + ex.getMessage();
			return msg;
		}
	}

	// Méthode statique pour modifier son mot de passe (service ChangerDeMdp)
	// La méthode doit recevoir 4 paramètres :
	// pseudo : le pseudo de l'utilisateur qui fait appel au service web
	// mdpSha1 : le mot de passe hashé en sha1
	// nouveauMdp : le nouveau mot de passe
	// confirmationMdp : la confirmation du nouveau mot de passe
	public static String changerDeMdp(String pseudo, String mdpSha1, String nouveauMdp, String confirmationMdp) {
		String reponse = "";
		try { // création d'un nouveau document XML à partir de l'URL du service web et des
				// paramètres
			String urlDuServiceWeb = _adresseHebergeur + _urlChangerDeMdp;
			urlDuServiceWeb += "?pseudo=" + pseudo;
			urlDuServiceWeb += "&mdp=" + mdpSha1;
			urlDuServiceWeb += "&nouveauMdp=" + nouveauMdp;
			urlDuServiceWeb += "&confirmationMdp=" + confirmationMdp;

			// création d'un flux en lecture (InputStream) à partir du service
			InputStream unFluxEnLecture = getFluxEnLecture(urlDuServiceWeb);

			// création d'un objet org.w3c.dom.Document à partir du flux ; il servira à
			// parcourir le flux XML
			Document leDocument = getDocumentXML(unFluxEnLecture);

			// parsing du flux XML
			Element racine = (Element) leDocument.getElementsByTagName("data").item(0);
			reponse = racine.getElementsByTagName("reponse").item(0).getTextContent();

			// retour de la réponse du service web
			return reponse;
		} catch (Exception ex) {
			String msg = "Erreur : " + ex.getMessage();
			return msg;
		}
	}

	// -------------------------------------------------------------------------------------------------
	// --------------------------------- méthodes restant à développer
	// ---------------------------------
	// -------------------------------------------------------------------------------------------------

	// Méthode statique pour demander un nouveau mot de passe (service DemanderMdp)
	// La méthode doit recevoir 1 paramètre :
	// pseudo : le pseudo de l'utilisateur
	public static String demanderMdp(String pseudo) {
		String reponse = "";
		try {
			// création d'un nouveau document XML à partir de l'URL du service web et des
			// paramètres
			String urlDuServiceWeb = _adresseHebergeur + _urlDemanderMdp;
			urlDuServiceWeb += "?pseudo=" + pseudo;

			// création d'un flux en lecture (InputStream) à partir du service
			InputStream unFluxEnLecture = getFluxEnLecture(urlDuServiceWeb);

			// création d'un objet org.w3c.dom.Document à partir du flux ; il servira à
			// parcourir le flux XML
			Document leDocument = getDocumentXML(unFluxEnLecture);

			// parsing du flux XML
			Element racine = (Element) leDocument.getElementsByTagName("data").item(0);
			reponse = racine.getElementsByTagName("reponse").item(0).getTextContent();

			// retour de la réponse du service web
			return reponse;
		} catch (Exception ex) {
			String msg = "Erreur : " + ex.getMessage();
			return msg;
		}
	}

	// Méthode statique pour obtenir la liste des utilisateurs que j'autorise
	// (service GetLesUtilisateursQueJautorise)
	// La méthode doit recevoir 3 paramètres :
	// pseudo : le pseudo de l'utilisateur qui fait appel au service web
	// mdpSha1 : le mot de passe hashé en sha1
	// lesUtilisateurs : collection (vide) à remplir à partir des données fournies
	// par le service web
	public static String getLesUtilisateursQueJautorise(String pseudo, String mdpSha1,
			ArrayList<Utilisateur> lesUtilisateurs) {
		String reponse = "";
		try { // création d'un nouveau document XML à partir de l'URL du service web et des
				// paramètres
			String urlDuServiceWeb = _adresseHebergeur + _urlSupprimerUnUtilisateur;
			urlDuServiceWeb += "?pseudo=" + pseudo;
			urlDuServiceWeb += "&mdp=" + mdpSha1;
			// urlDuServiceWeb += "&pseudoAsupprimer=" + pseudoAsupprimer;

			// création d'un flux en lecture (InputStream) à partir du service
			InputStream unFluxEnLecture = getFluxEnLecture(urlDuServiceWeb);

			// création d'un objet org.w3c.dom.Document à partir du flux ; il servira à
			// parcourir le flux XML
			Document leDocument = getDocumentXML(unFluxEnLecture);

			// parsing du flux XML
			Element racine = (Element) leDocument.getElementsByTagName("data").item(0);
			reponse = racine.getElementsByTagName("reponse").item(0).getTextContent();

			// retour de la réponse du service web
			return reponse;
		} catch (Exception ex) {
			String msg = "Erreur : " + ex.getMessage();
			return msg;
		}
	}

	// Méthode statique pour obtenir la liste des utilisateurs qui m'autorisent
	// (service GetLesUtilisateursQuiMautorisent)
	// La méthode doit recevoir 3 paramètres :
	// pseudo : le pseudo de l'utilisateur qui fait appel au service web
	// mdpSha1 : le mot de passe hashé en sha1
	// lesUtilisateurs : collection (vide) à remplir à partir des données fournies
	// par le service web
	public static String getLesUtilisateursQuiMautorisent(String pseudo, String mdpSha1,
			ArrayList<Utilisateur> lesUtilisateurs) {
		return ""; // METHODE A CREER ET TESTER
	}

	// Méthode statique pour demander une autorisation (service
	// DemanderUneAutorisation)
	// La méthode doit recevoir 5 paramètres :
	// pseudo : le pseudo de l'utilisateur qui fait appel au service web
	// mdpSha1 : le mot de passe hashé en sha1
	// pseudoDestinataire : le pseudo de l'utilisateur à qui on demande
	// l'autorisation
	// texteMessage : le texte d'un message accompagnant la demande
	// nomPrenom : le nom et le prénom du demandeur
	public static String demanderUneAutorisation(String pseudo, String mdpSha1, String pseudoDestinataire,
			String texteMessage, String nomPrenom) {
		return ""; // METHODE A CREER ET TESTER
	}

	// Méthode statique pour retirer une autorisation (service
	// RetirerUneAutorisation)
	// La méthode doit recevoir 4 paramètres :
	// pseudo : le pseudo de l'utilisateur qui fait appel au service web
	// mdpSha1 : le mot de passe hashé en sha1
	// pseudoARetirer : le pseudo de l'utilisateur à qui on veut retirer
	// l'autorisation
	// texteMessage : le texte d'un message pour un éventuel envoi de courriel
	public static String retirerUneAutorisation(String pseudo, String mdpSha1, String pseudoARetirer,
			String texteMessage) {
		return ""; // METHODE A CREER ET TESTER
	}

	// Méhode statique pour envoyer la position de l'utilisateur (service
	// EnvoyerPosition)
	// La méthode doit recevoir 3 paramètres :
	// pseudo : le pseudo de l'utilisateur qui fait appel au service web
	// mdpSha1 : le mot de passe hashé en sha1
	// lePoint : un objet PointDeTrace (vide) qui permettra de récupérer le numéro
	// attribué à partir des données fournies par le service web
	public static String envoyerPosition(String pseudo, String mdpSha1, PointDeTrace lePoint) {
		return ""; // METHODE A CREER ET TESTER
	}

	// Méthode statique pour obtenir un parcours et la liste de ses points (service
	// GetUnParcoursEtSesPoints)
	// La méthode doit recevoir 4 paramètres :
	// pseudo : le pseudo de l'utilisateur qui fait appel au service web
	// mdpSha1 : le mot de passe hashé en sha1
	// idTrace : l'id de la trace à consulter
	// laTrace : objet Trace (vide) à remplir à partir des données fournies par le
	// service web
	public static String getUnParcoursEtSesPoints(String pseudo, String mdpSha1, int idTrace, Trace laTrace) {
		return ""; // METHODE A CREER ET TESTER
	}

	// Méthode statique pour obtenir la liste des parcours d'un utilisateur (service
	// GetLesParcoursDunUtilisateur)
	// La méthode doit recevoir 4 paramètres :
	// pseudo : le pseudo de l'utilisateur qui fait appel au service web
	// mdpSha1 : le mot de passe hashé en sha1
	// idUtilisateur : l'id de l'utilisateur dont on veut la liste des parcours
	// lesTraces : collection (vide) à remplir à partir des données fournies par le
	// service web
	public static String getLesParcoursDunUtilisateur(String pseudo, String mdpSha1, String pseudoConsulte,
			ArrayList<Trace> lesTraces) {
		return ""; // METHODE A CREER ET TESTER
	}

	// Méthode statique pour supprimer un parcours (service SupprimerUnParcours)
	// La méthode doit recevoir 3 paramètres :
	// pseudo : le pseudo de l'utilisateur qui fait appel au service web
	// mdpSha1 : le mot de passe hashé en sha1
	// idTrace : l'id de la trace à supprimer
	public static String supprimerUnParcours(String pseudo, String mdpSha1, int idTrace) {
		return ""; // METHODE A CREER ET TESTER
	}

	// Méthode statique pour démarrer l'enregistrement d'un parcours (service
	// DemarrerEnregistrementParcours)
	// La méthode doit recevoir 3 paramètres :
	// pseudo : le pseudo de l'utilisateur qui fait appel au service web
	// mdpSha1 : le mot de passe hashé en sha1
	// laTrace : un objet Trace (vide) à remplir à partir des données fournies par
	// le service web
	public static String demarrerEnregistrementParcours(String pseudo, String mdpSha1, Trace laTrace) {
		return ""; // METHODE A CREER ET TESTER
	}

	// Méthode statique pour terminer l'enregistrement d'un parcours (service
	// ArreterEnregistrementParcours)
	// La méthode doit recevoir 3 paramètres :
	// pseudo : le pseudo de l'utilisateur qui fait appel au service web
	// mdpSha1 : le mot de passe hashé en sha1
	// idTrace : l'id de la trace à terminer
	public static String arreterEnregistrementParcours(String pseudo, String mdpSha1, int idTrace) {
		String reponse = "";
		try { // création d'un nouveau document XML à partir de l'URL du service web et des
				// paramètres
			String urlDuServiceWeb = _adresseHebergeur + _urlSupprimerUnUtilisateur;
			urlDuServiceWeb += "?pseudo=" + pseudo;
			urlDuServiceWeb += "&mdp=" + mdpSha1;
			urlDuServiceWeb += "&arreterEnregistrementParcours=" + idTrace;

			// création d'un flux en lecture (InputStream) à partir du service
			InputStream unFluxEnLecture = getFluxEnLecture(urlDuServiceWeb);

			// création d'un objet org.w3c.dom.Document à partir du flux ; il servira à
			// parcourir le flux XML
			Document leDocument = getDocumentXML(unFluxEnLecture);

			// parsing du flux XML
			Element racine = (Element) leDocument.getElementsByTagName("data").item(0);
			reponse = racine.getElementsByTagName("reponse").item(0).getTextContent();

			// retour de la réponse du service web
			return reponse;
		} catch (Exception ex) {
			String msg = "Erreur : " + ex.getMessage();
			return msg;
		}
	}

} // fin de la classe