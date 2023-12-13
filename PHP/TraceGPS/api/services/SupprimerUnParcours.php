<?php
namespace api\services;
use modele\DAO;
use DOMDocument;

/*
Projet TraceGPS - services web
fichier : services/SupprimerUnParcours.php
Dernière mise à jour : 13/12/2023 par MAREC louis

Rôle : ce service permet à un utilisateur de supprimer un de ses parcours 

Le service web doit recevoir 3 paramètres :
    pseudo : le pseudo de l'utilisateur qui demande à supprimer
    mdpSha1 : le mot de passe hashé en sha1 de l'utilisateur qui demande à supprimer
    idTrace : l'id de la trace à supprimer

Le service retourne un flux de données XML contenant un compte-rendu d'exécution

Les paramètres peuvent être passés par la méthode GET (pratique pour les tests, mais à éviter en exploitation) :
    http://<hébergeur>/SupprimerUnParcours.php?pseudo=europa&mdpSha1=13e3668bbee30b004380052b086457b014504b3e&idTrace=25

Les paramètres peuvent être passés par la méthode POST (à privilégier en exploitation pour la confidentialité des données) :
    http://<hébergeur>/SupprimerUnParcours.php
*/

// Connexion du serveur web à la base MySQL
$dao = new DAO();

// Récupération des données transmises
// la fonction $_GET récupère une donnée passée en paramètre dans l'URL par la méthode GET
// la fonction $_POST récupère une donnée envoyées par la méthode POST
// la fonction $_REQUEST récupère par défaut le contenu des variables $_GET, $_POST, $_COOKIE
$pseudo = ( empty($this->request['pseudo'])) ? "" : $this->request['pseudo'];
$mdpSha1 = ( empty($this->request['mdp'])) ? "" : $this->request['mdp'];
$idTrace = (empty($this->request['idTrace'])) ? "" : $this->request['idTrace'];
$lang = ( empty($this->request['lang'])) ? "" : $this->request['lang'];

// "xml" par défaut si le paramètre lang est absent ou incorrect
if ($lang != "json") $lang = "xml";

// Contrôle de la présence des paramètres
if ( $pseudo == "" || $mdpSha1 == "" || $idTrace == "" )
{	$code_reponse = 401;
	$msg = "Erreur : données incomplètes !";
}
else
{	if ( $dao->getNiveauConnexion($pseudo, $mdpSha1) == 0 )
    {   $code_reponse = 401;
		$msg = "Erreur : authentification incorrecte !";
    }
	else 
	{	// contrôle d'existence de idTrace
	    $laTrace = $dao->getUneTrace($idTrace);
	    if ($laTrace == null)
	    {  	$code_reponse = 401;
			$msg = "Erreur : parcours inexistant !";
	    }
	    else
	    {   // récupération de l'id de l'utilisateur demandeur et du propriétaire du parcours
    	    $idDemandeur = $dao->getUnUtilisateur($pseudo)->getId();
    	    $idProprietaire = $laTrace->getIdUtilisateur();
    	    
    	    if ( $idDemandeur != $idProprietaire )
    	    {   $code_reponse = 401;
				$msg = "Erreur : vous n'êtes pas le propriétaire du parcours !";
    	    }
            else
            {   // suppression du parcours
                $ok = $dao->supprimerUneTrace($idTrace);
                if ( ! $ok ) {
                    $code_reponse = 401;
					$msg = "Erreur : problème lors de la suppression du parcours !";
                }
                else {
                    $code_reponse = 200;
					$msg = "Parcours supprimé.";
                }
            }
	    }
	}
}
// ferme la connexion à MySQL
unset($dao);

// création du flux en sortie
if ($lang == "xml") {
    $content_type = "application/xml; charset=utf-8";      // indique le format XML pour la réponse
    $donnees = creerFluxXML($msg);
}
else {
    $content_type = "application/json; charset=utf-8";      // indique le format Json pour la réponse
    $donnees = creerFluxJSON($msg);
}

// envoi de la réponse HTTP
$this->envoyerReponse($code_reponse, $content_type, $donnees);

// fin du programme (pour ne pas enchainer sur les 2 fonctions qui suivent)
exit;

// ================================================================================================

// création du flux XML en sortie
function creerFluxXML($msg)
{	// crée une instance de DOMdocument (DOM : Document Object Model)
	$doc = new DOMDocument();
	
	// specifie la version et le type d'encodage
	$doc->version = '1.0';
	$doc->encoding = 'UTF-8';
	
	// crée un commentaire et l'encode en UTF-8
	$elt_commentaire = $doc->createComment('Service web SupprimerUnParcours - BTS SIO - Lycée De La Salle - Rennes');
	// place ce commentaire à la racine du document XML
	$doc->appendChild($elt_commentaire);
	
	// crée l'élément 'data' à la racine du document XML
	$elt_data = $doc->createElement('data');
	$doc->appendChild($elt_data);
	
	// place l'élément 'reponse' dans l'élément 'data'
	$elt_reponse = $doc->createElement('reponse', $msg);
	$elt_data->appendChild($elt_reponse);

	// Mise en forme finale
	$doc->formatOutput = true;
	
	// renvoie le contenu XML
	echo $doc->saveXML();
	return;
}

function creerFluxJSON($msg)
{
    /* Exemple de code JSON
         {
             "data":{
                "reponse": "authentification incorrecte."
             }
         }
     */
    
    // 2 notations possibles pour créer des tableaux associatifs (la deuxième est en commentaire)
    
    // construction de l'élément "data"
    $elt_data = ["reponse" => $msg];
//     $elt_data = array("reponse" => $msg);
    
    // construction de la racine
    $elt_racine = ["data" => $elt_data];
//     $elt_racine = array("data" => $elt_data);
    
    // retourne le contenu JSON (l'option JSON_PRETTY_PRINT gère les sauts de ligne et l'indentation)
    return json_encode($elt_racine, JSON_PRETTY_PRINT | JSON_UNESCAPED_UNICODE);
    
}
?>