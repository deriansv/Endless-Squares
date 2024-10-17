using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using SquareApi.Models;

namespace SquareApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SquaresController : ControllerBase
    {
        private readonly string _filePath = "squares.json";

        // GET: api/squares
        [HttpGet]
        public ActionResult<List<Square>> GetSquares()
        {
            if (!System.IO.File.Exists(_filePath))
            {
                return new List<Square>();
            }

            var jsonData = System.IO.File.ReadAllText(_filePath);
            var squares = JsonSerializer.Deserialize<List<Square>>(jsonData);

             if (squares == null)
            {
            return new List<Square>();  // Returnera en tom lista om den är null
            }
            return squares;
        }

        // POST: api/squares
        [HttpPost]
        public ActionResult<Square> PostSquare(Square square)
        {
            var squares = GetSquares().Value ?? new List<Square>();

            square.Id = squares.Count > 0 ? squares[^1].Id + 1 : 1; // Sätt ett unikt ID
            squares.Add(square);

            System.IO.File.WriteAllText(_filePath, JsonSerializer.Serialize(squares));
            return CreatedAtAction(nameof(GetSquares), new { id = square.Id }, square);
        }
    }
}
