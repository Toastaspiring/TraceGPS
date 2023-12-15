package testsunitaires;

import static org.junit.Assert.*;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Date;

import org.junit.Test;

import classes.Outils;
import classes.PasserelleServicesWebXML;
import classes.Point;
import classes.PointDeTrace;
import classes.Trace;
import classes.Utilisateur;

public class PasserelleServiceWebXMLTest {

	// Il marche ! (planchet)
	@Test
	public void testConnecter() {
		String msg = PasserelleServicesWebXML.connecter("admin", "adminnnnnnnn");
		assertEquals("Erreur : authentification incorrecte.", msg);
		msg = PasserelleServicesWebXML.connecter("admin", Outils.sha1("mdpadmin"));
		assertEquals("Administrateur authentifié.", msg);
		msg = PasserelleServicesWebXML.connecter("europa", "f82eb3d56258123beaa75bfd62d65768730989d5");
		assertEquals("Utilisateur authentifié.", msg);
	}

	// Il marche ! (planchet + edit DAO)
	@Test
	public void testCreerUnUtilisateur() {
		String msg = PasserelleServicesWebXML.creerUnUtilisateur("jim",
				"delasalle.sio.eleves@gmail.com", "1122334455");
		assertEquals("Erreur : pseudo trop court (8 car minimum) ou déjà existant.",
				msg);
		msg = PasserelleServicesWebXML.creerUnUtilisateur("turlututu",
				"delasalle.sio.elevesgmail.com", "1122334455");
		assertEquals("Erreur : adresse mail incorrecte ou déjà existante.", msg);
		msg = PasserelleServicesWebXML.creerUnUtilisateur("turlututu",
				"delasalle.sio.eleves@gmailcom", "1122334455");
		assertEquals("Erreur : adresse mail incorrecte ou déjà existante.", msg);
		msg = PasserelleServicesWebXML.creerUnUtilisateur("turlututu",
				"delasalle.sio.eleves@gmail.com", "1122334455");
		assertEquals("Erreur : adresse mail incorrecte ou déjà existante.", msg);
		msg = PasserelleServicesWebXML.creerUnUtilisateur("turlututu",
				"delasallesioeleves@gmail.com", "1122334455");
		assertEquals("Enregistrement effectué ; vous allez recevoir un courriel avec votre mot de passe.", msg);
		msg = PasserelleServicesWebXML.creerUnUtilisateur("turlututu",
				"de.la.salle.sio.eleves@gmail.com", "1122334455");
		assertEquals("Erreur : pseudo trop court (8 car minimum) ou déjà existant.",
				msg);
	}

	// Il marche ! (planchet)
	@Test
	public void testSupprimerUnUtilisateur() {
		String msg;
		msg = PasserelleServicesWebXML.supprimerUnUtilisateur("europa",
				Outils.sha1("mdputilisateurrrrrr"), "toto");
		assertEquals("Erreur : authentification incorrecte.", msg);
		msg = PasserelleServicesWebXML.supprimerUnUtilisateur("europa",
				Outils.sha1("mdputilisateur"), "toto");
		assertEquals("Erreur : authentification incorrecte.", msg);
		msg = PasserelleServicesWebXML.supprimerUnUtilisateur("admin",
				Outils.sha1("mdpadminnnnn"), "toto");
		assertEquals("Erreur : authentification incorrecte.", msg);
		msg = PasserelleServicesWebXML.supprimerUnUtilisateur("admin",
				Outils.sha1("mdpadmin"), "toto");
		assertEquals("Erreur : pseudo utilisateur inexistant.", msg);
		msg = PasserelleServicesWebXML.supprimerUnUtilisateur("admin",
				Outils.sha1("mdpadmin"), "neon");
		assertEquals("Erreur : suppression impossible ; cet utilisateur possède encore des traces.", msg);
		msg = PasserelleServicesWebXML.supprimerUnUtilisateur("admin",
				Outils.sha1("mdpadmin"), "turlututu");
		assertEquals("Suppression effectuée ; un courriel va être envoyé à l'utilisateur.", msg);
	}

