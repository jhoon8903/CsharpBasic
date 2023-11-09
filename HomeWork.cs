using System.Diagnostics;
using System.Numerics;

namespace Algorithm_Csharp;

public static class HomeWork
{
    public static void NameInput()
    {
        Console.WriteLine("이름과 나이를 입력해주세요.(두 항목 은 ' ' 공백으로 1칸 띄워주세요)");
        Console.WriteLine("예 : Daniel 34");
        Console.Write("이름 나이");
        string? input =  Console.ReadLine();
        string[] inputValue = input.Split(' ');
        Console.WriteLine($"입력하신 이름은 : {inputValue[0]}, 나이 : {inputValue[1]}");
    }

    public static void Calculate()
    {
        while (true)
        {
            Console.WriteLine("계산을 두 수를 입력 하세요"); 
            Console.Write("첫번째 숫지 'A' : ");
            double? value1 = double.Parse(Console.ReadLine() ?? string.Empty);
            Console.Write("두번째 숫자 'B' : ");
            double? value2 = double.Parse(Console.ReadLine() ?? string.Empty);

            Console.WriteLine($"A + B = {value1 + value2}");
            Console.WriteLine($"A - B = {value1 - value2}");
            Console.WriteLine($"A * B = {value1 * value2}");
            Console.WriteLine($"A / B = {value1 / value2}");
            Console.WriteLine($"A % B = {value1 % value2}");
            Console.WriteLine();
        }
    }

    public static void TemperatureCalculator()
    {
        Console.WriteLine("변환하고자 하는 섭씨온도를 입력해주세요."); 
        Console.Write("변환섭씨온도 ℃ : ");
        double? celsius = double.Parse(Console.ReadLine() ?? string.Empty);

        // F = ((9 / 5) * C) + 32
        double fahrenheitScale = (double)((double)(9 / 5) * celsius + 32);
        Console.WriteLine($"변환된 섭씨온도는 : 화씨 {fahrenheitScale}F 입니다.");
    }

    public static void BMI()
    { 
        Console.WriteLine("BMI를 측정하기 위한 데이터를 입력 하세요"); 
        Console.Write("체중 (kg) : ");
        double? weight = double.Parse(Console.ReadLine() ?? string.Empty);
        Console.Write("키 (m) : ");
        double? height = double.Parse(Console.ReadLine() ?? string.Empty);

        // BMI = 체중(kg) / 신장(m)^2
        double? bmi = weight / Math.Pow((double)height, 2);
        string status = weight switch
        {
            < 18.5 => "저체중",
            >= 18.5 and <= 23 => "정상",
            > 23 and <= 25 => "과체중",
            > 25 and <= 30 => "비만",
            > 30 => "고도비만",
            _ => ""
        };
        Console.WriteLine();
        Console.WriteLine($"당신의 BMI는 {bmi:F1} 이 수치는 '{status}' 입니다.");
    }

    public static void FindNumberGame()
    {
        while (true)
        {
            Random random = new Random();
            List<int> selectList = new List<int>();
            List<int> inputList = new List<int>();

            while (selectList.Count < 4)
            {
                selectList.Add(random.Next(0, 11));
            }

            Console.WriteLine("[0 ~ 9]까지의 숫자를 3개 입력하고 컴퓨터가 고른 숫자를 맞춰보세요");
            Console.Write("첫 번째 숫자 : ");
            int inputNumber1 = int.Parse(Console.ReadLine() ?? string.Empty);
            inputList.Add(inputNumber1);

            Console.Write("두 번째 숫자 : ");
            int inputNumber2 = int.Parse(Console.ReadLine() ?? string.Empty);
            inputList.Add(inputNumber2);

            Console.Write("세 번째 숫자 : ");
            int inputNumber3 = int.Parse(Console.ReadLine() ?? string.Empty);
            inputList.Add(inputNumber3);

            int matchCount = selectList.Count(selectNum => inputList.Contains(selectNum));

            switch (matchCount)
            {
                case 3:
                    Console.WriteLine(" 모두 맞았습니다.");
                    break;
                case 2:
                    Console.WriteLine("2개 맞았습니다.");
                    break;
                case 1:
                    Console.WriteLine("1개 맞았습니다.");
                    break;
                default:
                    Console.WriteLine("모두 틀렷습니다.");
                    break;
            }

            Console.Write("재시작 하시겠습니까? [Y / N]");
            string? restartKeyword = Console.ReadLine();
            if (restartKeyword is "Y" or "y")
            {
                continue;
            }
            Environment.Exit(0);
            Console.WriteLine();
            break;
        }
    }
}