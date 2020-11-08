package operator;

import structure.Expression;
import structure.Operator;
import structure.Value;
import value.IntNumber;

import java.math.BigInteger;
import java.util.List;
import java.util.stream.Collectors;

/**
 * TODO 6: Define operator Division
 * It should implement {@link Operator}, and will be used to construct {@link structure.Operation} objects.
 * Hint: refer to the operator.Addition class to see how to implement this one.
 * Hint: BigInteger.ONE and BigInteger.divide(BigInteger) are useful
 * Note: if there is only one operand, return 1 / the operand (integer division). Otherwise divide
 * the first operand by the rest of operands.
 */

public class Division implements Operator{
    @Override
    public Value operate(List<Expression> operands) {
        var sum = ((IntNumber)operands.get(0).eval()).getVal();
        if (operands.size() == 1)
        {
            return new IntNumber(BigInteger.ONE.divide(sum));
        }
        for (var operand : operands.stream().skip(1).collect(Collectors.toList())) {
            var value = (IntNumber) operand.eval();
            sum = sum.divide(value.getVal());
        }
        return new IntNumber(sum);
    }

    @Override
    public String symbol() {
        return "/";
    }
}