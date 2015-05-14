package ffrkx;

/**
 * Created by Chris on 5/12/2015.
 */
public class World {
    private final long id;
    private final int series;
    private final String name;
    private final short type;

    public World(long id, int series, String name, short type) {
        this.id = id;
        this.series = series;
        this.name = name;
        this.type = type;
    }

    public long getId() {
        return id;
    }

    public int getSeries() {
        return series;
    }

    public String getName() {
        return name;
    }

    public short getType() {
        return type;
    }
}