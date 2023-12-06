<?php
namespace api\services;
use modele\DAO;
use DOMDocument;
use modele\PointDeTrace;
/*
Projet TraceGPS - services web
fichier :  api/services/ChangerDeMdp.php
Dernière mise à jour : 21/11/2023 par Ethan DIVET

Rôle : ce service web permet à un utilisateur de demander une autorisation à un autre utilisateur.

Paramètres à fournir :
    pseudo : le pseudo de l'utilisateur qui demande l'autorisation
    mdp : le mot de passe hashé en sha1 de l'utilisateur qui demande l'autorisation
    pseudoDestinataire : le pseudo de l'utilisateur à qui on demande l'autorisation
    texteMessage : le texte d'un message accompagnant la demande
    nomPrenom : le nom et le prénom du demandeur
    lang : le langage utilisé pour le flux de données ("xml" ou "json")
    
Description du traitement :
    Vérifier que les données transmises sont complètes
    Vérifier l'authentification de l'utilisateur demandeur
    Vérifier que le pseudo de l'utilisateur destinataire existe
    Envoyer un courriel à l'utilisateur destinataire
*/

// connexion du serveur web à la base MySQL
$dao = new DAO();

// Récupération des données transmises
$pseudo = ( empty($this->request['pseudo'])) ? "" : $this->request['pseudo'];
$mdpSha1 = ( empty($this->request['mdp'])) ? "" : $this->request['mdp'];
$idTrace = (empty($this->request['idTrace'])) ? "" : $this->request['idTrace'];
$dateHeure = ( empty($this->request['dateHeure'])) ? "" : $this->request['dateHeure'];
$latitude = ( empty($this->request['latitude'])) ? "" : $this->request['latitude'];
$longitude = ( empty($this->request['longitude'])) ? "" : $this->request['longitude'];
$altitude = ( empty($this->request['altitude'])) ? "" : $this->request['altitude'];
$rythmeCardio = ( empty($this->request['rythmeCardio'])) ? "" : $this->request['rythmeCardio'];
$lang = ( empty($this->request['lang'])) ? "" : $this->request['lang'];

// "xml" par défaut si le paramètre lang est absent ou incorrect
if ($lang != "json") $lang = "xml";

// La méthode HTTP utilisée doit être GET
if ($this->getMethodeRequete() != "GET")
{	$msg = "Erreur : méthode HTTP incorrecte.";
    $code_reponse = 406;
    $donnees = [];
}
else {
    // Les paramètres doivent être présents
    if ( $pseudo == "" || $mdpSha1 == "" || $idTrace == "" || $dateHeure == "" || $latitude == "" || $longitude == "" || $altitude == "" || $rythmeCardio == "") {
        $msg = "Erreur : données incomplètes.";
        $code_reponse = 400;
        $donnees = [];
    }
    else
        {	
        // test de l'authentification de l'utilisateur
        // la méthode getNiveauConnexion de la classe DAO retourne les valeurs 0 (non identifié) ou 1 (utilisateur) ou 2 (administrateur)
        $niveauConnexion = $dao->getNiveauConnexion($pseudo, $mdpSha1);
    
        if ( $niveauConnexion != 1 )
        {  $msg = "Erreur : authentification incorrecte.";
           $code_reponse = 401;
           $donnees = [];
        }
        else
        {	
            $uneTrace = $dao->getUneTrace($idTrace);
            if (!($uneTrace))
            {   $msg = "Erreur : le numéro de trace n'existe pas.";
                $code_reponse = 401;
                $donnees = [];
            }
            else {
                $unUtilisateur = $dao->getUnUtilisateur($pseudo);
                if (!($unUtilisateur->getId() == $uneTrace->getIdUtilisateur()))
                {   $msg = "Erreur : le numéro de trace ne correspond pas à cet utilisateur.";
                    $code_reponse = 401;
                    $donnees = [];
                }
                else {
                    if ($uneTrace->getTerminee() == 1)
                    {   $msg = "Erreur : la trace est déjà terminée.";
                        $code_reponse = 401;
                        $donnees = [];
                    }
                    else{
                        $unPointDeTrace = new PointDeTrace($idTrace, $unID, $uneLatitude, $uneLongitude, $uneAltitude, $uneDateHeure, $unRythmeCardio, null, null, null);
                        $ok = $dao->creerUnPointDeTrace($unPointDeTrace);
                        $donnees = array(
                            "id" => $unPointDeTrace->getId()
                        );
                        if ($ok)
                        {   $msg = "Point créé.";
                            $code_reponse = 200;
                            $donnees = array(
                                "id" => $unPointDeTrace->getId()
                            );
                        }
                        else{
                            $msg = "Erreur : problème lors de l'enregistrement du point.";
                            $code_reponse = 401;
                            $donnees = [];
                        }
                    }
                }
            }
        }
    }
}

