using System.Security.Claims;
using Contacts.Contracts;
using Contacts.Models.Contact;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace Contacts.Controllers
{
    [Authorize]
    public class ContactsController : Controller
    {
        private readonly IContactService _contactService;

        public ContactsController(IContactService service)
        {
            _contactService = service;
        }

        public async Task<IActionResult> All()
        {
            var models = await _contactService.GetAllContactsAsync();

            return View(models);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            ContactViewModel model = new ContactViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ContactViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _contactService.AddNewContactAsync(model);

            return RedirectToAction("All");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var contact = await _contactService.FindContactByIdAsync(id);

            return View(contact);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ContactViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _contactService.EditContactAsync(model, id);

            return RedirectToAction("All");
        }

        public async Task<IActionResult> Team()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var contacts = await _contactService.GetAllMineContactsAsync(userId);

            return View(contacts);
        }

        public async Task<IActionResult> AddToTeam(int contactId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _contactService.AddContactToUserCollectionAsync(userId, contactId);

            return RedirectToAction("All");
        }

        public async Task<IActionResult> RemoveFromTeam(int contactId)
        {
            await _contactService.DeleteContactAsync(contactId);

            return RedirectToAction("Team");
        }
    }
}
