package ffrkx;

import javax.persistence.*;
import java.io.Serializable;

/**
 * Created by Chris on 5/13/2015.
 */
@Entity
public class Battle implements Serializable {
    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    private int id;

    @Column(name = "dungeon_id")
    private long dungeonId;

    @OneToOne(optional = false)
    @JoinColumn(name = "dungeon_id")
    private Dungeon dungeon;

    @Column(name = "name")
    private String name;

    @Column(name = "stamina")
    private short stamina;

    @Column(name="times_run")
    private int timesRun;

    public Battle(int id, Dungeon dungeon, String name, short stamina, int timesRun) {
        this.id = id;
        this.dungeon = dungeon;
        this.name = name;
        this.stamina = stamina;
        this.timesRun = timesRun;
    }

    public int getId() {
        return id;
    }

    public Dungeon getDungeon() {
        return dungeon;
    }

    public String getName() {
        return name;
    }

    public short getStamina() {
        return stamina;
    }

    public int getTimesRun() {
        return timesRun;
    }
}
