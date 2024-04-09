﻿// Projet TraceGPS
// fichier : modele/PasserelleServicesWebXML.cs
// Cette classe hérite de la classe PasserelleXML
// Elle fournit des méthodes pour appeler les différents services web
// Dernière mise à jour : 1/11/2021 par dp

using System;
using System.Net;
using System.IO;
using System.Xml;				// permet d'utiliser les classes XML
using System.Collections;

namespace TraceGPS
{
    public class PasserelleServicesWebXML : PasserelleXML
    {
        // Adresse de l'hébergeur Internet
        // private static String _adresseHebergeur = "http://sio.lyceedelasalle.fr/tracegps/api/";
        // Adresse du localhost en cas d'exécution sur le poste de développement (projet de tests des classes)
        private static String _adresseHebergeur = "http://localhost/developpement/TraceGPS/PHP/TraceGPS/api/";

        // Noms des services web déjà traités par la passerelle
        private static String _urlConnecter = "Connecter";
        private static String _urlGetTousLesUtilisateurs = "GetTousLesUtilisateurs";
        private static String _urlCreerUnUtilisateur = "CreerUnUtilisateur";
        private static String _urlSupprimerUnUtilisateur = "SupprimerUnUtilisateur";
        private static String _urlChangerDeMdp = "ChangerDeMdp";

        // noms des services web pas encore traités par la passerelle (à développer)	
        private static String _urlArreterEnregistrementParcours = "ArreterEnregistrementParcours";
        private static String _urlDemanderMdp = "DemanderMdp";
        private static String _urlDemanderUneAutorisation = "DemanderUneAutorisation";
        private static String _urlDemarrerEnregistrementParcours = "DemarrerEnregistrementParcours";
        private static String _urlEnvoyerPosition = "EnvoyerPosition";
        private static String _urlGetLesParcoursDunUtilisateur = "GetLesParcoursDunUtilisateur";
        private static String _urlGetLesUtilisateursQueJautorise = "GetLesUtilisateursQueJautorise";
        private static String _urlGetLesUtilisateursQuiMautorisent = "GetLesUtilisateursQuiMautorisent";
        private static String _urlGetUnParcoursEtSesPoints = "GetUnParcoursEtSesPoints";
        private static String _urlRetirerUneAutorisation = "RetirerUneAutorisation";
        private static String _urlSupprimerUnParcours = "SupprimerUnParcours";

        // -----------------------------------------------------------------------------------------------
        // ------------------------------------ méthodes déjà développées --------------------------------
        // -----------------------------------------------------------------------------------------------

        // Méthode statique pour se connecter (service Connecter)
        // La méthode doit recevoir 2 paramètres :
        //    pseudo : le pseudo de l'utilisateur qui fait appel au service web
        //    mdp : le mot de passe hashé en sha1
        public static String connecter(String pseudo, String mdpSha1)
        {
            String reponse = "";
            try
            {	// création d'un nouveau document XML à partir de l'URL du service web et des paramètres
                String urlDuServiceWeb = _adresseHebergeur + _urlConnecter;
                urlDuServiceWeb += "?pseudo=" + pseudo;
                urlDuServiceWeb += "&mdp=" + mdpSha1;

                // création d'un flux en lecture (StreamReader) à partir du service
                StreamReader unFluxEnLecture = getFluxEnLecture(urlDuServiceWeb);

                // création d'un objet XmlReader à partir du flux ; il servira à parcourir le flux XML
                XmlReader leDocument = getDocumentXML(unFluxEnLecture);

                // parsing du flux XML
                leDocument.ReadToFollowing("reponse"); leDocument.Read();
                reponse = leDocument.Value;

                // retour de la réponse du service web
                return reponse;
            }
            catch (Exception ex)
            {
                String msg = "Erreur : " + ex.Message;
                return msg;
            }
        }

