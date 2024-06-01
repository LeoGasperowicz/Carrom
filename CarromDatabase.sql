-- --------------------------------------------------------
-- Hôte:                         127.0.0.1
-- Version du serveur:           11.3.2-MariaDB - mariadb.org binary distribution
-- SE du serveur:                Win64
-- HeidiSQL Version:             12.6.0.6765
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Listage de la structure de la base pour carrom
DROP DATABASE IF EXISTS `carrom`;
CREATE DATABASE IF NOT EXISTS `carrom` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci */;
USE `carrom`;

-- Listage de la structure de la table carrom. player
DROP TABLE IF EXISTS `player`;
CREATE TABLE IF NOT EXISTS `player` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(255) DEFAULT NULL,
  `password` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Listage des données de la table carrom.player : ~7 rows (environ)
INSERT INTO `player` (`id`, `name`, `password`) VALUES
	(2, 'Leo', 'Leo1'),
	(3, 'Abdou', 'Abdou2'),
	(4, 'Carlos', 'Carlos3'),
	(5, 'Sophie', 'Soph'),
	(6, 'Isab', 'IsabG'),
	(7, 'nicos', '1234'),
	(8, 'Thomas', '1234');

-- Listage de la structure de la table carrom. score
DROP TABLE IF EXISTS `score`;
CREATE TABLE IF NOT EXISTS `score` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `idP` int(11) DEFAULT NULL,
  `date` datetime DEFAULT NULL,
  `score` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_username` (`idP`),
  CONSTRAINT `fk_username` FOREIGN KEY (`idP`) REFERENCES `player` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Listage des données de la table carrom.score : ~10 rows (environ)
INSERT INTO `score` (`id`, `idP`, `date`, `score`) VALUES
	(1, 2, '2024-04-09 13:40:35', 21),
	(2, 3, '2023-03-10 13:40:56', 12),
	(3, 5, '2024-04-09 06:41:13', 16),
	(4, 4, '2022-03-12 13:41:38', 19),
	(5, 2, '2024-05-10 13:42:02', 5),
	(6, 3, '2024-01-10 13:42:17', 15),
	(7, 4, '2024-02-15 13:42:42', 17),
	(8, 5, '2023-11-13 13:43:11', 14),
	(13, 2, '2024-05-21 19:45:23', 8),
	(14, 6, '2024-05-21 19:45:23', 0);

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
