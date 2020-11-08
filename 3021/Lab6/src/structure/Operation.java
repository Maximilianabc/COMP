package structure;

import java.util.ArrayList;


/**
 * TODO 3: Define the class Operation
 * It should implement {@link Expression}
 */

public class Operation implements Expression{
    protected Operator operator;
    protected ArrayList<Expression> operands;

    public Operation(Operator operator, ArrayList<Expression> operands) {
        super();
        this.operator = operator;
        this.operands = operands;
    }

    /**
     * TODO: 3.1
     * An operation has a operator and multiple operands
     * Override the toString method to convert the operation to a string.
     * The format is "(operator operand1 operand2 operand3 ...)".
     * E.g.
     * If the operator is "+", and the operands are "2, 3, 5".
     * Then the string representation of this operation is (+ 2 3 5).
     *
     * @return the string representation of the operation
     */
    @Override
    public String toString() {
        String ops = "";
        for (Expression p : operands)
        {
            ops += " " + p.toString();
        }
        return "(" + operator.symbol() + ops + ")";
    }

    @Override
    public Value eval()
    {
        return operator.operate(operands);
    }
}