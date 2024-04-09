// Projet TraceGPS
// fichier : modele/PasserelleFichierXML.cs
// Rôle : Cette classe abstraite hérite de la classe PasserelleXML
// Elle précise la signature de la méthode pour "parser" un fichier GPS afin de mettre à jour un objet Trace fourni en paramètre.
// Dernière mise à jour : 1/11/2021 par dp

using System;
using System.Net;
using System.IO;
using System.Xml;				// permet d'utiliser les classes XML

namespace TraceGPS
{
    public abstract class PasserelleFichierXML : PasserelleXML
    {
        // méthode abstraite pour mettre à jour un objet Trace (vide) à partir n'un fichier GPS
        // paramètre nomFichier : le nom du fichier contenant la trace
        // paramètre laTraceAcreer : l'objet Trace à mettre à jour
        // retourne : un message d'erreur de traitement (ou un message vide si pas d'erreur)
        public abstract String creerTrace(String nomFichier, Trace laTraceAcreer);
    }
}
