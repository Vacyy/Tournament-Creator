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


