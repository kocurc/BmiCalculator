using BMI.Enums;

namespace Bmi;

/// <summary>
/// BMI ratio calculator. It can either use SI or imperial units.
/// </summary>
public class BmiCalculator
{
    /// <value>Property <c>Weight</c> determines person's weight in kilograms as default.</value>
    public double Weight { get; set; }
    /// <value>Property <c>Height</c> determines person's height in meters as default.</value>
    public double Height { get; set; }
    /// <value><c>Age</c> of o a person.</value>
    public byte Age { get; set; }
    /// <value>Type of units used to calculate BMI ratio. Either SI or imperial. SI is used as default one.</value>
    public UnitsType UnitsType { get; set; } = UnitsType.SI;

    /// <summary>
    /// This constructor initializes the new BmiCalculator with units type set to SI.
    /// </summary>
    /// <param name="weight">Weight of a person.</param>
    /// <param name="height">Height of a person.</param>
    /// <param name="age">Age of a person.</param>
    public BmiCalculator(double weight, double height, byte age)
    {
        Weight = weight;
        Height = height;
        Age = age;
    }

    /// <summary>
    /// This constructor allows to initialize BmiCalculator with specific units type.
    /// </summary>
    /// <param name="weight">Weight of a person.</param>
    /// <param name="height">Height of a person.</param>
    /// <param name="age">Age of a person.</param>
    /// <param name="unitsType">Units type used for all parameters.</param>
    public BmiCalculator(double weight, double height, byte age, UnitsType unitsType)
    {
        Weight = weight;
        Height = height;
        Age = age;
        UnitsType = unitsType;
    }

    /// <summary>
    /// This method allows to calculate BMI ratio.
    /// </summary>
    /// <returns>BMI ratio in double value type.</returns>
    public double Calculate()
    {
        if (UnitsType == UnitsType.SI)
        {
            return Math.Round(Weight / Math.Pow(Height, 2), 2);
        }

        return Math.Round((Weight / Math.Pow(Height, 2) * 703), 2);
    }

    /// <summary>
    /// This method allows to categorize person's BMI category.
    /// </summary>
    /// <param name="bmiRatio">BMI ratio value.</param>
    /// <returns>It returns <c>BmiCategory</c> enum value.</returns>
    public static BmiCategory Categorize(double bmiRatio)
    {
        return bmiRatio switch
        {
            < 16.0 => BmiCategory.SevereThinness,
            >= 16.0 and < 17.0 => BmiCategory.ModerateThinness,
            >= 17.0 and < 18.5 => BmiCategory.MildThinness,
            >= 18.5 and < 25.0 => BmiCategory.Normal,
            >= 25 and < 30.0 => BmiCategory.Overweight,
            >= 30 and < 35.0 => BmiCategory.ObeseClassI,
            >= 35 and < 40.0 => BmiCategory.ObeseClassII,
            _ => BmiCategory.ObeseClassIII
        };
    }

    /// <summary>
    /// This method allows to determine whether BMI ratio of a person is correct according to its age.
    /// </summary>
    /// <param name="age">Age of a person.</param>
    /// <param name="bmiRatio">BMI ratio of a person.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException">Exception that is thrown when age of a person
    /// is less than minimal valid value, which is 19.</exception>
    public static bool IsRatioCorrect(byte age, double bmiRatio)
    {
        return age switch
        {
            < 19 => throw new ArgumentOutOfRangeException(nameof(age),
                "The minimum age possible for BMI indicator is 19 years."),
            >= 19 and <= 24 => bmiRatio is >= 19 and <= 24,
            >= 25 and <= 34 => bmiRatio is >= 20 and <= 25,
            >= 35 and <= 44 => bmiRatio is >= 21 and <= 26,
            >= 45 and <= 54 => bmiRatio is >= 22 and <= 27,
            >= 55 and <= 64 => bmiRatio is >= 23 and <= 28,
            _ => bmiRatio is >= 24 and <= 29
        };
    }
}
