package operator;

import structure.Expression;
import structure.Operator;
import structure.Value;
import value.IntNumber;

import java.math.BigInteger;
import java.util.List;
import java.util.stream.Collectors;


/**
 * TODO 7: Define operator.Exponent operation
 * It should implement {@link Operator}, and will be used to construct {@link structure.Operation} objects.
 * Hint: BigInteger.pow(int)
 */

public class Exponent implements Operator {
    @Override
    public Value operate(List<Expression> operands) {
        var sum = BigInteger.valueOf(((IntNumber)(operands.get(0).eval())).getVal().intValue());
        for (var operand : operands.stream().skip(1).collect(Collectors.toList())) {
            var value = (IntNumber) operand.eval();
            sum = sum.pow(value.getVal().intValue());
        }
        return new IntNumber(sum);
    }

    @Override
    public String symbol() {
        return "^";
    }
}