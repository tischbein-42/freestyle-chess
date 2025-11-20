namespace FreestyleChess.Models
{
    public class SerializableBoard
    {
        public List<SerializableSquare> Squares { get; set; } = new();
    }

    public class SerializableSquare
    {
        public int Rank { get; set; }
        public int File { get; set; }
        public SerializablePiece? Piece { get; set; }
    }

    public class SerializablePiece
    {
        public required string Type { get; set; }   // Statt Enum
        public required string Color { get; set; }  // Statt Enum
    }
}