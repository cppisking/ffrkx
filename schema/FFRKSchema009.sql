
# Remove the `dungeons`.`synergy` column and associated procedure.alter
ALTER TABLE `dungeons` DROP COLUMN `synergy`;

DROP procedure IF EXISTS `insert_dungeon_entry`;
DELIMITER $$
CREATE PROCEDURE `insert_dungeon_entry`(
	IN did INT,
    IN world_id INT,
    IN series_id INT,
    IN dname VARCHAR(100),
    IN dtype TINYINT,
    IN ddiff SMALLINT)
BEGIN
	# If it fails, it's because there is already an entry with this dungeon id.
    # Ignore the insertion, as well as this entire operation.
	INSERT IGNORE INTO dungeons VALUES (did, world_id, series_id, dname, dtype, ddiff);
END$$

DELIMITER ;

INSERT INTO `schema_version` VALUES (9);
