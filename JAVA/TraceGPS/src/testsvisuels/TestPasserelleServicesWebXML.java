package testsvisuels;

import java.text.ParseException;
import java.util.ArrayList;
import java.util.Date;

import classes.Outils;
import classes.PasserelleServicesWebXML;
import classes.PointDeTrace;
import classes.Trace;
import classes.Utilisateur;

public class TestPasserelleServicesWebXML {

	public static void main(String[] args) throws ParseException {
		
		String msg;
		
		//// test visuel de la méthode getTousLesUtilisateurs
		//ArrayList<Utilisateur> lesUtilisateurs = new ArrayList<Utilisateur>();
		//msg = PasserelleServicesWebXML.getTousLesUtilisateurs("europa", Outils.sha1("mdputilisateur"), lesUtilisateurs);
		//// affichage de la réponse
		//System.out.println(msg);
		//// affichage du nombre d'utilisateurs
		//System.out.println("Nombre d'utilisateurs : " + lesUtilisateurs.size());
		//// affichage de tous les utilisateurs
		//for (Utilisateur unUtilisateur : lesUtilisateurs)
		//{ System.out.println(unUtilisateur.toString());
		//}

		// test visuel de la méthode getLesUtilisateursQueJautorise
		ArrayList<Utilisateur> lesUtilisateurs = new ArrayList<Utilisateur>();
		msg = PasserelleServicesWebXML.getLesUtilisateursQueJautorise("europa", Outils.sha1("mdputilisateur"), lesUtilisateurs);
		// affichage de la réponse
		System.out.println(msg);
		// affichage du nombre d'utilisateurs
		System.out.println("Nombre d'utilisateurs : " + lesUtilisateurs.size());
		// affichage de tous les utilisateurs
		for (Utilisateur unUtilisateur : lesUtilisateurs)
		{ System.out.println(unUtilisateur.toString());
		}


		// test visuel de la méthode getLesUtilisateursQuiMautorisent

		
		// test visuel de la méthode getLesParcoursDunUtilisateur

		
		// test visuel de la méthode getUnParcoursEtSesPoints

	
	} // fin Main
} // fin class
