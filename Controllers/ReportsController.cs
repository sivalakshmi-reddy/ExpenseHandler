using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using ExpenseHandler.Data;
using ExpenseHandler.Entities;
using ExpenseHandler.Enums;
using ExpenseHandler.Models;
using ExpenseHandler.Features.ExpenseReports.Commands; // for Approve/Reject commands if you keep them

namespace ExpenseHandler.Controllers
{
    public class ReportsController : Controller
    {
        private readonly AppDbContext _context;

        public ReportsController(AppDbContext context)
        {
            _context = context;
        }

        // LIST
        public IActionResult Index()
        {
            // load related data so Category/Employee are 
            var reports = _context.ExpenseReports
                .Include(r => r.Employee)
                .Include(r => r.Items)
                    .ThenInclude(i => i.Category)
                .ToList();

            var model = reports.Select(r => new ReportViewModel
            {
                Id = r.Id,
                EmployeeName = r.Employee?.Name ?? "-",
                Status = r.Status.ToString(),
                CreatedAt = r.CreatedAt,
                SubmittedAt = r.SubmittedAt,
                ManagerComment = r.ManagerComment,
                Items = r.Items?.Select(i => new ReportItemViewModel
                {
                    Id = i.Id,
                    CategoryId = i.CategoryId,
                    CategoryName = i.Category?.Name ?? "-",
                    SpendDate = i.SpendDate,
                    Amount = i.Amount,
                    Description = i.Description
                }).ToList() ?? new List<ReportItemViewModel>()
            }).ToList();

            ViewBag.Employees = _context.Employees.ToList();
            return View(model);
        }

        // DETAILS
        public IActionResult Details(Guid id)
        {
            var r = _context.ExpenseReports
                .Include(x => x.Employee)
                .Include(x => x.Items)
                    .ThenInclude(i => i.Category)
                .FirstOrDefault(x => x.Id == id);

            if (r == null) return NotFound();

            var model = new ReportViewModel
            {
                Id = r.Id,
                EmployeeName = r.Employee?.Name ?? "-",
                Status = r.Status.ToString(),
                CreatedAt = r.CreatedAt,
                SubmittedAt = r.SubmittedAt,
                ManagerComment = r.ManagerComment,
                Items = r.Items.Select(i => new ReportItemViewModel
                {
                    Id = i.Id,
                    CategoryId = i.CategoryId,
                    CategoryName = i.Category?.Name ?? "-",
                    SpendDate = i.SpendDate,
                    Amount = i.Amount,
                    Description = i.Description
                }).ToList()
            };

            return View(model);
        }

        // CREATE (GET)
        public IActionResult Create()
        {
            ViewBag.Employees = _context.Employees.ToList();
            return View();
        }

        // CREATE (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Guid employeeId)
        {
            if (employeeId == Guid.Empty)
            {
                ModelState.AddModelError("", "Employee is required.");
                ViewBag.Employees = _context.Employees.ToList();
                return View();
            }

            var report = new ExpenseReport
            {
                Id = Guid.NewGuid(),
                EmployeeId = employeeId,
                Status = ReportStatus.Draft,
                CreatedAt = DateTime.UtcNow
            };

            _context.ExpenseReports.Add(report);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // SUBMIT REPORT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Submit(Guid id)
        {
            var report = _context.ExpenseReports.FirstOrDefault(r => r.Id == id);
            if (report == null)
            {
                TempData["Error"] = "Report not found.";
                return RedirectToAction("Index");
            }

            report.Status = ReportStatus.Submitted;
            report.SubmittedAt = DateTime.UtcNow;
            _context.SaveChanges();

            return RedirectToAction("Details", new { id });
        }

        // ADD ITEM (GET)
        public IActionResult AddItem(Guid reportId)
        {
            ViewBag.ReportId = reportId;
            ViewBag.Categories = _context.Categories.ToList();
            return View(new ReportItemViewModel { SpendDate = DateTime.Today });
        }

        // ADD ITEM (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddItem(Guid reportId, ReportItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                var item = new ExpenseItem
                {
                    Id = Guid.NewGuid(),
                    ReportId = reportId,
                    CategoryId = model.CategoryId.Value,   // ✅ safe now
                    SpendDate = model.SpendDate,
                    Amount = model.Amount,
                    Description = model.Description
                };

                _context.ExpenseItems.Add(item);
                _context.SaveChanges();

                return RedirectToAction("Details", new { id = reportId }); // ✅ redirect works
            }

            // Refill dropdown if validation fails
            ViewBag.ReportId = reportId;
            ViewBag.Categories = _context.Categories.ToList();
            return View(model);
        }

        // APPROVE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Approve(Guid id)
        {
            var command = new ApproveReportCommand(_context);
            command.Execute(id);
            return RedirectToAction("Details", new { id });
        }

        // REJECT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Reject(Guid id, string comment)
        {
            var command = new RejectReportCommand(_context);
            command.Execute(id, comment);
            return RedirectToAction("Details", new { id });
        }
    }
}
