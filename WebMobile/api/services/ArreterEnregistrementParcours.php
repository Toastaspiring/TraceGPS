<?php
use DOMDocument;

$dao = new DAO();


if ( empty ($_REQUEST ["pseudo"]) == true)  $pseudo = "";  else   $pseudo = $_REQUEST ["pseudo"];
if ( empty ($_REQUEST ["mdp"]) == true)  $mdpSha1 = "";  else   $mdpSha1 = $_REQUEST ["mdp"];
if ( empty ($_REQUEST ["idTrace"]) == true)  $idTrace = "";  else   $idTrace = $_REQUEST ["idTrace"];
if ( empty ($_REQUEST ["lang"]) == true) $lang = "";  else $lang = strtolower($_REQUEST ["lang"]);
// "xml" par défaut si le paramètre lang est absent ou incorrect
if ($lang != "json") $lang = "xml";

// Contrôle de la présence des paramètres
if ( $pseudo == "" || $mdpSha1 == "" || $idTrace == "" )
{	$msg = "Erreur : données incomplètes.";
    $code_reponse = 400;
}
else
{	// il faut être connecté pour supprimer un utilisateur
    if ( $dao->getNiveauConnexion($pseudo, $mdpSha1) == 0 )
    {   $msg = "Erreur : authentification incorrecte.";
        $code_reponse = 400;
    }   //
	else 
	{	// contrôle d'existence de idTrace
	    $uneTrace = $dao->getUneTrace($idTrace);
	    $unUtilisateur = $dao->getUnUtilisateur($pseudo);
	    if ($uneTrace == null)
	    {  $msg = "Erreur : parcours inexistant.";
            $code_reponse = 401;
	    }
	    else
	    {   // contrôle si l'utilisateur est bien le propriétaire du parcours à terminer
	        if ( $uneTrace->getIdUtilisateur() != $unUtilisateur->getId() ) {
	            $msg = "Erreur : le numéro de trace ne correspond pas à cet utilisateur.";
                $code_reponse = 401;
            }
	        else {
	            // si le parcours est déjà terminé
	            if ( $uneTrace->getTerminee() == 1)
	            {
	                $msg = "Erreur : cette trace est déjà terminée.";
                    $code_reponse = 500;
                }
	            else
	            {
    	            // modification des champs terminee et dateFin de la trace
    	            $ok = $dao->terminerUneTrace($idTrace);
    	            if ( ! $ok ) {
    	                $msg = "Erreur : problème lors de la fin de l'enregistrement de la trace.";
                        $code_reponse = 500;
                    }
                    else {
                        //tout s'est bien passé
                        $msg = "Enregistrement terminé.";
                        $code_reponse = 200;
                    }
	            }
            }
            
	    }
	}
}
// ferme la connexion à MySQL
unset($dao);

// création du flux en sortie
if ($lang == "xml") {
    $content_type = "application/xml; charset=utf-8;";
    $donnees = creerFluxXML($msg);
}
else {
    $content_type = "application/json; charset=utf-8;";
    $donnees = creerFluxJSON($msg);
}

$this->envoyerReponse($code_reponse, $content_type, $donnees);
// fin du programme (pour ne pas enchainer sur la fonction qui suit)
exit;
 


// création du flux XML en sortie
function creerFluxXML($msg)
{	// crée une instance de DOMdocument (DOM : Document Object Model)
	$doc = new DOMDocument();
	
	// specifie la version et le type d'encodage
	$doc->version = '1.0';
	$doc->encoding = 'UTF-8';
	
	// crée un commentaire et l'encode en UTF-8
	$elt_commentaire = $doc->createComment('Service web SupprimerUnUtilisateur - BTS SIO - Lycée De La Salle - Rennes');
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
	return $doc->saveXML();
	
}

// création du flux JSON en sortie
function creerFluxJSON($msg)
{
    /* Exemple de code JSON
    {
        "data": {
            "reponse": "............. (message retourné par le service web) ..............."
        }
    }
    */
    
    // construction de l'élément "data"
    $elt_data = ["reponse" => $msg];
    
    // construction de la racine
    $elt_racine = ["data" => $elt_data];
    
    // retourne le contenu JSON (l'option JSON_PRETTY_PRINT gère les sauts de ligne et l'indentation)
    return json_encode($elt_racine, JSON_PRETTY_PRINT | JSON_UNESCAPED_UNICODE);
}

?>