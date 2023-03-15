using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using bookStore.Models;
using bookStore.Entities;

namespace bookStore.Controllers;

public class HomeController : Controller
{
      private readonly ILogger<HomeController> _logger;
 private Microsoft.AspNetCore.Hosting.IHostingEnvironment Environment;
    public HomeController(ILogger<HomeController> logger, Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment)
    {   
        _logger = logger;
        Environment = _environment;
        
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult AddDetails(int Id,BookModel bookModel){
        return View(bookModel);
    }

public IActionResult Book_info(){

      using (var context = new BookDBContext())
        {
            var BookList = context.BookTables.ToList();
            return View(BookList);
        }
}
public IActionResult book_detail(int Id){
        using (var context = new BookDBContext())
        {
            var candidateList = context.BookTables.FirstOrDefault(x=>x.Id==Id);
           
            return View(candidateList);
        }
}
    // [HttpPost]
    public IActionResult AddBook(BookModel bookmodel,IFormFile Image){

 string wwwPath = this.Environment.WebRootPath;
        string contentPath = this.Environment.ContentRootPath;
 
        string path = Path.Combine(this.Environment.WebRootPath, "Uploads");
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
 
        List<string> uploadedFiles = new List<string>();
       
            string fileName = Path.GetFileName(Image.FileName);
                  using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
            {
                Image.CopyTo(stream);
                uploadedFiles.Add(fileName);
                ViewBag.Message += string.Format("<b>{0}</b> uploaded.<br />", fileName);
            }

            using(var context=new BookDBContext()){
                BookTable bookTable=new BookTable();
                bookTable.BookName=bookmodel.BookName;
                bookTable.Category=bookmodel.Category;
                bookTable.Image=fileName;
                bookTable.Author=bookmodel.Author;
                bookTable.Publisher=bookmodel.Publisher;
                bookTable.Description=bookmodel.Description;
               if (bookmodel.Id > 0)
            {
                bookTable.Id = bookmodel.Id;
                context.Update(bookTable);
            }
            else
            {
                context.Add(bookTable);
            }

            context.SaveChanges();

            }
            return RedirectToAction(actionName: "Book_info", controllerName: "Home");
    }

    public IActionResult DeleteBook(int Id)
    {
        using (var context = new BookDBContext())
        {
            var BookRecord = context.BookTables.FirstOrDefault(x => x.Id == Id);
            if (BookRecord != null)
            {
                context.BookTables.Remove(BookRecord);
                context.SaveChanges();

            }
            return RedirectToAction(actionName: "Book_info", controllerName: "Home");
        }
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
