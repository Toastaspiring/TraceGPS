// TP C# réalisé sous Visual Studio 2013
// Thème : affichage de trace GPS - Données globales au projet
// Auteur : dP
// Dernière mise à jour : 1/11/2021

using System;

namespace TraceGPS
{
    public class Global
    {
        // constantes
        public const String NOM_APPLICATION = "Trace GPS 1.1";  // le nom de l'application
        public const int FREQUENCE_AFFICHAGE = 30;     // nombre de secondes entre 2 réactualisations lors de l'affichage d'un parcours non terminé
        public const int FREQUENCE_ENVOI = 15;         // nombre de secondes entre 2 envois de position avec le simulateur

        // variables mémorisant les paramètres de connexion
        public static String pseudo = "";
        public static String mdpSha1 = "";
    }
}
