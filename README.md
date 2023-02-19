Your post states that you want to **wait for another method to be triggered before continuing** and then describes three "states of play" so my first suggestion is to identify in code exactly the things we need to wait for in the chess game loop.

    enum StateOfPlay
    {
        PlayerChooseFrom,
        PlayerChooseTo,
        OpponentTurn,
    }

***
**Game Board**

But before we start a game _loop_ we need a game _board_ so use `TableLayoutPanel` do do that.

    enum PlayerColor
    {
        White,
        Black,
    }
    enum StateOfPlay
    {
        PlayerChooseFrom,
        PlayerChooseTo,
        OpponentTurn,
    }
    public partial class MainForm : Form
    {
        public MainForm() => InitializeComponent();

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            // Fix rounding.
            tableLayoutPanel.Size = new Size(507, 507);
            for (int column = 0; column < 8; column++)
                for (int row = 0; row < 8; row++)
                    addSquare(row, column);
            // Random draw
            PlayerColor playerColor =  (PlayerColor)_rando.Next(2);
            Text = $"Player is {playerColor}";
            _ = playGameAsync(playerColor);
        }
    
This method adds a new `Square`.

    private void addSquare(int column, int row)
    {
        var color = ((column + row) % 2) == 0 ? Color.White : Color.Black;
        var square = new Square
        {
            BackColor = color,
            Column = column,
            Row = row,
            Size = new Size(80, 80),
            Margin = new Padding(0),
            Padding = new Padding(10),
            Anchor = (AnchorStyles)0xf,
            SizeMode = PictureBoxSizeMode.StretchImage,
        };
        tableLayoutPanel.Controls.Add(square, column, row);
        // Hook the mouse events here
        square.Click += onSquareClicked;
    }

    A `Square` is derived from `PictureBox` so that images of pieces can be displayed.

    class Square : PictureBox 
    {
        public int Column { get; internal set; }
        public int Row { get; internal set; }
        public string Notation => $"{(char)(Column + 'a')}{8 - Row}";
        public override string ToString() =>
            Piece == Piece ?
                $"Empty {BackColor.Name} square [column:{Column} row:{Row}]" :
                $"{Piece} piece [column:{Column} row:{Row}]";
        public Piece Piece { get; set; }
    }
    enum Player { Black, White };
    enum PieceType { Pawn, Rook, Knight, Bishop, Queen, King }
    class Piece
    {
        public Player Player { get; internal set; }
        public PieceType PieceType { get; internal set; }
        public override string ToString() =>
            $"{Player} {PieceType}";
    }


**Game Loop**

The goal is to run a loop that cycles these three states continuously, waiting at each step. However, the main Form is always running it's own Message Loop to detect mouse clicks and key presses and it's important not to block that loop with our own.

[![screenshot][1]][1] 

***
The `await` keyword causes a method to return _immediately_ which allows the UI loop to keep running. But when "something happens" that we're waiting for, the execution of this method will _resume_ on the next line after the `await`. A [semaphore](https://learn.microsoft.com/en-us/dotnet/api/system.threading.semaphoreslim) object says when to stop or go and is initialized here in the waiting state.

    SemaphoreSlim _semaphoreClick= new SemaphoreSlim(0, 1); 

When the game board is clicked during the players turn then the `Release() method will be called on the semaphore, allowing things to resume.

    private async Task playGameAsync(PlayerColor playerColor)
    {
        StateOfPlay = 
            playerColor.Equals(PlayerColor.White) ?
                StateOfPlay.PlayerChooseFrom :
                StateOfPlay.OpponentTurn;

        while(!_checkmate)
        {
            switch (StateOfPlay)
            {
                case StateOfPlay.PlayerChooseFrom:
                    await _semaphoreClick.WaitAsync();
                    StateOfPlay = StateOfPlay.PlayerChooseTo;
                    break;
                case StateOfPlay.PlayerChooseTo:
                    await _semaphoreClick.WaitAsync();
                    StateOfPlay = StateOfPlay.OpponentTurn;
                    break;
                case StateOfPlay.OpponentTurn:
                    await opponentMove();
                    StateOfPlay = StateOfPlay.PlayerChooseFrom;
                    break;
            }
        }
    }

***
**Player's turn**

Here we have to wait for each square to get clicked. A straightforward way to do this is with a `SemaphoreSlim` object and call `Release()` when the game board is clicked during the player's turn.

    Square _playerFrom, _playerTo, _opponentFrom, _opponentTo;

    private void onSquareClicked(object sender, EventArgs e)
    {
        if (sender is Square square)
        {
            switch (StateOfPlay)
            {
                case StateOfPlay.OpponentTurn:
                    // Disabled for opponent turn
                    return;
                case StateOfPlay.PlayerChooseFrom:
                    _playerFrom = square;
                    Text = $"Player {_playerFrom.Notation} : _";
                    break;
                case StateOfPlay.PlayerChooseTo:
                    _playerTo = square;
                    Text = $"Player {_playerFrom.Notation} : {_playerTo.Notation}";
                    richTextBox.SelectionColor = Color.DarkGreen;
                    richTextBox.AppendText($"{_playerFrom.Notation} : {_playerTo.Notation}{Environment.NewLine}");
                    break;
            }
            _semaphoreClick.Release();
        }
    }

***
**Opponents turn**

This simulates a computer opponent processing an algorithm to determine its next move.

    private async Task opponentMove()
    {
        Text = "Opponent thinking";
            
        for (int i = 0; i < _rando.Next(5, 10); i++)
        {
            Text += ".";
            await Task.Delay(1000);
        }
        string opponentMove = "xx : xx";
        Text = $"Opponent Moved {opponentMove}";
        richTextBox.SelectionColor = Color.DarkBlue;
        richTextBox.AppendText($"{opponentMove}{Environment.NewLine}");
    }

  [1]: https://i.stack.imgur.com/nt8nn.png