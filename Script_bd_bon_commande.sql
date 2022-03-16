-- phpMyAdmin SQL Dump
-- version 5.0.2
-- https://www.phpmyadmin.net/
--
-- Hôte : 127.0.0.1:3306
-- Généré le : sam. 05 mars 2022 à 11:59
-- Version du serveur :  5.7.31
-- Version de PHP : 7.3.21

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de données : `bdpdmold`
--

-- --------------------------------------------------------

--
-- Structure de la table `t_bon_commande`
--

DROP TABLE IF EXISTS `t_bon_commande`;
CREATE TABLE IF NOT EXISTS `t_bon_commande` (
  `RefBon` int(11) NOT NULL AUTO_INCREMENT,
  `id_Exercice` int(11) NOT NULL,
  `numero` varchar(20) NOT NULL,
  `date` varchar(20) NOT NULL,
  `attriutaire` varchar(100) NOT NULL,
  `quantite` int(10) NOT NULL,
  `peixUnitaire` decimal(25,0) NOT NULL,
  `montantHT` decimal(25,0) NOT NULL,
  `CodeProjet` varchar(20) NOT NULL,
  PRIMARY KEY (`RefBon`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
