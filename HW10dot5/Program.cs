using HW10dot5;
using Microsoft.Extensions.DependencyInjection;

internal class Program
{
  private const int _countNumbers = 2;
  private static void Main(string[] args)
  {
    var services = new ServiceCollection()
      .AddTransient<ICalculator, Calculator>();

    using var serviceProvider = services.BuildServiceProvider();

    var calculator = serviceProvider.GetRequiredService<ICalculator>();

    var numbers = EnterNumbers(_countNumbers);

    Console.WriteLine("Сумма чисел:");

    Console.WriteLine(calculator.Sum(numbers[0], numbers[1]));
  }

  private static List<double> EnterNumbers(int countNumbers)
  {
    List<double> numbers = [];

    for(int i = 0; i < countNumbers; i++)
    {
      bool isCorrect = false;

      while (isCorrect != true)
      {
        Console.Write($"Введите {i + 1}-е число:");

        var number = Console.ReadLine();

        try
        {
          Console.WriteLine($"Конвертируем {number}...");

          double convertedNumber = Convert.ToDouble(number);

          numbers.Add(convertedNumber);

          Console.WriteLine($"Успешно");

          isCorrect = true;
        }
        catch (FormatException ex)
        {
          Console.WriteLine(ex.Message);
        }
        catch (OverflowException ex)
        {
          Console.WriteLine(ex.Message);
        }
        catch (Exception)
        {
          Console.WriteLine("Непредвиденная ошибка");

          throw new Exception("Непредвиденная ошибка");
        }
        finally
        {
          Console.WriteLine($"Конец блока конвертации");
        }
      }
    }

    return numbers;
  }
}