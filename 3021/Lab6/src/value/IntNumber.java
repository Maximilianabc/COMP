package value;

import structure.Value;

import java.math.BigInteger;


/**
 * TODO 2: Define the IntNumber class
 * This class implement {@link Value} (since a integer number is technically an value in expression).
 * There can be other type of values, e.g. float number, but in this lab we only implement integer numbers.
 * <p>
 * In this class, we use {@link BigInteger} as the internal representation of an integer number, which is
 * {@link IntNumber#val} field.
 */
public class IntNumber implements Value {
    private final BigInteger val;

    public IntNumber(BigInteger val) {
        this.val = val;
    }

    public IntNumber(String val) {
        this.val = new BigInteger(val);
    }

    public BigInteger getVal() {
        return val;
    }

    @Override
    public int compareTo(Object o)
    {
        return this.val.compareTo((BigInteger)o);
    }

    @Override
    public Value eval()
    {
        return new IntNumber(val);
    }

    @Override
    public boolean equals(Object o)
    {
        return this.val.equals(((IntNumber)o).val);
    }

    @Override
    public int hashCode()
    {
        return super.hashCode();
    }

    @Override
    public String toString()
    {
        return this.val.toString();
    }
}