namespace CsharpBasic;
using static Console;
public static class TicTacToe
{
    // 기본 보드 초기값
    private static char[,]? _board;
    // 보드의 크기 
    private const int Size = 3;
    // 컴퓨터의 랜덤한 좌표 할당을 위한 Random 변수
    private static readonly Random Random = new Random();

    // 게임 시작
    public static void StartTicTacToe()
    {
        // 보드 초기화
        InitBoard();
        // 보드를 Console에 출력
        PrintBoard();
        // 게임 시작
        Play();
    }

    /// <summary>
    /// 보드의 초기화
    /// 2차원 배열중 정해진 좌표를 '공백'으로 할당하여 초기화
    /// </summary>
    private static void InitBoard()
    {
        /*
         * 2차원 배열을 보드에 할당하고
         * 해당 배열은 빈 공백으로 초기화
         */
        _board = new char[Size, Size];

        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                _board[i, j] = ' ';
            }
        }
    }

    /// <summary>
    /// 초기화 된 보드 및 선택된 선택지를 실행하고
    /// Console에 출력
    /// </summary>
    private static void PrintBoard()
    {
        Clear();
       
        // 상단에 열 번호 출력
        Write("   "); // 초기 공간
        for (int i = 0; i < Size; i++)
        {
            Write($"   {i}  ");
        }
        WriteLine();

        // 상단에 구분선 출력
        Write("   "); // 초기 공간
        WriteLine(new string('-', Size * 6 + 1)); // 구분선 길이 조정

        for (int i = 0; i < Size; i++)
        {
            // 좌측에 행 번호 출력
            Write($"{i}  |");

            for (int j = 0; j < Size; j++)
            {
                // 플레이어와 컴퓨터가 입력할 좌표는 빈 공간으로 표시
                Write($"  {_board![i, j]}  |"); // 행의 마지막 셀에도 세로 구분선 추가
            }

            // 줄을 바꿔서 다음 줄 생성
            WriteLine();

            if (i < Size - 1)
            {
                // 행 번호 바로 아래에 가로 구분 줄 생성
                Write("   "); // 행 번호와 맞추기 위한 공간
                WriteLine(new string('-', Size * 6 + 1)); // 구분선 길이 조정
            }
        }
    }

    /// <summary>
    /// 게임 플레이
    /// 1. 컴퓨터와 유저의 턴을 확인
    /// 2. 승리 여부를 확인
    /// 3. 승리자가 없다면 다음 사용자의 턴으로 변경
    /// </summary>
    private static void Play()
    {
        // 플레이어부터 시작
        bool isPlayerTurn = true;

        // 승리 조건이 충족될 때까지 루프
        while (true) 
        {
            char winner;
            if (isPlayerTurn)
            {
                Write("공격할 좌표를 입력 하세요 (예: 0,1) \n[0~2]까지 입력 가능 Play는 'X' 입니다.: "); 
                // input 값을 2차월 배열로 받음
                string? input = ReadLine();
                // 받은 배열을 ',' 를 기준으로 split
                string[]? separate = input?.Split(',');
                
                /*
                 * 입력값에 대한 예외처리
                 * 1. 입력받은 문자열의 값의 길이가 2가 아닐때
                 * 2. 배열의 0번이 int Type이 아닐때
                 * 3. 배열의 1번이 int Type이 아닐때
                 * 4. 입력값의 범위가 해당 범위를 넘어설 때
                 * 5. 입력하려는 좌표의 위치가 공백이 아닐때 
                 */
                if (separate!.Length != 2 
                    || !int.TryParse(separate[0], out int row) 
                    || !int.TryParse(separate[1], out int col) 
                    || row < 0 || row >= Size || col < 0 || col >= Size 
                    || _board![row, col] != ' ')
                {
                    WriteLine("\n이미 지정된 좌표 입니다. 다시 입력해주세요\n");
                    continue;
                }

                // 플레이어의 심볼을 'X'로 지정
                _board[row, col] = 'X';
                // 입력한 값을 Display에 표시
                PrintBoard();
                // 만약 유저의 승리확인을 하여 반환값이 있으면 해당 사용자의 정보를 콘솔에 출력
                winner = CheckForWinner();
                if (winner != ' ')
                {
                    WriteLine(winner + " 플레이어가 승리했습니다!");
                    break;
                }

                // 만약 보드가 모두 꽉 차게 되면 무승부를 콘솔에 출력 합니다.
                if (IsBoardFull())
                {
                    WriteLine("무승부입니다!");
                    break;
                }
            } 
            // 만약 모두 해당되지 않는다면 컴퓨터의 턴을 실행
            else
            {
                // 컴퓨터의 턴을 실행
                TakeComputerTurn();
                // 디스플레이에 출력
                PrintBoard();
                // 컴퓨터의 승리 확인
                winner = CheckForWinner();
                if (winner != ' ')
                {
                    WriteLine(winner + " 컴퓨터가 승리했습니다!");
                    break;
                }

                if (IsBoardFull())
                {
                    WriteLine("무승부입니다!");
                    break;
                }
            }
            /*
             * 턴 교대
             * 유저라면 Computer로 Computer라면 유저로
             */ 
            isPlayerTurn = !isPlayerTurn;
        }
    }

    /// <summary>
    /// 컴퓨터의 행동을 실행
    /// 정해진 Size에서 0 ~ 2 까지 중 선택
    /// 만약 비어있는 좌표를 확인하면 'O'를 할당
    /// </summary>
    private static void TakeComputerTurn()
    {
        /*
         * 컴퓨터의 턴이 실행되면 Random값을 지정된 사이즈 3 x 3에서 추출
         * 추출한 랜덤한 int를 2차원 배열인 보드에 할당
         */
        while (true)
        {
            int row = Random.Next(Size);
            int col = Random.Next(Size);

            if (_board != null && _board[row, col] != ' ') continue;
            if (_board != null) _board[row, col] = 'O';
            break;
        }
    }
    
    
    /// <summary>
    /// 승리를 확인하는 메서드
    /// 열과 행 그리고 대각선을 검사
    /// 반환값은 char 타입을 반환하며
    /// 플레이어가 승리하면, 'X'를 반환
    /// 컴퓨터가 승리하면, 'O'를 반환
    /// </summary>
    /// <returns></returns>
    private static char CheckForWinner()
    {
        // 행 검사
        for (int row = 0; row < Size; row++)
        {
            if (_board != null && _board[row, 0] != ' ' && _board[row, 0] == _board[row, 1] && _board[row, 1] == _board[row, 2])
            {
                return _board[row, 0];
            }
        }

        // 열 검사
        for (int col = 0; col < Size; col++)
        {
            if (_board != null && _board[0, col] != ' ' && _board[0, col] == _board[1, col] && _board[1, col] == _board[2, col])
            {
                return _board[0, col];
            }
        }

        // 대각선 검사
        if (_board != null && _board[0, 0] != ' ' && _board[0, 0] == _board[1, 1] && _board[1, 1] == _board[2, 2])
        {
            return _board[0, 0];
        }
        if (_board != null && _board[0, 2] != ' ' && _board[0, 2] == _board[1, 1] && _board[1, 1] == _board[2, 0])
        {
            return _board[0, 2];
        }
        // 승자가 없는 경우
        return ' ';
    }

    /// <summary>
    /// 보드가 가득 차면 true를 반환
    /// '' 공백이 있다면 false를 반환
    /// </summary>
    /// <returns></returns>
    private static bool IsBoardFull()
    {
        for (int row = 0; row < Size; row++)
        {
            for (int col = 0; col < Size; col++)
            {
                if (_board != null && _board[row, col] == ' ')
                {
                    return false;
                }
            }
        }
        return true;
    }
}