        // Méthode statique pour obtenir la liste de tous les utilisateurs de niveau 1 (service GetTousLesUtilisateurs)
        // La méthode doit recevoir 3 paramètres :
        //    pseudo : le pseudo de l'utilisateur qui fait appel au service web
        //    mdp : le mot de passe hashé en sha1
        //    lesUtilisateurs : collection (vide) à remplir à partir des données fournies par le service web
        public static String getTousLesUtilisateurs(String pseudo, String mdpSha1, ArrayList lesUtilisateurs)
        {
            String reponse = "";
            try
            {	// création d'un nouveau document XML à partir de l'URL du service web et des paramètres
                String urlDuServiceWeb = _adresseHebergeur + _urlGetTousLesUtilisateurs;
                urlDuServiceWeb += "?pseudo=" + pseudo;
                urlDuServiceWeb += "&mdp=" + mdpSha1;

                // création d'un flux en lecture (StreamReader) à partir du service
                StreamReader unFluxEnLecture = getFluxEnLecture(urlDuServiceWeb);

                // création d'un objet XmlReader à partir du flux ; il servira à parcourir le flux XML
                XmlReader leDocument = getDocumentXML(unFluxEnLecture);

                // parsing du flux XML
                leDocument.ReadToFollowing("reponse");
                leDocument.Read();
                reponse = leDocument.Value;

                /* Exemple de données obtenues pour un utilisateur :
                    <utilisateur>
                        <id>2</id>
                        <pseudo>callisto</pseudo>
                        <adrMail>delasalle.sio.eleves@gmail.com</adrMail>
                        <numTel>22.33.44.55.66</numTel>
                        <niveau>1</niveau>
                        <dateCreation>2018-01-19 20:11:24</dateCreation>
                        <nbTraces>2</nbTraces>
                        <dateDerniereTrace>2018-01-19 13:08:48</dateDerniereTrace>
                    </utilisateur>
                 */

                // vider d'abord la collection avant de la remplir
                lesUtilisateurs.Clear();

                // parcours de la liste des noeuds <utilisateur> et ajout dans la collection lesUtilisateurs
                while (leDocument.ReadToFollowing("utilisateur"))
                {
                    // parcours des balises intérieures d'un utilisateur
                    leDocument.ReadToFollowing("id"); leDocument.Read();
                    int unId = Convert.ToInt32(leDocument.Value);

                    leDocument.ReadToFollowing("pseudo"); leDocument.Read();
                    String unPseudo = leDocument.Value;

                    String unMdpSha1 = "";		// par sécurité, on ne récupère pas le mot de passe

                    leDocument.ReadToFollowing("adrMail"); leDocument.Read();
                    String uneAdrMail = leDocument.Value;

                    leDocument.ReadToFollowing("numTel"); leDocument.Read();
                    String unNumTel = leDocument.Value.ToString().Trim().Replace("\n", "");

                    leDocument.ReadToFollowing("niveau"); leDocument.Read();
                    int unNiveau = Convert.ToInt32(leDocument.Value);

                    leDocument.ReadToFollowing("dateCreation"); leDocument.Read();
                    DateTime uneDateCreation = Convert.ToDateTime(leDocument.Value.ToString());

                    leDocument.ReadToFollowing("nbTraces"); leDocument.Read();
                    int unNbTraces = Convert.ToInt32(leDocument.Value);

                    DateTime uneDateDerniereTrace = DateTime.MinValue;
                    if (unNbTraces > 0)
                    {   leDocument.ReadToFollowing("dateDerniereTrace"); leDocument.Read();
                        uneDateDerniereTrace = Convert.ToDateTime(leDocument.Value.ToString());
                    }

                    // crée un objet Utilisateur
                    Utilisateur unUtilisateur = new Utilisateur(unId, unPseudo, unMdpSha1, uneAdrMail, unNumTel, unNiveau, uneDateCreation, unNbTraces, uneDateDerniereTrace);

                    // ajoute l'utilisateur à la collection lesUtilisateurs
                    lesUtilisateurs.Add(unUtilisateur);
                } // continue au noeud suivant de type <utilisateur>

                // retour de la réponse du service web
                return reponse;
            }
            catch (Exception ex)
            {
                String msg = "Erreur : " + ex.Message;
                return msg;
            }
        }

        // Méthode statique pour créer un utilisateur (service CreerUnUtilisateur)
        // La méthode doit recevoir 3 paramètres :
        //   pseudo : le pseudo de l'utilisateur qui fait appel au service web
        //   adrMail : son adresse mail
        //   numTel : son numéro de téléphone
        public static String creerUnUtilisateur(String pseudo, String adrMail, String numTel)
        {
            String reponse = "";
            try
            {	// création d'un nouveau document XML à partir de l'URL du service web et des paramètres
                String urlDuServiceWeb = _adresseHebergeur + _urlCreerUnUtilisateur;
                urlDuServiceWeb += "?pseudo=" + pseudo;
                urlDuServiceWeb += "&adrMail=" + adrMail;
                urlDuServiceWeb += "&numTel=" + numTel;

                // création d'un flux en lecture (StreamReader) à partir du service
                StreamReader unFluxEnLecture = getFluxEnLecture(urlDuServiceWeb);

                // création d'un objet XmlReader à partir du flux ; il servira à parcourir le flux XML
                XmlReader leDocument = getDocumentXML(unFluxEnLecture);

                // parsing du flux XML
                leDocument.ReadToFollowing("reponse"); leDocument.Read();
                reponse = leDocument.Value;

                // retour de la réponse du service web
                return reponse;
            }
            catch (Exception ex)
            {
                String msg = "Erreur : " + ex.Message;
                return msg;
            }
        }

