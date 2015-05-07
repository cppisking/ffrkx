# This script adds dungeon entries for all of the daily dungeons, as well as fixes a few synergy values.

START TRANSACTION;

# Synergy used to be non-nullable, but to support but "Core" synergy as
# well as no synergy at all, we need it to be nullable.
ALTER TABLE `dungeons` 
CHANGE COLUMN `synergy` `synergy` TINYINT(3) UNSIGNED NULL DEFAULT NULL;

DROP procedure IF EXISTS `record_dungeon_entry`;
DROP procedure IF EXISTS `insert_dungeon_entry`;

DELIMITER $$
CREATE PROCEDURE `insert_dungeon_entry`(
	IN did INT,
    IN world_id INT,
    IN dname VARCHAR(45),
    IN dtype TINYINT,
    IN ddiff SMALLINT,
    IN dsyn TINYINT)
BEGIN
	# If it fails, it's because there is already an entry with this dungeon id.
    # Ignore the insertion, as well as this entire operation.
	INSERT IGNORE INTO dungeons VALUES (did, world_id, dname, dtype, ddiff, dsyn);
END$$
DELIMITER ;

DROP procedure IF EXISTS `insert_battle_entry`;

DELIMITER $$
CREATE PROCEDURE `insert_battle_entry` (
	IN bid INT,
    IN did INT,
    IN bname VARCHAR(45),
    IN bstam SMALLINT)
BEGIN
	# If it fails, it's because there is already an entry with this battle id.
    # Ignore the insertion, as well as this entire operation.
	INSERT IGNORE INTO battles VALUES (bid, did, bname, bstam, 0);
END$$
DELIMITER ;

# Fix a few incorrect synergy values
UPDATE `dungeons` SET `synergy`='6', `world`='106003' WHERE `id`='606011';
UPDATE `dungeons` SET `synergy`='6', `world`='106003' WHERE `id`='606012';
UPDATE `dungeons` SET `synergy`='6', `world`='106003' WHERE `id`='606013';

COMMIT;
