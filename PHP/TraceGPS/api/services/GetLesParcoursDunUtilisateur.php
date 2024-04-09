<?php
 
namespace api\services;
use modele\DAO;
use DOMDocument;
// connexion du serveur web a la base MySQL
$dao = new DAO();
// Recuperation des donnees transmises
$pseudo = ( empty($this->request['pseudo'])) ? "" : $this->request['pseudo'];
$mdpSha1 = ( empty($this->request['mdp'])) ? "" : $this->request['mdp'];
$pseudoConsulte =( empty($this->request['pseudoConsulte'])) ? "" : $this->request['pseudoConsulte'];
$lang = ( empty($this->request['lang'])) ? "" : $this->request['lang'];
 
if ($lang != "json") $lang = "xml";
 
if ($this->getMethodeRequete() != "GET")
{	$msg = "Erreur : methode HTTP incorrecte.";
    $code_reponse = 406;
}
else {
    // Les parametres doivent etre presents
    if ( $pseudo == "" || $mdpSha1 == "" || $pseudoConsulte == "")
    {	$msg = "Erreur : donnees incompletes.";
        $desTraces=null;
        $code_reponse = 400;
    }
    else if ($dao->getUnUtilisateur($pseudoConsulte)==false)
    {	$msg = "Erreur : pseudo consulte inexistant.";
        $desTraces=null;
        $code_reponse = 400;
    }
    else
    {	$niveauConnexion = $dao->getNiveauConnexion($pseudo, $mdpSha1);
    switch ($niveauConnexion)
    {   case 0 :
            $msg = "Erreur : authentification incorrecte.";
            $desTraces=null;
            $code_reponse = 401; break;
        case 1 :
            $lUtilisateur=$dao->getUnUtilisateur($pseudo);
            $utilisateurConsulte=$dao->getUnUtilisateur($pseudoConsulte);
            $desUtilisateursAutorisant=$dao->getLesUtilisateursAutorisant($lUtilisateur->getId());
            $desUtilisateursAutorisant[] = $lUtilisateur;
            foreach ($desUtilisateursAutorisant as $unUtilisateurAutorisant){
                $code_reponse = 400;
            if ($unUtilisateurAutorisant->getId() == $utilisateurConsulte->getId())
            {
                $lesTracesAutorise=$dao->getLesTraces($utilisateurConsulte->getId());
                $desTraces=array();
                foreach ($lesTracesAutorise as $uneTrace)
                {
                    $desTraces[]=$uneTrace;
                }
                if($desTraces ==[])
                {
                    $msg="Aucune trace pour l'utilisateur ".$utilisateurConsulte->getPseudo().".";
                }
                else
                {
                    $msg=strval(sizeof($desTraces))." trace(s) pour l'utilisateur ".$utilisateurConsulte->getPseudo().".";
                }
            }
            }
            if(!isset($msg)){
            $msg="Erreur : vous n'etes pas autorise par cet utilisateur.";
            $desTraces=null;
            $code_reponse = 400;break;}
    }
    }
}
// ferme la connexion a MySQL :
unset($dao);
 
// creation du flux en sortie
if ($lang == "xml") {
    $content_type = "application/xml; charset=utf-8";      // indique le format XML pour la reponse
    $donnees = creerFluxXML($msg,$desTraces);
}
else {
    $content_type = "application/json; charset=utf-8";      // indique le format Json pour la reponse
    $donnees = creerFluxJSON($msg,$desTraces);
}
 
// envoi de la reponse HTTP
$this->envoyerReponse($code_reponse, $content_type, $donnees);
 
// fin du programme (pour ne pas enchainer sur les 2 fonctions qui suivent)
exit;
 
//-------------------------------------------------------------------
 
