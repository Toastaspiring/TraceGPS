<?php
namespace api\services;
use modele\DAO;
use DOMDocument;
use modele\Outils;

/*
Projet TraceGPS - services web
fichier : api/services/DemanderMdp.php
Dernière mise à jour : 28/11/2023 par Ethan DIVET

Rôle : ce service web permet à un utilisateur de demander un nouveau mot de passe s'il l'a oublié.

Paramètres à fournir :
    pseudo : le pseudo de l'utilisateur
    lang : le langage utilisé pour le flux de données ("xml" ou "json") ; "xml" par défaut si le paramètre est absent ou incorrect

Les paramètres doivent être passés par la méthode GET :
    http://<hébergeur>/tracegps/api/DemanderMdp
*/

// Connexion du serveur web à la base MySQL
$dao = new DAO();

// Récupération des données transmises
$pseudo = (empty($this->request['pseudo'])) ? "" : $this->request['pseudo'];
$lang = (empty($this->request['lang'])) ? "" : $this->request['lang'];

// "xml" par défaut si le paramètre lang est absent ou incorrect
if ($lang != "json") {
    $lang = "xml";
}

// La méthode HTTP utilisée doit être GET
if ($this->getMethodeRequete() != "GET") {
    $msg = "Erreur : méthode HTTP incorrecte.";
    $code_reponse = 406;

} else {

    // Les paramètres doivent être présents
    if ($pseudo == "") {
        $msg = "Erreur : données incomplètes.";
        $code_reponse = 400;
    }
    else {

        // Vérifier si le pseudo existe
        if ( ! $dao->existePseudoUtilisateur($pseudo) ) {
            $msg = "Erreur : pseudo inexistant.";
            $code_reponse = 400;
        }

        else {
            // Générer un nouveau mot de passe
            $nouveauMdp = Outils::creerMdp();

            // Enregistrer le nouveau mot de passe après l'avoir mis en MD5
            $envoiMail = $dao->modifierMdpUtilisateur ($pseudo, $nouveauMdp);
            if ( ! $envoiMail ) {
    		    $msg = "Erreur : problème lors de l'enregistrement du mot de passe.";
    		    $code_reponse = 500;
    		}
    		else {
                // Envoyer un courriel à l'utilisateur avec son nouveau mot de passe
                $envoiMail = $dao->envoyerMdp($pseudo, $nouveauMdp);
                
                if (!$envoiMail) {
                    $msg = "Enregistrement effectué ; l'envoi du courriel de confirmation a rencontré un problème.";
                    $code_reponse = 500;
                
                } else {
                    $msg = "Vous allez recevoir un courriel avec votre nouveau mot de passe.";
                    $code_reponse = 200;
                }
            }
        }
    }
}

// Ferme la connexion à MySQL
unset($dao);

// Création du flux en sortie
if ($lang == "xml") {
    $content_type = "application/xml; charset=utf-8"; // Format XML pour la réponse
    $donnees = creerFluxXML($msg);
} 
else {
    $content_type = "application/json; charset=utf-8"; // Format JSON pour la réponse
    $donnees = creerFluxJSON($msg);
}

// Envoi de la réponse HTTP
$this->envoyerReponse($code_reponse, $content_type, $donnees);

// Fin du programme
exit;

// Fonction de création du flux XML en sortie
function creerFluxXML($msg)
{
    // Crée une instance de DOMdocument
    $doc = new DOMDocument();
    $doc->version = '1.0';
    $doc->encoding = 'UTF-8';
    
    // Crée l'élément 'data'
    $elt_data = $doc->createElement('data');
    $doc->appendChild($elt_data);
    
    // Place l'élément 'reponse' sous 'data'
    $elt_reponse = $doc->createElement('reponse', $msg);
    $elt_data->appendChild($elt_reponse);
    
    // Mise en forme finale
    $doc->formatOutput = true;
    
    // Renvoie le contenu XML
    return $doc->saveXML();
}

// Fonction de création du flux JSON en sortie
function creerFluxJSON($msg)
{
    $elt_data = ["reponse" => $msg];
    $elt_racine = ["data" => $elt_data];
    
    // Renvoie le contenu JSON
    return json_encode($elt_racine, JSON_PRETTY_PRINT | JSON_UNESCAPED_UNICODE);
}
?>
