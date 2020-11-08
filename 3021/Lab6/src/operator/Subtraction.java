package operator;

import structure.Expression;
import structure.Operator;
import structure.Value;
import value.IntNumber;

import java.math.BigInteger;
import java.util.List;
import java.util.stream.Collectors;

/**
 * TODO 5: Define operator.Subtraction operation
 * It should implement {@link Operator}, and will be used to construct {@link structure.Operation} objects.
 * Hint: refer to the operator.Addition class to see how to implement this one.
 * Hint: BigInteger.subtract(BigInteger) are useful for implementing eval()
 * Note: If the number of operands is 1, return the negation. Otherwise, subtract the rest of the operands from
 * the first operand
 */

public class Subtraction implements Operator{
    @Override
    public Value operate(List<Expression> operands) {
        var sum = ((IntNumber)operands.get(0).eval()).getVal();
        if (operands.size() == 1)
        {
            return new IntNumber(BigInteger.ZERO.subtract(sum));
        }
        for (var operand : operands.stream().skip(1).collect(Collectors.toList())) {
            var value = (IntNumber) operand.eval();
            sum = sum.subtract(value.getVal());
        }
        return new IntNumber(sum);
    }

    @Override
    public String symbol() {
        return "-";
    }
}
