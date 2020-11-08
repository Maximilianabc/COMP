package operator;

import structure.Expression;
import structure.Operator;
import structure.Value;
import value.IntNumber;

import java.math.BigInteger;
import java.util.List;

public class Addition implements Operator {
    @Override
    public Value operate(List<Expression> operands) {
        var sum = BigInteger.ZERO;
        for (var operand : operands) {
            var value = (IntNumber) operand.eval();
            sum = sum.add(value.getVal());
        }
        return new IntNumber(sum);
    }

    @Override
    public String symbol() {
        return "+";
    }


}
