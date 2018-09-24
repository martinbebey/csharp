using System;

public class Fractions
{
    private int numerator, denominator;

    public int Numerator
    {
        get
        {
            return numerator;
        }

        set
        {
            numerator = value;
        }
    }

    public int Denomnator
    {
        get
        {
            return denominator;
        }

        set
        {
            if(denominator == 0)
            {
                denominator = 1;
            }
        }
    }

    public Fractions()
    {
        numerator = 0;
        denominator = 1;
    }

    public Fractions(int num, int denom)
    {
        if (num != 0 && denom != 0)
        {
            int simplifier;

            if (num < denom)
            {
                simplifier = num;
            }

            else if (denom < num)
            {
                simplifier = denom;
            }

            else
            {
                simplifier = denom;
            }

            if (simplifier >= 0)
            {
                for (int i = simplifier; i > 1; --i)
                {
                    while (num % i == 0 && denom % i == 0)
                    {
                        num /= i;
                        denom /= i;
                    }
                }
            }

            else
            {
                for (int i = simplifier; i < -1; ++i)
                {
                    while (num % i == 0 && denom % i == 0)
                    {
                        num /= i;
                        denom /= i;
                    }
                }
            }
        }
            numerator = num;
            denominator = denom;
        
    }

    private void Reduce()
    {
        if (numerator != 0 && denominator != 0)
        {
            int simplifier;

            if (numerator < denominator)
            {
                simplifier = numerator;
            }

            else if (denominator < numerator)
            {
                simplifier = denominator;
            }

            else
            {
                simplifier = denominator;
            }

            if (simplifier >= 0)
            {
                for (int i = simplifier; i > 1; --i)
                {
                    while (numerator % i == 0 && denominator % i == 0)
                    {
                        numerator /= i;
                        denominator /= i;
                    }
                }
            }

            else
            {
                for (int i = simplifier; i < -1; ++i)
                {
                    while (numerator % i == 0 && denominator % i == 0)
                    {
                        numerator /= i;
                        denominator /= i;
                    }
                }
            }
        }
    }

    public string Print()
    {
        string fraction = numerator + "/" + denominator;
        return fraction;
    }

    public static Fractions OperationMultiply(Fractions fract1, Fractions fract2)
    {
        Fractions fract = new Fractions();
        fract.numerator = fract1.numerator * fract2.numerator;
        fract.denominator = fract1.denominator * fract2.denominator;
        fract.Reduce();

        return fract;
    }
}