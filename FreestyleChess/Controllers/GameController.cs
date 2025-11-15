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

    }
}