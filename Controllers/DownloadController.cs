using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScPlayerAPI.Models;
using ScPlayerAPI.Services;
using System;

namespace ScPlayerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DownloadController : ControllerBase
    {
        private readonly ILogger<MailController> _logger;
        private readonly IWebHostEnvironment _environment;
        private readonly FileManager _fileManager;

        public DownloadController(ILogger<MailController> logger, FileManager fileManager,  IWebHostEnvironment environment)
        {
            _logger = logger;
            _environment = environment;
            _fileManager = fileManager;
        }

        [HttpGet(nameof(AllFiles))]
        public ActionResult<IEnumerable<string>> AllFiles()
        {
            try
            {
                return Ok(_fileManager.GetFileNames());
            } catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet(nameof(AllFileWithDescription))]
        public ActionResult<IEnumerable<FileWithDescription>> AllFileWithDescription()
        {
            try
            {
                return Ok(_fileManager.GetFileWithDescriptions());
            } catch(Exception ex) { 
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet(nameof(DownloadZip))]
        public async Task<IActionResult> DownloadZip()
        {
            try
            {
                return await _fileManager.DownloadAllFiles();
            } catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet(nameof(Download))] 
        public async Task<IActionResult> Download(string fileName)
        {
            try
            {
                return await _fileManager.DownloadFileName(fileName);
            } catch(Exception ex) {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
