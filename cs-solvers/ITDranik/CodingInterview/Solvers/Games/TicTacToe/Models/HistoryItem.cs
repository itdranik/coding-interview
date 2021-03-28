namespace ITDranik.CodingInterview.Solvers.Games.TicTacToe.Models
{
    public record HistoryItem(GameState PreviousStateType, PlayerMark Mark, Cell Cell);
}
