using System;
using BMI.Enums;
using FluentAssertions;
using NUnit.Framework;
using Bmi;

namespace BmiTests;

[TestFixture]
public class UnitTests
{
    [Test]
    public void WhenUnitsTypeIsMetrical_ThenBmiRatioIsCorrect()
    {
        // Arrange
        BmiCalculator bmiCalculator = new(70.0, 1.70, 25);

        // Act
        var bmiRatio = bmiCalculator.Calculate();

        // Assert
        bmiRatio.Should().Be(24.22);
    }

    [Test]
    public void WhenUnitsTypeIsImperial_ThenBmiRatioIsCorrect()
    {
        // Arrange
        BmiCalculator bmiCalculator = new(154.32, 66.93, 25, UnitsType.Imperial);

        // Act
        var bmiRatio = bmiCalculator.Calculate();

        // Assert
        bmiRatio.Should().Be(24.22);
    }

    [Test]
    [TestCase(19, 18.99, false)]
    [TestCase(19, 19, true)]
    [TestCase(19, 24, true)]
    [TestCase(19, 24.01, false)]

    [TestCase(24, 18.99, false)]
    [TestCase(24, 19, true)]
    [TestCase(24, 24, true)]
    [TestCase(24, 24.01, false)]

    [TestCase(25, 19.99, false)]
    [TestCase(25, 20, true)]
    [TestCase(25, 25, true)]
    [TestCase(25, 25.01, false)]

    [TestCase(34, 19.99, false)]
    [TestCase(34, 20, true)]
    [TestCase(34, 25, true)]
    [TestCase(34, 25.01, false)]

    [TestCase(35, 20.99, false)]
    [TestCase(35, 21, true)]
    [TestCase(35, 26, true)]
    [TestCase(35, 26.01, false)]

    [TestCase(44, 20.99, false)]
    [TestCase(44, 21, true)]
    [TestCase(44, 26, true)]
    [TestCase(44, 26.01, false)]

    [TestCase(45, 21.99, false)]
    [TestCase(45, 22, true)]
    [TestCase(45, 27, true)]
    [TestCase(45, 27.01, false)]

    [TestCase(54, 21.99, false)]
    [TestCase(54, 22, true)]
    [TestCase(54, 27, true)]
    [TestCase(54, 27.01, false)]

    [TestCase(55, 22.99, false)]
    [TestCase(55, 23, true)]
    [TestCase(55, 28, true)]
    [TestCase(55, 28.01, false)]

    [TestCase(64, 22.99, false)]
    [TestCase(64, 23, true)]
    [TestCase(64, 28, true)]
    [TestCase(64, 28.01, false)]

    [TestCase(65, 23.99, false)]
    [TestCase(65, 24, true)]
    [TestCase(65, 29, true)]
    [TestCase(65, 29.01, false)]
    public void GivenIsBmiRatioCorrectForGivenAge(byte age, double bmiRatio, bool shouldBeCorrect)
    {
        // Asert
        BmiCalculator.IsRatioCorrect(age, bmiRatio).Should().Be(shouldBeCorrect);
    }

    [Test]
    public void GivenIsRatioCorrectMethod_WhenAgeIsLessThan19_ThenThrowsArgumentOutOfRangeException()
    {
        // Arrange
        Func<bool> calculation = () => BmiCalculator.IsRatioCorrect(18, 20);

        // Assert
        calculation.Should().Throw<ArgumentOutOfRangeException>()
            .Where(exception => exception.Message.Contains("The minimum age possible for BMI indicator is 19 years."));
    }

    [Test]
    public void GivenAge_WhenAgeIsChanged_ThenAgeValueCanBeRead()
    {
        // Arrange
        BmiCalculator bmiCalc = new(154.32, 66.93, 25, UnitsType.Imperial);

        // Act
        bmiCalc.Age = 50;

        // Assert
        bmiCalc.Age.Should().Be(50);
    }

    [Test]
    [TestCase(15.99, BmiCategory.SevereThinness)]
    [TestCase(16.00, BmiCategory.ModerateThinness)]
    [TestCase(16.99, BmiCategory.ModerateThinness)]
    [TestCase(17.00, BmiCategory.MildThinness)]
    [TestCase(18.49, BmiCategory.MildThinness)]
    [TestCase(18.50, BmiCategory.Normal)]
    [TestCase(24.99, BmiCategory.Normal)]
    [TestCase(25.00, BmiCategory.Overweight)]
    [TestCase(29.99, BmiCategory.Overweight)]
    [TestCase(30.00, BmiCategory.ObeseClassI)]
    [TestCase(34.99, BmiCategory.ObeseClassI)]
    [TestCase(35.00, BmiCategory.ObeseClassII)]
    [TestCase(39.99, BmiCategory.ObeseClassII)]
    [TestCase(40.00, BmiCategory.ObeseClassIII)]
    public void GivenIsBmiCategoryCorrectForBmiRatio(double bmiRatio, BmiCategory bmiCategory)
    {
        // Asert
        BmiCalculator.Categorize(bmiRatio).Should().Be(bmiCategory);
    }
}
