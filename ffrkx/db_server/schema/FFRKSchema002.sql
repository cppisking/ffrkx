DROP procedure IF EXISTS `wipe_drops`;

DELIMITER $$
CREATE PROCEDURE `wipe_drops` ()
BEGIN
	TRUNCATE TABLE drops;
    UPDATE battles SET times_run=0;
END
$$

DELIMITER ;

