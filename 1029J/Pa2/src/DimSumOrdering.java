import java.util.Scanner;

/**
 * This class creates 4 DimSum dishes, provides an interface for users
 * to order, and print out the order summary after the ordering
 *
 * The four dim sum dishes are:
 *   Barbecued Pork Bun
 *   Shrimp Dumpling
 *   Siu Mai
 *   Spring Roll
 */
public class DimSumOrdering
{
    // This stores the dim sum dishes in the system
    DimSum[] dishes;

    // The constructor of DimSumOrdering
    public DimSumOrdering()
    {
        //
        // Task 2 - Initialize the dim sum dishes in the system
        //
        dishes = new DimSum[4];
        dishes[0] = new DimSum("Barbecued Pork Bun", 25f);
        dishes[1] = new DimSum("Shrimp Dumpling", 38f);
        dishes[2] = new DimSum("Siu Mai", 30f);
        dishes[3] = new DimSum("Spring Roll", 25f);
    }
    
    // Start the ordering system
    public void start() {
        // Prepare a scanner for reading the input
        Scanner scanner = new Scanner(System.in);
        
        int i;
        String choice;
        do {
            // Print the menu
            System.out.println("Welcome to the G/F restaurant!");
            System.out.println("Please order your dim sum dishes:");
            System.out.println();
            
            //
            // Task 2 - Print the choices using a loop
            //
            for (i = 0; i < dishes.length; i++)
            {
                System.out.println(dishes[i].getName() + " - $" + dishes[i].getPrice());
            }

            System.out.println("5) Bill and payment");
            System.out.println();
            System.out.print("Please enter 1, 2, 3, 4 or 5: ");

            // Read the input
            choice = scanner.next();
            
            //
            // Task 2 - Based on the choice add the order
            //
            switch (choice)
            {
                case "1":
                    dishes[0].order(1);
                    break;
                case "2":
                    dishes[1].order(1);
                    break;
                case "3":
                    dishes[2].order(1);
                    break;
                case "4":
                    dishes[3].order(1);
                    break;
                default:
                    break;
            }
            System.out.println();
        } while (!choice.equals("5"));
        
        // Show the bill
        showBill();
    }
    
    // Show the order
    public void showBill() {
        System.out.println("Qty\tAmt\tDish");
        System.out.println("----------------------------------");
        
        //
        // Task 3 - Print the bill summary
        //
        for (int i = 0; i < dishes.length; i++)
        {
            float subtotal = dishes[i].getQuantity() * dishes[i].getPrice();
            System.out.println(dishes[i].getQuantity() + "\t$" + subtotal + "\t" + dishes[i].getName());
        }
        System.out.println("----------------------------------");

        //
        // Task 3 - Calculate and print the total amount
        //
        float total = 0;
        for (int i = 0; i < dishes.length; i++)
        {
            total += dishes[i].getQuantity() * dishes[i].getPrice();
        }
        System.out.println("Bill Total: $" + total);
    }
    
    // The main method to start the program
    public static void main(String[] args) {
        DimSumOrdering ordering = new DimSumOrdering();
        ordering.start();
    }

}
