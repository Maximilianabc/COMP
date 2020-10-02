import java.util.Arrays;

public class BusCompany
{
    private String name;
    private int numBus;
    private static int numCompanies = 0;
    private int arraySize = 5;
    private Bus[] Buses;

    public BusCompany(String Name)
    {
        name = Name;
        numBus = 0;
        Buses = new Bus[arraySize];
        numCompanies++;
    }

    public String getName()
    {
        return name;
    }

    public boolean createAndAddBus(int id, String model)
    {
        if (numBus == arraySize) return false;
        if (numBus != 0)
        {
            for (int i = 0; i < numBus; i++)
            {
                if (Buses[i].getID() == id) return false;
            }
        }
        numBus++;
        Buses[numBus - 1] = new Bus(id, model);
        return true;
    }

    public void removeAllBusses()
    {
        Arrays.fill(Buses, null);
        numBus = 0;
    }

    public static int getNumCompanies()
    {
        return numCompanies;
    }
}
