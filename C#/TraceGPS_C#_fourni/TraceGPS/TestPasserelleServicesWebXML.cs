using System;
using System.Windows.Forms;
using System.Collections;
namespace TraceGPS
{
    public class TestPasserelleServicesWebXML
    {
        static void Main()
        {
            String msg;
            ArrayList lesUtilisateurs;

            //// test visuel de la méthode connecter
            //msg = PasserelleServicesWebXML.connecter("admin", "adminnnnnnnn");
            //// affichage de la réponse
            //MessageBox.Show(msg, "Test 1.1", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //msg = PasserelleServicesWebXML.connecter("admin", Outils.sha1("mdpadmin"));
            //// affichage de la réponse
            //MessageBox.Show(msg, "Test 1.2", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //msg = PasserelleServicesWebXML.connecter("europa", Outils.sha1("mdputilisateur"));
            //// affichage de la réponse
            //MessageBox.Show(msg, "Test 1.3", MessageBoxButtons.OK, MessageBoxIcon.Information);


            //// test visuel de la méthode getTousLesUtilisateurs
            //lesUtilisateurs = new ArrayList();
            //msg = PasserelleServicesWebXML.getTousLesUtilisateurs("europa", Outils.sha1("mdputilisateur"), lesUtilisateurs);
            //// affichage de la réponse
            //MessageBox.Show(msg, "Test 2.1", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //// affichage du nombre d'utilisateurs
            //msg = "Nombre d'utilisateurs : " + lesUtilisateurs.Count;
            //MessageBox.Show(msg, "Test 2.2", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //// affichage de tous les utilisateurs
            //foreach (Utilisateur unUtilisateur in lesUtilisateurs)
            //{
            //    msg = unUtilisateur.toString();
            //    MessageBox.Show(msg, "Test 2.3", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}


            //// test visuel de la méthode creerUnUtilisateur
            //msg = PasserelleServicesWebXML.creerUnUtilisateur("jim", "delasalle.sio.eleves@gmail.com", "1122334455");
            //// Erreur : pseudo trop court (8 car minimum) ou déjà existant.
            //MessageBox.Show(msg, "Test 3.1", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //msg = PasserelleServicesWebXML.creerUnUtilisateur("turlututu", "delasalle.sio.elevesgmail.com", "1122334455");
            //// Erreur : adresse mail incorrecte ou déjà existante.
            //MessageBox.Show(msg, "Test 3.2", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //msg = PasserelleServicesWebXML.creerUnUtilisateur("turlututu", "delasalle.sio.eleves@gmailcom", "1122334455");
            //// Erreur : adresse mail incorrecte ou déjà existante.
            //MessageBox.Show(msg, "Test 3.3", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //msg = PasserelleServicesWebXML.creerUnUtilisateur("turlututu", "delasalle.sio.eleves@gmail.com", "1122334455");
            //// Erreur : adresse mail incorrecte ou déjà existante.
            //MessageBox.Show(msg, "Test 3.4", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //msg = PasserelleServicesWebXML.creerUnUtilisateur("turlututu", "delasallesioeleves@gmail.com", "1122334455");
            //// Enregistrement effectué ; vous allez recevoir un courriel avec votre mot de passe.
            //MessageBox.Show(msg, "Test 3.5", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //msg = PasserelleServicesWebXML.creerUnUtilisateur("turlututu", "de.la.salle.sio.eleves@gmail.com", "1122334455");
            //// Erreur : pseudo trop court (8 car minimum) ou déjà existant.
            //MessageBox.Show(msg, "Test 3.6", MessageBoxButtons.OK, MessageBoxIcon.Information);


            //// test visuel de la méthode supprimerUnUtilisateur
            //msg = PasserelleServicesWebXML.supprimerUnUtilisateur("europa", Outils.sha1("mdputilisateurrrrrr"), "toto");
            //// Erreur : authentification incorrecte.
            //MessageBox.Show(msg, "Test 4.1", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //msg = PasserelleServicesWebXML.supprimerUnUtilisateur("europa", Outils.sha1("mdputilisateur"), "toto");
            //// Erreur : authentification incorrecte.
            //MessageBox.Show(msg, "Test 4.2", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //msg = PasserelleServicesWebXML.supprimerUnUtilisateur("admin", Outils.sha1("mdpadminnnnn"), "toto");
            //// Erreur : authentification incorrecte.
            //MessageBox.Show(msg, "Test 4.3", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //msg = PasserelleServicesWebXML.supprimerUnUtilisateur("admin", Outils.sha1("mdpadmin"), "toto");
            //// Erreur : pseudo utilisateur inexistant.
            //MessageBox.Show(msg, "Test 4.4", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //msg = PasserelleServicesWebXML.supprimerUnUtilisateur("admin", Outils.sha1("mdpadmin"), "neon");
            //// Erreur : suppression impossible ; cet utilisateur possède encore des traces.
            //MessageBox.Show(msg, "Test 4.5", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //msg = PasserelleServicesWebXML.supprimerUnUtilisateur("admin", Outils.sha1("mdpadmin"), "turlututu");
            //// Suppression effectuée ; un courriel va être envoyé à l'utilisateur.
            //MessageBox.Show(msg, "Test 4.6", MessageBoxButtons.OK, MessageBoxIcon.Information);


            //// test visuel de la méthode changerDeMdp
            //msg = PasserelleServicesWebXML.changerDeMdp("europa", Outils.sha1("mdputilisateur"), "passepasse", "passepassepasse");
            //// Erreur : le nouveau mot de passe et sa confirmation sont différents.
            //MessageBox.Show(msg, "Test 5.1", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //msg = PasserelleServicesWebXML.changerDeMdp("europa", Outils.sha1("mdputilisateurrrr"), "passepasse", "passepasse");
            //// Erreur : authentification incorrecte.
            //MessageBox.Show(msg, "Test 5.2", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //msg = PasserelleServicesWebXML.changerDeMdp("europa", Outils.sha1("mdputilisateur"), "mdputilisateurrrr", "mdputilisateurrrr");
            //// Enregistrement effectué ; vous allez recevoir un courriel de confirmation.
            //MessageBox.Show(msg, "Test 5.3", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //msg = PasserelleServicesWebXML.changerDeMdp("europa", Outils.sha1("mdputilisateurrrr"), "mdputilisateur", "mdputilisateur");
            //// Enregistrement effectué ; vous allez recevoir un courriel de confirmation.
            //MessageBox.Show(msg, "Test 5.4", MessageBoxButtons.OK, MessageBoxIcon.Information);


            //// test visuel de la méthode demanderMdp
            //msg = PasserelleServicesWebXML.demanderMdp("jim");
            //// Erreur : pseudo inexistant.
            //MessageBox.Show(msg, "Test 6.1", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //msg = PasserelleServicesWebXML.demanderMdp("europa");
            //// Vous allez recevoir un courriel avec votre nouveau mot de passe.
            //MessageBox.Show(msg, "Test 6.2", MessageBoxButtons.OK, MessageBoxIcon.Information);


            //// test visuel de la méthode getLesUtilisateursQueJautorise
            //lesUtilisateurs = new ArrayList();
            //msg = PasserelleServicesWebXML.getLesUtilisateursQueJautorise("indigo", Outils.sha1("mdputilisateur"), lesUtilisateurs);
            //// affichage de la réponse
            //MessageBox.Show(msg, "Test 7.1", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //// affichage du nombre d'utilisateurs
            //msg = "Nombre d'utilisateurs : " + lesUtilisateurs.Count;
            //MessageBox.Show(msg, "Test 7.2", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //// affichage de tous les utilisateurs
            //foreach (Utilisateur unUtilisateur in lesUtilisateurs)
            //{
            //    msg = unUtilisateur.toString();
            //    MessageBox.Show(msg, "Test 7.3", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}


            //// test visuel de la méthode getLesUtilisateursQuiMautorisent
            //lesUtilisateurs = new ArrayList();
            //msg = PasserelleServicesWebXML.getLesUtilisateursQuiMautorisent("juno", Outils.sha1("mdputilisateur"), lesUtilisateurs);
            //// affichage de la réponse
            //MessageBox.Show(msg, "Test 8.1", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //// affichage du nombre d'utilisateurs
            //msg = "Nombre d'utilisateurs : " + lesUtilisateurs.Count;
            //MessageBox.Show(msg, "Test 8.2", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //// affichage de tous les utilisateurs
            //foreach (Utilisateur unUtilisateur in lesUtilisateurs)
            //{
            //    msg = unUtilisateur.toString();
            //    MessageBox.Show(msg, "Test 8.3", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}


            //// test visuel de la méthode demanderUneAutorisation
            //msg = PasserelleServicesWebXML.demanderUneAutorisation("europa", Outils.sha1("mdputilisateurrrrrr"), "toto", "", "");
            //// Erreur : données incomplètes.
            //MessageBox.Show(msg, "Test 9.1", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //msg = PasserelleServicesWebXML.demanderUneAutorisation("europa", Outils.sha1("mdputilisateurrrrrr"), "toto", "coucou", "charles-edouard");
            //// Erreur : authentification incorrecte.
            //MessageBox.Show(msg, "Test 9.2", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //msg = PasserelleServicesWebXML.demanderUneAutorisation("europa", Outils.sha1("mdputilisateur"), "toto", "coucou", "charles-edouard");
            //// Erreur : pseudo utilisateur inexistant.
            //MessageBox.Show(msg, "Test 9.3", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //msg = PasserelleServicesWebXML.demanderUneAutorisation("europa", Outils.sha1("mdputilisateur"), "galileo", "coucou", "charles-edouard");
            //// galileo va recevoir un courriel avec votre demande.
            //MessageBox.Show(msg, "Test 9.4", MessageBoxButtons.OK, MessageBoxIcon.Information);


            //// test visuel de la méthode retirerUneAutorisation
            //msg = PasserelleServicesWebXML.retirerUneAutorisation("europa", Outils.sha1("mdputilisateurrrrrr"), "toto", "coucou");
            //// Erreur : authentification incorrecte.
            //MessageBox.Show(msg, "Test 10.1", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //msg = PasserelleServicesWebXML.retirerUneAutorisation("europa", Outils.sha1("mdputilisateur"), "toto", "coucou");
            //// Erreur : pseudo utilisateur inexistant.
            //MessageBox.Show(msg, "Test 10.2", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //msg = PasserelleServicesWebXML.retirerUneAutorisation("europa", Outils.sha1("mdputilisateur"), "juno", "coucou");
            //// Erreur : l'autorisation n'était pas accordée.
            //MessageBox.Show(msg, "Test 10.3", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //msg = PasserelleServicesWebXML.retirerUneAutorisation("neon", Outils.sha1("mdputilisateur"), "oxygen", "coucou");
            //// Autorisation supprimée ; oxygen va recevoir un courriel de notification.
            //MessageBox.Show(msg, "Test 10.4", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //msg = PasserelleServicesWebXML.retirerUneAutorisation("neon", Outils.sha1("mdputilisateur"), "photon", "");
            //// Autorisation supprimée.
            //MessageBox.Show(msg, "Test 10.5", MessageBoxButtons.OK, MessageBoxIcon.Information);


            //// test visuel de la méthode envoyerPosition
            //DateTime laDate = Convert.ToDateTime("24/01/2018 13:42:21");
            //PointDeTrace lePoint = new PointDeTrace(23, 0, 48.15, -1.68, 50, laDate, 80);
            //msg = PasserelleServicesWebXML.envoyerPosition("europa", Outils.sha1("mdputilisateurrrrrr"), lePoint);
            //// Erreur : authentification incorrecte.
            //MessageBox.Show(msg, "Test 11.1", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //lePoint = new PointDeTrace(2333, 0, 48.15, -1.68, 50, laDate, 80);
            //msg = PasserelleServicesWebXML.envoyerPosition("europa", Outils.sha1("mdputilisateur"), lePoint);
            //// Erreur : le numéro de trace n'existe pas.
            //MessageBox.Show(msg, "Test 11.2", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //lePoint = new PointDeTrace(22, 0, 48.15, -1.68, 50, laDate, 80);
            //msg = PasserelleServicesWebXML.envoyerPosition("europa", Outils.sha1("mdputilisateur"), lePoint);
            //// Erreur : le numéro de trace ne correspond pas à cet utilisateur.
            //MessageBox.Show(msg, "Test 11.3", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //lePoint = new PointDeTrace(43, 0, 48.15, -1.68, 50, laDate, 80);
            //msg = PasserelleServicesWebXML.envoyerPosition("europa", Outils.sha1("mdputilisateur"), lePoint);
            //// Point créé.
            //MessageBox.Show(msg, "Test 11.4", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //MessageBox.Show(lePoint.toString(), "Test 11.5", MessageBoxButtons.OK, MessageBoxIcon.Information);


            //// test visuel de la méthode getUnParcoursEtSesPoints
            //Trace laTrace = new Trace();
            //msg = PasserelleServicesWebXML.getUnParcoursEtSesPoints("europa", Outils.sha1("mdputilisateur"), 22, laTrace);
            //// affichage de la réponse
            //MessageBox.Show(msg, "Test 12.1", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //// affichage de la trace
            //msg = laTrace.toString();
            //MessageBox.Show(msg, "Test 12.2", MessageBoxButtons.OK, MessageBoxIcon.Information);


            // test visuel de la méthode getLesParcoursDunUtilisateur
            //ArrayList lesTraces = new ArrayList();
            //msg = PasserelleServicesWebXML.getLesParcoursDunUtilisateur("juno", Outils.sha1("mdputilisateur"), "indigo", lesTraces);
            //// affichage de la réponse
            //MessageBox.Show(msg, "Test 13.1", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //// affichage du nombre de traces
            //msg = "Nombre de traces : " + lesTraces.Count;
            //MessageBox.Show(msg, "Test 13.2", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //// affichage de toutes les traces
            //foreach (Trace uneTrace in lesTraces)
            //{
            //    msg = uneTrace.toString();
            //    MessageBox.Show(msg, "Test 13.3", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}


            //// test visuel de la méthode supprimerUnParcours
            //msg = PasserelleServicesWebXML.supprimerUnParcours("europa", Outils.sha1("mdputilisateurrrrrr"), 10);
            //// Erreur : authentification incorrecte.
            //MessageBox.Show(msg, "Test 14.1", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //msg = PasserelleServicesWebXML.supprimerUnParcours("europa", Outils.sha1("mdputilisateur"), 100);
            //// Erreur : parcours inexistant.
            //MessageBox.Show(msg, "Test 14.2", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //msg = PasserelleServicesWebXML.supprimerUnParcours("europa", Outils.sha1("mdputilisateur"), 22);
            //// Erreur : vous n'êtes pas le propriétaire de ce parcours.
            //MessageBox.Show(msg, "Test 14.3", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //msg = PasserelleServicesWebXML.supprimerUnParcours("europa", Outils.sha1("mdputilisateur"), 4);
            //// Parcours supprimé.
            //MessageBox.Show(msg, "Test 14.4", MessageBoxButtons.OK, MessageBoxIcon.Information);


            //// test visuel de la méthode demarrerEnregistrementParcours
            //Trace laTrace = new Trace();
            //msg = PasserelleServicesWebXML.demarrerEnregistrementParcours("europa", Outils.sha1("mdputilisateurrrrrr"), laTrace);
            //// Erreur : authentification incorrecte.
            //MessageBox.Show(msg, "Test 15.1", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //laTrace = new Trace();
            //msg = PasserelleServicesWebXML.demarrerEnregistrementParcours("europa", Outils.sha1("mdputilisateur"), laTrace);
            //// Trace créée.
            //MessageBox.Show(msg, "Test 15.2", MessageBoxButtons.OK, MessageBoxIcon.Information);


            // test visuel de la méthode arreterEnregistrementParcours
            msg = PasserelleServicesWebXML.arreterEnregistrementParcours("europa", Outils.sha1("mdputilisateurrrr"), 23);
            // Erreur : authentification incorrecte.
            MessageBox.Show(msg, "Test 16.1", MessageBoxButtons.OK, MessageBoxIcon.Information);
            msg = PasserelleServicesWebXML.arreterEnregistrementParcours("europa", Outils.sha1("mdputilisateur"), 230);
            // Erreur : parcours inexistant.
            MessageBox.Show(msg, "Test 16.2", MessageBoxButtons.OK, MessageBoxIcon.Information);
            msg = PasserelleServicesWebXML.arreterEnregistrementParcours("kepler", Outils.sha1("mdputilisateur"), 5);
            // Erreur : le numéro de trace ne correspond pas à cet utilisateur.
            MessageBox.Show(msg, "Test 16.3", MessageBoxButtons.OK, MessageBoxIcon.Information);
            msg = PasserelleServicesWebXML.arreterEnregistrementParcours("europa", Outils.sha1("mdputilisateur"), 42);
            // Erreur : cette trace est déjà terminée.
            MessageBox.Show(msg, "Test 16.4", MessageBoxButtons.OK, MessageBoxIcon.Information);
            msg = PasserelleServicesWebXML.arreterEnregistrementParcours("europa", Outils.sha1("mdputilisateur"), 43);
            // Enregistrement terminé.
            MessageBox.Show(msg, "Test 16.5", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
