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

        public void SetupChess960()
        {
            InitializeEmptyBoard(); // Leeres Board erstellen

            var rand = new Random();

            // 1. Läufer auf unterschiedlichen Farben platzieren
            int lightSquare = rand.Next(0, 4) * 2; // 0,2,4,6
            int darkSquare = rand.Next(0, 4) * 2 + 1; // 1,3,5,7

            Piece[] backRank = new Piece[8];
            backRank[lightSquare] = new Piece(PieceType.Bishop, PieceColor.White);
            backRank[darkSquare] = new Piece(PieceType.Bishop, PieceColor.White);

            // 2. König zwischen Türmen platzieren
            // Erst Plätze für verbleibende Figuren finden
            List<int> emptySquares = Enumerable.Range(0, 8).Where(i => backRank[i] == null).ToList();

            // Setze Könige und Türme
            int kingIndex = emptySquares[rand.Next(1, emptySquares.Count - 1)]; // nicht erste oder letzte leer
            backRank[kingIndex] = new Piece(PieceType.King, PieceColor.White);

            emptySquares = emptySquares.Where(i => i != kingIndex).ToList();
            backRank[emptySquares[0]] = new Piece(PieceType.Rook, PieceColor.White);
            backRank[emptySquares[1]] = new Piece(PieceType.Rook, PieceColor.White);

            // 3. Restliche Figuren (Queen, Knight) zufällig platzieren
            emptySquares = emptySquares.Where(i => backRank[i] == null).ToList();
            List<PieceType> remaining = new List<PieceType> { PieceType.Queen, PieceType.Knight, PieceType.Knight };
            remaining = remaining.OrderBy(x => rand.Next()).ToList();

            for (int i = 0; i < remaining.Count; i++)
                backRank[emptySquares[i]] = new Piece(remaining[i], PieceColor.White);

            // 4. Setze erste Reihe (weiß) und achte, dass schwarze Figuren spiegelbildlich sind
            for (int file = 0; file < 8; file++)
            {
                Squares[0, file].Piece = backRank[file]; // Weiß
                Squares[7, file].Piece = new Piece(backRank[file].Type, PieceColor.Black); // Schwarz
            }

            // 5. Fülle zweite und siebte Reihe mit Bauern
            for (int file = 0; file < 8; file++)
            {
                Squares[1, file].Piece = new Piece(PieceType.Pawn, PieceColor.White);
                Squares[6, file].Piece = new Piece(PieceType.Pawn, PieceColor.Black);
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