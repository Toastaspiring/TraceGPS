// Projet TraceGPS - API Java
// Fichier : Passerelle.java
// Cette classe abstraite fournit les outils permettant d'obtenir un document XML à partir d'un fichier ou d'un service web
// Dernière mise à jour : 26/3/2018 par Jim

package classes;

import java.io.BufferedInputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.InputStream;

import java.net.HttpURLConnection;
import java.net.URL;

import javax.xml.parsers.DocumentBuilder;
import javax.xml.parsers.DocumentBuilderFactory;

import org.w3c.dom.Document;

public abstract class PasserelleXML {

    // méthode protégée statique pour obtenir un flux en lecture (java.io.InputStream)
    // à partir de l'adresse d'un fichier ou de l'URL d'un service web
    protected static InputStream getFluxEnLecture(String adrFichierOuServiceWeb)
    {
		InputStream unFluxEnLecture;
		try
		{
			if (adrFichierOuServiceWeb.startsWith("http"))
			{	// connexion HTTP au service web
				URL url = new URL(adrFichierOuServiceWeb);
				HttpURLConnection urlConnection = (HttpURLConnection) url.openConnection();

				// récupération de la réponse dans un flux en lecture (InputStream)
				unFluxEnLecture = new BufferedInputStream(urlConnection.getInputStream());

			}
			else
			{	// création d'un flux en lecture (InputStream) depuis le fichier
				unFluxEnLecture = new FileInputStream(new File(adrFichierOuServiceWeb));
			}
			return unFluxEnLecture;
		}
		catch (Exception ex)
		{	return null;
		}	
    }

    // méthode protégée statique pour obtenir document XML (org.w3c.dom.Document)
    // à partir d'un flux de données en lecture (java.io.InputStream)
	protected static Document getDocumentXML(InputStream unFluxEnLecture)
	{
		try
		{
			// création d'une instance de DocumentBuilderFactory et DocumentBuilder
			DocumentBuilderFactory leDBF = DocumentBuilderFactory.newInstance();
			DocumentBuilder leDB = leDBF.newDocumentBuilder();
	
			// on crée un nouveau document XML avec en argument le flux XML
			Document leDocument = leDB.parse(unFluxEnLecture);
			return leDocument;
		}
		catch (Exception ex)
		{	return null;
		}	
	}
}
