using Microsoft.AspNetCore.Mvc;
using FreestyleChess.Models;
using System.Text.Json;

namespace FreestyleChess.Controllers
{
    public class GameController : Controller
    {

        // GET: /Game/
        public IActionResult Board()
        {
            var board = new Board();
            return View(board);
        }

        [HttpPost]
        public IActionResult RandomPosition()
        {
            var board = new Board();
            board.SetupChess960();  // Methode, die Chess960 Startposition erzeugt

                // Board in Session speichern
            var serializableBoard = board.ToSerializable();
            string boardJson = JsonSerializer.Serialize(serializableBoard);
            HttpContext.Session.SetString("CurrentPosition", boardJson);

            return View("Board", board); // Board View mit neuem Board

        }

        [HttpPost]
        public IActionResult PlayPosition()
        {
            // Board aus Session abrufen
            string? boardJson = HttpContext.Session.GetString("CurrentPosition");

            if (string.IsNullOrEmpty(boardJson))
            {
                // Kein Board gespeichert → zurück zur Generierung
                return RedirectToAction("RandomPosition");
            }

            var serializableBoard = JsonSerializer.Deserialize<SerializableBoard>(boardJson);

            if (serializableBoard == null)
            {
                // Deserialization failed → handle the error
                return RedirectToAction("RandomPosition");
            }

            // In echtes Board zurückverwandeln
            var board = new Board();
            foreach (var sq in serializableBoard.Squares)
            {
                if (sq.Piece != null)
                {
                    board.Squares[sq.Rank, sq.File].Piece = new Piece(sq.Piece.Type, sq.Piece.Color);
                }
            }

            // Return or process the board as needed
            return View("Gameplay", board);
        }

    }
}