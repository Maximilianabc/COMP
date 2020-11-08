package structure;

/**
 * TODO 1:
 * Create an interface "Expression".
 *
 * It has one public abstract method, eval(), which returns a {@link Value} which
 * represents the calculation result of this expression.
 *
 * Subclasses of {@link Expression} should override {@link Object#toString()}} to make it printable.
 */

public interface Expression {
    Value eval();
}