        // Méthode statique pour supprimer un utilisateur (service SupprimerUnUtilisateur)
        // Ce service permet à un administrateur de supprimer un utilisateur (à condition qu'il ne possède aucune trace enregistrée)
        // La méthode doit recevoir 3 paramètres :
        //   pseudo : le pseudo de l'administrateur qui fait appel au service web
        //   mdp : le mot de passe hashé en sha1
        //   pseudoAsupprimer : le pseudo de l'utilisateur à supprimer
        public static String supprimerUnUtilisateur(String pseudo, String mdpSha1, String pseudoAsupprimer)
        {
            String reponse = "";
            try
            {	// création d'un nouveau document XML à partir de l'URL du service web et des paramètres
                String urlDuServiceWeb = _adresseHebergeur + _urlSupprimerUnUtilisateur;
                urlDuServiceWeb += "?pseudo=" + pseudo;
                urlDuServiceWeb += "&mdp=" + mdpSha1;
                urlDuServiceWeb += "&pseudoAsupprimer=" + pseudoAsupprimer;

                // création d'un flux en lecture (StreamReader) à partir du service
                StreamReader unFluxEnLecture = getFluxEnLecture(urlDuServiceWeb);

                // création d'un objet XmlReader à partir du flux ; il servira à parcourir le flux XML
                XmlReader leDocument = getDocumentXML(unFluxEnLecture);

                // parsing du flux XML
                leDocument.ReadToFollowing("reponse"); leDocument.Read();
                reponse = leDocument.Value;

                // retour de la réponse du service web
                return reponse;
            }
            catch (Exception ex)
            {
                String msg = "Erreur : " + ex.Message;
                return msg;
            }
        }

        // Méthode statique pour modifier son mot de passe (service ChangerDeMdp)
        // La méthode doit recevoir 4 paramètres :
        //    pseudo : le pseudo de l'utilisateur qui fait appel au service web
        //    mdp : le mot de passe hashé en sha1
        //    nouveauMdp : le nouveau mot de passe
        //    confirmationMdp : la confirmation du nouveau mot de passe
        public static String changerDeMdp(String pseudo, String mdpSha1, String nouveauMdp, String confirmationMdp)
        {
            String reponse = "";
            try
            {	// création d'un nouveau document XML à partir de l'URL du service web et des paramètres
                String urlDuServiceWeb = _adresseHebergeur + _urlChangerDeMdp;
                urlDuServiceWeb += "?pseudo=" + pseudo;
                urlDuServiceWeb += "&mdp=" + mdpSha1;
                urlDuServiceWeb += "&nouveauMdp=" + nouveauMdp;
                urlDuServiceWeb += "&confirmationMdp=" + confirmationMdp;

                // création d'un flux en lecture (StreamReader) à partir du service
                StreamReader unFluxEnLecture = getFluxEnLecture(urlDuServiceWeb);

                // création d'un objet XmlReader à partir du flux ; il servira à parcourir le flux XML
                XmlReader leDocument = getDocumentXML(unFluxEnLecture);

                // parsing du flux XML
                leDocument.ReadToFollowing("reponse"); leDocument.Read();
                reponse = leDocument.Value;

                // retour de la réponse du service web
                return reponse;
            }
            catch (Exception ex)
            {
                String msg = "Erreur : " + ex.Message;
                return msg;
            }
        }



        // -----------------------------------------------------------------------------------------------
        // ---------------------------------- méthodes restant à développer ------------------------------
        // -----------------------------------------------------------------------------------------------

