namespace FreestyleChess.Models
{
    public class MoveRequest
    {
        public int FromRank { get; set; }
        public int FromFile { get; set; }
        public int ToRank { get; set; }
        public int ToFile { get; set; }
    }
}