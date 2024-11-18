using ExpenseMVCWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ExpenseMVCWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ExpensesDbContext _expensesDbContext;

        public HomeController(ILogger<HomeController> logger, ExpensesDbContext expensesDbContext)
        {
            _logger = logger;
            _expensesDbContext = expensesDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Expenses()
        {
            var allExpenses = _expensesDbContext.Expenses.ToList();
            var totalExpenses = allExpenses.Sum(x => x.Value);
            ViewBag.Expenses = totalExpenses;
            return View(allExpenses);
        }

        public IActionResult CreateEditExpense(int? id)
        {
            if (id != null)
            {
                var exp = _expensesDbContext.Expenses.SingleOrDefault(x => x.Id == id);
                return View(exp);
            }
            return View();
        }

        public IActionResult CreateEditExpenseForm(Expense expenseModel)
        {
            if(expenseModel.Id == 0)
            {
                _expensesDbContext.Expenses.Add(expenseModel);                
            }
            else
            {
                _expensesDbContext.Expenses.Update(expenseModel);
            }
            _expensesDbContext.SaveChanges();

            return RedirectToAction("Expenses");
        }

        public IActionResult DeleteExpense(int id)
        {
            var exp = _expensesDbContext.Expenses.SingleOrDefault(x => x.Id == id);
            _expensesDbContext.Expenses.Remove(exp);
            _expensesDbContext.SaveChanges();
            return RedirectToAction("Expenses");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
