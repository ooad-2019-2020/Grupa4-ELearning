﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using E_Learning.Models;
using E_Learning.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Controllers
{
    public class NalogController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NalogController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Prijava()
        {
            return View();
        }

        [HttpPost]
        public RedirectToActionResult Prijava(string Email, string Password)
        {
            /*
            Console.WriteLine("Hi!");
            Console.WriteLine(Email);
            Console.WriteLine(Password);
            */

            return RedirectToAction("Index", "Home");
        }


        public IActionResult Registracija()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registracija([Bind("Id,Ime,Prezime,Email,Password")] Korisnik korisnik)
        {
            if (ModelState.IsValid)
            {
                if(await _context.Korisnik.FirstOrDefaultAsync(k => k.Email == korisnik.Email) != null) return View();
                _context.Add(korisnik);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}