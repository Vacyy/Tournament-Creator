-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Cze 24, 2024 at 06:22 PM
-- Wersja serwera: 10.4.32-MariaDB
-- Wersja PHP: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `tournamentdb`
--

DELIMITER $$
--
-- Procedury
--
CREATE DEFINER=`root`@`localhost` PROCEDURE `UpdateWinnerAndNextMatch` (IN `BracketID` INT, IN `tournamentID` INT, IN `labelClicked` INT)   BEGIN
    DECLARE nextBracketID INT;
    DECLARE isEven INT;
    DECLARE matchID INT;
    DECLARE winnerID INT;
    DECLARE round INT;
    DECLARE teamToReplace INT; 
    DECLARE done INT DEFAULT 0;
    DECLARE curMatchID INT;
    DECLARE curTeamID1 INT;
    DECLARE curTeamID2 INT;
    DECLARE curWinnerID INT;

    -- Declare cursor for iterating over the matches
    DECLARE cur CURSOR FOR 
        SELECT match_ID, Team_ID_1, Team_ID_2, Winner_ID
        FROM matches
        WHERE tournament_ID = tournamentID AND bracket_ID > (nextBracketID-1);

    -- Declare continue handler for cursor
    DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1;

    -- Get match_ID of the current match
    SET matchID = (SELECT match_ID FROM matches WHERE tournament_ID = tournamentID AND bracket_id = BracketID);

    -- Calculate next bracket ID
    SET nextBracketID = 8 + (BracketID DIV 2);

    -- Determine if the current bracketID is even or odd
    SET isEven = (BracketID MOD 2);

    -- Update the winner of the current match
    IF labelClicked = 1 THEN
        -- If label 1 was clicked, set Team_ID_1 as the winner
        UPDATE matches
        SET Winner_ID = Team_ID_1
        WHERE match_id = matchID;
        SET teamToReplace = (SELECT Team_ID_2 FROM matches WHERE match_id = matchID);
    ELSE
        -- If label 2 was clicked, set Team_ID_2 as the winner 
        UPDATE matches
        SET Winner_ID = Team_ID_2
        WHERE match_id = matchID;
        SET teamToReplace = (SELECT Team_ID_1 FROM matches WHERE match_id = matchID);
    END IF;

    -- Get the winnerID that was assigned
    SET winnerID = (SELECT Winner_ID FROM matches WHERE match_ID = matchID);

    -- Assign the winner to the next match
    IF isEven = 0 THEN
        -- If bracketID is even, assign to Team_ID_1
        UPDATE matches
        SET Team_ID_1 = winnerID
        WHERE bracket_id = nextBracketID AND Tournament_ID = tournamentID;
    ELSE
        -- If bracketID is odd, assign to Team_ID_2
        UPDATE matches
        SET Team_ID_2 = winnerID
        WHERE bracket_id = nextBracketID AND Tournament_ID = tournamentID;
    END IF;

    -- Open cursor
    OPEN cur;

    -- Loop through the matches and replace the losing team if found
    read_loop: LOOP
        FETCH cur INTO curMatchID, curTeamID1, curTeamID2, curWinnerID;

        IF done THEN
            LEAVE read_loop;
        END IF;

        -- Replace the losing team with the winner in subsequent matches
        IF curTeamID1 = teamToReplace THEN
            UPDATE matches
            SET Team_ID_1 = winnerID
            WHERE match_ID = curMatchID;
        END IF;

        IF curTeamID2 = teamToReplace THEN
            UPDATE matches
            SET Team_ID_2 = winnerID
            WHERE match_ID = curMatchID;
        END IF;

        -- Replace the winning team if it matches the team to replace
        IF curWinnerID = teamToReplace THEN
            UPDATE matches
            SET Winner_ID = winnerID
            WHERE match_ID = curMatchID;
        END IF;
    END LOOP;

    -- Close cursor
    CLOSE cur;
END$$

DELIMITER ;

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `matches`
--