	@Test
	public void testChangerDeMdp() {
		String msg = PasserelleServicesWebXML.changerDeMdp("europa",
				Outils.sha1("mdputilisateur"), "passepasse", "passepassepasse");
		assertEquals("Erreur : le nouveau mot de passe et sa confirmation sont différents.", msg);
		msg = PasserelleServicesWebXML.changerDeMdp("europa",
				Outils.sha1("mdputilisateurrrr"), "passepasse", "passepasse");
		assertEquals("Erreur : authentification incorrecte.", msg);
		msg = PasserelleServicesWebXML.changerDeMdp("europa",
				Outils.sha1("mdputilisateur"), "mdputilisateurrrr", "mdputilisateurrrr");
		assertEquals("Enregistrement effectué ; vous allez recevoir un courriel de confirmation.", msg);
		msg = PasserelleServicesWebXML.changerDeMdp("europa",
				Outils.sha1("mdputilisateurrrr"), "mdputilisateur", "mdputilisateur");
		assertEquals("Enregistrement effectué ; vous allez recevoir un courriel de confirmation.", msg);
	}

	// Il marche ! (ethan)
	@Test
	public void testDemanderMdp() {
		String msg = PasserelleServicesWebXML.demanderMdp("jim");
		assertEquals("Erreur : pseudo inexistant.", msg);
		msg = PasserelleServicesWebXML.demanderMdp("europa");
		assertEquals("Vous allez recevoir un courriel avec votre nouveau mot de passe.", msg);
	}

	@Test
	public void testDemanderUneAutorisation() {
		String msg = PasserelleServicesWebXML.demanderUneAutorisation("europa", Outils.sha1("mdputilisateurrrrrr"),
				"toto", "", "");
		assertEquals("Erreur : données incomplètes.", msg);
		msg = PasserelleServicesWebXML.demanderUneAutorisation("europa", Outils.sha1("mdputilisateurrrrrr"), "toto",
				"coucou", "charles-edouard");
		assertEquals("Erreur : authentification incorrecte.", msg);
		msg = PasserelleServicesWebXML.demanderUneAutorisation("europa", Outils.sha1("mdputilisateur"), "toto",
				"coucou", "charles-edouard");
		assertEquals("Erreur : pseudo utilisateur inexistant.", msg);
		msg = PasserelleServicesWebXML.demanderUneAutorisation("europa", Outils.sha1("mdputilisateur"), "galileo",
				"coucou", "charles-edouard");
		assertEquals("galileo va recevoir un courriel avec votre demande.", msg);
	}

	@Test
	public void testRetirerUneAutorisation() {
		String msg = PasserelleServicesWebXML.retirerUneAutorisation("europa", Outils.sha1("mdputilisateurrrrrr"), "toto", "coucou");
		assertEquals("Erreur : authentification incorrecte.", msg);
		msg = PasserelleServicesWebXML.retirerUneAutorisation("europa", Outils.sha1("mdputilisateur"), "toto", "coucou");
		assertEquals("Erreur : pseudo utilisateur inexistant.", msg);
		msg = PasserelleServicesWebXML.retirerUneAutorisation("europa", Outils.sha1("mdputilisateur"), "juno", "coucou");
		assertEquals("Erreur : l'autorisation n'était pas accordée.", msg);
		msg = PasserelleServicesWebXML.retirerUneAutorisation("neon", Outils.sha1("mdputilisateur"), "oxygen", "coucou");
		assertEquals("Autorisation supprimée ; oxygen va recevoir un courriel de notification.", msg);
		msg = PasserelleServicesWebXML.retirerUneAutorisation("neon", Outils.sha1("mdputilisateur"), "photon", "");
		assertEquals("Autorisation supprimée.", msg);
	}

