import structure.Expression;
import structure.Value;
import value.IntNumber;

import java.util.*;
import java.math.BigInteger;

public class Calculator {
    public static void genAndSort(int n) {
        Random rand = new Random();
        ArrayList<Value> nums = new ArrayList<>();
        for (int i = 0; i < n; ++i) {
            nums.add(new IntNumber(new BigInteger(10, rand)));
        }
        System.out.println("Generated numbers:");
        var joiner1 = new StringJoiner(" ");
        nums.forEach(value -> joiner1.add(value.toString()));
        System.out.println(joiner1.toString());

        Collections.sort(nums);

        System.out.println("\nSorted in increasing order: ");
        var joiner2 = new StringJoiner(" ");
        nums.forEach(value -> joiner2.add(value.toString()));
        System.out.println(joiner2.toString());
        System.out.printf("%n%n");
    }

    public static void main(String[] args) {
        genAndSort(10);
        String line;
        try (Scanner reader = new Scanner(System.in)) {
            Parser parser = new Parser();
            System.out.println("Please enter an expression:");
            while (true) {
                System.out.print("> ");
                line = reader.nextLine();
                if (line.isEmpty()) {
                    System.out.println("Goodbye!");
                    break;
                }
                Expression expr;
                try {
                    expr = parser.parse(line);
                } catch (Exception ex) {
                    System.err.println("Invalid expression: " + ex.getMessage());
                    continue;
                }
                System.out.println("expr = " + expr);
                System.out.printf("res = %s%n%n", expr.eval());
            }
        } catch (Exception ex) {
            System.err.println(ex.getMessage());
            ex.printStackTrace();
            System.exit(-1);
        }
    }

}
