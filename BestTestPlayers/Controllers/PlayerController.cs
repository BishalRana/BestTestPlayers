using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BestTestPlayers.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;



namespace BestTestPlayers.Controllers
{
    public class PlayerController : Controller
    {
        private  PlayerContext _context;

        public PlayerController(PlayerContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public IActionResult Index( string name,string country)
        {
            // Use LINQ to get list of player.
            var  averageQuery = from m in _context.Player
                                                  orderby m.Country
                                                  select m.Country;

            var players = from m in _context.Player
                         select m;

            if (!String.IsNullOrEmpty(name))
            {
                players = players.Where(s => s.Name.Contains(name));
            }

            if (!String.IsNullOrEmpty(country))
            {
                players = players.Where(x => x.Country == country);
            }

            PlayerAverageViewModel playerAverageVM = new PlayerAverageViewModel();
            ViewBag.country = new SelectList(averageQuery.Distinct());
            ViewBag.name = "";
            playerAverageVM.players = players.ToList();

            return View(playerAverageVM);       
        }

        // GET: Player/Create
        public IActionResult Create()
        {
            return View();
        }


        // POST: Player/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Name,DOB,Country,HighestScore,Type,Average,Matches")] Player player)
        {
            if (ModelState.IsValid)
            {
                _context.Add(player);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(player);
        }

        // GET: Player/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Player player = await _context.Player
                                      .SingleOrDefaultAsync(m => m.Id == id);
            if (player == null)
            {
                return NotFound();
            }

            return View(player);
        }

        // GET: Player/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Player player = await _context.Player.SingleOrDefaultAsync(m => m.Id == id);
            if (player == null)
            {
                return NotFound();
            }
            return View(player);
        }

        // POST: Player/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,DOB,Country,HighestScore,Type,Average,Matches")] Player player)
        {
            if (id != player.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(player);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayerExists(player.Id))
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
            return View(player);
        }

        // GET: Player/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Player player = await _context.Player
                                      .SingleOrDefaultAsync(m => m.Id == id);
            if (player == null)
            {
                return NotFound();
            }

            return View(player);
        }

        // POST: Player/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Player.SingleOrDefaultAsync(m => m.Id == id);
            _context.Player.Remove(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlayerExists(int id)
        {
            return _context.Player.Any(e => e.Id == id);
        }

    }

   
}
