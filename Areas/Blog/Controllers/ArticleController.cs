using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebBlog.Data;
using WebBlog.Models;

namespace WebBlog.Areas.Blog.Controllers
{
    [Area("Blog")]
    [Route("/blog")]
    public class ArticleController : Controller
    {
        private readonly WebBlogDbContext _context;

        public ArticleController(WebBlogDbContext context)
        {
            _context = context;
        }

        // GET: Article
        [Route("/blog/home")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Articles.ToListAsync());
        }

        // GET: Article/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articleModel = await _context.Articles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (articleModel == null)
            {
                return NotFound();
            }

            return View(articleModel);
        }

        // GET: Article/Create
        [Route("/blog/create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Article/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Email,Content")] ArticleModel articleModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(articleModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(articleModel);
        }

        // GET: Article/Edit/5
        [Route("/blog/edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articleModel = await _context.Articles.FindAsync(id);
            if (articleModel == null)
            {
                return NotFound();
            }
            return View(articleModel);
        }

        // POST: Article/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Email,Content")] ArticleModel articleModel)
        {
            if (id != articleModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(articleModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticleModelExists(articleModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(articleModel);
        }

        // GET: Article/Delete/5
        [Route("/blog/delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articleModel = await _context.Articles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (articleModel == null)
            {
                return NotFound();
            }

            return View(articleModel);
        }

        // POST: Article/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var articleModel = await _context.Articles.FindAsync(id);
            if (articleModel != null)
            {
                _context.Articles.Remove(articleModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticleModelExists(int id)
        {
            return _context.Articles.Any(e => e.Id == id);
        }
    }
}
