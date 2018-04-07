using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApplicationUpload.Data;

namespace WebApplicationUpload.Controllers
{
    [Produces("application/json")]
    [Route("/api/upload")]
    public class UploadController : Controller
    {
        private ILogger<UploadController> _loggerFactory;
        private readonly WebApplicationUploadDbContext _context;

        public UploadController(ILogger<UploadController> loggerFactory, WebApplicationUploadDbContext context)
        {
            _loggerFactory = loggerFactory;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var getImages = await _context.Images.ToListAsync();
            return Ok(getImages);
        }
        [HttpPost, DisableRequestSizeLimit]
        public async Task Upload(IFormFile file)
        {
            if (file == null) throw new Exception("File is null");
            if (file.Length == 0) throw new Exception("File is empty");

            using (Stream stream = file.OpenReadStream())
            {
                using (var binaryReader = new BinaryReader(stream))
                {
                    var fileContent = binaryReader.ReadBytes((int)file.Length);
                    await _context.Images.AddAsync(new Image
                    {
                        ContentType = file.ContentType,
                        FileContent = fileContent,
                        FileName = file.FileName
                    });
                    await _context.SaveChangesAsync();
                    _loggerFactory.LogInformation($"{ fileContent} { file.FileName} { file.ContentType}");

                }
            }
        }

    }

}