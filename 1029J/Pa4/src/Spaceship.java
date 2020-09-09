import greenfoot.*;  // (World, Actor, GreenfootImage, Greenfoot and MouseInfo)

/**
 * The main actor of the game that can be controlled by the player.
 */
public class Spaceship extends Actor
{
    /**
     * Move the spaceship
     */
    public void act() 
    {
        // Rotate the spaceship based on the key pressed
        if (Greenfoot.isKeyDown("left")) {
            turn(-3);
        }
        else if (Greenfoot.isKeyDown("right")) {
            turn(3);
        }

        // Move the spaceship based on the key pressed
        if (Greenfoot.isKeyDown("up")) {
            move(4);
        }
        
        //
        // Task 2 - Shoot the bullet
        //
        
        // If the player presses the spacebar key
        //    if there is currently no bullet in the game
        //        add a bullet

        // Check if a rock hits the spaceship
        if (!getObjectsInRange(60, Rock.class).isEmpty()) {
            // Show the explosion
            Explosion explosion = new Explosion();
            getWorld().addObject(explosion, getX(), getY());
            
            // Remove the spaceship
            getWorld().removeObject(this);
            
            // Stop the game
            Greenfoot.stop();
        }
    }    
}