        // Méthode statique pour obtenir la liste des utilisateurs que j'autorise (service GetLesUtilisateursQueJautorise)
        // La méthode doit recevoir 3 paramètres :
        //    pseudo : le pseudo de l'utilisateur qui fait appel au service web
        //    mdp : le mot de passe hashé en sha1
        //    lesUtilisateurs : collection (vide) à remplir à partir des données fournies par le service web
        public static String getLesUtilisateursQueJautorise(String pseudo, String mdpSha1, ArrayList lesUtilisateurs)
        {
            String reponse = "";
            try
            {	// création d'un nouveau document XML à partir de l'URL du service web et des paramètres
                String urlDuServiceWeb = _adresseHebergeur + _urlGetLesUtilisateursQueJautorise;
                urlDuServiceWeb += "?pseudo=" + pseudo;
                urlDuServiceWeb += "&mdp=" + mdpSha1;

                // création d'un flux en lecture (StreamReader) à partir du service
                StreamReader unFluxEnLecture = getFluxEnLecture(urlDuServiceWeb);

                // création d'un objet XmlReader à partir du flux ; il servira à parcourir le flux XML
                XmlReader leDocument = getDocumentXML(unFluxEnLecture);

                // parsing du flux XML
                leDocument.ReadToFollowing("reponse");
                leDocument.Read();
                reponse = leDocument.Value;

                /* Exemple de données obtenues pour un utilisateur :
                    <utilisateur>
                        <id>2</id>
                        <pseudo>callisto</pseudo>
                        <adrMail>delasalle.sio.eleves@gmail.com</adrMail>
                        <numTel>22.33.44.55.66</numTel>
                        <niveau>1</niveau>
                        <dateCreation>2018-01-19 20:11:24</dateCreation>
                        <nbTraces>2</nbTraces>
                        <dateDerniereTrace>2018-01-19 13:08:48</dateDerniereTrace>
                    </utilisateur>
                 */

                // vider d'abord la collection avant de la remplir
                lesUtilisateurs.Clear();

                // parcours de la liste des noeuds <utilisateur> et ajout dans la collection lesUtilisateurs
                while (leDocument.ReadToFollowing("utilisateur"))
                {
                    // parcours des balises intérieures d'un utilisateur
                    leDocument.ReadToFollowing("id"); leDocument.Read();
                    int unId = Convert.ToInt32(leDocument.Value);

                    leDocument.ReadToFollowing("pseudo"); leDocument.Read();
                    String unPseudo = leDocument.Value;

                    String unMdpSha1 = "";		// par sécurité, on ne récupère pas le mot de passe

                    leDocument.ReadToFollowing("adrMail"); leDocument.Read();
                    String uneAdrMail = leDocument.Value;

                    leDocument.ReadToFollowing("numTel"); leDocument.Read();
                    String unNumTel = leDocument.Value.ToString().Trim().Replace("\n", "");

                    leDocument.ReadToFollowing("niveau"); leDocument.Read();
                    int unNiveau = Convert.ToInt32(leDocument.Value);

                    leDocument.ReadToFollowing("dateCreation"); leDocument.Read();
                    DateTime uneDateCreation = Convert.ToDateTime(leDocument.Value.ToString());

                    leDocument.ReadToFollowing("nbTraces"); leDocument.Read();
                    int unNbTraces = Convert.ToInt32(leDocument.Value);

                    DateTime uneDateDerniereTrace = DateTime.MinValue;
                    if (unNbTraces > 0)
                    {
                        leDocument.ReadToFollowing("dateDerniereTrace"); leDocument.Read();
                        uneDateDerniereTrace = Convert.ToDateTime(leDocument.Value.ToString());
                    }

                    // crée un objet Utilisateur
                    Utilisateur unUtilisateur = new Utilisateur(unId, unPseudo, unMdpSha1, uneAdrMail, unNumTel, unNiveau, uneDateCreation, unNbTraces, uneDateDerniereTrace);

                    // ajoute l'utilisateur à la collection lesUtilisateurs
                    lesUtilisateurs.Add(unUtilisateur);
                } // continue au noeud suivant de type <utilisateur>

                // retour de la réponse du service web
                return reponse;
            }
            catch (Exception ex)
            {
                String msg = "Erreur : " + ex.Message;
                return msg;
            }
        }

