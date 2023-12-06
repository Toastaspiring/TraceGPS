<?php
namespace api\services;
use modele\DAO;
use DOMDocument;
use modele\Trace;

$dao = new DAO();

// Récupération des données transmises
// la fonction $_GET récupère une donnée passée en paramètre dans l'URL par la méthode GET
// la fonction $_POST récupère une donnée envoyées par la méthode POST
// la fonction $_REQUEST récupère par défaut le contenu des variables $_GET, $_POST, $_COOKIE
if ( empty ($_REQUEST ["pseudo"]) == true)  $pseudo = "";  else   $pseudo = $_REQUEST ["pseudo"];
if ( empty ($_REQUEST ["mdp"]) == true)  $mdpSha1 = "";  else   $mdpSha1 = $_REQUEST ["mdp"];
if ( empty ($_REQUEST ["lang"]) == true) $lang = "";  else $lang = strtolower($_REQUEST ["lang"]);
// "xml" par défaut si le paramètre lang est absent ou incorrect
if ($lang != "json") $lang = "xml";

// initialisation du nombre de réponses
$nbReponses = 0;
$uneTrace = array();

// Contrôle de la présence des paramètres
if ( $pseudo == "" || $mdpSha1 == "" )
{	$msg = "Erreur : données incomplètes.";
    $code_reponse = 400;
}
else
{	if ( $dao->getNiveauConnexion($pseudo, $mdpSha1) == 0 )
    {$msg = "Erreur : authentification incorrecte.";
    $code_reponse = 400;
    }
    else
    {	// récupération des informations de l'utilisateur
        $utilisateur = $dao->getUnUtilisateur($pseudo);
        $code_reponse = 200;
        $lesTraces = sizeof($dao->getToutesLesTraces());
        $newId = $lesTraces+1;
        $uneTrace = new Trace($newId, date("Y-m-d H-i-s"), null, 0, $utilisateur->getId());
        $ok = $dao->creerUneTrace($uneTrace);
        if ( ! $ok ) {
            $msg = "Erreur : problème lors de la création de la trace.";
            $code_reponse = 400;

        }
        else {
            // tout a fonctionné
            $msg = "Trace créée.";
            $code_reponse = 200;
        }
    }
}
// ferme la connexion à MySQL
unset($dao);

// création du flux en sortie
if ($lang == "xml") {
    $content_type = "application/xml; charset=utf-8;";
    $donnees = creerFluxXML($msg, $uneTrace);
}
else {
    $content_type = "application/json; charset=utf-8;";
    $donnees = creerFluxJSON($msg, $uneTrace);
}

$this->envoyerReponse($code_reponse, $content_type, $donnees);
// fin du programme (pour ne pas enchainer sur la fonction qui suit)
exit;

// création du flux XML en sortie
function creerFluxXML($msg, $uneTrace)
{
    $doc = new DOMDocument();
    
    // specifie la version et le type d'encodage
    $doc->version = '1.0';
    $doc->encoding = 'UTF-8';
    
    // crée un commentaire et l'encode en UTF-8
    $elt_commentaire = $doc->createComment('DemarrerEnregistrementParcours');
    // place ce commentaire à la racine du document XML
    $doc->appendChild($elt_commentaire);
    
    // crée l'élément 'data' à la racine du document XML
    $elt_data = $doc->createElement('data');
    $doc->appendChild($elt_data);
    
    // place l'élément 'reponse' dans l'élément 'data'
    $elt_reponse = $doc->createElement('reponse', $msg);
    $elt_data->appendChild($elt_reponse);
    
    // traitement des utilisateurs
    if ($uneTrace) {
        // place l'élément 'donnees' dans l'élément 'data'
        $elt_donnees = $doc->createElement('donnees');
        $elt_data->appendChild($elt_donnees);
            // crée un élément vide 'trace'
            $elt_trace = $doc->createElement('trace');
            // place l'élément 'trace' dans l'élément 'uneTrace'
            //$elt_uneTrace->appendChild($elt_trace);
            $elt_donnees->appendChild($elt_trace);
            
            // crée les éléments enfants de l'élément 'trace'
            $elt_id         = $doc->createElement('id', $uneTrace->getId());
            $elt_trace->appendChild($elt_id);
            
            $elt_dateHeureDebut     = $doc->createElement('dateHeureDebut', $uneTrace->getDateHeureDebut());
            $elt_trace->appendChild($elt_dateHeureDebut);
            
            $elt_terminee    = $doc->createElement('terminee',$uneTrace->getTerminee());
            $elt_trace->appendChild($elt_terminee);
            
            $elt_idUtilisateur    = $doc->createElement('idUtilisateur', $uneTrace->getIdUtilisateur());
            $elt_trace->appendChild($elt_idUtilisateur);
        
    }
    
    // Mise en forme finale
    $doc->formatOutput = true;
    
    // renvoie le contenu XML
    return $doc->saveXML();
}

// création du flux JSON en sortie
function creerFluxJSON($msg, $laTrace)
{
    /* Exemple de code JSON
     {
     "data": {
     "reponse": "Erreur : authentification incorrecte."
     }
     }
     */
    // construction de l'élément "data"
    $elt_data = ["reponse" => $msg];
    
    if($laTrace != null){
        // construction de la racine
        $elt_racine = ["data" => $elt_data];
        
        $laTraceAffichee = array();
        $unObjetTrace = array();
        $unObjetTrace["id"] = $laTrace->getId();
        $unObjetTrace["dateHeureDebut"] = $laTrace->getDateHeureDebut() ;
        $unObjetTrace["terminee"] = 0 ;
        $unObjetTrace["idUtilisateur"] = $laTrace->getIdutilisateur();
        $laTraceAffichee[] = $unObjetTrace;
        
        $elt_laTrace = ["trace" => $laTraceAffichee];
        // construction de l'élément "data"
        $elt_data = ["reponse" => $msg, "donnees" => $elt_laTrace];
    }
    
    $elt_racine = ["data" => $elt_data];
    // retourne le contenu JSON (l'option JSON_PRETTY_PRINT gère les sauts de ligne et l'indentation)
    return json_encode($elt_racine, JSON_PRETTY_PRINT | JSON_UNESCAPED_UNICODE);
}
?>