-- phpMyAdmin SQL Dump
-- version 4.0.10.20
-- https://www.phpmyadmin.net
--
-- Počítač: localhost
-- Vygenerováno: Úte 10. dub 2018, 19:10
-- Verze serveru: 5.5.58-38.10
-- Verze PHP: 5.3.3

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Databáze: `bednaja14`
--

-- --------------------------------------------------------

--
-- Struktura tabulky `Json-api`
--
-- Vytvořeno: Pon 27. lis 2017, 14:07
--

CREATE TABLE IF NOT EXISTS `Json-api` (
  `id` int(10) NOT NULL AUTO_INCREMENT,
  `jmeno` varchar(20) COLLATE utf8mb4_bin NOT NULL,
  `prijmeni` varchar(20) COLLATE utf8mb4_bin NOT NULL,
  `rc` int(10) NOT NULL,
  `pohlavi` tinyint(1) NOT NULL,
  `narozeni` date NOT NULL,
  `heslo` varchar(20) COLLATE utf8mb4_bin NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_bin AUTO_INCREMENT=14 ;

-- --------------------------------------------------------

--
-- Struktura tabulky `obednavka`
--
-- Vytvořeno: Pon 27. lis 2017, 14:13
--

CREATE TABLE IF NOT EXISTS `obednavka` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `id_obednavky` int(11) NOT NULL,
  `id_zbozi` int(11) NOT NULL,
  `mnozstvi` int(11) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=4 ;

-- --------------------------------------------------------

--
-- Struktura tabulky `obednavky`
--
-- Vytvořeno: Stř 28. bře 2018, 06:10
--

CREATE TABLE IF NOT EXISTS `obednavky` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `id_majitele` int(11) NOT NULL,
  `datum` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `stav` tinyint(1) NOT NULL,
  `nazev` varchar(20) NOT NULL DEFAULT 'Obednavka',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=5 ;

-- --------------------------------------------------------

--
-- Struktura tabulky `teploty`
--
-- Vytvořeno: Stř 28. úno 2018, 07:59
--

CREATE TABLE IF NOT EXISTS `teploty` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `id_zarizeni` int(5) NOT NULL,
  `teplota` int(5) NOT NULL,
  `ModifiedTime` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `CreatedTime` datetime NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_bin AUTO_INCREMENT=2 ;

-- --------------------------------------------------------

--
-- Struktura tabulky `zbozi`
--
-- Vytvořeno: Pon 27. lis 2017, 12:52
--

CREATE TABLE IF NOT EXISTS `zbozi` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nazev` varchar(20) NOT NULL,
  `cena` int(11) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=3 ;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