        // Méthode statique pour obtenir la liste des utilisateurs qui m'autorisent (service GetLesUtilisateursQuiMautorisent)
        // La méthode doit recevoir 3 paramètres :
        //    pseudo : le pseudo de l'utilisateur qui fait appel au service web
        //    mdp : le mot de passe hashé en sha1
        //    lesUtilisateurs : collection (vide) à remplir à partir des données fournies par le service web
        public static String getLesUtilisateursQuiMautorisent(String pseudo, String mdpSha1, ArrayList lesUtilisateurs)
        {
            String reponse = "";
            try
            {	// création d'un nouveau document XML à partir de l'URL du service web et des paramètres
                String urlDuServiceWeb = _adresseHebergeur + _urlGetLesUtilisateursQuiMautorisent;
                urlDuServiceWeb += "?pseudo=" + pseudo;
                urlDuServiceWeb += "&mdp=" + mdpSha1;

                // création d'un flux en lecture (StreamReader) à partir du service
                StreamReader unFluxEnLecture = getFluxEnLecture(urlDuServiceWeb);

                // création d'un objet XmlReader à partir du flux ; il servira à parcourir le flux XML
                XmlReader leDocument = getDocumentXML(unFluxEnLecture);

                // parsing du flux XML
                leDocument.ReadToFollowing("reponse");
                leDocument.Read();
                reponse = leDocument.Value;

                /* Exemple de données obtenues pour un utilisateur :
                    <utilisateur>
                        <id>2</id>
                        <pseudo>callisto</pseudo>
                        <adrMail>delasalle.sio.eleves@gmail.com</adrMail>
                        <numTel>22.33.44.55.66</numTel>
                        <niveau>1</niveau>
                        <dateCreation>2018-01-19 20:11:24</dateCreation>
                        <nbTraces>2</nbTraces>
                        <dateDerniereTrace>2018-01-19 13:08:48</dateDerniereTrace>
                    </utilisateur>
                 */

                // vider d'abord la collection avant de la remplir
                lesUtilisateurs.Clear();

                // parcours de la liste des noeuds <utilisateur> et ajout dans la collection lesUtilisateurs
                while (leDocument.ReadToFollowing("utilisateur"))
                {
                    // parcours des balises intérieures d'un utilisateur
                    leDocument.ReadToFollowing("id"); leDocument.Read();
                    int unId = Convert.ToInt32(leDocument.Value);

                    leDocument.ReadToFollowing("pseudo"); leDocument.Read();
                    String unPseudo = leDocument.Value;

                    String unMdpSha1 = "";		// par sécurité, on ne récupère pas le mot de passe

                    leDocument.ReadToFollowing("adrMail"); leDocument.Read();
                    String uneAdrMail = leDocument.Value;

                    leDocument.ReadToFollowing("numTel"); leDocument.Read();
                    String unNumTel = leDocument.Value.ToString().Trim().Replace("\n", "");

                    leDocument.ReadToFollowing("niveau"); leDocument.Read();
                    int unNiveau = Convert.ToInt32(leDocument.Value);

                    leDocument.ReadToFollowing("dateCreation"); leDocument.Read();
                    DateTime uneDateCreation = Convert.ToDateTime(leDocument.Value.ToString());

                    leDocument.ReadToFollowing("nbTraces"); leDocument.Read();
                    int unNbTraces = Convert.ToInt32(leDocument.Value);

                    DateTime uneDateDerniereTrace = DateTime.MinValue;
                    if (unNbTraces > 0)
                    {
                        leDocument.ReadToFollowing("dateDerniereTrace"); leDocument.Read();
                        uneDateDerniereTrace = Convert.ToDateTime(leDocument.Value.ToString());
                    }

                    // crée un objet Utilisateur
                    Utilisateur unUtilisateur = new Utilisateur(unId, unPseudo, unMdpSha1, uneAdrMail, unNumTel, unNiveau, uneDateCreation, unNbTraces, uneDateDerniereTrace);

                    // ajoute l'utilisateur à la collection lesUtilisateurs
                    lesUtilisateurs.Add(unUtilisateur);
                } // continue au noeud suivant de type <utilisateur>

                // retour de la réponse du service web
                return reponse;
            }
            catch (Exception ex)
            {
                String msg = "Erreur : " + ex.Message;
                return msg;
            }
        }

