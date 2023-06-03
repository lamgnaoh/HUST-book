using Microsoft.AspNetCore.Mvc;


namespace AssignmentApp.API.Controllers;

[ApiController]
[Route("[controller]")]
public class FileController : ControllerBase
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public FileController(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    [HttpPost]
    [Route("upload")]
    public IActionResult UploadFiles(List<IFormFile> files)
    {
        if (files.Count == 0)
        {
            return BadRequest();
        }

        string directoryPath =Path.Combine( _webHostEnvironment.ContentRootPath,"Static/UploadFiles");
        foreach (var file in files)
        {
            string filepath = Path.Combine(directoryPath, file.FileName);
            using (var stream = new FileStream(filepath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            
        }
        return Ok("File uploaded!"); 
    }
    
    [HttpGet]
    [Route("download")]
    public async Task<IActionResult> DownloadFiles(string filename)
    {
        if (filename == null)
        {
            return Content("Filename not present");
        }

        string path =Path.Combine( _webHostEnvironment.ContentRootPath,"Static/UploadFiles",filename);
        var memory = new MemoryStream();
        using (var stream = new FileStream(path, FileMode.Open))
        {
            await stream.CopyToAsync(memory);
        }

        memory.Position = 0;

        return File(memory, GetContentType(path), Path.GetFileName(path));
    }
    private string GetContentType(string path)  
    {  
        var types = GetMimeTypes();  
        var ext = Path.GetExtension(path).ToLowerInvariant();  
        return types[ext];  
    }  
   
    private Dictionary<string, string> GetMimeTypes()  
    {  
        return new Dictionary<string, string>  
        {  
            {".txt", "text/plain"},  
            {".pdf", "application/pdf"},  
            {".doc", "application/vnd.ms-word"},  
            {".docx", "application/vnd.ms-word"},  
            {".xls", "application/vnd.ms-excel"},  
            {".xlsx", "application/vnd.openxmlformats  officedocument.spreadsheetml.sheet"},  
            {".png", "image/png"},  
            {".jpg", "image/jpeg"},  
            {".jpeg", "image/jpeg"},  
            {".gif", "image/gif"},  
            {".csv", "text/csv"}  
        };  
        }
    
}