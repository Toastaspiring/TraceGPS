-- phpMyAdmin SQL Dump
-- version 4.1.14
-- http://www.phpmyadmin.net
--
-- Client :  127.0.0.1
-- Générée le :  Lun 11 Décembre 2017 à 16:24
-- Version du serveur :  5.6.17
-- Version de PHP :  5.5.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;


--
-- Structure de la table 'tracegps_utilisateurs'
--
DROP TABLE IF EXISTS tracegps_utilisateurs;
CREATE TABLE IF NOT EXISTS tracegps_utilisateurs (
  id int(11) NOT NULL AUTO_INCREMENT,
  pseudo varchar(30) NOT NULL,
  mdpSha1 varchar(40) NOT NULL,
  adrMail varchar(75) NOT NULL,
  numTel varchar(14) DEFAULT NULL,
  niveau int(1) NOT NULL DEFAULT '1',
  dateCreation timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (id),
  UNIQUE KEY pseudo (pseudo)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

--
-- Structure de la table 'tracegps_autorisations'
--
DROP TABLE IF EXISTS tracegps_autorisations;
CREATE TABLE IF NOT EXISTS tracegps_autorisations (
  idAutorisant int(11) NOT NULL,
  idAutorise int(11) NOT NULL,
  PRIMARY KEY (idAutorisant,idAutorise)
) ENGINE=InnoDB Default charset=UTF8;


--
-- Structure de la table 'tracegps_traces'
--
DROP TABLE IF EXISTS tracegps_traces;
CREATE TABLE IF NOT EXISTS tracegps_traces (
  id int(11) NOT NULL AUTO_INCREMENT,
  dateDebut timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  dateFin timestamp NULL DEFAULT NULL,
  terminee tinyint(1) NOT NULL,
  idUtilisateur int(11) NOT NULL,
  PRIMARY KEY (id)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;


--
-- Structure de la table 'tracegps_points'
--
DROP TABLE IF EXISTS tracegps_points;
CREATE TABLE IF NOT EXISTS tracegps_points (
  idTrace int(11) NOT NULL,
  id int(11) NOT NULL,
  latitude double NOT NULL,
  longitude double NOT NULL,
  altitude double NOT NULL,
  dateHeure timestamp NOT NULL,
  rythmeCardio int(3) NOT NULL,
  PRIMARY KEY (idTrace,id)
) ENGINE=InnoDB Default charset=UTF8;


--
-- Ajout des clés étrangères
--
Alter table tracegps_autorisations add constraint fkuser1 foreign key (idAutorisant) references tracegps_utilisateurs(id) ;
Alter table tracegps_autorisations add constraint fkuser2 foreign key (idAutorise) references tracegps_utilisateurs(id) ;
Alter table tracegps_traces add constraint fkuser3 foreign key (idUtilisateur) references tracegps_utilisateurs(id) ;
Alter table tracegps_points add constraint fktrace foreign key (idTrace) references tracegps_traces(id) ;


/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

