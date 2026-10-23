// Decompiled with JetBrains decompiler
// Type: System.Char
// Assembly: System.Private.CoreLib, Version=10.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e
// MVID: 6FD3D614-6A59-49A2-8EE6-17A3289020A2
// Assembly location: /usr/share/dotnet/shared/Microsoft.NETCore.App/10.0.13/System.Private.CoreLib.dll
// XML documentation location: /usr/share/dotnet/packs/Microsoft.NETCore.App.Ref/10.0.13/ref/net10.0/System.Runtime.xml

using System.Buffers;
using System.Buffers.Binary;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Text;

#nullable enable
namespace System;

/// <summary>Represents a character as a UTF-16 code unit.</summary>
[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
[Serializable]
public readonly struct Char : 
  IComparable,
  IComparable<char>,
  IEquatable<char>,
  IConvertible,
  ISpanFormattable,
  IFormattable,
  IBinaryInteger<char>,
  IBinaryNumber<char>,
  IBitwiseOperators<char, char, char>,
  INumber<char>,
  IComparisonOperators<char, char, bool>,
  IEqualityOperators<char, char, bool>,
  IModulusOperators<char, char, char>,
  INumberBase<char>,
  IAdditionOperators<char, char, char>,
  IAdditiveIdentity<char, char>,
  IDecrementOperators<char>,
  IDivisionOperators<char, char, char>,
  IIncrementOperators<char>,
  IMultiplicativeIdentity<char, char>,
  IMultiplyOperators<char, char, char>,
  ISpanParsable<char>,
  IParsable<char>,
  ISubtractionOperators<char, char, char>,
  IUnaryPlusOperators<char, char>,
  IUnaryNegationOperators<char, char>,
  IUtf8SpanFormattable,
  IUtf8SpanParsable<char>,
  IShiftOperators<char, int, char>,
  IMinMaxValue<char>,
  IUnsignedNumber<char>,
  IUtfChar<char>,
  IBinaryIntegerParseAndFormatInfo<char>
{
  private readonly char m_value;
  /// <summary>Represents the largest possible value of a <see cref="T:System.Char" />. This field is constant.</summary>
  public const char MaxValue = '\uFFFF';
  /// <summary>Represents the smallest possible value of a <see cref="T:System.Char" />. This field is constant.</summary>
  public const char MinValue = '\0';

  private static unsafe ReadOnlySpan<byte> Latin1CharInfo
  {
    get
    {
      return new ReadOnlySpan<byte>((void*) &\u003CPrivateImplementationDetails\u003E.\u0031D715D2A2ED1CDD8C368F519DF4B8B9748F65E031AEA80652432FBBA5C35DFE6, 256 /*0x0100*/);
    }
  }

  private static bool IsLatin1(char c) => (uint) c < (uint) char.Latin1CharInfo.Length;

  /// <summary>Returns <see langword="true" /> if <paramref name="c" /> is an ASCII character ([ U+0000..U+007F ]).</summary>
  /// <param name="c">The character to analyze.</param>
  /// <returns>
  /// <see langword="true" /> if <paramref name="c" /> is an ASCII character; <see langword="false" /> otherwise.</returns>
  public static bool IsAscii(char c) => c <= '\u007F';

  private static UnicodeCategory GetLatin1UnicodeCategory(char c)
  {
    return (UnicodeCategory) ((int) char.Latin1CharInfo[(int) c] & 31 /*0x1F*/);
  }

  /// <summary>Returns the hash code for this instance.</summary>
  /// <returns>A 32-bit signed integer hash code.</returns>
  public override int GetHashCode() => (int) this | (int) this << 16 /*0x10*/;

  /// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
  /// <param name="obj">An object to compare with this instance or <see langword="null" />.</param>
  /// <returns>
  /// <see langword="true" /> if <paramref name="obj" /> is an instance of <see cref="T:System.Char" /> and equals the value of this instance; otherwise, <see langword="false" />.</returns>
  public override bool Equals([NotNullWhen(true)] object? obj)
  {
    return obj is char ch && (int) this == (int) ch;
  }

  /// <summary>Returns a value that indicates whether this instance is equal to the specified <see cref="T:System.Char" /> object.</summary>
  /// <param name="obj">An object to compare to this instance.</param>
  /// <returns>
  /// <see langword="true" /> if the <paramref name="obj" /> parameter equals the value of this instance; otherwise, <see langword="false" />.</returns>
  [NonVersionable]
  public bool Equals(char obj) => (int) this == (int) obj;

  /// <summary>Compares this instance to a specified object and indicates whether this instance precedes, follows, or appears in the same position in the sort order as the specified <see cref="T:System.Object" />.</summary>
  /// <param name="value">An object to compare this instance to, or <see langword="null" />.</param>
  /// <exception cref="T:System.ArgumentException">
  /// <paramref name="value" /> is not a <see cref="T:System.Char" /> object.</exception>
  /// <returns>A signed number indicating the position of this instance in the sort order in relation to the <paramref name="value" /> parameter.
  /// 
  /// <list type="table"><listheader><term> Return Value</term><description> Description</description></listheader><item><term> Less than zero</term><description> This instance precedes <paramref name="value" />.</description></item><item><term> Zero</term><description> This instance has the same position in the sort order as <paramref name="value" />.</description></item><item><term> Greater than zero</term><description> This instance follows <paramref name="value" />.
  /// 
  /// -or-
  /// 
  /// <paramref name="value" /> is <see langword="null" />.</description></item></list></returns>
  public int CompareTo(object? value)
  {
    if (value == null)
      return 1;
    if (!(value is char ch))
      throw new ArgumentException(SR.Arg_MustBeChar);
    return (int) this - (int) ch;
  }

  /// <summary>Compares this instance to a specified <see cref="T:System.Char" /> object and indicates whether this instance precedes, follows, or appears in the same position in the sort order as the specified <see cref="T:System.Char" /> object.</summary>
  /// <param name="value">A <see cref="T:System.Char" /> object to compare.</param>
  /// <returns>A signed number indicating the position of this instance in the sort order in relation to the <paramref name="value" /> parameter.
  /// 
  /// <list type="table"><listheader><term> Return Value</term><description> Description</description></listheader><item><term> Less than zero</term><description> This instance precedes <paramref name="value" />.</description></item><item><term> Zero</term><description> This instance has the same position in the sort order as <paramref name="value" />.</description></item><item><term> Greater than zero</term><description> This instance follows <paramref name="value" />.</description></item></list></returns>
  public int CompareTo(char value) => (int) this - (int) value;

  /// <summary>Converts the value of this instance to its equivalent string representation.</summary>
  /// <returns>The string representation of the value of this instance.</returns>
  public override string ToString() => char.ToString(this);

  /// <summary>Converts the value of this instance to its equivalent string representation using the specified culture-specific format information.</summary>
  /// <param name="provider">(Reserved) An object that supplies culture-specific formatting information.</param>
  /// <returns>The string representation of the value of this instance as specified by <paramref name="provider" />.</returns>
  public string ToString(IFormatProvider? provider) => char.ToString(this);

  /// <summary>Converts the specified Unicode character to its equivalent string representation.</summary>
  /// <param name="c">The Unicode character to convert.</param>
  /// <returns>The string representation of the value of <paramref name="c" />.</returns>
  public static string ToString(char c) => string.CreateFromChar(c);

  #nullable disable
  /// <summary>Tries to format the value of the current instance into the provided span of characters.</summary>
  /// <param name="destination">The span in which to write this instance's value formatted as a span of characters.</param>
  /// <param name="charsWritten">When this method returns, the number of characters that were written in <paramref name="destination" />.</param>
  /// <param name="format">A span containing the characters that represent a standard or custom format string that defines the acceptable format for <paramref name="destination" />.</param>
  /// <param name="provider">An optional object that supplies culture-specific formatting information for <paramref name="destination" />.</param>
  /// <returns>
  /// <see langword="true" /> if the formatting was successful; otherwise, <see langword="false" />.</returns>
  bool ISpanFormattable.TryFormat(
    Span<char> destination,
    out int charsWritten,
    ReadOnlySpan<char> format,
    IFormatProvider provider)
  {
    if (!destination.IsEmpty)
    {
      destination[0] = this;
      charsWritten = 1;
      return true;
    }
    charsWritten = 0;
    return false;
  }

  /// <summary>Tries to format the value of the current instance as UTF-8 into the provided span of bytes.</summary>
  /// <param name="utf8Destination">The span in which to write this instance's value formatted as a span of bytes.</param>
  /// <param name="bytesWritten">When this method returns, contains the number of bytes that were written in <paramref name="utf8Destination" />.</param>
  /// <param name="format">A span containing the characters that represent a standard or custom format string that defines the acceptable format for <paramref name="utf8Destination" />.</param>
  /// <param name="provider">An optional object that supplies culture-specific formatting information for <paramref name="utf8Destination" />.</param>
  /// <returns>
  /// <see langword="true" /> if the formatting was successful; otherwise, <see langword="false" />.</returns>
  bool IUtf8SpanFormattable.TryFormat(
    Span<byte> utf8Destination,
    out int bytesWritten,
    ReadOnlySpan<char> format,
    IFormatProvider provider)
  {
    return new Rune(this).TryEncodeToUtf8(utf8Destination, out bytesWritten);
  }

  /// <summary>Formats the value of the current instance using the specified format.</summary>
  /// <param name="format">The format to use.
  /// -or-
  /// A <see langword="null" /> reference (<see langword="Nothing" /> in Visual Basic) to use the default format defined for the type of the <see cref="T:System.IFormattable" /> implementation.</param>
  /// <param name="formatProvider">The provider to use to format the value.
  /// -or-
  /// A <see langword="null" /> reference (<see langword="Nothing" /> in Visual Basic) to obtain the numeric format information from the current locale setting of the operating system.</param>
  /// <returns>The value of the current instance in the specified format.</returns>
  string IFormattable.ToString(string format, IFormatProvider formatProvider)
  {
    return char.ToString(this);
  }

  #nullable enable
  /// <summary>Converts the value of the specified string to its equivalent Unicode character.</summary>
  /// <param name="s">A string that contains a single character, or <see langword="null" />.</param>
  /// <exception cref="T:System.ArgumentNullException">
  /// <paramref name="s" /> is <see langword="null" />.</exception>
  /// <exception cref="T:System.FormatException">The length of <paramref name="s" /> is not 1.</exception>
  /// <returns>A Unicode character equivalent to the sole character in <paramref name="s" />.</returns>
  public static char Parse(string s)
  {
    if (s == null)
      ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
    return char.Parse(s.AsSpan());
  }

  #nullable disable
  internal static char Parse(ReadOnlySpan<char> s)
  {
    if (s.Length != 1)
      ThrowHelper.ThrowFormatException_NeedSingleChar();
    return s[0];
  }

  #nullable enable
  /// <summary>Converts the value of the specified string to its equivalent Unicode character. A return code indicates whether the conversion succeeded or failed.</summary>
  /// <param name="s">A string that contains a single character, or <see langword="null" />.</param>
  /// <param name="result">When this method returns, contains a Unicode character equivalent to the sole character in <paramref name="s" />, if the conversion succeeded, or an undefined value if the conversion failed. The conversion fails if the <paramref name="s" /> parameter is <see langword="null" /> or the length of <paramref name="s" /> is not 1. This parameter is passed uninitialized.</param>
  /// <returns>
  /// <see langword="true" /> if the <paramref name="s" /> parameter was converted successfully; otherwise, <see langword="false" />.</returns>
  public static bool TryParse([NotNullWhen(true)] string? s, out char result)
  {
    if (s != null)
      return char.TryParse(s.AsSpan(), out result);
    result = char.MinValue;
    return false;
  }

  #nullable disable
  internal static bool TryParse(ReadOnlySpan<char> s, out char result)
  {
    if (s.Length != 1)
    {
      result = char.MinValue;
      return false;
    }
    result = s[0];
    return true;
  }

  /// <param name="utf8Text" />
  /// <param name="provider" />
  static char IUtf8SpanParsable<char>.Parse(ReadOnlySpan<byte> utf8Text, IFormatProvider provider)
  {
    Rune result;
    int bytesConsumed;
    if (Rune.DecodeFromUtf8(utf8Text, out result, out bytesConsumed) != OperationStatus.Done || bytesConsumed != utf8Text.Length)
      ThrowHelper.ThrowFormatInvalidString();
    if (!result.IsBmp)
      Number.ThrowOverflowException<char>();
    return (char) result.Value;
  }

  /// <param name="utf8Text" />
  /// <param name="provider" />
  /// <param name="result" />
  static bool IUtf8SpanParsable<char>.TryParse(
    ReadOnlySpan<byte> utf8Text,
    IFormatProvider provider,
    out char result)
  {
    Rune result1;
    int bytesConsumed;
    if (Rune.DecodeFromUtf8(utf8Text, out result1, out bytesConsumed) != OperationStatus.Done || bytesConsumed != utf8Text.Length || !result1.IsBmp)
    {
      result = char.MinValue;
      return false;
    }
    result = (char) result1.Value;
    return true;
  }

  /// <summary>Indicates whether a character is categorized as an ASCII letter.</summary>
  /// <param name="c">The character to evaluate.</param>
  /// <returns>
  /// <see langword="true" /> if <paramref name="c" /> is an ASCII letter; otherwise, <see langword="false" />.</returns>
  public static bool IsAsciiLetter(char c) => (uint) (((int) c | 32 /*0x20*/) - 97) <= 25U;

  /// <summary>Indicates whether a character is categorized as a lowercase ASCII letter.</summary>
  /// <param name="c">The character to evaluate.</param>
  /// <returns>
  /// <see langword="true" /> if <paramref name="c" /> is a lowercase ASCII letter; otherwise, <see langword="false" />.</returns>
  public static bool IsAsciiLetterLower(char c) => char.IsBetween(c, 'a', 'z');

  /// <summary>Indicates whether a character is categorized as an uppercase ASCII letter.</summary>
  /// <param name="c">The character to evaluate.</param>
  /// <returns>
  /// <see langword="true" /> if <paramref name="c" /> is an uppercase ASCII letter; otherwise, <see langword="false" />.</returns>
  public static bool IsAsciiLetterUpper(char c) => char.IsBetween(c, 'A', 'Z');

  /// <summary>Indicates whether a character is categorized as an ASCII digit.</summary>
  /// <param name="c">The character to evaluate.</param>
  /// <returns>
  /// <see langword="true" /> if <paramref name="c" /> is an ASCII digit; otherwise, <see langword="false" />.</returns>
  public static bool IsAsciiDigit(char c) => char.IsBetween(c, '0', '9');

  /// <summary>Indicates whether a character is categorized as an ASCII letter or digit.</summary>
  /// <param name="c">The character to evaluate.</param>
  /// <returns>
  /// <see langword="true" /> if <paramref name="c" /> is an ASCII letter or digit; otherwise, <see langword="false" />.</returns>
  public static bool IsAsciiLetterOrDigit(char c)
  {
    return char.IsAsciiLetter(c) | char.IsBetween(c, '0', '9');
  }

  /// <summary>Indicates whether a character is categorized as an ASCII hexademical digit.</summary>
  /// <param name="c">The character to evaluate.</param>
  /// <returns>
  /// <see langword="true" /> if <paramref name="c" /> is a hexademical digit; otherwise, <see langword="false" />.</returns>
  public static bool IsAsciiHexDigit(char c) => HexConverter.IsHexChar((int) c);

  /// <summary>Indicates whether a character is categorized as an ASCII upper-case hexademical digit.</summary>
  /// <param name="c">The character to evaluate.</param>
  /// <returns>
  /// <see langword="true" /> if <paramref name="c" /> is a hexademical digit; otherwise, <see langword="false" />.</returns>
  public static bool IsAsciiHexDigitUpper(char c) => HexConverter.IsHexUpperChar((int) c);

  /// <summary>Indicates whether a character is categorized as an ASCII lower-case hexademical digit.</summary>
  /// <param name="c">The character to evaluate.</param>
  /// <returns>
  /// <see langword="true" /> if <paramref name="c" /> is a lower-case hexademical digit; otherwise, <see langword="false" />.</returns>
  public static bool IsAsciiHexDigitLower(char c) => HexConverter.IsHexLowerChar((int) c);

  /// <summary>Indicates whether the specified Unicode character is categorized as a decimal digit.</summary>
  /// <param name="c">The Unicode character to evaluate.</param>
  /// <returns>
  /// <see langword="true" /> if <paramref name="c" /> is a decimal digit; otherwise, <see langword="false" />.</returns>
  public static bool IsDigit(char c)
  {
    return char.IsLatin1(c) ? char.IsBetween(c, '0', '9') : CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.DecimalDigitNumber;
  }

  /// <summary>Indicates whether a character is within the specified inclusive range.</summary>
  /// <param name="c">The character to evaluate.</param>
  /// <param name="minInclusive">The lower bound, inclusive.</param>
  /// <param name="maxInclusive">The upper bound, inclusive.</param>
  /// <returns>
  /// <see langword="true" /> if <paramref name="c" /> is within the specified range; otherwise, <see langword="false" />.</returns>
  public static bool IsBetween(char c, char minInclusive, char maxInclusive)
  {
    return (uint) c - (uint) minInclusive <= (uint) maxInclusive - (uint) minInclusive;
  }

  private static bool IsBetween(UnicodeCategory c, UnicodeCategory min, UnicodeCategory max)
  {
    return (uint) (c - min) <= (uint) (max - min);
  }

  internal static bool CheckLetter(UnicodeCategory uc)
  {
    return char.IsBetween(uc, UnicodeCategory.UppercaseLetter, UnicodeCategory.OtherLetter);
  }

  /// <summary>Indicates whether the specified Unicode character is categorized as a Unicode letter.</summary>
  /// <param name="c">The Unicode character to evaluate.</param>
  /// <returns>
  /// <see langword="true" /> if <paramref name="c" /> is a letter; otherwise, <see langword="false" />.</returns>
  public static bool IsLetter(char c)
  {
    return char.IsAscii(c) ? ((uint) char.Latin1CharInfo[(int) c] & 96U /*0x60*/) > 0U : char.CheckLetter(CharUnicodeInfo.GetUnicodeCategory(c));
  }

  private static bool IsWhiteSpaceLatin1(char c)
  {
    return ((uint) char.Latin1CharInfo[(int) c] & 128U /*0x80*/) > 0U;
  }

  /// <summary>Indicates whether the specified Unicode character is categorized as white space.</summary>
  /// <param name="c">The Unicode character to evaluate.</param>
  /// <returns>
  /// <see langword="true" /> if <paramref name="c" /> is white space; otherwise, <see langword="false" />.</returns>
  public static bool IsWhiteSpace(char c)
  {
    return char.IsLatin1(c) ? char.IsWhiteSpaceLatin1(c) : CharUnicodeInfo.GetIsWhiteSpace(c);
  }

  /// <summary>Indicates whether the specified Unicode character is categorized as an uppercase letter.</summary>
  /// <param name="c">The Unicode character to evaluate.</param>
  /// <returns>
  /// <see langword="true" /> if <paramref name="c" /> is an uppercase letter; otherwise, <see langword="false" />.</returns>
  public static bool IsUpper(char c)
  {
    return char.IsLatin1(c) ? ((uint) char.Latin1CharInfo[(int) c] & 64U /*0x40*/) > 0U : CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.UppercaseLetter;
  }

  /// <summary>Indicates whether the specified Unicode character is categorized as a lowercase letter.</summary>
  /// <param name="c">The Unicode character to evaluate.</param>
  /// <returns>
  /// <see langword="true" /> if <paramref name="c" /> is a lowercase letter; otherwise, <see langword="false" />.</returns>
  public static bool IsLower(char c)
  {
    return char.IsLatin1(c) ? ((uint) char.Latin1CharInfo[(int) c] & 32U /*0x20*/) > 0U : CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.LowercaseLetter;
  }

  internal static bool CheckPunctuation(UnicodeCategory uc)
  {
    return char.IsBetween(uc, UnicodeCategory.ConnectorPunctuation, UnicodeCategory.OtherPunctuation);
  }

  /// <summary>Indicates whether the specified Unicode character is categorized as a punctuation mark.</summary>
  /// <param name="c">The Unicode character to evaluate.</param>
  /// <returns>
  /// <see langword="true" /> if <paramref name="c" /> is a punctuation mark; otherwise, <see langword="false" />.</returns>
  public static bool IsPunctuation(char c)
  {
    return char.CheckPunctuation(char.IsLatin1(c) ? char.GetLatin1UnicodeCategory(c) : CharUnicodeInfo.GetUnicodeCategory(c));
  }

  internal static bool CheckLetterOrDigit(UnicodeCategory uc)
  {
    return (287 & 1 << (int) (uc & (UnicodeCategory.Format | UnicodeCategory.Surrogate))) != 0;
  }

  /// <summary>Indicates whether the specified Unicode character is categorized as a letter or a decimal digit.</summary>
  /// <param name="c">The Unicode character to evaluate.</param>
  /// <returns>
  /// <see langword="true" /> if <paramref name="c" /> is a letter or a decimal digit; otherwise, <see langword="false" />.</returns>
  public static bool IsLetterOrDigit(char c)
  {
    return char.CheckLetterOrDigit(char.IsLatin1(c) ? char.GetLatin1UnicodeCategory(c) : CharUnicodeInfo.GetUnicodeCategory(c));
  }

  #nullable enable
  /// <summary>Converts the value of a specified Unicode character to its uppercase equivalent using specified culture-specific formatting information.</summary>
  /// <param name="c">The Unicode character to convert.</param>
  /// <param name="culture">An object that supplies culture-specific casing rules.</param>
  /// <exception cref="T:System.ArgumentNullException">
  /// <paramref name="culture" /> is <see langword="null" />.</exception>
  /// <returns>The uppercase equivalent of <paramref name="c" />, modified according to <paramref name="culture" />, or the unchanged value of <paramref name="c" /> if <paramref name="c" /> is already uppercase, has no uppercase equivalent, or is not alphabetic.</returns>
  public static char ToUpper(char c, CultureInfo culture)
  {
    if (culture == null)
      ThrowHelper.ThrowArgumentNullException(ExceptionArgument.culture);
    return culture.TextInfo.ToUpper(c);
  }

  /// <summary>Converts the value of a Unicode character to its uppercase equivalent.</summary>
  /// <param name="c">The Unicode character to convert.</param>
  /// <returns>The uppercase equivalent of <paramref name="c" />, or the unchanged value of <paramref name="c" /> if <paramref name="c" /> is already uppercase, has no uppercase equivalent, or is not alphabetic.</returns>
  public static char ToUpper(char c) => CultureInfo.CurrentCulture.TextInfo.ToUpper(c);

  /// <summary>Converts the value of a Unicode character to its uppercase equivalent using the casing rules of the invariant culture.</summary>
  /// <param name="c">The Unicode character to convert.</param>
  /// <returns>The uppercase equivalent of the <paramref name="c" /> parameter, or the unchanged value of <paramref name="c" />, if <paramref name="c" /> is already uppercase or not alphabetic.</returns>
  public static char ToUpperInvariant(char c) => TextInfo.ToUpperInvariant(c);

  /// <summary>Converts the value of a specified Unicode character to its lowercase equivalent using specified culture-specific formatting information.</summary>
  /// <param name="c">The Unicode character to convert.</param>
  /// <param name="culture">An object that supplies culture-specific casing rules.</param>
  /// <exception cref="T:System.ArgumentNullException">
  /// <paramref name="culture" /> is <see langword="null" />.</exception>
  /// <returns>The lowercase equivalent of <paramref name="c" />, modified according to <paramref name="culture" />, or the unchanged value of <paramref name="c" />, if <paramref name="c" /> is already lowercase or not alphabetic.</returns>
  public static char ToLower(char c, CultureInfo culture)
  {
    if (culture == null)
      ThrowHelper.ThrowArgumentNullException(ExceptionArgument.culture);
    return culture.TextInfo.ToLower(c);
  }

  /// <summary>Converts the value of a Unicode character to its lowercase equivalent.</summary>
  /// <param name="c">The Unicode character to convert.</param>
  /// <returns>The lowercase equivalent of <paramref name="c" />, or the unchanged value of <paramref name="c" />, if <paramref name="c" /> is already lowercase or not alphabetic.</returns>
  public static char ToLower(char c) => CultureInfo.CurrentCulture.TextInfo.ToLower(c);

  /// <summary>Converts the value of a Unicode character to its lowercase equivalent using the casing rules of the invariant culture.</summary>
  /// <param name="c">The Unicode character to convert.</param>
  /// <returns>The lowercase equivalent of the <paramref name="c" /> parameter, or the unchanged value of <paramref name="c" />, if <paramref name="c" /> is already lowercase or not alphabetic.</returns>
  public static char ToLowerInvariant(char c) => TextInfo.ToLowerInvariant(c);

  /// <summary>Returns the <see cref="T:System.TypeCode" /> for value type <see cref="T:System.Char" />.</summary>
  /// <returns>The enumerated constant, <see cref="F:System.TypeCode.Char" />.</returns>
  public TypeCode GetTypeCode() => TypeCode.Char;

  #nullable disable
  /// <summary>Note This conversion is not supported. Attempting to do so throws an <see cref="T:System.InvalidCastException" />.</summary>
  /// <param name="provider">This parameter is ignored.</param>
  /// <exception cref="T:System.InvalidCastException">This conversion is not supported.</exception>
  /// <returns>This conversion is not supported. No value is returned.</returns>
  bool IConvertible.ToBoolean(IFormatProvider provider)
  {
    throw new InvalidCastException(SR.Format(SR.InvalidCast_FromTo, (object) nameof (Char), (object) "Boolean"));
  }

  /// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToChar(System.IFormatProvider)" />.</summary>
  /// <param name="provider">This parameter is ignored.</param>
  /// <returns>The value of the current <see cref="T:System.Char" /> object unchanged.</returns>
  char IConvertible.ToChar(IFormatProvider provider) => this;

  /// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToSByte(System.IFormatProvider)" />.</summary>
  /// <param name="provider">This parameter is ignored.</param>
  /// <returns>The converted value of the current <see cref="T:System.Char" /> object.</returns>
  sbyte IConvertible.ToSByte(IFormatProvider provider) => Convert.ToSByte(this);

  /// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToByte(System.IFormatProvider)" />.</summary>
  /// <param name="provider">This parameter is ignored.</param>
  /// <returns>The converted value of the current <see cref="T:System.Char" /> object.</returns>
  byte IConvertible.ToByte(IFormatProvider provider) => Convert.ToByte(this);

  /// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt16(System.IFormatProvider)" />.</summary>
  /// <param name="provider">This parameter is ignored.</param>
  /// <returns>The converted value of the current <see cref="T:System.Char" /> object.</returns>
  short IConvertible.ToInt16(IFormatProvider provider) => Convert.ToInt16(this);

  /// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt16(System.IFormatProvider)" />.</summary>
  /// <param name="provider">An <see cref="T:System.IFormatProvider" /> object. (Specify <see langword="null" /> because the <paramref name="provider" /> parameter is ignored.)</param>
  /// <returns>The converted value of the current <see cref="T:System.Char" /> object.</returns>
  ushort IConvertible.ToUInt16(IFormatProvider provider) => Convert.ToUInt16(this);

  /// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt32(System.IFormatProvider)" />.</summary>
  /// <param name="provider">This parameter is ignored.</param>
  /// <returns>The converted value of the current <see cref="T:System.Char" /> object.</returns>
  int IConvertible.ToInt32(IFormatProvider provider) => Convert.ToInt32(this);

  /// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt32(System.IFormatProvider)" />.</summary>
  /// <param name="provider">An <see cref="T:System.IFormatProvider" /> object. (Specify <see langword="null" /> because the <paramref name="provider" /> parameter is ignored.)</param>
  /// <returns>The converted value of the current <see cref="T:System.Char" /> object.</returns>
  uint IConvertible.ToUInt32(IFormatProvider provider) => Convert.ToUInt32(this);

  /// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt64(System.IFormatProvider)" />.</summary>
  /// <param name="provider">This parameter is ignored.</param>
  /// <returns>The converted value of the current <see cref="T:System.Char" /> object.</returns>
  long IConvertible.ToInt64(IFormatProvider provider) => Convert.ToInt64(this);

  /// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt64(System.IFormatProvider)" />.</summary>
  /// <param name="provider">An <see cref="T:System.IFormatProvider" /> object. (Specify <see langword="null" /> because the <paramref name="provider" /> parameter is ignored.)</param>
  /// <returns>The converted value of the current <see cref="T:System.Char" /> object.</returns>
  ulong IConvertible.ToUInt64(IFormatProvider provider) => Convert.ToUInt64(this);

  /// <summary>Note This conversion is not supported. Attempting to do so throws an <see cref="T:System.InvalidCastException" />.</summary>
  /// <param name="provider">This parameter is ignored.</param>
  /// <exception cref="T:System.InvalidCastException">This conversion is not supported.</exception>
  /// <returns>No value is returned.</returns>
  float IConvertible.ToSingle(IFormatProvider provider)
  {
    throw new InvalidCastException(SR.Format(SR.InvalidCast_FromTo, (object) nameof (Char), (object) "Single"));
  }

  /// <summary>Note This conversion is not supported. Attempting to do so throws an <see cref="T:System.InvalidCastException" />.</summary>
  /// <param name="provider">This parameter is ignored.</param>
  /// <exception cref="T:System.InvalidCastException">This conversion is not supported.</exception>
  /// <returns>No value is returned.</returns>
  double IConvertible.ToDouble(IFormatProvider provider)
  {
    throw new InvalidCastException(SR.Format(SR.InvalidCast_FromTo, (object) nameof (Char), (object) "Double"));
  }

  /// <summary>Note This conversion is not supported. Attempting to do so throws an <see cref="T:System.InvalidCastException" />.</summary>
  /// <param name="provider">This parameter is ignored.</param>
  /// <exception cref="T:System.InvalidCastException">This conversion is not supported.</exception>
  /// <returns>No value is returned.</returns>
  Decimal IConvertible.ToDecimal(IFormatProvider provider)
  {
    throw new InvalidCastException(SR.Format(SR.InvalidCast_FromTo, (object) nameof (Char), (object) "Decimal"));
  }

  /// <summary>Note This conversion is not supported. Attempting to do so throws an <see cref="T:System.InvalidCastException" />.</summary>
  /// <param name="provider">This parameter is ignored.</param>
  /// <exception cref="T:System.InvalidCastException">This conversion is not supported.</exception>
  /// <returns>No value is returned.</returns>
  DateTime IConvertible.ToDateTime(IFormatProvider provider)
  {
    throw new InvalidCastException(SR.Format(SR.InvalidCast_FromTo, (object) nameof (Char), (object) "DateTime"));
  }

  /// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToType(System.Type,System.IFormatProvider)" />.</summary>
  /// <param name="type">A <see cref="T:System.Type" /> object.</param>
  /// <param name="provider">An <see cref="T:System.IFormatProvider" /> object.</param>
  /// <exception cref="T:System.ArgumentNullException">
  /// <paramref name="type" /> is <see langword="null" />.</exception>
  /// <exception cref="T:System.InvalidCastException">The value of the current <see cref="T:System.Char" /> object cannot be converted to the type specified by the <paramref name="type" /> parameter.</exception>
  /// <returns>An object of the specified type.</returns>
  object IConvertible.ToType(Type type, IFormatProvider provider)
  {
    return Convert.DefaultToType((IConvertible) this, type, provider);
  }

  /// <summary>Indicates whether the specified Unicode character is categorized as a control character.</summary>
  /// <param name="c">The Unicode character to evaluate.</param>
  /// <returns>
  /// <see langword="true" /> if <paramref name="c" /> is a control character; otherwise, <see langword="false" />.</returns>
  public static bool IsControl(char c) => (uint) ((int) c + 1 & -129) <= 32U /*0x20*/;

  #nullable enable
  /// <summary>Indicates whether the character at the specified position in a specified string is categorized as a control character.</summary>
  /// <param name="s">A string.</param>
  /// <param name="index">The position of the character to evaluate in <paramref name="s" />.</param>
  /// <exception cref="T:System.ArgumentNullException">
  /// <paramref name="s" /> is <see langword="null" />.</exception>
  /// <exception cref="T:System.ArgumentOutOfRangeException">
  /// <paramref name="index" /> is less than zero or greater than the last position in <paramref name="s" />.</exception>
  /// <returns>
  /// <see langword="true" /> if the character at position <paramref name="index" /> in <paramref name="s" /> is a control character; otherwise, <see langword="false" />.</returns>
  public static bool IsControl(string s, int index)
  {
    if (s == null)
      ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
    if ((uint) index >= (uint) s.Length)
      ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index);
    return char.IsControl(s[index]);
  }

  /// <summary>Indicates whether the character at the specified position in a specified string is categorized as a decimal digit.</summary>
  /// <param name="s">A string.</param>
  /// <param name="index">The position of the character to evaluate in <paramref name="s" />.</param>
  /// <exception cref="T:System.ArgumentNullException">
  /// <paramref name="s" /> is <see langword="null" />.</exception>
  /// <exception cref="T:System.ArgumentOutOfRangeException">
  /// <paramref name="index" /> is less than zero or greater than the last position in <paramref name="s" />.</exception>
  /// <returns>
  /// <see langword="true" /> if the character at position <paramref name="index" /> in <paramref name="s" /> is a decimal digit; otherwise, <see langword="false" />.</returns>
  public static bool IsDigit(string s, int index)
  {
    if (s == null)
      ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
    if ((uint) index >= (uint) s.Length)
      ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index);
    char c = s[index];
    return char.IsLatin1(c) ? char.IsBetween(c, '0', '9') : CharUnicodeInfo.GetUnicodeCategoryInternal(s, index) == UnicodeCategory.DecimalDigitNumber;
  }

  /// <summary>Indicates whether the character at the specified position in a specified string is categorized as a Unicode letter.</summary>
  /// <param name="s">A string.</param>
  /// <param name="index">The position of the character to evaluate in <paramref name="s" />.</param>
  /// <exception cref="T:System.ArgumentNullException">
  /// <paramref name="s" /> is <see langword="null" />.</exception>
  /// <exception cref="T:System.ArgumentOutOfRangeException">
  /// <paramref name="index" /> is less than zero or greater than the last position in <paramref name="s" />.</exception>
  /// <returns>
  /// <see langword="true" /> if the character at position <paramref name="index" /> in <paramref name="s" /> is a letter; otherwise, <see langword="false" />.</returns>
  public static bool IsLetter(string s, int index)
  {
    if (s == null)
      ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
    if ((uint) index >= (uint) s.Length)
      ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index);
    char ch = s[index];
    return char.IsAscii(ch) ? ((uint) char.Latin1CharInfo[(int) ch] & 96U /*0x60*/) > 0U : char.CheckLetter(CharUnicodeInfo.GetUnicodeCategoryInternal(s, index));
  }

  /// <summary>Indicates whether the character at the specified position in a specified string is categorized as a letter or a decimal digit.</summary>
  /// <param name="s">A string.</param>
  /// <param name="index">The position of the character to evaluate in <paramref name="s" />.</param>
  /// <exception cref="T:System.ArgumentNullException">
  /// <paramref name="s" /> is <see langword="null" />.</exception>
  /// <exception cref="T:System.ArgumentOutOfRangeException">
  /// <paramref name="index" /> is less than zero or greater than the last position in <paramref name="s" />.</exception>
  /// <returns>
  /// <see langword="true" /> if the character at position <paramref name="index" /> in <paramref name="s" /> is a letter or a decimal digit; otherwise, <see langword="false" />.</returns>
  public static bool IsLetterOrDigit(string s, int index)
  {
    if (s == null)
      ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
    if ((uint) index >= (uint) s.Length)
      ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index);
    char c = s[index];
    return char.CheckLetterOrDigit(char.IsLatin1(c) ? char.GetLatin1UnicodeCategory(c) : CharUnicodeInfo.GetUnicodeCategoryInternal(s, index));
  }

  /// <summary>Indicates whether the character at the specified position in a specified string is categorized as a lowercase letter.</summary>
  /// <param name="s">A string.</param>
  /// <param name="index">The position of the character to evaluate in <paramref name="s" />.</param>
  /// <exception cref="T:System.ArgumentNullException">
  /// <paramref name="s" /> is <see langword="null" />.</exception>
  /// <exception cref="T:System.ArgumentOutOfRangeException">
  /// <paramref name="index" /> is less than zero or greater than the last position in <paramref name="s" />.</exception>
  /// <returns>
  /// <see langword="true" /> if the character at position <paramref name="index" /> in <paramref name="s" /> is a lowercase letter; otherwise, <see langword="false" />.</returns>
  public static bool IsLower(string s, int index)
  {
    if (s == null)
      ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
    if ((uint) index >= (uint) s.Length)
      ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index);
    char ch = s[index];
    return char.IsLatin1(ch) ? ((uint) char.Latin1CharInfo[(int) ch] & 32U /*0x20*/) > 0U : CharUnicodeInfo.GetUnicodeCategoryInternal(s, index) == UnicodeCategory.LowercaseLetter;
  }

  internal static bool CheckNumber(UnicodeCategory uc)
  {
    return char.IsBetween(uc, UnicodeCategory.DecimalDigitNumber, UnicodeCategory.OtherNumber);
  }

  /// <summary>Indicates whether the specified Unicode character is categorized as a number.</summary>
  /// <param name="c">The Unicode character to evaluate.</param>
  /// <returns>
  /// <see langword="true" /> if <paramref name="c" /> is a number; otherwise, <see langword="false" />.</returns>
  public static bool IsNumber(char c)
  {
    if (!char.IsLatin1(c))
      return char.CheckNumber(CharUnicodeInfo.GetUnicodeCategory(c));
    return char.IsAscii(c) ? char.IsBetween(c, '0', '9') : char.CheckNumber(char.GetLatin1UnicodeCategory(c));
  }

  /// <summary>Indicates whether the character at the specified position in a specified string is categorized as a number.</summary>
  /// <param name="s">A string.</param>
  /// <param name="index">The position of the character to evaluate in <paramref name="s" />.</param>
  /// <exception cref="T:System.ArgumentNullException">
  /// <paramref name="s" /> is <see langword="null" />.</exception>
  /// <exception cref="T:System.ArgumentOutOfRangeException">
  /// <paramref name="index" /> is less than zero or greater than the last position in <paramref name="s" />.</exception>
  /// <returns>
  /// <see langword="true" /> if the character at position <paramref name="index" /> in <paramref name="s" /> is a number; otherwise, <see langword="false" />.</returns>
  public static bool IsNumber(string s, int index)
  {
    if (s == null)
      ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
    if ((uint) index >= (uint) s.Length)
      ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index);
    char c = s[index];
    if (!char.IsLatin1(c))
      return char.CheckNumber(CharUnicodeInfo.GetUnicodeCategoryInternal(s, index));
    return char.IsAscii(c) ? char.IsBetween(c, '0', '9') : char.CheckNumber(char.GetLatin1UnicodeCategory(c));
  }

  /// <summary>Indicates whether the character at the specified position in a specified string is categorized as a punctuation mark.</summary>
  /// <param name="s">A string.</param>
  /// <param name="index">The position of the character to evaluate in <paramref name="s" />.</param>
  /// <exception cref="T:System.ArgumentNullException">
  /// <paramref name="s" /> is <see langword="null" />.</exception>
  /// <exception cref="T:System.ArgumentOutOfRangeException">
  /// <paramref name="index" /> is less than zero or greater than the last position in <paramref name="s" />.</exception>
  /// <returns>
  /// <see langword="true" /> if the character at position <paramref name="index" /> in <paramref name="s" /> is a punctuation mark; otherwise, <see langword="false" />.</returns>
  public static bool IsPunctuation(string s, int index)
  {
    if (s == null)
      ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
    if ((uint) index >= (uint) s.Length)
      ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index);
    char c = s[index];
    return char.CheckPunctuation(char.IsLatin1(c) ? char.GetLatin1UnicodeCategory(c) : CharUnicodeInfo.GetUnicodeCategoryInternal(s, index));
  }

  internal static bool CheckSeparator(UnicodeCategory uc)
  {
    return char.IsBetween(uc, UnicodeCategory.SpaceSeparator, UnicodeCategory.ParagraphSeparator);
  }

  private static bool IsSeparatorLatin1(char c) => c == ' ' || c == ' ';

  /// <summary>Indicates whether the specified Unicode character is categorized as a separator character.</summary>
  /// <param name="c">The Unicode character to evaluate.</param>
  /// <returns>
  /// <see langword="true" /> if <paramref name="c" /> is a separator character; otherwise, <see langword="false" />.</returns>
  public static bool IsSeparator(char c)
  {
    return char.IsLatin1(c) ? char.IsSeparatorLatin1(c) : char.CheckSeparator(CharUnicodeInfo.GetUnicodeCategory(c));
  }

  /// <summary>Indicates whether the character at the specified position in a specified string is categorized as a separator character.</summary>
  /// <param name="s">A string.</param>
  /// <param name="index">The position of the character to evaluate in <paramref name="s" />.</param>
  /// <exception cref="T:System.ArgumentNullException">
  /// <paramref name="s" /> is <see langword="null" />.</exception>
  /// <exception cref="T:System.ArgumentOutOfRangeException">
  /// <paramref name="index" /> is less than zero or greater than the last position in <paramref name="s" />.</exception>
  /// <returns>
  /// <see langword="true" /> if the character at position <paramref name="index" /> in <paramref name="s" /> is a separator character; otherwise, <see langword="false" />.</returns>
  public static bool IsSeparator(string s, int index)
  {
    if (s == null)
      ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
    if ((uint) index >= (uint) s.Length)
      ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index);
    char c = s[index];
    return char.IsLatin1(c) ? char.IsSeparatorLatin1(c) : char.CheckSeparator(CharUnicodeInfo.GetUnicodeCategoryInternal(s, index));
  }

  /// <summary>Indicates whether the specified character has a surrogate code unit.</summary>
  /// <param name="c">The Unicode character to evaluate.</param>
  /// <returns>
  /// <see langword="true" /> if <paramref name="c" /> is either a high surrogate or a low surrogate; otherwise, <see langword="false" />.</returns>
  public static bool IsSurrogate(char c) => char.IsBetween(c, '\uD800', '\uDFFF');

  /// <summary>Indicates whether the character at the specified position in a specified string has a surrogate code unit.</summary>
  /// <param name="s">A string.</param>
  /// <param name="index">The position of the character to evaluate in <paramref name="s" />.</param>
  /// <exception cref="T:System.ArgumentNullException">
  /// <paramref name="s" /> is <see langword="null" />.</exception>
  /// <exception cref="T:System.ArgumentOutOfRangeException">
  /// <paramref name="index" /> is less than zero or greater than the last position in <paramref name="s" />.</exception>
  /// <returns>
  /// <see langword="true" /> if the character at position <paramref name="index" /> in <paramref name="s" /> is a either a high surrogate or a low surrogate; otherwise, <see langword="false" />.</returns>
  public static bool IsSurrogate(string s, int index)
  {
    if (s == null)
      ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
    if ((uint) index >= (uint) s.Length)
      ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index);
    return char.IsSurrogate(s[index]);
  }

  internal static bool CheckSymbol(UnicodeCategory uc)
  {
    return char.IsBetween(uc, UnicodeCategory.MathSymbol, UnicodeCategory.OtherSymbol);
  }

  /// <summary>Indicates whether the specified Unicode character is categorized as a symbol character.</summary>
  /// <param name="c">The Unicode character to evaluate.</param>
  /// <returns>
  /// <see langword="true" /> if <paramref name="c" /> is a symbol character; otherwise, <see langword="false" />.</returns>
  public static bool IsSymbol(char c)
  {
    return char.CheckSymbol(char.IsLatin1(c) ? char.GetLatin1UnicodeCategory(c) : CharUnicodeInfo.GetUnicodeCategory(c));
  }

  /// <summary>Indicates whether the character at the specified position in a specified string is categorized as a symbol character.</summary>
  /// <param name="s">A string.</param>
  /// <param name="index">The position of the character to evaluate in <paramref name="s" />.</param>
  /// <exception cref="T:System.ArgumentNullException">
  /// <paramref name="s" /> is <see langword="null" />.</exception>
  /// <exception cref="T:System.ArgumentOutOfRangeException">
  /// <paramref name="index" /> is less than zero or greater than the last position in <paramref name="s" />.</exception>
  /// <returns>
  /// <see langword="true" /> if the character at position <paramref name="index" /> in <paramref name="s" /> is a symbol character; otherwise, <see langword="false" />.</returns>
  public static bool IsSymbol(string s, int index)
  {
    if (s == null)
      ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
    if ((uint) index >= (uint) s.Length)
      ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index);
    char c = s[index];
    return char.CheckSymbol(char.IsLatin1(c) ? char.GetLatin1UnicodeCategory(c) : CharUnicodeInfo.GetUnicodeCategoryInternal(s, index));
  }

  /// <summary>Indicates whether the character at the specified position in a specified string is categorized as an uppercase letter.</summary>
  /// <param name="s">A string.</param>
  /// <param name="index">The position of the character to evaluate in <paramref name="s" />.</param>
  /// <exception cref="T:System.ArgumentNullException">
  /// <paramref name="s" /> is <see langword="null" />.</exception>
  /// <exception cref="T:System.ArgumentOutOfRangeException">
  /// <paramref name="index" /> is less than zero or greater than the last position in <paramref name="s" />.</exception>
  /// <returns>
  /// <see langword="true" /> if the character at position <paramref name="index" /> in <paramref name="s" /> is an uppercase letter; otherwise, <see langword="false" />.</returns>
  public static bool IsUpper(string s, int index)
  {
    if (s == null)
      ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
    if ((uint) index >= (uint) s.Length)
      ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index);
    char ch = s[index];
    return char.IsLatin1(ch) ? ((uint) char.Latin1CharInfo[(int) ch] & 64U /*0x40*/) > 0U : CharUnicodeInfo.GetUnicodeCategoryInternal(s, index) == UnicodeCategory.UppercaseLetter;
  }

  /// <summary>Indicates whether the character at the specified position in a specified string is categorized as white space.</summary>
  /// <param name="s">A string.</param>
  /// <param name="index">The position of the character to evaluate in <paramref name="s" />.</param>
  /// <exception cref="T:System.ArgumentNullException">
  /// <paramref name="s" /> is <see langword="null" />.</exception>
  /// <exception cref="T:System.ArgumentOutOfRangeException">
  /// <paramref name="index" /> is less than zero or greater than the last position in <paramref name="s" />.</exception>
  /// <returns>
  /// <see langword="true" /> if the character at position <paramref name="index" /> in <paramref name="s" /> is white space; otherwise, <see langword="false" />.</returns>
  public static bool IsWhiteSpace(string s, int index)
  {
    if (s == null)
      ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
    if ((uint) index >= (uint) s.Length)
      ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index);
    return char.IsWhiteSpace(s[index]);
  }

  /// <summary>Categorizes a specified Unicode character into a group identified by one of the <see cref="T:System.Globalization.UnicodeCategory" /> values.</summary>
  /// <param name="c">The Unicode character to categorize.</param>
  /// <returns>A <see cref="T:System.Globalization.UnicodeCategory" /> value that identifies the group that contains <paramref name="c" />.</returns>
  public static UnicodeCategory GetUnicodeCategory(char c)
  {
    return char.IsLatin1(c) ? char.GetLatin1UnicodeCategory(c) : CharUnicodeInfo.GetUnicodeCategory((int) c);
  }

  /// <summary>Categorizes the character at the specified position in a specified string into a group identified by one of the <see cref="T:System.Globalization.UnicodeCategory" /> values.</summary>
  /// <param name="s">A <see cref="T:System.String" />.</param>
  /// <param name="index">The character position in <paramref name="s" />.</param>
  /// <exception cref="T:System.ArgumentNullException">
  /// <paramref name="s" /> is <see langword="null" />.</exception>
  /// <exception cref="T:System.ArgumentOutOfRangeException">
  /// <paramref name="index" /> is less than zero or greater than the last position in <paramref name="s" />.</exception>
  /// <returns>A <see cref="T:System.Globalization.UnicodeCategory" /> enumerated constant that identifies the group that contains the character at position <paramref name="index" /> in <paramref name="s" />.</returns>
  public static UnicodeCategory GetUnicodeCategory(string s, int index)
  {
    if (s == null)
      ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
    if ((uint) index >= (uint) s.Length)
      ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index);
    return char.IsLatin1(s[index]) ? char.GetLatin1UnicodeCategory(s[index]) : CharUnicodeInfo.GetUnicodeCategoryInternal(s, index);
  }

  /// <summary>Converts the specified numeric Unicode character to a double-precision floating point number.</summary>
  /// <param name="c">The Unicode character to convert.</param>
  /// <returns>The numeric value of <paramref name="c" /> if that character represents a number; otherwise, -1.0.</returns>
  public static double GetNumericValue(char c) => CharUnicodeInfo.GetNumericValue(c);

  /// <summary>Converts the numeric Unicode character at the specified position in a specified string to a double-precision floating point number.</summary>
  /// <param name="s">A <see cref="T:System.String" />.</param>
  /// <param name="index">The character position in <paramref name="s" />.</param>
  /// <exception cref="T:System.ArgumentNullException">
  /// <paramref name="s" /> is <see langword="null" />.</exception>
  /// <exception cref="T:System.ArgumentOutOfRangeException">
  /// <paramref name="index" /> is less than zero or greater than the last position in <paramref name="s" />.</exception>
  /// <returns>The numeric value of the character at position <paramref name="index" /> in <paramref name="s" /> if that character represents a number; otherwise, -1.</returns>
  public static double GetNumericValue(string s, int index)
  {
    if (s == null)
      ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
    if ((uint) index >= (uint) s.Length)
      ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index);
    return CharUnicodeInfo.GetNumericValueInternal(s, index);
  }

  /// <summary>Indicates whether the specified <see cref="T:System.Char" /> object is a high surrogate.</summary>
  /// <param name="c">The Unicode character to evaluate.</param>
  /// <returns>
  /// <see langword="true" /> if the numeric value of the <paramref name="c" /> parameter ranges from U+D800 through U+DBFF; otherwise, <see langword="false" />.</returns>
  public static bool IsHighSurrogate(char c) => char.IsBetween(c, '\uD800', '\uDBFF');

  /// <summary>Indicates whether the <see cref="T:System.Char" /> object at the specified position in a string is a high surrogate.</summary>
  /// <param name="s">A string.</param>
  /// <param name="index">The position of the character to evaluate in <paramref name="s" />.</param>
  /// <exception cref="T:System.ArgumentNullException">
  /// <paramref name="s" /> is <see langword="null" />.</exception>
  /// <exception cref="T:System.ArgumentOutOfRangeException">
  /// <paramref name="index" /> is not a position within <paramref name="s" />.</exception>
  /// <returns>
  /// <see langword="true" /> if the numeric value of the specified character in the <paramref name="s" /> parameter ranges from U+D800 through U+DBFF; otherwise, <see langword="false" />.</returns>
  public static bool IsHighSurrogate(string s, int index)
  {
    if (s == null)
      ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
    if ((uint) index >= (uint) s.Length)
      ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index);
    return char.IsHighSurrogate(s[index]);
  }

  /// <summary>Indicates whether the specified <see cref="T:System.Char" /> object is a low surrogate.</summary>
  /// <param name="c">The character to evaluate.</param>
  /// <returns>
  /// <see langword="true" /> if the numeric value of the <paramref name="c" /> parameter ranges from U+DC00 through U+DFFF; otherwise, <see langword="false" />.</returns>
  public static bool IsLowSurrogate(char c) => char.IsBetween(c, '\uDC00', '\uDFFF');

  /// <summary>Indicates whether the <see cref="T:System.Char" /> object at the specified position in a string is a low surrogate.</summary>
  /// <param name="s">A string.</param>
  /// <param name="index">The position of the character to evaluate in <paramref name="s" />.</param>
  /// <exception cref="T:System.ArgumentNullException">
  /// <paramref name="s" /> is <see langword="null" />.</exception>
  /// <exception cref="T:System.ArgumentOutOfRangeException">
  /// <paramref name="index" /> is not a position within <paramref name="s" />.</exception>
  /// <returns>
  /// <see langword="true" /> if the numeric value of the specified character in the <paramref name="s" /> parameter ranges from U+DC00 through U+DFFF; otherwise, <see langword="false" />.</returns>
  public static bool IsLowSurrogate(string s, int index)
  {
    if (s == null)
      ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
    if ((uint) index >= (uint) s.Length)
      ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index);
    return char.IsLowSurrogate(s[index]);
  }

  /// <summary>Indicates whether two adjacent <see cref="T:System.Char" /> objects at a specified position in a string form a surrogate pair.</summary>
  /// <param name="s">A string.</param>
  /// <param name="index">The starting position of the pair of characters to evaluate within <paramref name="s" />.</param>
  /// <exception cref="T:System.ArgumentNullException">
  /// <paramref name="s" /> is <see langword="null" />.</exception>
  /// <exception cref="T:System.ArgumentOutOfRangeException">
  /// <paramref name="index" /> is not a position within <paramref name="s" />.</exception>
  /// <returns>
  /// <see langword="true" /> if the <paramref name="s" /> parameter includes adjacent characters at positions <paramref name="index" /> and <paramref name="index" /> + 1, and the numeric value of the character at position <paramref name="index" /> ranges from U+D800 through U+DBFF, and the numeric value of the character at position <paramref name="index" />+1 ranges from U+DC00 through U+DFFF; otherwise, <see langword="false" />.</returns>
  public static bool IsSurrogatePair(string s, int index)
  {
    if (s == null)
      ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
    if ((uint) index >= (uint) s.Length)
      ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index);
    return (uint) (index + 1) < (uint) s.Length && char.IsSurrogatePair(s[index], s[index + 1]);
  }

  /// <summary>Indicates whether the two specified <see cref="T:System.Char" /> objects form a surrogate pair.</summary>
  /// <param name="highSurrogate">The character to evaluate as the high surrogate of a surrogate pair.</param>
  /// <param name="lowSurrogate">The character to evaluate as the low surrogate of a surrogate pair.</param>
  /// <returns>
  /// <see langword="true" /> if the numeric value of the <paramref name="highSurrogate" /> parameter ranges from U+D800 through U+DBFF, and the numeric value of the <paramref name="lowSurrogate" /> parameter ranges from U+DC00 through U+DFFF; otherwise, <see langword="false" />.</returns>
  public static bool IsSurrogatePair(char highSurrogate, char lowSurrogate)
  {
    return ((uint) highSurrogate - 55296U | (uint) lowSurrogate - 56320U) <= 1023U /*0x03FF*/;
  }

  /// <summary>Converts the specified Unicode code point into a UTF-16 encoded string.</summary>
  /// <param name="utf32">A 21-bit Unicode code point.</param>
  /// <exception cref="T:System.ArgumentOutOfRangeException">
  /// <paramref name="utf32" /> is not a valid 21-bit Unicode code point ranging from U+0 through U+10FFFF, excluding the surrogate pair range from U+D800 through U+DFFF.</exception>
  /// <returns>A string consisting of one <see cref="T:System.Char" /> object or a surrogate pair of <see cref="T:System.Char" /> objects equivalent to the code point specified by the <paramref name="utf32" /> parameter.</returns>
  public static string ConvertFromUtf32(int utf32)
  {
    return UnicodeUtility.IsValidUnicodeScalar((uint) utf32) ? Rune.UnsafeCreate((uint) utf32).ToString() : throw new ArgumentOutOfRangeException(nameof (utf32), SR.ArgumentOutOfRange_InvalidUTF32);
  }

  /// <summary>Converts the value of a UTF-16 encoded surrogate pair into a Unicode code point.</summary>
  /// <param name="highSurrogate">A high surrogate code unit (that is, a code unit ranging from U+D800 through U+DBFF).</param>
  /// <param name="lowSurrogate">A low surrogate code unit (that is, a code unit ranging from U+DC00 through U+DFFF).</param>
  /// <exception cref="T:System.ArgumentOutOfRangeException">
  /// <paramref name="highSurrogate" /> is not in the range U+D800 through U+DBFF, or <paramref name="lowSurrogate" /> is not in the range U+DC00 through U+DFFF.</exception>
  /// <returns>The 21-bit Unicode code point represented by the <paramref name="highSurrogate" /> and <paramref name="lowSurrogate" /> parameters.</returns>
  public static int ConvertToUtf32(char highSurrogate, char lowSurrogate)
  {
    uint highSurrogateOffset = (uint) highSurrogate - 55296U;
    uint num = (uint) lowSurrogate - 56320U;
    if ((highSurrogateOffset | num) > 1023U /*0x03FF*/)
      char.ConvertToUtf32_ThrowInvalidArgs(highSurrogateOffset);
    return ((int) highSurrogateOffset << 10) + ((int) lowSurrogate - 56320) + 65536 /*0x010000*/;
  }

  [StackTraceHidden]
  private static void ConvertToUtf32_ThrowInvalidArgs(uint highSurrogateOffset)
  {
    if (highSurrogateOffset > 1023U /*0x03FF*/)
      throw new ArgumentOutOfRangeException("highSurrogate", SR.ArgumentOutOfRange_InvalidHighSurrogate);
    throw new ArgumentOutOfRangeException("lowSurrogate", SR.ArgumentOutOfRange_InvalidLowSurrogate);
  }

  /// <summary>Converts the value of a UTF-16 encoded character or surrogate pair at a specified position in a string into a Unicode code point.</summary>
  /// <param name="s">A string that contains a character or surrogate pair.</param>
  /// <param name="index">The index position of the character or surrogate pair in <paramref name="s" />.</param>
  /// <exception cref="T:System.ArgumentNullException">
  /// <paramref name="s" /> is <see langword="null" />.</exception>
  /// <exception cref="T:System.ArgumentOutOfRangeException">
  /// <paramref name="index" /> is not a position within <paramref name="s" />.</exception>
  /// <exception cref="T:System.ArgumentException">The specified index position contains a surrogate pair, and either the first character in the pair is not a valid high surrogate or the second character in the pair is not a valid low surrogate.</exception>
  /// <returns>The 21-bit Unicode code point represented by the character or surrogate pair at the position in the <paramref name="s" /> parameter specified by the <paramref name="index" /> parameter.</returns>
  public static int ConvertToUtf32(string s, int index)
  {
    if (s == null)
      ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
    if ((uint) index >= (uint) s.Length)
      ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_IndexMustBeLess);
    int num1 = (int) s[index] - 55296;
    if ((uint) num1 <= 2047U /*0x07FF*/)
    {
      bool flag = true;
      if (num1 <= 1023 /*0x03FF*/)
      {
        if ((uint) (index + 1) < (uint) s.Length)
        {
          int num2 = (int) s[index + 1] - 56320;
          if ((uint) num2 <= 1023U /*0x03FF*/)
            return num1 * 1024 /*0x0400*/ + num2 + 65536 /*0x010000*/;
        }
        flag = false;
      }
      throw new ArgumentException(SR.Format(flag ? SR.Argument_InvalidLowSurrogate : SR.Argument_InvalidHighSurrogate, (object) index), nameof (s));
    }
    return (int) s[index];
  }

  /// <summary>Adds two values together to compute their sum.</summary>
  /// <param name="left">The value to which <paramref name="right" /> is added.</param>
  /// <param name="right">The value that's added to <paramref name="left" />.</param>
  /// <returns>The sum of <paramref name="left" /> and <paramref name="right" />.</returns>
  static char IAdditionOperators<char, char, char>.operator +(char left, char right)
  {
    return (char) ((uint) left + (uint) right);
  }

  /// <summary>Adds two values together to compute their sum.</summary>
  /// <param name="left">The value to which <paramref name="right" /> is added.</param>
  /// <param name="right">The value that's added to <paramref name="left" />.</param>
  /// <returns>The sum of <paramref name="left" /> and <paramref name="right" />.</returns>
  static char IAdditionOperators<char, char, char>.operator checked +(char left, char right)
  {
    return checked ((char) ((int) left + (int) right));
  }

  /// <summary>Gets the additive identity of the current type.</summary>
  static char IAdditiveIdentity<char, char>.AdditiveIdentity => char.MinValue;

  /// <summary>Computes the number of leading zero bits in a value.</summary>
  /// <param name="value">The value whose leading zero bits are to be counted.</param>
  /// <returns>The number of leading zero bits in <paramref name="value" />.</returns>
  static char IBinaryInteger<char>.LeadingZeroCount(char value)
  {
    return (char) (BitOperations.LeadingZeroCount((uint) value) - 16 /*0x10*/);
  }

  /// <summary>Computes the number of bits that are set in a value.</summary>
  /// <param name="value">The value whose set bits are to be counted.</param>
  /// <returns>The number of set bits in <paramref name="value" />.</returns>
  static char IBinaryInteger<char>.PopCount(char value)
  {
    return (char) BitOperations.PopCount((uint) value);
  }

  /// <summary>Rotates a value left by a given amount.</summary>
  /// <param name="value">The value that's rotated left by <paramref name="rotateAmount" />.</param>
  /// <param name="rotateAmount">The amount by which <paramref name="value" /> is rotated left.</param>
  /// <returns>The result of rotating <paramref name="value" /> left by <paramref name="rotateAmount" />.</returns>
  static char IBinaryInteger<char>.RotateLeft(char value, int rotateAmount)
  {
    return (char) ((int) value << (rotateAmount & 15) | (int) value >> (16 /*0x10*/ - rotateAmount & 15));
  }

  /// <summary>Rotates a value right by a given amount.</summary>
  /// <param name="value">The value that's rotated right by <paramref name="rotateAmount" />.</param>
  /// <param name="rotateAmount">The amount by which <paramref name="value" /> is rotated right.</param>
  /// <returns>The result of rotating <paramref name="value" /> right by <paramref name="rotateAmount" />.</returns>
  static char IBinaryInteger<char>.RotateRight(char value, int rotateAmount)
  {
    return (char) ((int) value >> (rotateAmount & 15) | (int) value << (16 /*0x10*/ - rotateAmount & 15));
  }

  /// <summary>Computes the number of trailing zero bits in a value.</summary>
  /// <param name="value">The value whose trailing zero bits are to be counted.</param>
  /// <returns>The number of trailing zero bits in <paramref name="value" />.</returns>
  static char IBinaryInteger<char>.TrailingZeroCount(char value)
  {
    return (char) (BitOperations.TrailingZeroCount((int) value << 16 /*0x10*/) - 16 /*0x10*/);
  }

  #nullable disable
  /// <param name="source" />
  /// <param name="isUnsigned" />
  /// <param name="value" />
  static bool IBinaryInteger<char>.TryReadBigEndian(
    ReadOnlySpan<byte> source,
    bool isUnsigned,
    out char value)
  {
    char ch1 = char.MinValue;
    if (source.Length != 0)
    {
      if (!isUnsigned && sbyte.IsNegative((sbyte) source[0]))
      {
        value = ch1;
        return false;
      }
      if (source.Length > 2)
      {
        ref ReadOnlySpan<byte> local = ref source;
        if (local.Slice(0, local.Length - 2).ContainsAnyExcept<byte>((byte) 0))
        {
          value = ch1;
          return false;
        }
      }
      ref byte local1 = ref MemoryMarshal.GetReference<byte>(source);
      if (source.Length >= 2)
      {
        char ch2 = Unsafe.ReadUnaligned<char>(ref Unsafe.Add<byte>(ref local1, source.Length - 2));
        int num = BitConverter.IsLittleEndian ? 1 : 0;
        ch1 = BinaryPrimitives.ReverseEndianness(ch2);
      }
      else
        ch1 = (char) local1;
    }
    value = ch1;
    return true;
  }

  /// <param name="source" />
  /// <param name="isUnsigned" />
  /// <param name="value" />
  static bool IBinaryInteger<char>.TryReadLittleEndian(
    ReadOnlySpan<byte> source,
    bool isUnsigned,
    out char value)
  {
    char ch = char.MinValue;
    if (source.Length != 0)
    {
      if (!isUnsigned)
      {
        ref ReadOnlySpan<byte> local = ref source;
        if (sbyte.IsNegative((sbyte) local[local.Length - 1]))
        {
          value = ch;
          return false;
        }
      }
      if (source.Length > 2)
      {
        ref ReadOnlySpan<byte> local = ref source;
        if (local.Slice(2, local.Length - 2).ContainsAnyExcept<byte>((byte) 0))
        {
          value = ch;
          return false;
        }
      }
      ref byte local1 = ref MemoryMarshal.GetReference<byte>(source);
      if (source.Length >= 2)
      {
        ch = Unsafe.ReadUnaligned<char>(ref local1);
        if (BitConverter.IsLittleEndian)
          goto label_10;
      }
      ch = (char) local1;
    }
label_10:
    value = ch;
    return true;
  }

  int IBinaryInteger<char>.GetShortestBitLength()
  {
    return 16 /*0x10*/ - (int) ushort.LeadingZeroCount((ushort) this);
  }

  /// <summary>Gets the number of bytes that will be written as part of <see cref="M:System.Numerics.IBinaryInteger`1.TryWriteLittleEndian(System.Span{System.Byte},System.Int32@)" />.</summary>
  /// <returns>The number of bytes that will be written as part of <see cref="M:System.Numerics.IBinaryInteger`1.TryWriteLittleEndian(System.Span{System.Byte},System.Int32@)" />.</returns>
  int IBinaryInteger<char>.GetByteCount() => 2;

  bool IBinaryInteger<char>.TryWriteBigEndian(Span<byte> destination, out int bytesWritten)
  {
    if (BinaryPrimitives.TryWriteUInt16BigEndian(destination, (ushort) this))
    {
      bytesWritten = 2;
      return true;
    }
    bytesWritten = 0;
    return false;
  }

  bool IBinaryInteger<char>.TryWriteLittleEndian(Span<byte> destination, out int bytesWritten)
  {
    if (BinaryPrimitives.TryWriteUInt16LittleEndian(destination, (ushort) this))
    {
      bytesWritten = 2;
      return true;
    }
    bytesWritten = 0;
    return false;
  }

  /// <summary>Gets an instance of the binary type in which all bits are set.</summary>
  static char IBinaryNumber<char>.AllBitsSet => char.MaxValue;

  /// <summary>Determines if a value is a power of two.</summary>
  /// <param name="value">The value to be checked.</param>
  /// <returns>
  /// <see langword="true" /> if <paramref name="value" /> is a power of two; otherwise, <see langword="false" />.</returns>
  static bool IBinaryNumber<char>.IsPow2(char value) => ushort.IsPow2((ushort) value);

  /// <summary>Computes the log2 of a value.</summary>
  /// <param name="value">The value whose log2 is to be computed.</param>
  /// <returns>The log2 of <paramref name="value" />.</returns>
  static char IBinaryNumber<char>.Log2(char value) => (char) ushort.Log2((ushort) value);

  /// <summary>Computes the bitwise-and of two values.</summary>
  /// <param name="left">The value to <see langword="and" /> with <paramref name="right" />.</param>
  /// <param name="right">The value to <see langword="and" /> with <paramref name="left" />.</param>
  /// <returns>The bitwise-and of <paramref name="left" /> and <paramref name="right" />.</returns>
  static char IBitwiseOperators<char, char, char>.operator &(char left, char right)
  {
    return (char) ((uint) left & (uint) right);
  }

  /// <summary>Computes the bitwise-or of two values.</summary>
  /// <param name="left">The value to <see langword="or" /> with <paramref name="right" />.</param>
  /// <param name="right">The value to <see langword="or" /> with <paramref name="left" />.</param>
  /// <returns>The bitwise-or of <paramref name="left" /> and <paramref name="right" />.</returns>
  static char IBitwiseOperators<char, char, char>.operator |(char left, char right)
  {
    return (char) ((uint) left | (uint) right);
  }

  /// <summary>Computes the exclusive-or of two values.</summary>
  /// <param name="left">The value to xor with <paramref name="right" />.</param>
  /// <param name="right">The value to xor with <paramref name="left" />.</param>
  /// <returns>The exclusive-or of <paramref name="left" /> and <paramref name="right" />.</returns>
  static char IBitwiseOperators<char, char, char>.operator ^(char left, char right)
  {
    return (char) ((uint) left ^ (uint) right);
  }

  /// <summary>Computes the ones-complement representation of a given value.</summary>
  /// <param name="value">The value for which to compute the ones-complement.</param>
  /// <returns>The ones-complement of <paramref name="value" />.</returns>
  static char IBitwiseOperators<char, char, char>.operator ~(char value) => ~value;

  /// <summary>Compares two values to determine which is less.</summary>
  /// <param name="left">The value to compare with <paramref name="right" />.</param>
  /// <param name="right">The value to compare with <paramref name="left" />.</param>
  /// <returns>
  /// <see langword="true" /> if <paramref name="left" /> is less than <paramref name="right" />; otherwise, <see langword="false" />.</returns>
  static bool IComparisonOperators<char, char, bool>.operator <(char left, char right)
  {
    return (int) left < (int) right;
  }

  /// <summary>Compares two values to determine which is less or equal.</summary>
  /// <param name="left">The value to compare with <paramref name="right" />.</param>
  /// <param name="right">The value to compare with <paramref name="left" />.</param>
  /// <returns>
  /// <see langword="true" /> if <paramref name="left" /> is less than or equal to <paramref name="right" />; otherwise, <see langword="false" />.</returns>
  static bool IComparisonOperators<char, char, bool>.operator <=(char left, char right)
  {
    return (int) left <= (int) right;
  }

  /// <summary>Compares two values to determine which is greater.</summary>
  /// <param name="left">The value to compare with <paramref name="right" />.</param>
  /// <param name="right">The value to compare with <paramref name="left" />.</param>
  /// <returns>
  /// <see langword="true" /> if <paramref name="left" /> is greater than <paramref name="right" />; otherwise, <see langword="false" />.</returns>
  static bool IComparisonOperators<char, char, bool>.operator >(char left, char right)
  {
    return (int) left > (int) right;
  }

  /// <summary>Compares two values to determine which is greater or equal.</summary>
  /// <param name="left">The value to compare with <paramref name="right" />.</param>
  /// <param name="right">The value to compare with <paramref name="left" />.</param>
  /// <returns>
  /// <see langword="true" /> if <paramref name="left" /> is greater than or equal to <paramref name="right" />; otherwise, <see langword="false" />.</returns>
  static bool IComparisonOperators<char, char, bool>.operator >=(char left, char right)
  {
    return (int) left >= (int) right;
  }

  /// <summary>Decrements a value.</summary>
  /// <param name="value">The value to decrement.</param>
  /// <returns>The result of decrementing <paramref name="value" />.</returns>
  static char IDecrementOperators<char>.operator --(char value) => --value;

  /// <summary>Decrements a value.</summary>
  /// <param name="value">The value to decrement.</param>
  /// <returns>The result of decrementing <paramref name="value" />.</returns>
  static char IDecrementOperators<char>.operator checked --(char value) => checked (--value);

  /// <summary>Divides one value by another to compute their quotient.</summary>
  /// <param name="left">The value that <paramref name="right" /> divides.</param>
  /// <param name="right">The value that divides <paramref name="left" />.</param>
  /// <returns>The quotient of <paramref name="left" /> divided by <paramref name="right" />.</returns>
  static char IDivisionOperators<char, char, char>.operator /(char left, char right)
  {
    return (char) ((uint) left / (uint) right);
  }

  /// <summary>Compares two values to determine equality.</summary>
  /// <param name="left">The value to compare with <paramref name="right" />.</param>
  /// <param name="right">The value to compare with <paramref name="left" />.</param>
  /// <returns>
  /// <see langword="true" /> if <paramref name="left" /> is equal to <paramref name="right" />; otherwise, <see langword="false" />.</returns>
  static bool IEqualityOperators<char, char, bool>.operator ==(char left, char right)
  {
    return (int) left == (int) right;
  }

  /// <summary>Compares two values to determine inequality.</summary>
  /// <param name="left">The value to compare with <paramref name="right" />.</param>
  /// <param name="right">The value to compare with <paramref name="left" />.</param>
  /// <returns>
  /// <see langword="true" /> if <paramref name="left" /> is not equal to <paramref name="right" />; otherwise, <see langword="false" />.</returns>
  static bool IEqualityOperators<char, char, bool>.operator !=(char left, char right)
  {
    return (int) left != (int) right;
  }

  /// <summary>Increments a value.</summary>
  /// <param name="value">The value to increment.</param>
  /// <returns>The result of incrementing <paramref name="value" />.</returns>
  static char IIncrementOperators<char>.operator ++(char value) => ++value;

  /// <summary>Increments a value.</summary>
  /// <param name="value">The value to increment.</param>
  /// <returns>The result of incrementing <paramref name="value" />.</returns>
  static char IIncrementOperators<char>.operator checked ++(char value) => checked (++value);

  /// <summary>Gets the minimum value of the current type.</summary>
  static char IMinMaxValue<char>.MinValue => char.MinValue;

  /// <summary>Gets the maximum value of the current type.</summary>
  static char IMinMaxValue<char>.MaxValue => char.MaxValue;

  /// <summary>Divides two values together to compute their modulus or remainder.</summary>
  /// <param name="left">The value that <paramref name="right" /> divides.</param>
  /// <param name="right">The value that divides <paramref name="left" />.</param>
  /// <returns>The modulus or remainder of <paramref name="left" /> divided by <paramref name="right" />.</returns>
  static char IModulusOperators<char, char, char>.operator %(char left, char right)
  {
    return (char) ((uint) left % (uint) right);
  }

  /// <summary>Gets the multiplicative identity of the current type.</summary>
  static char IMultiplicativeIdentity<char, char>.MultiplicativeIdentity => '\u0001';

  /// <summary>Multiplies two values together to compute their product.</summary>
  /// <param name="left">The value that <paramref name="right" /> multiplies.</param>
  /// <param name="right">The value that multiplies <paramref name="left" />.</param>
  /// <returns>The product of <paramref name="left" /> multiplied by <paramref name="right" />.</returns>
  static char IMultiplyOperators<char, char, char>.operator *(char left, char right)
  {
    return (char) ((uint) left * (uint) right);
  }

  /// <summary>Multiplies two values together to compute their product.</summary>
  /// <param name="left">The value that <paramref name="right" /> multiplies.</param>
  /// <param name="right">The value that multiplies <paramref name="left" />.</param>
  /// <returns>The product of <paramref name="left" /> multiplied by <paramref name="right" />.</returns>
  static char IMultiplyOperators<char, char, char>.operator checked *(char left, char right)
  {
    return checked ((char) ((int) left * (int) right));
  }

  /// <summary>Gets the value <c>1</c> for the type.</summary>
  static char INumberBase<char>.One => '\u0001';

  /// <summary>Gets the radix, or base, for the type.</summary>
  static int INumberBase<char>.Radix => 2;

  /// <summary>Gets the value <c>0</c> for the type.</summary>
  static char INumberBase<char>.Zero => char.MinValue;

  /// <summary>Computes the absolute of a value.</summary>
  /// <param name="value">The value for which to get its absolute.</param>
  /// <returns>The absolute of <paramref name="value" />.</returns>
  static char INumberBase<char>.Abs(char value) => value;

  /// <summary>Determines if a value is in its canonical representation.</summary>
  /// <param name="value">The value to be checked.</param>
  /// <returns>
  /// <see langword="true" /> if <paramref name="value" /> is in its canonical representation; otherwise, <see langword="false" />.</returns>
  static bool INumberBase<char>.IsCanonical(char value) => true;

  /// <summary>Determines if a value represents a complex number.</summary>
  /// <param name="value">The value to be checked.</param>
  /// <returns>
  /// <see langword="true" /> if <paramref name="value" /> is a complex number; otherwise, <see langword="false" />.</returns>
  static bool INumberBase<char>.IsComplexNumber(char value) => false;

  /// <summary>Determines if a value represents an even integral number.</summary>
  /// <param name="value">The value to be checked.</param>
  /// <returns>
  /// <see langword="true" /> if <paramref name="value" /> is an even integer; otherwise, <see langword="false" />.</returns>
  static bool INumberBase<char>.IsEvenInteger(char value) => ((int) value & 1) == 0;

  /// <summary>Determines if a value is finite.</summary>
  /// <param name="value">The value to be checked.</param>
  /// <returns>
  /// <see langword="true" /> if <paramref name="value" /> is finite; otherwise, <see langword="false" />.</returns>
  static bool INumberBase<char>.IsFinite(char value) => true;

  /// <summary>Determines if a value represents a pure imaginary number.</summary>
  /// <param name="value">The value to be checked.</param>
  /// <returns>
  /// <see langword="true" /> if <paramref name="value" /> is a pure imaginary number; otherwise, <see langword="false" />.</returns>
  static bool INumberBase<char>.IsImaginaryNumber(char value) => false;

  /// <summary>Determines if a value is infinite.</summary>
  /// <param name="value">The value to be checked.</param>
  /// <returns>
  /// <see langword="true" /> if <paramref name="value" /> is infinite; otherwise, <see langword="false" />.</returns>
  static bool INumberBase<char>.IsInfinity(char value) => false;

  /// <summary>Determines if a value represents an integral number.</summary>
  /// <param name="value">The value to be checked.</param>
  /// <returns>
  /// <see langword="true" /> if <paramref name="value" /> is an integer; otherwise, <see langword="false" />.</returns>
  static bool INumberBase<char>.IsInteger(char value) => true;

  /// <summary>Determines if a value is NaN.</summary>
  /// <param name="value">The value to be checked.</param>
  /// <returns>
  /// <see langword="true" /> if <paramref name="value" /> is NaN; otherwise, <see langword="false" />.</returns>
  static bool INumberBase<char>.IsNaN(char value) => false;

  /// <summary>Determines if a value is negative.</summary>
  /// <param name="value">The value to be checked.</param>
  /// <returns>
  /// <see langword="true" /> if <paramref name="value" /> is negative; otherwise, <see langword="false" />.</returns>
  static bool INumberBase<char>.IsNegative(char value) => false;

  /// <summary>Determines if a value is negative infinity.</summary>
  /// <param name="value">The value to be checked.</param>
  /// <returns>
  /// <see langword="true" /> if <paramref name="value" /> is negative infinity; otherwise, <see langword="false" />.</returns>
  static bool INumberBase<char>.IsNegativeInfinity(char value) => false;

  /// <summary>Determines if a value is normal.</summary>
  /// <param name="value">The value to be checked.</param>
  /// <returns>
  /// <see langword="true" /> if <paramref name="value" /> is normal; otherwise, <see langword="false" />.</returns>
  static bool INumberBase<char>.IsNormal(char value) => value > char.MinValue;

  /// <summary>Determines if a value represents an odd integral number.</summary>
  /// <param name="value">The value to be checked.</param>
  /// <returns>
  /// <see langword="true" /> if <paramref name="value" /> is an odd integer; otherwise, <see langword="false" />.</returns>
  static bool INumberBase<char>.IsOddInteger(char value) => ((uint) value & 1U) > 0U;

  /// <summary>Determines if a value is positive.</summary>
  /// <param name="value">The value to be checked.</param>
  /// <returns>
  /// <see langword="true" /> if <paramref name="value" /> is positive; otherwise, <see langword="false" />.</returns>
  static bool INumberBase<char>.IsPositive(char value) => true;

  /// <summary>Determines if a value is positive infinity.</summary>
  /// <param name="value">The value to be checked.</param>
  /// <returns>
  /// <see langword="true" /> if <paramref name="value" /> is positive infinity; otherwise, <see langword="false" />.</returns>
  static bool INumberBase<char>.IsPositiveInfinity(char value) => false;

  /// <summary>Determines if a value represents a real number.</summary>
  /// <param name="value">The value to be checked.</param>
  /// <returns>
  /// <see langword="true" /> if <paramref name="value" /> is a real number; otherwise, <see langword="false" />.</returns>
  static bool INumberBase<char>.IsRealNumber(char value) => true;

  /// <summary>Determines if a value is subnormal.</summary>
  /// <param name="value">The value to be checked.</param>
  /// <returns>
  /// <see langword="true" /> if <paramref name="value" /> is subnormal; otherwise, <see langword="false" />.</returns>
  static bool INumberBase<char>.IsSubnormal(char value) => false;

  /// <summary>Determines if a value is zero.</summary>
  /// <param name="value">The value to be checked.</param>
  /// <returns>
  /// <see langword="true" /> if <paramref name="value" /> is zero; otherwise, <see langword="false" />.</returns>
  static bool INumberBase<char>.IsZero(char value) => value == char.MinValue;

  /// <summary>Compares two values to compute which is greater.</summary>
  /// <param name="x">The value to compare with <paramref name="y" />.</param>
  /// <param name="y">The value to compare with <paramref name="x" />.</param>
  /// <returns>
  /// <paramref name="x" /> if it is greater than <paramref name="y" />; otherwise, <paramref name="y" />.</returns>
  static char INumberBase<char>.MaxMagnitude(char x, char y)
  {
    return (char) Math.Max((ushort) x, (ushort) y);
  }

  /// <summary>Compares two values to compute which has the greater magnitude and returning the other value if an input is <c>NaN</c>.</summary>
  /// <param name="x">The value to compare with <paramref name="y" />.</param>
  /// <param name="y">The value to compare with <paramref name="x" />.</param>
  /// <returns>
  /// <paramref name="x" /> if it is greater than <paramref name="y" />; otherwise, <paramref name="y" />.</returns>
  static char INumberBase<char>.MaxMagnitudeNumber(char x, char y)
  {
    return (char) Math.Max((ushort) x, (ushort) y);
  }

  /// <summary>Compares two values to compute which is lesser.</summary>
  /// <param name="x">The value to compare with <paramref name="y" />.</param>
  /// <param name="y">The value to compare with <paramref name="x" />.</param>
  /// <returns>
  /// <paramref name="x" /> if it is less than <paramref name="y" />; otherwise, <paramref name="y" />.</returns>
  static char INumberBase<char>.MinMagnitude(char x, char y)
  {
    return (char) Math.Min((ushort) x, (ushort) y);
  }

  /// <summary>Compares two values to compute which has the lesser magnitude and returning the other value if an input is <c>NaN</c>.</summary>
  /// <param name="x">The value to compare with <paramref name="y" />.</param>
  /// <param name="y">The value to compare with <paramref name="x" />.</param>
  /// <returns>
  /// <paramref name="x" /> if it is less than <paramref name="y" />; otherwise, <paramref name="y" />.</returns>
  static char INumberBase<char>.MinMagnitudeNumber(char x, char y)
  {
    return (char) Math.Min((ushort) x, (ushort) y);
  }

  /// <summary>Computes an estimate of (<paramref name="left" /> * <paramref name="right" />) + <paramref name="addend" />.</summary>
  /// <param name="left">The value to be multiplied with <paramref name="right" />.</param>
  /// <param name="right">The value to be multiplied with <paramref name="left" />.</param>
  /// <param name="addend">The value to be added to the result of <paramref name="left" /> multiplied by <paramref name="right" />.</param>
  /// <returns>An estimate of (<paramref name="left" /> * <paramref name="right" />) + <paramref name="addend" />.</returns>
  static char INumberBase<char>.MultiplyAddEstimate(char left, char right, char addend)
  {
    return (char) ((uint) left * (uint) right + (uint) addend);
  }

  /// <summary>Parses a string into a value.</summary>
  /// <param name="s">The string to parse.</param>
  /// <param name="style">A bitwise combination of number styles that can be present in <paramref name="s" />.</param>
  /// <param name="provider">An object that provides culture-specific formatting information about <paramref name="s" />.</param>
  /// <returns>The result of parsing <paramref name="s" />.</returns>
  static char INumberBase<char>.Parse(string s, NumberStyles style, IFormatProvider provider)
  {
    return char.Parse(s);
  }

  /// <summary>Parses a span of characters into a value.</summary>
  /// <param name="s">The span of characters to parse.</param>
  /// <param name="style">A bitwise combination of number styles that can be present in <paramref name="s" />.</param>
  /// <param name="provider">An object that provides culture-specific formatting information about <paramref name="s" />.</param>
  /// <returns>The result of parsing <paramref name="s" />.</returns>
  static char INumberBase<char>.Parse(
    ReadOnlySpan<char> s,
    NumberStyles style,
    IFormatProvider provider)
  {
    return char.Parse(s);
  }

  /// <param name="value" />
  /// <param name="result" />
  /// <typeparam name="TOther" />
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  static bool INumberBase<char>.TryConvertFromChecked<TOther>(TOther value, out char result)
  {
    if (typeof (TOther) == typeof (byte))
    {
      byte num = (byte) (object) value;
      result = (char) num;
      return true;
    }
    if (typeof (TOther) == typeof (Decimal))
    {
      Decimal num = (Decimal) (object) value;
      result = (char) num;
      return true;
    }
    if (typeof (TOther) == typeof (ushort))
    {
      ushort num = (ushort) (object) value;
      result = (char) num;
      return true;
    }
    if (typeof (TOther) == typeof (uint))
    {
      uint num = (uint) (object) value;
      result = checked ((char) num);
      return true;
    }
    if (typeof (TOther) == typeof (ulong))
    {
      ulong num = (ulong) (object) value;
      result = checked ((char) num);
      return true;
    }
    if (typeof (TOther) == typeof (UInt128))
    {
      UInt128 uint128 = (UInt128) (object) value;
      result = checked ((char) uint128);
      return true;
    }
    if (typeof (TOther) == typeof (UIntPtr))
    {
      UIntPtr num = (UIntPtr) (object) value;
      result = checked ((char) num);
      return true;
    }
    result = char.MinValue;
    return false;
  }

  /// <param name="value" />
  /// <param name="result" />
  /// <typeparam name="TOther" />
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  static bool INumberBase<char>.TryConvertFromSaturating<TOther>(TOther value, out char result)
  {
    if (typeof (TOther) == typeof (byte))
    {
      byte num = (byte) (object) value;
      result = (char) num;
      return true;
    }
    if (typeof (TOther) == typeof (Decimal))
    {
      Decimal num = (Decimal) (object) value;
      result = num >= 65535M ? char.MaxValue : (num <= 0M ? char.MinValue : (char) num);
      return true;
    }
    if (typeof (TOther) == typeof (ushort))
    {
      ushort num = (ushort) (object) value;
      result = (char) num;
      return true;
    }
    if (typeof (TOther) == typeof (uint))
    {
      uint num = (uint) (object) value;
      result = num >= (uint) ushort.MaxValue ? char.MaxValue : (char) num;
      return true;
    }
    if (typeof (TOther) == typeof (ulong))
    {
      ulong num = (ulong) (object) value;
      result = num >= (ulong) ushort.MaxValue ? char.MaxValue : (char) num;
      return true;
    }
    if (typeof (TOther) == typeof (UInt128))
    {
      UInt128 uint128 = (UInt128) (object) value;
      result = uint128 >= (UInt128) char.MaxValue ? char.MaxValue : (char) uint128;
      return true;
    }
    if (typeof (TOther) == typeof (UIntPtr))
    {
      UIntPtr num = (UIntPtr) (object) value;
      result = num >= new UIntPtr(65535) ? char.MaxValue : (char) num;
      return true;
    }
    result = char.MinValue;
    return false;
  }

  /// <param name="value" />
  /// <param name="result" />
  /// <typeparam name="TOther" />
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  static bool INumberBase<char>.TryConvertFromTruncating<TOther>(TOther value, out char result)
  {
    if (typeof (TOther) == typeof (byte))
    {
      byte num = (byte) (object) value;
      result = (char) num;
      return true;
    }
    if (typeof (TOther) == typeof (Decimal))
    {
      Decimal num = (Decimal) (object) value;
      result = num >= 65535M ? char.MaxValue : (num <= 0M ? char.MinValue : (char) num);
      return true;
    }
    if (typeof (TOther) == typeof (ushort))
    {
      ushort num = (ushort) (object) value;
      result = (char) num;
      return true;
    }
    if (typeof (TOther) == typeof (uint))
    {
      uint num = (uint) (object) value;
      result = (char) num;
      return true;
    }
    if (typeof (TOther) == typeof (ulong))
    {
      ulong num = (ulong) (object) value;
      result = (char) num;
      return true;
    }
    if (typeof (TOther) == typeof (UInt128))
    {
      UInt128 uint128 = (UInt128) (object) value;
      result = (char) uint128;
      return true;
    }
    if (typeof (TOther) == typeof (UIntPtr))
    {
      UIntPtr num = (UIntPtr) (object) value;
      result = (char) num;
      return true;
    }
    result = char.MinValue;
    return false;
  }

  /// <summary>Tries to convert an instance of the the current type to another type, throwing an overflow exception for any values that fall outside the representable range of the current type.</summary>
  /// <param name="value">The value that's used to create the instance of <typeparamref name="TOther" />.</param>
  /// <param name="result">When this method returns, contains an instance of <typeparamref name="TOther" /> converted from <paramref name="value" />.</param>
  /// <typeparam name="TOther">The type to which <paramref name="value" /> should be converted.</typeparam>
  /// <returns>
  /// <see langword="false" /> if <typeparamref name="TOther" /> is not supported; otherwise, <see langword="true" />.</returns>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  static bool INumberBase<char>.TryConvertToChecked<TOther>(char value, [MaybeNullWhen(false)] out TOther result)
  {
    if (typeof (TOther) == typeof (double))
    {
      double num = (double) value;
      result = (TOther) (ValueType) num;
      return true;
    }
    if (typeof (TOther) == typeof (Half))
    {
      Half half = (Half) value;
      result = (TOther) (ValueType) half;
      return true;
    }
    if (typeof (TOther) == typeof (short))
    {
      short num = checked ((short) value);
      result = (TOther) (ValueType) num;
      return true;
    }
    if (typeof (TOther) == typeof (int))
    {
      int num = (int) value;
      result = (TOther) (ValueType) num;
      return true;
    }
    if (typeof (TOther) == typeof (long))
    {
      long num = (long) value;
      result = (TOther) (ValueType) num;
      return true;
    }
    if (typeof (TOther) == typeof (Int128))
    {
      Int128 int128 = (Int128) value;
      result = (TOther) (ValueType) int128;
      return true;
    }
    if (typeof (TOther) == typeof (IntPtr))
    {
      char ch = value;
      result = (TOther) (ValueType) (IntPtr) ch;
      return true;
    }
    if (typeof (TOther) == typeof (sbyte))
    {
      sbyte num = checked ((sbyte) value);
      result = (TOther) (ValueType) num;
      return true;
    }
    if (typeof (TOther) == typeof (float))
    {
      float num = (float) value;
      result = (TOther) (ValueType) num;
      return true;
    }
    result = default (TOther);
    return false;
  }

  /// <summary>Tries to convert an instance of the the current type to another type, saturating any values that fall outside the representable range of the current type.</summary>
  /// <param name="value">The value that's used to create the instance of <typeparamref name="TOther" />.</param>
  /// <param name="result">When this method returns, contains an instance of <typeparamref name="TOther" /> converted from <paramref name="value" />.</param>
  /// <typeparam name="TOther">The type to which <paramref name="value" /> should be converted.</typeparam>
  /// <returns>
  /// <see langword="false" /> if <typeparamref name="TOther" /> is not supported; otherwise, <see langword="true" />.</returns>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  static bool INumberBase<char>.TryConvertToSaturating<TOther>(char value, [MaybeNullWhen(false)] out TOther result)
  {
    if (typeof (TOther) == typeof (double))
    {
      double num = (double) value;
      result = (TOther) (ValueType) num;
      return true;
    }
    if (typeof (TOther) == typeof (Half))
    {
      Half half = (Half) value;
      result = (TOther) (ValueType) half;
      return true;
    }
    if (typeof (TOther) == typeof (short))
    {
      short num = value >= '翿' ? short.MaxValue : (short) value;
      result = (TOther) (ValueType) num;
      return true;
    }
    if (typeof (TOther) == typeof (int))
    {
      int num = (int) value;
      result = (TOther) (ValueType) num;
      return true;
    }
    if (typeof (TOther) == typeof (long))
    {
      long num = (long) value;
      result = (TOther) (ValueType) num;
      return true;
    }
    if (typeof (TOther) == typeof (Int128))
    {
      Int128 int128 = (Int128) value;
      result = (TOther) (ValueType) int128;
      return true;
    }
    if (typeof (TOther) == typeof (IntPtr))
    {
      char ch = value;
      result = (TOther) (ValueType) (IntPtr) ch;
      return true;
    }
    if (typeof (TOther) == typeof (sbyte))
    {
      sbyte num = value >= '\u007F' ? sbyte.MaxValue : (sbyte) value;
      result = (TOther) (ValueType) num;
      return true;
    }
    if (typeof (TOther) == typeof (float))
    {
      float num = (float) value;
      result = (TOther) (ValueType) num;
      return true;
    }
    result = default (TOther);
    return false;
  }

  /// <summary>Tries to convert an instance of the the current type to another type, truncating any values that fall outside the representable range of the current type.</summary>
  /// <param name="value">The value that's used to create the instance of <typeparamref name="TOther" />.</param>
  /// <param name="result">When this method returns, contains an instance of <typeparamref name="TOther" /> converted from <paramref name="value" />.</param>
  /// <typeparam name="TOther">The type to which <paramref name="value" /> should be converted.</typeparam>
  /// <returns>
  /// <see langword="false" /> if <typeparamref name="TOther" /> is not supported; otherwise, <see langword="true" />.</returns>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  static bool INumberBase<char>.TryConvertToTruncating<TOther>(char value, [MaybeNullWhen(false)] out TOther result)
  {
    if (typeof (TOther) == typeof (double))
    {
      double num = (double) value;
      result = (TOther) (ValueType) num;
      return true;
    }
    if (typeof (TOther) == typeof (Half))
    {
      Half half = (Half) value;
      result = (TOther) (ValueType) half;
      return true;
    }
    if (typeof (TOther) == typeof (short))
    {
      short num = (short) value;
      result = (TOther) (ValueType) num;
      return true;
    }
    if (typeof (TOther) == typeof (int))
    {
      int num = (int) value;
      result = (TOther) (ValueType) num;
      return true;
    }
    if (typeof (TOther) == typeof (long))
    {
      long num = (long) value;
      result = (TOther) (ValueType) num;
      return true;
    }
    if (typeof (TOther) == typeof (Int128))
    {
      Int128 int128 = (Int128) value;
      result = (TOther) (ValueType) int128;
      return true;
    }
    if (typeof (TOther) == typeof (IntPtr))
    {
      char ch = value;
      result = (TOther) (ValueType) (IntPtr) ch;
      return true;
    }
    if (typeof (TOther) == typeof (sbyte))
    {
      sbyte num = (sbyte) value;
      result = (TOther) (ValueType) num;
      return true;
    }
    if (typeof (TOther) == typeof (float))
    {
      float num = (float) value;
      result = (TOther) (ValueType) num;
      return true;
    }
    result = default (TOther);
    return false;
  }

  /// <param name="s" />
  /// <param name="style" />
  /// <param name="provider" />
  /// <param name="result" />
  static bool INumberBase<char>.TryParse(
    [NotNullWhen(true)] string s,
    NumberStyles style,
    IFormatProvider provider,
    out char result)
  {
    return char.TryParse(s, out result);
  }

  /// <param name="s" />
  /// <param name="style" />
  /// <param name="provider" />
  /// <param name="result" />
  static bool INumberBase<char>.TryParse(
    ReadOnlySpan<char> s,
    NumberStyles style,
    IFormatProvider provider,
    out char result)
  {
    return char.TryParse(s, out result);
  }

  /// <summary>Parses a string into a value.</summary>
  /// <param name="s">The string to parse.</param>
  /// <param name="provider">An object that provides culture-specific formatting information about <paramref name="s" />.</param>
  /// <returns>The result of parsing <paramref name="s" />.</returns>
  static char IParsable<char>.Parse(string s, IFormatProvider provider) => char.Parse(s);

  /// <param name="s" />
  /// <param name="provider" />
  /// <param name="result" />
  static bool IParsable<char>.TryParse([NotNullWhen(true)] string s, IFormatProvider provider, out char result)
  {
    return char.TryParse(s, out result);
  }

  /// <summary>Shifts a value left by a given amount.</summary>
  /// <param name="value">The value that is shifted left by <paramref name="shiftAmount" />.</param>
  /// <param name="shiftAmount">The amount by which <paramref name="value" /> is shifted left.</param>
  /// <returns>The result of shifting <paramref name="value" /> left by <paramref name="shiftAmount" />.</returns>
  static char IShiftOperators<char, int, char>.operator <<(char value, int shiftAmount)
  {
    return (char) ((uint) value << (shiftAmount & 15));
  }

  /// <summary>Shifts a value right by a given amount.</summary>
  /// <param name="value">The value that is shifted right by <paramref name="shiftAmount" />.</param>
  /// <param name="shiftAmount">The amount by which <paramref name="value" /> is shifted right.</param>
  /// <returns>The result of shifting <paramref name="value" /> right by <paramref name="shiftAmount" />.</returns>
  static char IShiftOperators<char, int, char>.operator >>(char value, int shiftAmount)
  {
    return (char) ((uint) value >> (shiftAmount & 15));
  }

  /// <summary>Shifts a value right by a given amount.</summary>
  /// <param name="value">The value that is shifted right by <paramref name="shiftAmount" />.</param>
  /// <param name="shiftAmount">The amount by which <paramref name="value" /> is shifted right.</param>
  /// <returns>The result of shifting <paramref name="value" /> right by <paramref name="shiftAmount" />.</returns>
  static char IShiftOperators<char, int, char>.operator >>>(char value, int shiftAmount)
  {
    return (char) ((uint) value >> (shiftAmount & 15));
  }

  /// <summary>Parses a span of characters into a value.</summary>
  /// <param name="s">The span of characters to parse.</param>
  /// <param name="provider">An object that provides culture-specific formatting information about <paramref name="s" />.</param>
  /// <returns>The result of parsing <paramref name="s" />.</returns>
  static char ISpanParsable<char>.Parse(ReadOnlySpan<char> s, IFormatProvider provider)
  {
    return char.Parse(s);
  }

  /// <param name="s" />
  /// <param name="provider" />
  /// <param name="result" />
  static bool ISpanParsable<char>.TryParse(
    ReadOnlySpan<char> s,
    IFormatProvider provider,
    out char result)
  {
    return char.TryParse(s, out result);
  }

  /// <summary>Subtracts two values to compute their difference.</summary>
  /// <param name="left">The value from which <paramref name="right" /> is subtracted.</param>
  /// <param name="right">The value that's subtracted from <paramref name="left" />.</param>
  /// <returns>The value of <paramref name="right" /> subtracted from <paramref name="left" />.</returns>
  static char ISubtractionOperators<char, char, char>.operator -(char left, char right)
  {
    return (char) ((uint) left - (uint) right);
  }

  /// <summary>Subtracts two values to compute their difference.</summary>
  /// <param name="left">The value from which <paramref name="right" /> is subtracted.</param>
  /// <param name="right">The value that's subtracted from <paramref name="left" />.</param>
  /// <returns>The value of <paramref name="right" /> subtracted from <paramref name="left" />.</returns>
  static char ISubtractionOperators<char, char, char>.operator checked -(char left, char right)
  {
    return checked ((char) ((int) left - (int) right));
  }

  /// <summary>Computes the unary negation of a value.</summary>
  /// <param name="value">The value for which to compute the unary negation.</param>
  /// <returns>The unary negation of <paramref name="value" />.</returns>
  static char IUnaryNegationOperators<char, char>.operator -(char value) => -value;

  /// <summary>Computes the checked unary negation of a value.</summary>
  /// <param name="value">The value for which to compute the unary negation.</param>
  /// <returns>The unary negation of <paramref name="value" />.</returns>
  static char IUnaryNegationOperators<char, char>.operator checked -(char value)
  {
    return checked (-value);
  }

  /// <summary>Computes the unary plus of a value.</summary>
  /// <param name="value">The value for which to compute the unary plus.</param>
  /// <returns>The unary plus of <paramref name="value" />.</returns>
  static char IUnaryPlusOperators<char, char>.operator +(char value) => value;

  static char IUtfChar<char>.CastFrom(byte value) => (char) value;

  static char IUtfChar<char>.CastFrom(char value) => value;

  static char IUtfChar<char>.CastFrom(int value) => (char) value;

  static char IUtfChar<char>.CastFrom(uint value) => (char) value;

  static char IUtfChar<char>.CastFrom(ulong value) => (char) value;

  static uint IUtfChar<char>.CastToUInt32(char value) => (uint) value;

  static bool IBinaryIntegerParseAndFormatInfo<char>.IsSigned => false;

  static int IBinaryIntegerParseAndFormatInfo<char>.MaxDigitCount => 5;

  static int IBinaryIntegerParseAndFormatInfo<char>.MaxHexDigitCount => 4;

  static char IBinaryIntegerParseAndFormatInfo<char>.MaxValueDiv10 => 'ᦙ';

  #nullable enable
  static string IBinaryIntegerParseAndFormatInfo<char>.OverflowMessage => SR.Overflow_Char;

  static bool IBinaryIntegerParseAndFormatInfo<char>.IsGreaterThanAsUnsigned(char left, char right)
  {
    return (int) left > (int) right;
  }

  static char IBinaryIntegerParseAndFormatInfo<char>.MultiplyBy10(char value)
  {
    return (char) ((uint) value * 10U);
  }

  static char IBinaryIntegerParseAndFormatInfo<char>.MultiplyBy16(char value)
  {
    return (char) ((uint) value * 16U /*0x10*/);
  }
}