// creation du flux XML en sortie
function creerFluxXML($msg,$desTraces)
{
    /* Exemple de code XML
<?xml version="1.0" encoding="UTF-8"?>
<!--Service web Connecter - BTS SIO - Lycee De La Salle - Rennes-->
<data>
<reponse>Erreur : donnees incompletes.</reponse>
</data>
     */
    // cree une instance de DOMdocument (DOM : Document Object Model)
    $doc = new DOMDocument();
    // specifie la version et le type d'encodage
    $doc->version = '1.0';
    $doc->encoding = 'UTF-8';
    // cree un commentaire et l'encode en UTF-8
    $elt_commentaire = $doc->createComment('Service web Connecter - BTS SIO - Lycee De La Salle - Rennes');
    // place ce commentaire a la racine du document XML
    $doc->appendChild($elt_commentaire);
    // cree l'element 'data' a la racine du document XML
    $elt_data = $doc->createElement('data');
    $doc->appendChild($elt_data);
    // place l'element 'reponse' juste apres l'element 'data'
    $elt_reponse = $doc->createElement('reponse', $msg);
    $elt_data->appendChild($elt_reponse);
    if($desTraces!=null)
    {
        $elt_donnees = $doc->createElement('donnees');
        $elt_data->appendChild($elt_donnees);
        $elt_lesTraces = $doc->createElement('LesTraces');
        $elt_donnees->appendChild($elt_lesTraces);
        foreach ($desTraces as $uneTrace)
        {
            // place l'element 'donnees' juste apres l'element 'data'

            // place l'element 'trace' juste apres l'element 'donnees'

            $elt_trace=$doc->createElement('trace');
            $elt_lesTraces->appendChild($elt_trace);
            $elt_id = $doc->createElement('id',$uneTrace->getId());
            $elt_trace->appendChild($elt_id);

            $elt_dateHeureDebut = $doc->createElement('dateHeureDebut',$uneTrace->getDateHeureDebut());
            $elt_trace->appendChild($elt_dateHeureDebut);

            $elt_terminee = $doc->createElement('terminee',$uneTrace->getTerminee());
            $elt_trace->appendChild($elt_terminee);

            if ($uneTrace->getDateHeureFin()!=null)
            {
                $elt_dateHeureFin = $doc->createElement('dateHeureFin',$uneTrace->getDateHeureFin());
                $elt_trace->appendChild($elt_dateHeureFin);
            }

            $elt_idUtilisateur = $doc->createElement('idUtilisateur',$uneTrace->getIdUtilisateur());
            $elt_trace->appendChild($elt_idUtilisateur);

        }
    }
    // Mise en forme finale
    $doc->formatOutput = true;
    // renvoie le contenu XML
    return $doc->saveXML();
}
 
// ================================================================================================
 
// creation du flux JSON en sortie
function creerFluxJSON($msg,$desTraces)
{
    /* Exemple de code JSON
     {
     "data":{
     "reponse": "authentification incorrecte."
     }
     }
     */
    if($desTraces!=null)
    {
        $elt_lesTraces=[];
        foreach ($desTraces as $uneTrace)
        {
            $elt_trace=["id"=>$uneTrace->getId(),"dateHeureDebut"=>$uneTrace->getDAteHeureDebut(),"terminee"=>$uneTrace->getTerminee(),"dateHeureFin"=>$uneTrace->getDateHeureFin(),"idUtilisateur"=>$uneTrace->getIdUtilisateur()];
            $elt_lesTraces[]=$elt_trace;
        }

        // 2 notations possibles pour creer des tableaux associatifs (la deuxieme est en commentaire)
        $elt_donnees=["lesTraces"=>$elt_lesTraces];
        $elt_data = ["reponse" => $msg,"donnees"=>$elt_donnees];
    }
    else{$elt_data = ["reponse" => $msg];}
    // construction de l'element "data"
    //     $elt_data = array("reponse" => $msg);
    // construction de la racine
    $elt_racine = ["data" => $elt_data];
    //     $elt_racine = array("data" => $elt_data);
    // retourne le contenu JSON (l'option JSON_PRETTY_PRINT gere les sauts de ligne et l'indentation)
    return json_encode($elt_racine, JSON_PRETTY_PRINT);
}
 
// ================================================================================================