package ffrkx;

/**
 * Created by Chris on 5/13/2015.
 */
public class Item {
    private final int id;
    private final String name;
    private final short rarity;
    private final short realm;
    private final short type;
    private final short subtype;

    public Item(int id, String name, short rarity, short realm, short type, short subtype) {
        this.id = id;
        this.name = name;
        this.rarity = rarity;
        this.realm = realm;
        this.type = type;
        this.subtype = subtype;
    }

    public int getId() {
        return id;
    }

    public String getName() {
        return name;
    }

    public short getRarity() {
        return rarity;
    }

    public short getRealm() {
        return realm;
    }

    public short getType() {
        return type;
    }

    public short getSubtype() {
        return subtype;
    }
}
