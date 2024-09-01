using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Website.Models;
using Website.Persistence;

namespace Website.Controllers
{
    public class DrinkController : Controller
    {
        private readonly DbContext _dbContext;
        private readonly IValidator<Drink> _validator;

        public DrinkController(DbContext dbContext, IValidator<Drink> validator)
        {
            _dbContext = dbContext;
            _validator = validator;
        }

        // GET: Drink
        public async Task<ActionResult> Index(CancellationToken cancellationToken)
        {
            var drinks = await _dbContext.Drinks.GetAllAsync(cancellationToken);
            return View(new DrinkListViewModel { Drinks = drinks });
        }

        // GET: Drink/Details/5
        public async Task<ActionResult> Details(int id, CancellationToken cancellationToken)
        {
            var drink = await _dbContext.Drinks.GetByIdAsync(id, cancellationToken);
            if (drink == null)
                return NotFound();
            return View(drink);
        }

        // GET: Drink/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Drink/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Drink drink, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(drink, cancellationToken);
            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
                return View(drink);
            }

            var createdDrink = await _dbContext.Drinks.CreateAsync(drink, cancellationToken);
            return RedirectToAction(nameof(Details), new { id = createdDrink.Id });
        }

        // GET: Drink/Edit/5
        public async Task<ActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var drink = await _dbContext.Drinks.GetByIdAsync(id, cancellationToken);
            if (drink == null)
                return NotFound();
            return View(drink);
        }

        // POST: Drink/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Drink drink, CancellationToken cancellationToken)
        {
            if (id != drink.Id)
                return NotFound();

            var validationResult = await _validator.ValidateAsync(drink, cancellationToken);
            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
                return View(drink);
            }

            var updatedDrink = await _dbContext.Drinks.UpdateAsync(drink, cancellationToken);
            return RedirectToAction(nameof(Details), new { id = updatedDrink.Id });
        }

        // GET: Drink/Delete/5
        public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            await _dbContext.Drinks.DeleteAsync(id, cancellationToken);
            return RedirectToAction(nameof(Index));
        }
    }
}
