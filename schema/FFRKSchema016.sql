# There's not really a schema change here, but I need people to update
# the client so that they stop recording bogus magicite data.

INSERT INTO schema_version VALUES (16, true);