	// Il marche ! (louis)
	@Test
	public void testEnvoyerPosition() throws ParseException {
		Date laDate = Outils.convertirEnDateHeure("24/01/2018 13:42:21");
		PointDeTrace lePoint = new PointDeTrace(23, 0, 48.15, -1.68, 50, laDate, 80);
		String msg = PasserelleServicesWebXML.envoyerPosition("europa", "13e3668bbee30b004380052b086457b014b3e", lePoint);
		assertEquals("Erreur : authentification incorrecte.", msg);
		lePoint = new PointDeTrace(2333, 0, 48.15, -1.68, 50, laDate, 80);
		msg = PasserelleServicesWebXML.envoyerPosition("europa", "13e3668bbee30b004380052b086457b014504b3e", lePoint);
		assertEquals("Erreur : le numéro de trace n'existe pas.", msg);
		lePoint = new PointDeTrace(22, 0, 48.15, -1.68, 50, laDate, 80);
		msg = PasserelleServicesWebXML.envoyerPosition("europa", "13e3668bbee30b004380052b086457b014504b3e", lePoint);
		assertEquals("Erreur : le numéro de trace ne correspond pas à cet utilisateur.", msg);
		lePoint = new PointDeTrace(4, 0, 48.15, -1.68, 50, laDate, 80);
		msg = PasserelleServicesWebXML.envoyerPosition("europa", "13e3668bbee30b004380052b086457b014504b3e", lePoint);
		assertEquals("Point créé.", msg);
	}

	// Il marche !(ethan)
	@Test
	public void testDemarrerEnregistrementParcours() {
		Trace laTrace = new Trace();
		String msg = PasserelleServicesWebXML.demarrerEnregistrementParcours("europa",
				Outils.sha1("mdputilisateurrrrrr"), laTrace);
		assertEquals("Erreur : authentification incorrecte.", msg);
		laTrace = new Trace();
		msg = PasserelleServicesWebXML.demarrerEnregistrementParcours("europa", Outils.sha1("mdputilisateur"), laTrace);
		assertEquals("Trace créée.", msg);
	}

	@Test
	public void testArreterEnregistrementParcours() {
		String msg;
		msg = PasserelleServicesWebXML.arreterEnregistrementParcours("europa", Outils.sha1("mdputilisateurrrrrr"), 23);
		assertEquals("Erreur : authentification incorrecte.", msg);
		msg = PasserelleServicesWebXML.arreterEnregistrementParcours("europa", Outils.sha1("mdputilisateur"), 230);
		assertEquals("Erreur : parcours inexistant.", msg);
		msg = PasserelleServicesWebXML.arreterEnregistrementParcours("europa", Outils.sha1("mdputilisateur"), 5);
		assertEquals("Erreur : le numéro de trace ne correspond pas à cet utilisateur.", msg);
		msg = PasserelleServicesWebXML.arreterEnregistrementParcours("europa", Outils.sha1("mdputilisateur"), 4);
		assertEquals("Erreur : cette trace est déjà terminée.", msg);
		msg = PasserelleServicesWebXML.arreterEnregistrementParcours("europa", Outils.sha1("mdputilisateur"), 23);
		assertEquals("Enregistrement terminé.", msg);
	}

	//il marche
	@Test
	public void testSupprimerUnUnParcours() {
		String msg = PasserelleServicesWebXML.supprimerUnParcours("europa",
				Outils.sha1("mdputilisateurrrrrr"), 10);
		assertEquals("Erreur : authentification incorrecte.", msg);
		msg = PasserelleServicesWebXML.supprimerUnParcours("europa",
				Outils.sha1("mdputilisateur"), 100);
				System.err.println(Outils.sha1("mdputilisateur"));
		assertEquals("Erreur : parcours inexistant.", msg);
		msg = PasserelleServicesWebXML.supprimerUnParcours("europa",
				Outils.sha1("mdputilisateur"), 22);
		assertEquals("Erreur : vous n'êtes pas le propriétaire de ce parcours.",
				msg);
		msg = PasserelleServicesWebXML.supprimerUnParcours("europa",
				Outils.sha1("mdputilisateur"), 3);
		assertEquals("Parcours supprimé.", msg);
	}

} // fin du test
