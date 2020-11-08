package operator;

import structure.Expression;
import structure.Operator;
import structure.Value;
import value.IntNumber;

import java.math.BigInteger;
import java.util.List;


/**
 * TODO 4: Define operator.Multiplication operation
 * It should implement {@link Operator}, and will be used to construct {@link structure.Operation} objects.
 * Hint: refer to the operator.Addition class to see how to implement this one. It's pretty similar.
 * Hint: Use the constant BigInteger.ONE and the method BigInteger.multiply(BigInteger) to implement the eval method
 */
public class Multiplication implements Operator{
    @Override
    public Value operate(List<Expression> operands) {
        var sum = BigInteger.ONE;
        for (var operand : operands) {
            var value = (IntNumber) operand.eval();
            sum = sum.multiply(value.getVal());
        }
        return new IntNumber(sum);
    }

    @Override
    public String symbol() {
        return "*";
    }
}
