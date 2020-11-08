package structure;

import java.util.List;

/**
 * The interface that defines what an operator should do on the operands.
 */
public interface Operator {
    /**
     * Operate on the list of operands and return the result {@link Value}.
     *
     * For example, {@link operator.Addition} operator defines how to do additions on a list of operands.
     * @param operands a list of operands to operate
     * @return the operation result
     */
    Value operate(List<Expression> operands);

    /**
     * Return a symbol to represent the operator.
     *
     * This is used in {@link Expression#toString()} to print expressions.
     * @return a symbol to represent the operator
     */
    String symbol();
}
