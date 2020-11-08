import operator.*;
import structure.Expression;
import structure.Operation;
import value.IntNumber;

import java.io.IOException;
import java.math.BigInteger;
import java.util.ArrayList;

public class Parser {

    public Expression parse(String line) throws IOException {
        var tokens = new ArrayList<String>();
        for (var i = 0; i < line.length(); ++i) {
            var c = line.charAt(i);
            if (c == ' ' || c == '\t')
                continue;
            if (c == '(' || c == ')')
                tokens.add(String.valueOf(c));
            else {
                var token = String.valueOf(c);
                for (; i + 1 < line.length(); ++i) {
                    c = line.charAt(i + 1);
                    if (c == ' ' || c == '\t' || c == '(' || c == ')') {
                        break;
                    } else {
                        token += c;
                    }
                }
                tokens.add(token);
            }
        }
        if (tokens.get(0).equals("(") && tokens.get(tokens.size() - 1).equals(")")) {
            return parse(tokens, 1, tokens.size() - 1);
        } else if (tokens.size() == 1 && tokens.get(0).matches("[\\+-]?\\d+")) {
            return new IntNumber(new BigInteger(tokens.get(0)));
        } else {
            throw new IOException("Invalid expression");
        }
    }

    private Expression parse(ArrayList<String> tokens, int first, int last) throws IOException {
        var operator = tokens.get(first);
        var operands = new ArrayList<Expression>();
        for (var i = first + 1; i < last; ++i) {
            var token = tokens.get(i);
            Expression operand;
            if (token.equals("(")) {
                var numLeft = 1;
                var j = i + 1;
                for (; numLeft != 0 && j < last; ++j) {
                    if (tokens.get(j).equals("("))
                        ++numLeft;
                    else if (tokens.get(j).equals(")"))
                        --numLeft;
                }
                if (numLeft != 0)
                    throw new IOException("Parentheses mismatch");
                operand = parse(tokens, i + 1, j - 1);
                i = j - 1;
            } else if (token.matches("[\\+-]?\\d+")) {
                operand = new IntNumber(new BigInteger(token));
            } else {
                throw new IOException("Invalid operand " + token);
            }
            operands.add(operand);
        }
        Expression expr;
        switch (operator) {
            case "+" -> {
                if (operands.size() == 0) {
                    throw new IOException("The + operator expects more than 1 operand");
                }
                expr = new Operation(new Addition(), operands);
            }
            case "*" -> {
                if (operands.size() == 0) {
                    throw new IOException("The * operator expects more than 1 operand");
                }
                expr = new Operation(new Multiplication(), operands);
            }
            case "-" -> {
                if (operands.size() == 0) {
                    throw new IOException("The - operator expects more than 1 operand");
                }
                expr = new Operation(new Subtraction(), operands);
            }
            case "/" -> {
                if (operands.size() == 0) {
                    throw new IOException("The / operator expects more than 1 operand");
                }
                expr = new Operation(new Division(), operands);
            }
            case "^" -> {
                if (operands.size() != 2) {
                    throw new IOException("The ^ operator expects exactly 2 operands");
                }
                expr = new Operation(new Exponent(), operands);
            }
            default -> throw new IOException("Unsupported operator " + operator);
        }
        return expr;

    }
}
