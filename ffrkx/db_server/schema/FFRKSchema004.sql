ALTER TABLE `worlds` DROP COLUMN `realm`,
                     ADD COLUMN `series` INT(10) UNSIGNED NULL AFTER `id`,
                     ADD COLUMN `type` SMALLINT UNSIGNED NOT NULL AFTER `series`,
                     ADD COLUMN `name` VARCHAR(45) NULL AFTER `type`;

DROP procedure IF EXISTS `insert_world_entry`;

DELIMITER $$
CREATE PROCEDURE `insert_world_entry`(
	IN wid INT,
    IN series INT,
    IN type SMALLINT,
    IN name VARCHAR(45))
BEGIN
	# If it fails, it's because there is already an entry with this battle id.
    # Ignore the insertion, as well as this entire operation.
	INSERT IGNORE INTO worlds VALUES (wid, series, type, name);
END
$$

DELIMITER ;

ALTER TABLE `dungeons` DROP COLUMN `synergy`;

INSERT INTO schema_version VALUES (4);
