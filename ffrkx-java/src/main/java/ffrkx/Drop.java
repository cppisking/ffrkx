package ffrkx;

import javax.persistence.Column;
import javax.persistence.Entity;
import java.io.Serializable;

/**
 * Created by Chris on 5/13/2015.
 */
@Entity
public class Drop implements Serializable {
    @Column(nullable = false)
    private Battle battle;

    @Column(nullable = false)
    private Item item;

    @Column(nullable = false)
    private Enemy enemy;

    @Column(nullable = false)
    private int count;

    protected Drop() {
    }

    public Drop(Battle battle, Item item, Enemy enemy, int count) {
        this.battle = battle;
        this.item = item;
        this.enemy = enemy;
        this.count = count;
    }

    public Battle getBattle() {
        return battle;
    }

    public Item getItem() {
        return item;
    }

    public Enemy getEnemy() {
        return enemy;
    }

    public int getCount() {
        return count;
    }
}
