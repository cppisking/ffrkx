package ffrkx;

/**
 * Created by Chris on 5/13/2015.
 */
public class Enemy {
    private final int id;
    private final String name;

    public Enemy(int id, String name) {
        this.id = id;
        this.name = name;
    }

    public int getId() {
        return id;
    }

    public String getName() {
        return name;
    }
}
