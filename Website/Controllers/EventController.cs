using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Website.Models;
using Website.Persistence;
using Website.Repositories;

namespace Website.Controllers
{
    public class EventController : Controller
    {
        private readonly DbContext _dbContext;
        private readonly IValidator<Event> _validator;
        private readonly ILogger<EventController> _logger;

        public EventController(DbContext dbContext, IValidator<Event> validator, ILogger<EventController> logger)
        {
            _dbContext = dbContext;
            _validator = validator;
            _logger = logger;
        }

        // GET: Event
        public async Task<ActionResult> Index([FromQuery] bool showHistoricEvents = false, CancellationToken cancellationToken = default)
        {
            var events = await _dbContext.Events.GetAllAsync(showHistoricEvents, cancellationToken);
            return View(new EventListViewModel { IsShowingHistoricEvents = showHistoricEvents, Events = events });
        }

        // GET: Event/Details/5
        public async Task<ActionResult> Details(int id, CancellationToken cancellationToken)
        {
            var exists = await _dbContext.Events.ExistsAsync(id, cancellationToken);
            if (!exists)
                return NotFound();

            var @event = await _dbContext.Events.GetByIdAsync(id, cancellationToken);

            // Get the event associated employees
            var associatedEmployees = await _dbContext.EventEmployee.GetEmployeesForEventAsync(id, cancellationToken);

            if (associatedEmployees is not null) 
            {
                @event!.AssociatedEmployees = associatedEmployees.ToList();
            }

            return View(@event);
        }

        // GET: Event/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Event/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Event @event, List<int> selectedEmployees, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(@event, cancellationToken);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
                return View(@event);
            }

            var createdEvent = await _dbContext.Events.CreateAsync(@event, cancellationToken);

            // Add the selected employees to the event
            foreach (var employeeId in selectedEmployees)
            {
                await _dbContext.EventEmployee.AddEmployeeToEventAsync(createdEvent.Id, employeeId, cancellationToken);
            }

            return RedirectToAction(nameof(Details), new { id = createdEvent.Id });
        }

        // GET: Event/Edit/5
        public async Task<ActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var exists = await _dbContext.Events.ExistsAsync(id, cancellationToken);
            if (!exists)
                return NotFound();

            var @event = await _dbContext.Events.GetByIdAsync(id, cancellationToken);
            // Get the event associated employees
            var associatedEmployees = await _dbContext.EventEmployee.GetEmployeesForEventAsync(id, cancellationToken);

            if (associatedEmployees is not null)
            {
                @event!.AssociatedEmployees = associatedEmployees.ToList();
            }
            var allEmployees = await _dbContext.Employees.GetAllAsync(cancellationToken);

            var currentAttendeeCount = @event!.AssociatedEmployees.Count;
            var remainingCapacity = @event.MaximumCapacity.HasValue
                ? Math.Max(0, @event.MaximumCapacity.Value - currentAttendeeCount)
                : int.MaxValue;

            ViewBag.AllEmployees = allEmployees.ToList();
            ViewBag.RemainingCapacity = remainingCapacity;

            return View(@event);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Event @event, List<int> selectedEmployeeIds, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(@event, cancellationToken);

            if (@event.MaximumCapacity.HasValue && selectedEmployeeIds.Count > @event.MaximumCapacity.Value)
            {
                ModelState.AddModelError("", $@"Selected Employees exceed the Event maximum capacity. Please unselect the extra selections and try again.");
            }

            // Need to re-hydrate the view if validation failed
            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
                var allEmployees = await _dbContext.Employees.GetAllAsync(cancellationToken);
                var currentAttendeeCount = selectedEmployeeIds.Count;
                var remainingCapacity = @event.MaximumCapacity.HasValue
                    ? Math.Max(0, @event.MaximumCapacity.Value - currentAttendeeCount)
                    : int.MaxValue;
                ViewBag.AllEmployees = allEmployees.ToList();
                ViewBag.RemainingCapacity = remainingCapacity;
                ViewBag.SelectedEmployeeIds = selectedEmployeeIds;
                return View(@event);
            }

            var updatedEvent = await _dbContext.Events.UpdateAsync(@event, cancellationToken);

            var currentEmployees = await _dbContext.EventEmployee.GetEmployeesForEventAsync(id, cancellationToken);
            var currentEmployeeIds = currentEmployees.Select(e => e.Id).ToList();

            var employeesToAdd = selectedEmployeeIds.Except(currentEmployeeIds);
            var employeesToRemove = currentEmployeeIds.Except(selectedEmployeeIds);

            foreach (var employeeId in employeesToAdd)
            {
                await _dbContext.EventEmployee.AddEmployeeToEventAsync(id, employeeId, cancellationToken);
            }

            foreach (var employeeId in employeesToRemove)
            {
                await _dbContext.EventEmployee.RemoveEmployeeFromEventAsync(id, employeeId, cancellationToken);
            }

            return RedirectToAction(nameof(Details), new { id = updatedEvent.Id });
        }

        // GET: Event/Delete/5
        public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var exists = await _dbContext.Events.ExistsAsync(id, cancellationToken);
            if (!exists)
                return NotFound();

            await _dbContext.Events.DeleteAsync(id, cancellationToken);
            return RedirectToAction(nameof(Index));
        }
    }
}
