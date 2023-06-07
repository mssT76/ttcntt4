using System;
using System.Security.Cryptography;
using WebVPP.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebVPP.Models;

namespace WebVPP.Controllers
{
        public class AccountController : BaseController
        {
            private readonly BtVppContext _context;

            public AccountController(BtVppContext context)
            {
                _context = context;
            }

            // GET: Users
            public async Task<IActionResult> Index()
            {
                return _context.Accounts != null ?
                            View(await _context.Accounts.ToListAsync()) :
                            Problem("Entity set 'BtVppContext.Accounts'  is null.");
            }

            // GET: Users/Details/5
            public async Task<IActionResult> Details(int? id)
            {
                if (id == null || _context.Accounts == null)
                {
                    return NotFound();
                }

                var user = await _context.Accounts
                    .FirstOrDefaultAsync(m => m.AccountId == id);
                if (user == null)
                {
                    return NotFound();
                }

                return View(user);
            }

            // GET: Users/Create
            public IActionResult Create()
            {
                return View();
            }

            // POST: Users/Create
            // To protect from overposting attacks, enable the specific properties you want to bind to.
            // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create([Bind("AccountId,FullName,PassWord,RoleId")] Account user)
            {
                if (ModelState.IsValid)
                {

                    SHA256 hashPassWord = SHA256.Create();
                    user.PassWord = Extension.HashMD5.GetHash(hashPassWord, user.PassWord);
                    user.RoleId = 2;
                    _context.Add(user);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(user);
            }

            public IActionResult Login()
            {
                return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]   
            public async Task<IActionResult> Login(Account user)
            {

                var loginUser = await _context.Accounts.FirstOrDefaultAsync(m => m.FullName == user.FullName);
                if (loginUser == null)
                {
                    ModelState.AddModelError(string.Empty, "Danh nhap that bai");
                    return View(user);
                }
                SHA256 hashMethod = SHA256.Create();

                if (Extension.HashMD5.VerifyHash(hashMethod, user.PassWord, loginUser.PassWord))
                {
                    CurrentUser = loginUser;
                    ViewBag.User = loginUser;
                    return RedirectToAction("Index", "Home");
                }

                ViewBag.Pw = Extension.HashMD5.GetHash(hashMethod, user.PassWord);
                ModelState.AddModelError(string.Empty, "Danh nhap that bai");
                return View(user);

            }

            public IActionResult Logout()
            {
                HttpContext.Session.Remove("UserName");
                return RedirectToAction("Privacy", "Home");
            }

            // GET: Users/Edit/5
            public async Task<IActionResult> Edit(int? id)
            {
                if (id == null || _context.Accounts == null)
                {
                    return NotFound();
                }

                var user = await _context.Accounts.FindAsync(id);
                if (user == null)
                {
                    return NotFound();
                }
                return View(user);
            }

            // POST: Users/Edit/5
            // To protect from overposting attacks, enable the specific properties you want to bind to.
            // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, [Bind("AccountId,FullName,PassWord,RoleId")] Account user)
            {
                if (id != user.AccountId)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(user);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!UserExists(user.AccountId))
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
                return View(user);
            }

            // GET: Users/Delete/5
            public async Task<IActionResult> Delete(int? id)
            {
                if (id == null || _context.Accounts == null)
                {
                    return NotFound();
                }

                var user = await _context.Accounts
                    .FirstOrDefaultAsync(m => m.AccountId == id);
                if (user == null)
                {
                    return NotFound();
                }

                return View(user);
            }

            // POST: Users/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                if (_context.Accounts == null)
                {
                    return Problem("Entity set 'BtVppContext.Accounts'  is null.");
                }
                var user = await _context.Accounts.FindAsync(id);
                if (user != null)
                {
                    _context.Accounts.Remove(user);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            private bool UserExists(int id)
            {
                return (_context.Accounts?.Any(e => e.AccountId == id)).GetValueOrDefault();
            }
        }
    }
