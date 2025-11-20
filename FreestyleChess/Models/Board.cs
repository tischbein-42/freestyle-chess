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

            Piece[] backRank = new Piece[8];
            var emptySquares = new List<int> {0,1,2,3,4,5,6,7};


            // 1. König und Türme setzen
            int kingSquare = rand.Next(1, 7);
            int leftRook = rand.Next(0, kingSquare);
            int rightRook = rand.Next(kingSquare + 1, 8);

            backRank[kingSquare] = new Piece(PieceType.King, PieceColor.White);
            backRank[leftRook] = new Piece(PieceType.Rook, PieceColor.White);
            backRank[rightRook] = new Piece(PieceType.Rook, PieceColor.White);

            emptySquares.Remove(kingSquare);
            emptySquares.Remove(leftRook);
            emptySquares.Remove(rightRook);


            // 2. Läufer auf unterschiedlichen Farben platzieren

            var DarkSquares = emptySquares.Where(x => x % 2 == 0).ToList();
            var LightSquares = emptySquares.Where(x => x % 2 == 1).ToList();

            int darkSquare = DarkSquares[rand.Next(DarkSquares.Count)];
            int lightSquare = LightSquares[rand.Next(LightSquares.Count)];

            backRank[darkSquare] = new Piece(PieceType.Bishop, PieceColor.White);
            backRank[lightSquare] = new Piece(PieceType.Bishop, PieceColor.White);

            emptySquares.Remove(darkSquare);
            emptySquares.Remove(lightSquare);

            // 3. Restliche Figuren (Queen, Knight) zufällig platzieren

            List<PieceType> remaining = new List<PieceType> { PieceType.Queen, PieceType.Knight, PieceType.Knight };
            remaining = remaining.OrderBy(x => rand.Next()).ToList();
            for (int i = 0; i < remaining.Count; i++)
                backRank[emptySquares[i]] = new Piece(remaining[i], PieceColor.White);

            // 4. Setze erste Reihe (weiß) und achte, dass schwarze Figuren spiegelbildlich sind
            for (int file = 0; file < 8; file++)
            {
                Squares[7, file].Piece = backRank[file]; // Weiß
                Squares[0, file].Piece = new Piece(backRank[file].Type, PieceColor.Black); // Schwarz
            }

            // 5. Fülle zweite und siebte Reihe mit Bauern
            for (int file = 0; file < 8; file++)
            {
                Squares[6, file].Piece = new Piece(PieceType.Pawn, PieceColor.White);
                Squares[1, file].Piece = new Piece(PieceType.Pawn, PieceColor.Black);
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