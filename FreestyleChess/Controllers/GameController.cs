using Microsoft.AspNetCore.Mvc;
using FreestyleChess.Models;
using System.Text.Json;

namespace FreestyleChess.Controllers
{
    public class GameController : Controller
    {

        // GET: /Game/
        public IActionResult BoardView()
        {
            var board = new Board();
            return View("Board", board);
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
            Board board = Board.FromSerializable(serializableBoard);

            // Return or process the board as needed
            return View("Gameplay", board);
        }


        [HttpPost]
        public IActionResult Move([FromBody] MoveRequest request)
        {
            string? boardJson = HttpContext.Session.GetString("CurrentPosition");
            if (string.IsNullOrEmpty(boardJson))
            {
                return BadRequest("No board in session.");
            }

            var serializableBoard = JsonSerializer.Deserialize<SerializableBoard>(boardJson);

            Board board = Board.FromSerializable(serializableBoard!);

            bool moveSucess = board.MovePiece(request.FromRank, request.FromFile, request.ToRank, request.ToFile);

            if (!moveSucess)
            {
                return BadRequest("Invalid move.");
            }

            SerializableBoard updatedBoard = board.ToSerializable();
            string updatedBoardJson = JsonSerializer.Serialize(updatedBoard);
            HttpContext.Session.SetString("CurrentPosition", updatedBoardJson);

            return Json(updatedBoard); 
        }


    }
}