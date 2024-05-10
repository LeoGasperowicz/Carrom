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

-- Listage des données de la table carrom.player : ~4 rows (environ)
INSERT INTO `player` (`id`, `name`, `password`) VALUES
	(2, 'Leo', 'Leo1'),
	(3, 'Abdou', 'Abdou2'),
	(4, 'Carlos', 'Carlos3'),
	(5, 'Sophie', 'Soph');

-- Listage des données de la table carrom.score : ~8 rows (environ)
INSERT INTO `score` (`id`, `idP`, `date`, `score`) VALUES
	(1, 2, '2024-04-09 13:40:35', 21),
	(2, 3, '2023-03-10 13:40:56', 12),
	(3, 5, '2024-04-09 06:41:13', 16),
	(4, 4, '2022-03-12 13:41:38', 19),
	(5, 2, '2024-05-10 13:42:02', 5),
	(6, 3, '2024-01-10 13:42:17', 15),
	(7, 4, '2024-02-15 13:42:42', 17),
	(8, 5, '2023-11-13 13:43:11', 14);

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
