package structure;

/**
 * TODO 8: Define Value interface
 * This interface should extend {@link Expression} because a value is also a kind of expression in our calculator.
 * <p>
 * In addition it should also extend {@link Comparable} interface to make the list containing {@link Value} objects
 * can be sorted.
 * <p>
 * Subclasses of {@link Value} should override {@link Object#equals(Object)} and {@link Object#hashCode()} to support
 * equality check between {@link Value} objects.
 */
public interface Value extends Expression, Comparable{

}
