using HW10dot5;
using HW10dot5.Enums;
using HW10dot5.Log;
using Microsoft.Extensions.DependencyInjection;

internal class Program
{
  private const int _countNumbers = 2;
  private static void Main(string[] args)
  {
    var services = new ServiceCollection()
      .AddTransient<ICalculator, Calculator>()
      .AddSingleton<ILogger, Logger>();

    using var serviceProvider = services.BuildServiceProvider();

    var calculator = serviceProvider.GetRequiredService<ICalculator>();

    var logger = serviceProvider.GetRequiredService<ILogger>();

    var numbers = EnterNumbers(_countNumbers, logger);

    logger.Info(message: "Сумма чисел:", typeOfText: TypeOfText.Message);

    logger.Info(message: calculator.Sum(numbers[0], numbers[1]).ToString(), typeOfText: TypeOfText.Message);
  }

  private static List<double> EnterNumbers(int countNumbers, ILogger logger)
  {
    List<double> numbers = [];

    for(int i = 0; i < countNumbers; i++)
    {
      bool isCorrect = false;

      while (isCorrect != true)
      {
        logger.Info(message: $"Введите {i + 1}-е число:", typeOfText: TypeOfText.Message);

        var number = Console.ReadLine();

        try
        {
          logger.Info(message: $"Конвертируем {number}...", typeOfText: TypeOfText.Message);

          double convertedNumber = Convert.ToDouble(number);

          numbers.Add(convertedNumber);

          logger.Info(message: $"Успешно", typeOfText: TypeOfText.Message);

          isCorrect = true;
        }
        catch (FormatException ex)
        {
          logger.Info(message: ex.Message, typeOfText: TypeOfText.Error);
        }
        catch (OverflowException ex)
        {
          logger.Info(message: ex.Message, typeOfText: TypeOfText.Error);
        }
        catch (Exception)
        {
          logger.Info(message: "Непредвиденная ошибка", typeOfText: TypeOfText.Error);

          throw new Exception("Непредвиденная ошибка");
        }
        finally
        {
          logger.Info($"Конец блока конвертации", typeOfText: TypeOfText.Message);
        }
      }
    }

    return numbers;
  }
}