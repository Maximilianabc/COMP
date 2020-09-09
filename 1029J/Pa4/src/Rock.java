import greenfoot.*;  // (World, Actor, GreenfootImage, Greenfoot and MouseInfo)

/**
 * The enemy of the game that moves in random direction.
 */
public class Rock extends Actor
{
    /**
     * The constructor of the class which initializes the direction of the rock
     */
    public Rock() {
        super();

        // Randomly turn the rock
        turn(Greenfoot.getRandomNumber(360));
    }

    /**
     * Move the rock
     */
    public void act() 
    {
        // Move the rock in a unit of time
        move(4);

        // Remove the rock if it is out of the world
        World world = getWorld();
        if (getX() < 0 || getX() >= world.getWidth() ||
            getY() < 0 || getY() >= world.getHeight()) {
            world.removeObject(this);
        }
    }    
}
