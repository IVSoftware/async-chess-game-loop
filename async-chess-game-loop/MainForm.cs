using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace async_chess_game_loop
{
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

        Random _rando = new Random();
        SemaphoreSlim _semaphoreClick= new SemaphoreSlim(0, 1);

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

        StateOfPlay _stateOfPlay = (StateOfPlay)(-1);
        StateOfPlay StateOfPlay
        {
            get => _stateOfPlay;
            set
            {
                if (!Equals(_stateOfPlay, value))
                {
                    _stateOfPlay = value;
                    switch (StateOfPlay)
                    {
                        case StateOfPlay.PlayerChooseFrom:
                            _opponentClock.Stop();
                            _ = _playerClock.Start();
                            break;
                        case StateOfPlay.OpponentTurn:
                            _playerClock.Stop();
                            _ = _opponentClock.Start();
                            break;
                    }
                }
            }
        }

        bool _checkmate = false;

        Square
            _playerFrom,
            _playerTo,
            _opponentFrom,
            _opponentTo;
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
        protected override CreateParams CreateParams
        {
            get
            {
                const int WS_EX_COMPOSITED = 0x02000000;
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= WS_EX_COMPOSITED;
                return cp;
            }
        }
    }

    class ChessClock : Label
    {
        Stopwatch _stopwatch= new Stopwatch();
        internal async Task Start()
        {
            BackColor= Color.LightBlue;
            _stopwatch.Start();
            while(_stopwatch.IsRunning)
            {
                await Task.Delay(500);
                Text = _stopwatch.Elapsed.ToString(@"h\:mm\:ss");
            }
        }

        internal void Stop()
        {
            BackColor= SystemColors.Control;
            _stopwatch.Stop();
        }
    }

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
}