        // Méthode statique pour obtenir la liste des parcours d'un utilisateur (service GetLesParcoursDunUtilisateur)
        // La méthode doit recevoir 4 paramètres :
        //    pseudo : le pseudo de l'utilisateur qui fait appel au service web
        //    mdp : le mot de passe hashé en sha1
        //    idUtilisateur : l'id de l'utilisateur dont on veut la liste des parcours
        //    lesTraces : collection (vide) à remplir à partir des données fournies par le service web
        public static String getLesParcoursDunUtilisateur(String pseudo, String mdpSha1, String pseudoConsulte, ArrayList lesTraces)
        {
            String reponse = "";
            try
            {	// création d'un nouveau document XML à partir de l'URL du service web et des paramètres
                String urlDuServiceWeb = _adresseHebergeur + _urlGetLesParcoursDunUtilisateur;
                urlDuServiceWeb += "?pseudo=" + pseudo;
                urlDuServiceWeb += "&mdpSha1=" + mdpSha1;
                urlDuServiceWeb += "&pseudoConsulte=" + pseudoConsulte;

                // création d'un flux en lecture (StreamReader) à partir du service
                StreamReader unFluxEnLecture = getFluxEnLecture(urlDuServiceWeb);

                // création d'un objet XmlReader à partir du flux ; il servira à parcourir le flux XML
                XmlReader leDocument = getDocumentXML(unFluxEnLecture);

                // parsing du flux XML
                leDocument.ReadToFollowing("reponse");
                leDocument.Read();
                reponse = leDocument.Value;

                /* Exemple de données obtenues pour un utilisateur :
                    <utilisateur>
                        <id>2</id>
                        <pseudo>callisto</pseudo>
                        <adrMail>delasalle.sio.eleves@gmail.com</adrMail>
                        <numTel>22.33.44.55.66</numTel>
                        <niveau>1</niveau>
                        <dateCreation>2018-01-19 20:11:24</dateCreation>
                        <nbTraces>2</nbTraces>
                        <dateDerniereTrace>2018-01-19 13:08:48</dateDerniereTrace>
                    </utilisateur>
                 */

                // vider d'abord la collection avant de la remplir
                lesTraces.Clear();

                // parcours de la liste des noeuds <utilisateur> et ajout dans la collection lesUtilisateurs
                while (leDocument.ReadToFollowing("trace"))
                {
                    // parcours des balises intérieures d'un utilisateur
                    leDocument.ReadToFollowing("id"); leDocument.Read();
                    int unId = Convert.ToInt32(leDocument.Value);

                    leDocument.ReadToFollowing("dateHeureDebut"); leDocument.Read();
                    DateTime uneDateHeureDebut = DateTime.Parse(leDocument.Value);

                    leDocument.ReadToFollowing("terminee"); leDocument.Read();
                    Boolean unTerminee = Convert.ToBoolean(leDocument.Value);

                    leDocument.ReadToFollowing("dateHeureFin"); leDocument.Read();
                    DateTime uneDateHeureFin = DateTime.Parse(leDocument.Value);

                    leDocument.ReadToFollowing("idUtilisateur"); leDocument.Read();
                    int unIdUtilisateur = Convert.ToInt32(leDocument.Value);

                    // crée un objet Utilisateur
                    Trace uneTrace = new Trace(unId, uneDateHeureDebut, uneDateHeureFin, unTerminee, unIdUtilisateur);

                    // ajoute l'utilisateur à la collection lesUtilisateurs
                    lesTraces.Add(uneTrace);
                } // continue au noeud suivant de type <utilisateur>

                // retour de la réponse du service web
                return reponse;
            }
            catch (Exception ex)
            {
                String msg = "Erreur : " + ex.Message;
                return msg;
            }
        }

        // Méthode statique pour obtenir un parcours et la liste de ses points (service GetUnParcoursEtSesPoints)
        // La méthode doit recevoir 4 paramètres :
        //    pseudo : le pseudo de l'utilisateur qui fait appel au service web
        //    mdp : le mot de passe hashé en sha1
        //    idTrace : l'id de la trace à consulter
        //    laTrace : objet Trace (vide) à remplir à partir des données fournies par le service web
        public static String getUnParcoursEtSesPoints(String pseudo, String mdpSha1, int idTrace, Trace laTrace)
        {
            return "";          // code provisoire, méthode à écrire et à tester !!!
        }

        // Méthode statique pour demander un nouveau mot de passe (service DemanderMdp)
        // La méthode doit recevoir 1 paramètre :
        //    pseudo : le pseudo de l'utilisateur
        public static String demanderMdp(String pseudo)
        {
            String reponse = "";
            try
            {	// création d'un nouveau document XML à partir de l'URL du service web et des paramètres
                String urlDuServiceWeb = _adresseHebergeur + _urlDemanderMdp;
                urlDuServiceWeb += "?pseudo=" + pseudo;

                // création d'un flux en lecture (StreamReader) à partir du service
                StreamReader unFluxEnLecture = getFluxEnLecture(urlDuServiceWeb);

                // création d'un objet XmlReader à partir du flux ; il servira à parcourir le flux XML
                XmlReader leDocument = getDocumentXML(unFluxEnLecture);

                // parsing du flux XML
                leDocument.ReadToFollowing("reponse"); leDocument.Read();
                reponse = leDocument.Value;

                // retour de la réponse du service web
                return reponse;
            }
            catch (Exception ex)
            {
                String msg = "Erreur : " + ex.Message;
                return msg;
            }          // code provisoire, méthode à écrire et à tester !!!
        }