// ferme la connexion à MySQL :
unset($dao);

// création du flux en sortie
if ($lang == "xml") {
    $content_type = "application/xml; charset=utf-8";      // indique le format XML pour la réponse
    $donnees = creerFluxXML ($msg, $donnees);
}
else {
    $content_type = "application/json; charset=ISO-8859-15";      // indique le format Json pour la réponse
    $donnees = creerFluxJSON ($msg, $donnees);
}

// envoi de la réponse HTTP
$this->envoyerReponse($code_reponse, $content_type, $donnees);

// fin du programme (pour ne pas enchainer sur les 2 fonctions qui suivent)
exit;

// ================================================================================================

// création du flux XML en sortie
function creerFluxXML($msg, $donnees)
{	
    /* Exemple de code XML
         <?xml version="1.0" encoding="UTF-8"?>
         <!--Service web Connecter - BTS SIO - Lycée De La Salle - Rennes-->
         <data>
            <reponse>Erreur : données incomplètes.</reponse>
         </data>
     */
    
    // crée une instance de DOMdocument (DOM : Document Object Model)
	$doc = new DOMDocument();
	
	// specifie la version et le type d'encodage
	$doc->version = '1.0';
	$doc->encoding = 'UTF-8';
	
	// crée un commentaire et l'encode en UTF-8
	$elt_commentaire = $doc->createComment('Service web Connecter - BTS SIO - Lycée De La Salle - Rennes');
	// place ce commentaire à la racine du document XML
	$doc->appendChild($elt_commentaire);
	
	// crée l'élément 'data' à la racine du document XML
	$elt_data = $doc->createElement('data');
	$doc->appendChild($elt_data);
	
	// place l'élément 'reponse' juste après l'élément 'data'
	$elt_reponse = $doc->createElement('reponse', $msg);
    $elt_donnees = $doc->createElement('donnees');
    if ($donnees == []){
        $elt_donnees->appendChild($doc->createElement('Id', "[]"));
    }else{
        $elt_donnees->appendChild($doc->createElement('Id', $donnees['id']));
    }
    
	$elt_data->appendChild($elt_reponse);
	
	// Mise en forme finale
	$doc->formatOutput = true;
	
	// renvoie le contenu XML
	return $doc->saveXML();
}

// ================================================================================================

// création du flux JSON en sortie
function creerFluxJSON($msg, $donnees)
{
    /* Exemple de code JSON
         {
             "data":{
                "reponse": "authentification incorrecte."
             }
         }
     */
    
    // 2 notations possibles pour créer des tableaux associatifs (la deuxième est en commentaire)
    if ($donnees == []) {
        $elt_data = array(
            "reponse" => $msg,
            "donnees" => "[]"
        );
    }else{
        $elt_data = array(
            "reponse" => $msg,
            "donnees" => $donnees['id']
        );
    }
    // construction de l'élément "data"
    
    
    // construction de la racine
    $elt_racine = ["data" => $elt_data];
//     $elt_racine = array("data" => $elt_data);
    
    // retourne le contenu JSON (l'option JSON_PRETTY_PRINT gère les sauts de ligne et l'indentation)
    return json_encode($elt_racine, JSON_PRETTY_PRINT | JSON_UNESCAPED_UNICODE);
    
}

// ================================================================================================
?>