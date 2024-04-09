// Projet TraceGPS
// fichier : modele/PasserelleXML.cs
// Rôle : Cette classe abstraite fixe les outils pour "parser" un fichier GPS et mettre à jour un objet Trace
// Dernière mise à jour : 1/11/2021 par dp

using System;
using System.Net;
using System.IO;
using System.Xml;				// permet d'utiliser les classes XML

namespace TraceGPS
{
    public abstract class PasserelleXML
    {
        // méthode protégée statique pour obtenir un flux en lecture à partir de l'adresse d'un fichier ou de l'URL d'un service web
        // paramètre adrFichierOuServiceWeb : le nom du fichier contenant la trace (String)
        // retourne : un flux de données en lecture (System.IO.StreamReader)
        protected static StreamReader getFluxEnLecture(String adrFichierOuServiceWeb)
        {
            StreamReader unFluxEnLecture;
            if (adrFichierOuServiceWeb.StartsWith("http"))
            {   // l'adresse fournie est l'URL d'un service web car elle commence par "http"
                // création d'une requête http
                HttpWebRequest uneRequeteHttp = (HttpWebRequest)WebRequest.Create(adrFichierOuServiceWeb);
                uneRequeteHttp.Method = WebRequestMethods.Http.Get;
                // récupération de la réponse
                WebResponse uneReponseHttp = uneRequeteHttp.GetResponse();
                // création d'un flux en lecture (SteamReader) à partir de la réponse web
                unFluxEnLecture = new StreamReader(uneReponseHttp.GetResponseStream());
            }
            else
            {   // l'adresse fournie est celle d'un fichier
                // création d'un flux en lecture (StreamReader) depuis le fichier
                unFluxEnLecture = File.OpenText(adrFichierOuServiceWeb);
            }
            return unFluxEnLecture;
        }

        // méthode protégée statique pour obtenir un document XML à partir d'un flux de données en lecture
        // paramètre unFluxEnLecture : un flux de données en lecture (System.IO.StreamReader)
        // retourne : un document XML (System.Xml.Document)
        protected static XmlReader getDocumentXML(StreamReader unFluxEnLecture)
        {
            try
            {
                XmlReader leDocument = XmlReader.Create(unFluxEnLecture);
                return leDocument;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
