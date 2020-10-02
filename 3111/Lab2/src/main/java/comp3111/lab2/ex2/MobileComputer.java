package comp3111.lab2.ex2;

/*MobileComputer class needs to implement interface Chargeable
* so that the MobileComputer object, m, can be passed to charge(Chargeable c)
* in Charger class.*/
public class MobileComputer extends Computer implements Chargeable
{
    private int battery;
    public MobileComputer()
    {
        secret = "MobileComputer secret";
        battery = 5;
    }

    @Override
    public void work()
    {
        if (battery > 0)
        {
            System.out.println("It is working on my lap.");
            battery--;
        }
        else
        {
            System.out.println("Running out of battery");
        }
    }
    public void charge()
    {
        if (battery < 10)
            battery++;
    }
}