        // Méthode statique pour demander une autorisation (service DemanderUneAutorisation)
        // La méthode doit recevoir 5 paramètres :
        //   pseudo : le pseudo de l'utilisateur qui fait appel au service web
        //   mdp : le mot de passe hashé en sha1
        //   pseudoDestinataire : le pseudo de l'utilisateur à qui on demande l'autorisation
        //   texteMessage : le texte d'un message accompagnant la demande
        //   nomPrenom : le nom et le prénom du demandeur
        public static String demanderUneAutorisation(String pseudo, String mdpSha1, String pseudoDestinataire, String texteMessage, String nomPrenom)
        {
            String reponse = "";
            try
            {	// création d'un nouveau document XML à partir de l'URL du service web et des paramètres
                String urlDuServiceWeb = _adresseHebergeur + _urlDemanderUneAutorisation;
                urlDuServiceWeb += "?pseudo=" + pseudo;
                urlDuServiceWeb += "&mdp=" + mdpSha1;
                urlDuServiceWeb += "&pseudoDestinataire=" + pseudoDestinataire;
                urlDuServiceWeb += "&texteMessage=" + texteMessage;
                urlDuServiceWeb += "&nomPrenom=" + nomPrenom;

                // création d'un flux en lecture (StreamReader) à partir du service
                StreamReader unFluxEnLecture = getFluxEnLecture(urlDuServiceWeb);

                // création d'un objet XmlReader à partir du flux ; il servira à parcourir le flux XML
                XmlReader leDocument = getDocumentXML(unFluxEnLecture);

                // parsing du flux XML
                leDocument.ReadToFollowing("reponse"); leDocument.Read();
                reponse = leDocument.Value;

                // retour de la réponse du service web
                return reponse;
            }
            catch (Exception ex)
            {
                String msg = "Erreur : " + ex.Message;
                return msg;
            }         // code provisoire, méthode à écrire et à tester !!!
        }

        // Méthode statique pour retirer une autorisation (service RetirerUneAutorisation)
        // La méthode doit recevoir 4 paramètres :
        //   pseudo : le pseudo de l'utilisateur qui fait appel au service web
        //   mdp : le mot de passe hashé en sha1
        //   pseudoARetirer : le pseudo de l'utilisateur à qui on veut retirer l'autorisation
        //   texteMessage : le texte d'un message pour un éventuel envoi de courriel
        public static String retirerUneAutorisation(String pseudo, String mdpSha1, String pseudoARetirer, String texteMessage)
        {
            String reponse = "";
            try
            {	// création d'un nouveau document XML à partir de l'URL du service web et des paramètres
                String urlDuServiceWeb = _adresseHebergeur + _urlRetirerUneAutorisation;
                urlDuServiceWeb += "?pseudo=" + pseudo;
                urlDuServiceWeb += "&mdp=" + mdpSha1;
                urlDuServiceWeb += "&pseudoARetirer=" + pseudoARetirer;
                urlDuServiceWeb += "&=texteMessage=" + texteMessage;

                // création d'un flux en lecture (StreamReader) à partir du service
                StreamReader unFluxEnLecture = getFluxEnLecture(urlDuServiceWeb);

                // création d'un objet XmlReader à partir du flux ; il servira à parcourir le flux XML
                XmlReader leDocument = getDocumentXML(unFluxEnLecture);

                // parsing du flux XML
                leDocument.ReadToFollowing("reponse"); leDocument.Read();
                reponse = leDocument.Value;

                // retour de la réponse du service web
                return reponse;
            }
            catch (Exception ex)
            {
                String msg = "Erreur : " + ex.Message;
                return msg;
            }
        }

        // Méthode statique pour supprimer un parcours (service SupprimerUnParcours)
        // La méthode doit recevoir 3 paramètres :
        //   pseudo : le pseudo de l'utilisateur qui fait appel au service web
        //   mdp : le mot de passe hashé en sha1
        //   idTrace : l'id de la trace à supprimer
        public static String supprimerUnParcours(String pseudo, String mdpSha1, int idTrace)
        {
            String reponse = "";
            try
            {	// création d'un nouveau document XML à partir de l'URL du service web et des paramètres
                String urlDuServiceWeb = _adresseHebergeur + _urlRetirerUneAutorisation;
                urlDuServiceWeb += "?pseudo=" + pseudo;
                urlDuServiceWeb += "&mdp=" + mdpSha1;
                urlDuServiceWeb += "&numeroTrace=" + idTrace;

                // création d'un flux en lecture (StreamReader) à partir du service
                StreamReader unFluxEnLecture = getFluxEnLecture(urlDuServiceWeb);

                // création d'un objet XmlReader à partir du flux ; il servira à parcourir le flux XML
                XmlReader leDocument = getDocumentXML(unFluxEnLecture);

                // parsing du flux XML
                leDocument.ReadToFollowing("reponse"); leDocument.Read();
                reponse = leDocument.Value;

                // retour de la réponse du service web
                return reponse;
            }
            catch (Exception ex)
            {
                String msg = "Erreur : " + ex.Message;
                return msg;
            }
        }

