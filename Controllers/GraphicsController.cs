using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [Route("api/graphics")]
    [ApiController]
    public class GraphicsController : ControllerBase
    {
        private readonly AppDbContext db;

        public GraphicsController(AppDbContext context)
        {
            db = context;
        }

        [HttpPost]
        public IActionResult Add([FromBody] Graphics graphics)
        {
            db.Graphics.Add(graphics);
            db.SaveChanges();

            return Ok(new { message = "Graphic added successfully", data = graphics });
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<Graphics> all_graphics = db.Graphics.ToList();

            return Ok(new { data = all_graphics });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Graphics? graphic = db.Graphics.FirstOrDefault(x => x.Id == id);

            if (graphic != null)
            {
                return Ok(new { data = graphic });
            }
            else
            {
                return NotFound(new { message = "Graphic not found" });
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] Graphics graphics)
        {
            db.Graphics.Update(graphics);
            db.SaveChanges();

            return Ok(new { message = "Graphic updated successfully", data = graphics });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Graphics? graphic = db.Graphics.FirstOrDefault(x => x.Id == id);

            if (graphic != null)
            {
                db.Graphics.Remove(graphic);
                db.SaveChanges();
                return Ok(new { message = "Graphic deleted successfully", data = graphic });
            }
            else
            {
                return NotFound(new { message = "Graphic not found" });
            }
        }

        [HttpGet("postall")]
        public async Task<IActionResult> PostAll()
        {
            string svgDirectory = @"E:\Zini Tecnologies Projects\FreeLogoCreator\backend\svgs";

            if (string.IsNullOrEmpty(svgDirectory) || !Directory.Exists(svgDirectory))
            {
                return BadRequest("Invalid directory path.");
            }

            var svgFiles = Directory.GetFiles(svgDirectory, "*.svg");

            var graphicsList = new List<Graphics>();
            foreach (var file in svgFiles)
            {
                string svgContent = await System.IO.File.ReadAllTextAsync(file);
                string title = Path.GetFileNameWithoutExtension(file);
                string description = $"Description for {title}";

                graphicsList.Add(new Graphics
                {
                    graphic = svgContent,
                    title = title,
                    description = description,
                    created_at = DateTime.UtcNow,
                    updated_at = DateTime.UtcNow
                });
            }

            db.Graphics.AddRange(graphicsList);

            db.SaveChanges();

            return Ok($"{graphicsList.Count} SVG files have been added to the database.");
        }
    }
}
