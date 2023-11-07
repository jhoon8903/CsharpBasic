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


    private static char[,]? _board;
    private const int Size = 3;
    private static readonly Random Random = new Random();
    public static void TicTacToe()
    {
        _board = new char[Size, Size];
        InitBoard();
        DisplayBoard();
        Play();
    }

    private static void InitBoard()
    {
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                // if (_board != null) 
                    _board[i, j] = ' ';
            }
        }
    }

    private static void DisplayBoard()
    { 
        Console.WriteLine();
        Console.WriteLine();
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                Console.Write(_board[i, j]);

                if (j < Size - 1)
                {
                    Console.Write("|");
                }
            }
            Console.WriteLine();
            if (i < Size - 1)
            {
                Console.WriteLine("-----");
            }
        }
    }

    private static void Play()
    {
        bool isPlayerTurn = true; // 플레이어부터 시작
        while (true) // 또는 승리 조건이 충족될 때까지 루프
        {
            var winner = ' ';
            if (isPlayerTurn)
            {
                Console.Write("공격할 좌표를 입력 하세요 (예: 0,1) \n[0~2]까지 입력 가능 Play는 'X' 입니다.: ");
                string input = Console.ReadLine();
                string[] separate = input.Split(',');

                if (separate.Length != 2 
                    || !int.TryParse(separate[0], out int row) 
                    || !int.TryParse(separate[1], out int col) 
                    || row < 0 || row >= Size || col < 0 || col >= Size || _board[row, col] != ' ')
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 시도하세요.");
                    continue;
                }

                _board[row, col] = 'X'; // 플레이어의 심볼을 'X'로 가정
                DisplayBoard();
                winner = CheckForWinner();
                if (winner != ' ')
                {
                    Console.WriteLine(winner + " 플레이어가 승리했습니다!");
                    break;
                }

                if (IsBoardFull())
                {
                    Console.WriteLine("무승부입니다!");
                    break;
                }
            }
            else // 컴퓨터의 턴
            {
                TakeComputerTurn();
                DisplayBoard();
                winner = CheckForWinner();
                if (winner != ' ')
                {
                    Console.WriteLine(winner + " 컴퓨터가 승리했습니다!");
                    break;
                }

                if (IsBoardFull())
                {
                    Console.WriteLine("무승부입니다!");
                    break;
                }
            }
            // 턴 교대
            isPlayerTurn = !isPlayerTurn;
        }
    }

    private static void TakeComputerTurn()
    {
        while (true)
        {
            int row = Random.Next(Size);
            int col = Random.Next(Size);

            if (_board != null && _board[row, col] != ' ') continue;
            if (_board != null) _board[row, col] = 'O';
            break;
        }
    }
    
    private static char CheckForWinner()
    {
        // 행 검사
        for (int row = 0; row < Size; row++)
        {
            if (_board[row, 0] != ' ' && _board[row, 0] == _board[row, 1] && _board[row, 1] == _board[row, 2])
            {
                return _board[row, 0];
            }
        }

        // 열 검사
        for (int col = 0; col < Size; col++)
        {
            if (_board[0, col] != ' ' && _board[0, col] == _board[1, col] && _board[1, col] == _board[2, col])
            {
                return _board[0, col];
            }
        }

        // 대각선 검사
        if (_board[0, 0] != ' ' && _board[0, 0] == _board[1, 1] && _board[1, 1] == _board[2, 2])
        {
            return _board[0, 0];
        }

        if (_board[0, 2] != ' ' && _board[0, 2] == _board[1, 1] && _board[1, 1] == _board[2, 0])
        {
            return _board[0, 2];
        }
        // 승자가 없는 경우
        return ' ';
    }

    private static bool IsBoardFull()
    {
        for (int row = 0; row < Size; row++)
        {
            for (int col = 0; col < Size; col++)
            {
                if (_board[row, col] == ' ')
                {
                    return false;
                }
            }
        }
        return true;
    }
}

public static class SnakeGame
{
    public enum SnakeStatus { MOVE, IDLE, EAT, DIE }

}

public static class FoodCreator
{

}