# Increase the same of some of the text fields.  45 characters
# apparently isn't long enough for battles, worlds, or dungeons.

ALTER TABLE `battles` 
CHANGE COLUMN `name` `name` VARCHAR(100) NOT NULL ;

ALTER TABLE `dungeons` 
CHANGE COLUMN `name` `name` VARCHAR(100) NOT NULL ;

ALTER TABLE `worlds` 
CHANGE COLUMN `name` `name` VARCHAR(100) NULL DEFAULT NULL ;

DROP procedure IF EXISTS `insert_battle_entry`;
DROP procedure IF EXISTS `insert_world_entry`;
DROP procedure IF EXISTS `insert_dungeon_entry`;

DELIMITER $$
CREATE PROCEDURE `insert_battle_entry`(
	IN bid INT,
    IN did INT,
    IN bname VARCHAR(100),
    IN bstam SMALLINT)
BEGIN
	# If it fails, it's because there is already an entry with this battle id.
    # Ignore the insertion, as well as this entire operation.
	INSERT IGNORE INTO battles VALUES (bid, did, bname, bstam, 0);
END$$

CREATE PROCEDURE `insert_world_entry`(
	IN wid INT,
    IN series INT,
    IN type SMALLINT,
    IN name VARCHAR(100))
BEGIN
	# If it fails, it's because there is already an entry with this battle id.
    # Ignore the insertion, as well as this entire operation.
	INSERT IGNORE INTO worlds VALUES (wid, series, type, name);
END$$

CREATE PROCEDURE `insert_dungeon_entry`(
	IN did INT,
    IN world_id INT,
    IN series_id INT,
    IN dname VARCHAR(100),
    IN dtype TINYINT,
    IN ddiff SMALLINT,
    IN dsyn TINYINT)
BEGIN
	# If it fails, it's because there is already an entry with this dungeon id.
    # Ignore the insertion, as well as this entire operation.
	INSERT IGNORE INTO dungeons VALUES (did, world_id, series_id, dname, dtype, ddiff, dsyn);
END$$

DELIMITER ;

INSERT INTO schema_version VALUES (7);