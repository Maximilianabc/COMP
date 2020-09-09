import greenfoot.*;  // (World, Actor, GreenfootImage, Greenfoot and MouseInfo)

/**
 * Write a description of class Bullet here.
 * 
 * @author (your name) 
 * @version (a version number or a date)
 */
public class Bullet extends Actor
{
    public Bullet()
    {
        super();
    }
    /**
     * Act - do whatever the Bullet wants to do. This method is called whenever
     * the 'Act' or 'Run' button gets pressed in the environment.
     */
    public void act() 
    {
        move(6);  
        World world = getWorld();
        if (!getObjectsInRange(20, Rock.class).isEmpty()) {
            Rock rock = (Rock) getObjectsInRange(20, Rock.class).get(0);
            world.removeObject(rock);
            world.removeObject(this);
            Score score = (Score)world.getObjects(Score.class).get(0);
            score.increase();
            return;
        }
        if (getX() < 0 || getX() >= world.getWidth() ||
            getY() < 0 || getY() >= world.getHeight()) {
            world.removeObject(this);
        }
    }    
}
