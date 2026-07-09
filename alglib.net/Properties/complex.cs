using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

public partial class alglib
{
    public partial struct complex : IEquatable<complex>, IFormattable
    {
        public readonly bool Equals(complex value) => this == value;
        public override readonly string ToString() => ToString(null, null);
        public readonly string ToString([StringSyntax(StringSyntaxAttribute.NumericFormat)] string format) => ToString(format, null);
        public readonly string ToString(IFormatProvider provider) => ToString(null, provider);
        public readonly string ToString([StringSyntax(StringSyntaxAttribute.NumericFormat)] string format, IFormatProvider provider)
        {
#if NET6_0_OR_GREATER
            System.Runtime.CompilerServices.DefaultInterpolatedStringHandler handler = new(4, 2, provider, stackalloc char[512]);
            handler.AppendLiteral("<");
            handler.AppendFormatted(x, format);
            handler.AppendLiteral("; ");
            handler.AppendFormatted(y, format);
            handler.AppendLiteral(">");
            return handler.ToStringAndClear();
#else
            return $"<{x.ToString(format, provider)}; {y.ToString(format, provider)}>";
#endif
        }
    }

#if !NETFRAMEWORK || NET40_OR_GREATER
    public partial struct complex
    {
        public complex(System.Numerics.Complex complex) : this(complex.Real, complex.Imaginary) { }

        public static implicit operator System.Numerics.Complex(complex complex) => new complex(complex.x, complex.y);
        public static implicit operator complex(System.Numerics.Complex complex) => new complex(complex);
    }
#endif

#if NET7_0_OR_GREATER
    public partial struct complex
        : System.Numerics.IAdditionOperators<complex, complex, complex>,
        System.Numerics.IAdditiveIdentity<complex, complex>,
        System.Numerics.IDecrementOperators<complex>,
        System.Numerics.IDivisionOperators<complex, complex, complex>,
        System.Numerics.IEqualityOperators<complex, complex, bool>,
        System.Numerics.IIncrementOperators<complex>,
        System.Numerics.IMultiplicativeIdentity<complex, complex>,
        System.Numerics.IMultiplyOperators<complex, complex, complex>,
        System.Numerics.ISubtractionOperators<complex, complex, complex>,
        System.Numerics.IUnaryNegationOperators<complex, complex>,
        System.Numerics.IUnaryPlusOperators<complex, complex>
    {
        static complex System.Numerics.IAdditiveIdentity<complex, complex>.AdditiveIdentity => new(0.0, 0.0);
        public static complex operator --(complex value) => value with { x = value.x - 1 };
        public static complex operator ++(complex value) => value with { x = value.x + 1 };
        static complex System.Numerics.IMultiplicativeIdentity<complex, complex>.MultiplicativeIdentity => new(1.0, 0.0);
    }
#endif
}
