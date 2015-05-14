package ffrkx;

/**
 * Created by Chris on 5/13/2015.
 */
public class Dungeon {
    private final int id;
    private final World world;
    private final String name;
    private final short type;
    private final short difficulty;

    public Dungeon(int id, World world, String name, short type, short difficulty) {
        this.id = id;
        this.world = world;
        this.name = name;
        this.type = type;
        this.difficulty = difficulty;
    }

    public int getId() {
        return id;
    }

    public World getWorld() {
        return world;
    }

    public String getName() {
        return name;
    }

    public short getType() {
        return type;
    }

    public short getDifficulty() {
        return difficulty;
    }
}