CREATE TABLE `matches` (
  `Match_ID` int(11) NOT NULL,
  `Tournament_ID` int(11) NOT NULL,
  `Round` int(11) NOT NULL,
  `Team_ID_1` int(11) DEFAULT NULL,
  `Team_ID_2` int(11) DEFAULT NULL,
  `Winner_ID` int(11) DEFAULT NULL,
  `bracket_id` int(5) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `matches`
--

INSERT INTO `matches` (`Match_ID`, `Tournament_ID`, `Round`, `Team_ID_1`, `Team_ID_2`, `Winner_ID`, `bracket_id`) VALUES
(634, 10, 1, 104, 100, NULL, 0),
(635, 10, 1, 102, 105, NULL, 1),
(636, 10, 1, 103, 99, NULL, 2),
(637, 10, 1, 106, 101, NULL, 3),
(638, 10, 2, NULL, NULL, NULL, 8),
(639, 10, 2, NULL, NULL, NULL, 9),
(640, 10, 3, NULL, NULL, NULL, 12),
(650, 8, 1, 81, 71, 81, 0),
(651, 8, 1, 73, 86, 86, 1),
(652, 8, 1, 85, 83, 83, 2),
(653, 8, 1, 82, 77, 82, 3),
(654, 8, 1, 80, 76, 76, 4),
(655, 8, 1, 79, 78, 78, 5),
(656, 8, 1, 74, 75, 74, 6),
(657, 8, 1, 72, 84, 72, 7),
(658, 8, 2, 81, 86, 86, 8),
(659, 8, 2, 83, 82, 82, 9),
(660, 8, 2, 76, 78, NULL, 10),
(661, 8, 2, 74, 72, NULL, 11),
(662, 8, 3, 86, 82, NULL, 12),
(663, 8, 3, NULL, NULL, NULL, 13),
(664, 8, 4, NULL, NULL, NULL, 14);

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `team`
--

CREATE TABLE `team` (
  `Team_ID` int(100) NOT NULL,
  `Name` varchar(30) NOT NULL,
  `Tournament_ID` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `team`
--

INSERT INTO `team` (`Team_ID`, `Name`, `Tournament_ID`) VALUES
(71, 'Francja', 8),
(72, 'Hiszpania', 8),
(73, 'Anglia', 8),
(74, 'Szkocja', 8),
(75, 'Szwecja', 8),
(76, 'Portugalia', 8),
(77, 'Niemcy', 8),
(78, 'Włochy', 8),
(79, 'Belgia', 8),
(80, 'Niderlandy', 8),
(81, 'Szwajcaria', 8),
(82, 'Chorwacja', 8),
(83, 'Ukraina', 8),
(84, 'Dania', 8),
(85, 'Turcja', 8),
(86, 'Austria', 8),
(99, 'Team A', 10),
(100, 'Team B', 10),
(101, 'Team C', 10),
(102, 'Team D', 10),
(103, 'Team E', 10),
(104, 'Team F', 10),
(105, 'Team G', 10),
(106, 'Team H', 10);

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `tournament`
--

CREATE TABLE `tournament` (
  `Tournament_ID` int(11) NOT NULL,
  `Name` varchar(60) NOT NULL,
  `Teams_Counter` int(11) NOT NULL,
  `User_ID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tournament`
--

INSERT INTO `tournament` (`Tournament_ID`, `Name`, `Teams_Counter`, `User_ID`) VALUES
(8, 'Mistrzostwa Europy 2024 w Siatkówkę', 16, 2),
(10, 'Szkolny turniej w piłkę ręczną', 8, 2);

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `users`
--

CREATE TABLE `users` (
  `user_ID` int(11) NOT NULL,
  `username` varchar(16) NOT NULL,
  `email` varchar(320) NOT NULL,
  `password` char(64) NOT NULL,
  `Admin` tinyint(1) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`user_ID`, `username`, `email`, `password`, `Admin`) VALUES
(2, 'Vacyy', 'muj@pies.com', 'ecc32f25bb31e943a39339abee1d733b1e05356f928b9ba27a9ab05d177bad51', 0),
(4, 'Kamczito', 'przykladowy-mail@gmail.com', 'd8ed8ca27d83a63df6982905ea53b4613b9d7974edcee06f301cf43d63177f47', 0),
(9, 'admin', 'admin@admin.com', '8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918', 1);

--
-- Indeksy dla zrzutów tabel
--

--
-- Indeksy dla tabeli `matches`
--
ALTER TABLE `matches`
  ADD PRIMARY KEY (`Match_ID`),
  ADD KEY `Team_ID_1` (`Team_ID_1`),
  ADD KEY `Team_ID_2` (`Team_ID_2`),
  ADD KEY `Tournament_ID` (`Tournament_ID`),
  ADD KEY `Winner_ID` (`Winner_ID`);

--
-- Indeksy dla tabeli `team`
--
ALTER TABLE `team`
  ADD PRIMARY KEY (`Team_ID`),
  ADD KEY `Tournament_ID` (`Tournament_ID`);

--
-- Indeksy dla tabeli `tournament`
--
ALTER TABLE `tournament`
  ADD PRIMARY KEY (`Tournament_ID`),
  ADD KEY `User_ID` (`User_ID`);

--
-- Indeksy dla tabeli `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`user_ID`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `matches`
--
ALTER TABLE `matches`
  MODIFY `Match_ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=665;

--
-- AUTO_INCREMENT for table `team`
--
ALTER TABLE `team`
  MODIFY `Team_ID` int(100) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=268;

--
-- AUTO_INCREMENT for table `tournament`
--
ALTER TABLE `tournament`
  MODIFY `Tournament_ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=40;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `user_ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `matches`
--
ALTER TABLE `matches`
  ADD CONSTRAINT `matches_ibfk_1` FOREIGN KEY (`Team_ID_1`) REFERENCES `team` (`Team_ID`) ON DELETE CASCADE,
  ADD CONSTRAINT `matches_ibfk_2` FOREIGN KEY (`Team_ID_2`) REFERENCES `team` (`Team_ID`) ON DELETE CASCADE,
  ADD CONSTRAINT `matches_ibfk_3` FOREIGN KEY (`Tournament_ID`) REFERENCES `tournament` (`Tournament_ID`) ON DELETE CASCADE,
  ADD CONSTRAINT `matches_ibfk_4` FOREIGN KEY (`Winner_ID`) REFERENCES `team` (`Team_ID`) ON DELETE CASCADE;

--
-- Constraints for table `team`
--
ALTER TABLE `team`
  ADD CONSTRAINT `team_ibfk_1` FOREIGN KEY (`Tournament_ID`) REFERENCES `tournament` (`Tournament_ID`) ON DELETE CASCADE;

--
-- Constraints for table `tournament`
--
ALTER TABLE `tournament`
  ADD CONSTRAINT `tournament_ibfk_1` FOREIGN KEY (`User_ID`) REFERENCES `users` (`user_ID`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
