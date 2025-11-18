using Microsoft.AspNetCore.Mvc;
using FreestyleChess.Models;

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
            return View("Board", board); // Board View mit neuem Board
        }

    }
}