        // Méthode statique pour démarrer l'enregistrement d'un parcours (service DemarrerEnregistrementParcours)
        // La méthode doit recevoir 3 paramètres :
        //    pseudo : le pseudo de l'utilisateur qui fait appel au service web
        //    mdp : le mot de passe hashé en sha1
        //    laTrace : un objet Trace (vide) à remplir à partir des données fournies par le service web
        public static String demarrerEnregistrementParcours(String pseudo, String mdpSha1, Trace laTrace)
        {
            String reponse = "";
            try
            {	// création d'un nouveau document XML à partir de l'URL du service web et des paramètres
                String urlDuServiceWeb = _adresseHebergeur + _urlRetirerUneAutorisation;
                urlDuServiceWeb += "?pseudo=" + pseudo;
                urlDuServiceWeb += "&mdp=" + mdpSha1;

                // création d'un flux en lecture (StreamReader) à partir du service
                StreamReader unFluxEnLecture = getFluxEnLecture(urlDuServiceWeb);

                // création d'un objet XmlReader à partir du flux ; il servira à parcourir le flux XML
                XmlReader leDocument = getDocumentXML(unFluxEnLecture);

                // parsing du flux XML
                leDocument.ReadToFollowing("reponse"); leDocument.Read();
                reponse = leDocument.Value;

                // parcours de la liste des noeuds <utilisateur> et ajout dans la collection lesUtilisateurs
                while (leDocument.ReadToFollowing("trace"))
                {
                    // parcours des balises intérieures d'un utilisateur
                    leDocument.ReadToFollowing("id"); leDocument.Read();
                    int unId = Convert.ToInt32(leDocument.Value);

                    leDocument.ReadToFollowing("dateHeureDebut"); leDocument.Read();
                    DateTime uneDateHeureDebut = DateTime.Parse(leDocument.Value);

                    leDocument.ReadToFollowing("terminee"); leDocument.Read();
                    Boolean unTerminee = Convert.ToBoolean(leDocument.Value);

                    leDocument.ReadToFollowing("dateHeureFin"); leDocument.Read();
                    DateTime uneDateHeureFin = DateTime.Parse(leDocument.Value);

                    leDocument.ReadToFollowing("idUtilisateur"); leDocument.Read();
                    int unIdUtilisateur = Convert.ToInt32(leDocument.Value);


                    laTrace.setId(unId);
                    laTrace.setDateHeureDebut(uneDateHeureDebut);

                } // continue au noeud suivant de type <utilisateur>

                // retour de la réponse du service web
                return reponse;
            }
            catch (Exception ex)
            {
                String msg = "Erreur : " + ex.Message;
                return msg;
            }
        }

        // Méhode statique pour envoyer la position de l'utilisateur (service EnvoyerPosition)
        // La méthode doit recevoir 3 paramètres :
        //    pseudo : le pseudo de l'utilisateur qui fait appel au service web
        //    mdp : le mot de passe hashé en sha1
        //    lePoint : un objet PointDeTrace (vide) qui permettra de récupérer le numéro attribué à partir des données fournies par le service web
        public static String envoyerPosition(String pseudo, String mdpSha1, PointDeTrace lePoint)
        {
            return "";          // code provisoire, méthode à écrire et à tester !!!
        }

        // Méthode statique pour terminer l'enregistrement d'un parcours (service ArreterEnregistrementParcours)
        // La méthode doit recevoir 3 paramètres :
        //    pseudo : le pseudo de l'utilisateur qui fait appel au service web
        //    mdp : le mot de passe hashé en sha1
        //    idTrace : l'id de la trace à terminer
        public static String arreterEnregistrementParcours(String pseudo, String mdpSha1, int idTrace)
        {
            String reponse = "";
            try
            {	// création d'un nouveau document XML à partir de l'URL du service web et des paramètres
                String urlDuServiceWeb = _adresseHebergeur + _urlArreterEnregistrementParcours;
                urlDuServiceWeb += "?pseudo=" + pseudo;
                urlDuServiceWeb += "&mdp=" + mdpSha1;
                urlDuServiceWeb += "&idTrace=" + idTrace;

                // création d'un flux en lecture (StreamReader) à partir du service
                StreamReader unFluxEnLecture = getFluxEnLecture(urlDuServiceWeb);

                // création d'un objet XmlReader à partir du flux ; il servira à parcourir le flux XML
                XmlReader leDocument = getDocumentXML(unFluxEnLecture);

                // parsing du flux XML
                leDocument.ReadToFollowing("reponse"); leDocument.Read();
                reponse = leDocument.Value;

                // retour de la réponse du service web
                return reponse;
            }
            catch (Exception ex)
            {
                String msg = "Erreur : " + ex.Message;
                return msg;
            }
        }

    }
}
