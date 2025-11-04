-- phpMyAdmin SQL Dump
-- version 5.2.2
-- https://www.phpmyadmin.net/
--
-- Host: mysql
-- Generation Time: Aug 06, 2025 at 05:55 PM
-- Server version: 8.0.43
-- PHP Version: 8.2.27

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `dmit2025`
--

-- --------------------------------------------------------

--
-- Table structure for table `dkim54_catalogue_admin`
--

CREATE TABLE `dkim54_catalogue_admin` (
  `account_id` int NOT NULL,
  `users` varchar(16) NOT NULL,
  `hashed_pass` varchar(72) NOT NULL,
  `profile_name` varchar(128) DEFAULT 'default.jpeg'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `dkim54_catalogue_admin`
--

INSERT INTO `dkim54_catalogue_admin` (`account_id`, `users`, `hashed_pass`, `profile_name`) VALUES
(1, 'admin', '$2b$12$3OVFYuLif6au0H2SpKg/c.3jBPfYYX20iDOTqfjIjNF0hD3FYxyEm', 'default.jpeg'),
(2, 'instructor', '$2y$12$wklHCIk6f5fkpKAVvJO2wO17MFhJgKXOXoNw59bXWyFBUvpIRUH6K', 'default.jpeg');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `dkim54_catalogue_admin`
--
ALTER TABLE `dkim54_catalogue_admin`
  ADD PRIMARY KEY (`account_id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `dkim54_catalogue_admin`
--
ALTER TABLE `dkim54_catalogue_admin`
  MODIFY `account_id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;



-- phpMyAdmin SQL Dump
-- version 5.2.2
-- https://www.phpmyadmin.net/
--
-- Host: mysql
-- Generation Time: Aug 06, 2025 at 05:55 PM
-- Server version: 8.0.43
-- PHP Version: 8.2.27

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `dmit2025`
--







-- phpMyAdmin SQL Dump
-- version 5.2.2
-- https://www.phpmyadmin.net/
--
-- Host: mysql
-- Generation Time: Aug 08, 2025 at 04:00 AM
-- Server version: 8.0.43
-- PHP Version: 8.2.27

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `dmit2025`
--

-- --------------------------------------------------------

--
-- Table structure for table `dkim54_catalogue_items`
--

CREATE TABLE `dkim54_catalogue_items` (
  `id` int NOT NULL,
  `name` varchar(50) NOT NULL,
  `race` enum('Terran','Zerg','Protoss') NOT NULL,
  `unit_type` enum('Ground','Air','Building') NOT NULL,
  `health` int NOT NULL,
  `shield` int NOT NULL DEFAULT '0',
  `armor` int NOT NULL DEFAULT '0',
  `attack_damage` int NOT NULL,
  `attack_range` int NOT NULL,
  `movement_speed` decimal(4,2) NOT NULL,
  `mineral_cost` int NOT NULL,
  `gas_cost` int NOT NULL,
  `build_time` int NOT NULL,
  `description` varchar(128) NOT NULL,
  `tags` varchar(255) DEFAULT NULL,
  `has_cloak` tinyint(1) NOT NULL DEFAULT '0',
  `img_description` varchar(128) DEFAULT 'Starcraft Unit',
  `filename` varchar(255) NOT NULL DEFAULT 'default.png'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `dkim54_catalogue_items`
--

INSERT INTO `dkim54_catalogue_items` (`id`, `name`, `race`, `unit_type`, `health`, `shield`, `armor`, `attack_damage`, `attack_range`, `movement_speed`, `mineral_cost`, `gas_cost`, `build_time`, `description`, `tags`, `has_cloak`, `img_description`, `filename`) VALUES
(1, 'Marine', 'Terran', 'Ground', 45, 0, 10, 6, 5, 2.25, 50, 0, 18, 'Basic Terran infantry.', 'Basic Terran unit,rifle', 0, 'Standard Terran soldier', '68938c54e08c57.72509078.png'),
(2, 'Firebat', 'Terran', 'Ground', 50, 0, 1, 8, 1, 1.88, 50, 25, 24, 'Short-range flame unit.', 'infantry,flame', 0, 'Fire-wielding infantry', '68938c64bd6a20.60005389.png'),
(3, 'Medic', 'Terran', 'Ground', 60, 0, 1, 0, 0, 2.25, 50, 25, 24, 'Heals biological units.', 'support,healer', 0, 'Healing unit', '68938c70bc2f27.46622405.png'),
(4, 'Siege Tank', 'Terran', 'Ground', 175, 0, 1, 15, 12, 2.25, 150, 100, 45, 'Heavy artillery with siege mode.', 'mechanical,artillery', 0, 'Tank with deployable cannon', '68938c77d97961.74864555.png'),
(5, 'Wraith', 'Terran', 'Air', 120, 0, 0, 8, 6, 3.75, 150, 100, 60, 'Cloakable air unit.', 'air,cloak', 1, 'Light air fighter', '689391520d2447.79707849.png'),
(6, 'Zergling', 'Zerg', 'Ground', 35, 0, 0, 5, 0, 4.13, 25, 0, 17, 'Fast melee unit.', 'melee,fast', 0, 'Tiny zerg melee', '68939164928855.81961785.png'),
(7, 'Hydralisk', 'Zerg', 'Ground', 80, 0, 0, 10, 4, 3.15, 75, 25, 28, 'Versatile ranged unit.', 'ranged,versatile', 0, 'Zerg ranged attacker', '6893916d41e728.95887485.png'),
(8, 'Mutalisk', 'Zerg', 'Air', 120, 0, 0, 9, 3, 4.13, 100, 100, 40, 'Fast, mobile flyer.', 'air,mobile', 0, 'Flying zerg unit', '6893917a2011f1.85142625.png'),
(9, 'Ultralisk', 'Zerg', 'Ground', 500, 0, 5, 35, 1, 3.06, 300, 200, 60, 'Massive melee unit.', 'heavy,melee', 0, 'Large zerg tank', '68939184523a10.71203421.png'),
(10, 'Lurker', 'Zerg', 'Ground', 125, 0, 1, 20, 6, 2.30, 50, 100, 50, 'Burrowing spike attacker.', 'burrow,area damage', 0, 'Stealth ground attacker', '6893919f378d16.74633986.png'),
(11, 'Zealot', 'Protoss', 'Ground', 100, 60, 1, 8, 0, 3.15, 100, 0, 38, 'Basic melee fighter.', 'melee,infantry', 0, 'Close combat protoss', '689391b6cf6717.97655151.png'),
(12, 'Dragoon', 'Protoss', 'Ground', 100, 80, 1, 20, 5, 2.25, 125, 50, 50, 'Ranged ground unit.', 'mechanical,ranged', 0, 'Long-range attacker', '689391c4058d92.72764790.png'),
(13, 'High Templar', 'Protoss', 'Ground', 40, 40, 0, 0, 0, 2.25, 50, 150, 45, 'Casts powerful spells.', 'caster,support', 0, 'Psionic spellcaster', '6893925db1f779.94270394.png'),
(14, 'Dark Templar', 'Protoss', 'Ground', 40, 40, 1, 40, 1, 2.81, 125, 100, 55, 'Cloaked melee attacker.', 'cloak,melee', 1, 'Stealth protoss warrior', '68939268af8de9.59458896.png'),
(15, 'Archon', 'Protoss', 'Ground', 10, 350, 0, 30, 2, 3.06, 100, 0, 12, 'Powerful psionic unit.', 'psionic,splash', 0, 'Energy-based unit', '689392733998f7.90908802.png'),
(16, 'Scout', 'Protoss', 'Air', 150, 100, 0, 14, 4, 4.13, 275, 125, 60, 'Air-to-air and air-to-ground.', 'air,versatile', 0, 'Protoss air scout', '68939281de67b3.69163614.png'),
(17, 'Carrier', 'Protoss', 'Air', 300, 150, 4, 6, 8, 2.63, 350, 250, 120, 'Launches interceptors.', 'capital,air', 0, 'Heavy protoss airship', '68939289cf4630.42455553.png'),
(18, 'Reaver', 'Protoss', 'Ground', 100, 80, 0, 100, 8, 2.25, 200, 100, 55, 'Slow, powerful artillery.', 'artillery,ground', 0, 'Payload-based attacker', '68939295392089.55824942.png'),
(19, 'Observer', 'Protoss', 'Air', 40, 20, 0, 0, 0, 3.38, 25, 75, 30, 'Cloaked flying scout.', 'cloak,scout', 1, 'Invisible recon unit', '6893929e897455.37894739.png'),
(20, 'Arbiter', 'Protoss', 'Air', 200, 150, 1, 10, 3, 2.95, 100, 350, 70, 'Casts stasis and cloak field.', 'support,caster', 1, 'Cloaking support air unit', '689392aebe19e8.24015316.png'),
(28, 'Battlecruiser', 'Terran', 'Air', 500, 0, 3, 25, 6, 1.88, 400, 300, 90, 'Terran capital ship with powerful Yamato Gun.', 'Capital,Heavy,Air', 0, 'Terran Air Unit', '689392bf8c5315.01246531.png'),
(29, 'Guardian', 'Zerg', 'Air', 150, 0, 2, 20, 10, 1.57, 150, 200, 60, 'Long-range flying Zerg siege unit evolved from Mutalisk.', 'Air,Siege,Evolved', 0, 'Zerg Air Unit', '689392c9e04ce6.21936678.png'),
(30, 'Devourer', 'Zerg', 'Air', 250, 0, 2, 25, 5, 1.74, 150, 250, 60, 'Anti-air Zerg unit that slows enemy attacks with acid spores.', 'Air,AntiAir,Evolved', 0, 'Zerg Air Unit', '689392da4bb535.83733468.png'),
(31, 'Valkyrie', 'Terran', 'Air', 200, 0, 1, 6, 5, 3.33, 250, 125, 50, 'Terran anti-air unit with multiple missiles.', 'Air,AntiAir,Fast', 0, 'Terran Air Unit', '689392e65e9c29.53539359.png'),
(32, 'Defiler', 'Zerg', 'Ground', 80, 0, 1, 0, 0, 2.63, 50, 150, 50, 'Zerg spellcaster that uses Dark Swarm and Plague to control the battlefield.', 'Caster,Support,Ground', 0, 'Zerg Ground Unit', '689392f0476d57.94499424.png'),
(33, 'Supply Depot', 'Terran', 'Building', 400, 0, 1, 0, 0, 0.00, 100, 0, 30, 'Provides supply for Terran units.', 'supply,defense', 0, 'Starcraft Terran Supply Depot building', '689576157e14a3.08995354.png');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `dkim54_catalogue_items`
--
ALTER TABLE `dkim54_catalogue_items`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `dkim54_catalogue_items`
--
ALTER TABLE `dkim54_catalogue_items`
  MODIFY `id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=102;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
