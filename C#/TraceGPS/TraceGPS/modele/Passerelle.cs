using System;
using System.Net;
using System.IO;
using System.Xml;				// permet d'utiliser les classes XML

namespace TraceGPS
{
    /**
     * Cette classe abstraite détermine les outils permettant de "parser" un fichier GPX pour mettre à jour un objet Trace.
     * @author dP
     *
     */
    public abstract class Passerelle
    {
        /**
         * méthode protégée statique pour obtenir un flux en lecture
         * à partir de l'adresse d'un fichier ou de l'URL d'un service web
         * @param adrFichierOuServiceWeb : le nom du fichier contenant la trace (String)
         * @return : un flux de données en lecture (System.IO.StreamReader)
         */
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

        /**
         * méthode protégée statique pour obtenir document XML
         * à partir d'un flux de données en lecture
         * @param unFluxEnLecture : un flux de données en lecture (System.IO.StreamReader)
         * @return : un document XML (System.Xml.Document)
         */
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

        /**
         * méthode abstraite pour mettre à jour un objet Trace (vide) à partir n'un fichier GPX
         * @param nomFichier : le nom du fichier contenant la trace
         * @param laTraceAcreer : l'objet Trace à mettre à jour
         * @return : un message d'erreur de traitement (ou un message vide si pas d'erreur)
         */
        public abstract String creerTrace(String nomFichier, Trace laTraceAcreer);
    }
}
