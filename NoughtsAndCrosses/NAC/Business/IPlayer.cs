namespace NAC.Business
{
    public interface IPlayer
    {
        string Name { get; }
        Board.SquareState SquareState { get; }
        string ToString();
    }
}