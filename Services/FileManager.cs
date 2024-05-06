using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ScPlayerAPI.Models;
using System.IO.Compression;

namespace ScPlayerAPI.Services
{
    public class FileManager
    {

        IWebHostEnvironment env;

        private const string dataPathName = "data";

        public FileManager(IWebHostEnvironment environment)
        {
            env = environment;

            var path = Path.Combine(env.ContentRootPath, dataPathName);

            if (!Path.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        
        public async Task<IActionResult> DownloadFileName(string fileName)
        {
            try
            {
                var dataPath = Path.Combine(env.ContentRootPath, dataPathName);
                var fullFileName = Path.Combine(dataPath, fileName);
                return new FileContentResult(await System.IO.File.ReadAllBytesAsync(fullFileName), "application/octet-stream") { FileDownloadName = fileName};
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IActionResult> DownloadAllFiles()
        {
            try
            {

                var dataPath = Path.Combine(env.ContentRootPath, dataPathName);
                if (Path.Exists(dataPath))
                {
                    var zipFile = dataPath + ".zip";
                    if (File.Exists(zipFile)) File.Delete(zipFile);
                    ZipFile.CreateFromDirectory(dataPath, zipFile);
                    return new FileContentResult(await System.IO.File.ReadAllBytesAsync(zipFile), "application/octet-stream") { FileDownloadName = Path.GetFileName(zipFile) };
                }
                throw new FileNotFoundException("file not found");
            } catch (Exception ex)
            {
                throw new FileNotFoundException(ex.Message);
            }
            
            
        }


        public IEnumerable<FileWithDescription> GetFileWithDescriptions()
        {
            var dataPath = Path.Combine(env.ContentRootPath, dataPathName);
            


            return Directory.GetFiles(dataPath).Select(s => new FileWithDescription() { 
                FileName = Path.GetFileName(s), 
                Description = Path.GetFileNameWithoutExtension(s) switch { "CryptoConsoleGUI" => "Крипто консоль", "sc_StreamingApi" => "Sc плеер", "SCPanel.v1.0" => "Панель управления", _ => Path.GetFileNameWithoutExtension(s)},
//                Description = () => "", 
                FileSize = new FileInfo(s).Length });

        }

        public IEnumerable<string> GetFileNames()
        {
            var dataPath = Path.Combine(env.ContentRootPath, dataPathName);
            if (!Path.Exists(dataPath))
            {


            }


            IEnumerable<string> files = Directory.GetFiles(dataPath).Select(s => Path.GetFileName(s));

            return files;
        } 
    }
}
