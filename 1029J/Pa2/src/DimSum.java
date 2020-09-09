/**
 * This class contains the information of a dim sum dish
 */

public class DimSum
{
    //
    // Task 1 - create the attributes
    //
    // The name of the dim sum
    // The price of the dim sum
    // The quantity ordered
    String Name = "";
    float Price = 0;
    int Quantity = 0;
    
    // Constructor of the class
    public DimSum(String name, float price) {
        //
        // Task 1 - Initialize the attributes of the dim sum
        //
        this.Name = name;
        this.Price = price;
    }
    
    // Return the name of the dim sum
    public String getName() {
        //
        // Task 1 - Return the name of the dim sum
        //
        return this.Name;
    }
    
    // Return the price of the dim sum
    public float getPrice() {
        //
        // Task 1 - Return the price of the dim sum
        //
        return this.Price;
    }
    
    // Return the quantity ordered
    public int getQuantity() {
        //
        // Task 1 - Return the quantity of the dim sum
        // 
        return this.Quantity;
    }
    
    // Order this dim sum dish
    public void order(int quantity) {
        //
        // Task 1 - Increase the quantity ordered
        //
        this.Quantity += quantity;
    }
}
