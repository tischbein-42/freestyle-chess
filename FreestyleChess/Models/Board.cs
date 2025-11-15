namespace FreestyleChess.Models
{
    public class Board
    {
        // 8x8 array representing the chess board
        public Square[,] Squares { get; set; } = new Square[8, 8];

        // Constructor to initialize an empty board
        public Board()
        {
            InitializeEmptyBoard();
        }

        // Method to initialize an empty board
        public void InitializeEmptyBoard()
        {
            for (int rank = 0; rank < 8; rank++)
            {
                for (int file = 0; file < 8; file++)
                {
                    Squares[rank, file] = new Square(rank, file);
                }
            }
        }

        // Method to get the piece at a specific square
        public Piece? GetPieceAt(int rank, int file)
        {
            return Squares[rank, file].Piece;
        }

        // Method to place a piece at a specific square
        public void PlacePiece(Piece piece, int rank, int file)
        {
            Squares[rank, file].Piece = piece;
        }

        // Method to remove a piece from a specific square
        public void RemovePiece(int rank, int file)
        {
            Squares[rank, file].Piece = null;
        }
        
        // Method to clear the board
        public void ClearBoard()
        {
            for (int r = 0; r < 8; r++)
                for (int f = 0; f < 8; f++)
                    Squares[r, f].Piece = null;
        }
    }
}