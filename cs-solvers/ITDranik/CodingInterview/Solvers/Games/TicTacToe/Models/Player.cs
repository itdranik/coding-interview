namespace ITDranik.CodingInterview.Solvers.Games.TicTacToe.Models
{
    public class Player
    {
        public PlayerMark Mark { get; }

        public Player(PlayerMark mark)
        {
            Mark = mark;
        }
    }
}
