using System;
using System.Security.Cryptography;
using System.Text;

namespace TraceGPS
{
    public class Outils
    {
        // pour tester si une chaine représente un nombre valide
        public static bool IsNumeric(String Test)
        {
            double X;
            try
            {
                X = Convert.ToDouble(Test);
                return true;
            }
            catch
            {
                return false;
            }
        }

        // pour reformater un numéro de téléphone en 5 groupes de 2 chiffres séparés par des points
        public static String corrigerTelephone(String laChaine)
        {
            String temp;
            String resultat;
            temp = laChaine;
            temp = temp.Replace(" ", "");		// supprime les espaces
            temp = temp.Replace(".", "");		// supprime les points
            temp = temp.Replace(",", "");		// supprime les virgules
            temp = temp.Replace("-", "");		// supprime les tirets
            temp = temp.Replace("_", "");		// supprime les underscore
            temp = temp.Replace("/", "");		// supprime les slash
            if (temp.Length == 10 && Outils.IsNumeric(temp))
            {
                resultat = temp.Substring(0, 2) + ".";
                resultat += temp.Substring(2, 2) + ".";
                resultat += temp.Substring(4, 2) + ".";
                resultat += temp.Substring(6, 2) + ".";
                resultat += temp.Substring(8, 2);
                return resultat;
            }
            else
            {
                return laChaine;
            }
        }

        public static String sha1(String donneeAencoder)
        {
            byte[] tableauAhacher = Encoding.ASCII.GetBytes(donneeAencoder);
            byte[] tableauResultat;

            SHA1 sha = new SHA1CryptoServiceProvider();
            tableauResultat = sha.ComputeHash(tableauAhacher);
            string hashage = BitConverter.ToString(tableauResultat).Replace("-", "");
            return hashage;
        }
    }
}
