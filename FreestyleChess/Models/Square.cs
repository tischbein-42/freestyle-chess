namespace FreestyleChess.Models
{
    public class Square
    {
        public int Rank { get; set; }
        public int File { get; set; }
        public Piece? Piece { get; set; }
        public bool IsLightSquare => (Rank + File) % 2 == 0;

        public Square() { }

        public Square(int rank, int file)
        {
            Rank = rank;
            File = file;
        }
    }
}