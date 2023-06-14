using Contacts.Contracts;
using Contacts.Data;
using Contacts.Data.Models;
using Contacts.Models.Contact;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Contacts.Services
{
    [Authorize]
    public class ContactService : IContactService
    {
        private readonly ContactsDbContext _data;

        public ContactService(ContactsDbContext data)
        {
            _data = data;
        }

        public async Task<IEnumerable<ContactViewModel>> GetAllContactsAsync()
        {
            var contactEntities = await _data.Contacts.ToArrayAsync();

            var contactModels = contactEntities.Select(c => new ContactViewModel()
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber,
                Address = c.Address,
                Website = c.Website
            }).ToArray();

            return contactModels;
        }

        public async Task AddNewContactAsync(ContactViewModel model)
        {
            var contact = new Contact()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
                Website = model.Website
            };

            await _data.AddAsync(contact);
            await _data.SaveChangesAsync();
        }

        public async Task EditContactAsync(ContactViewModel model, int id)
        {
            var entity = await _data.Contacts.FindAsync(id);

            entity.FirstName = model.FirstName;
            entity.LastName = model.LastName;
            entity.Email = model.Email;
            entity.PhoneNumber = model.PhoneNumber;
            entity.Address = model.Address;
            entity.Website = model.Website;


            await _data.SaveChangesAsync();
        }

        public async Task<ContactViewModel?> FindContactByIdAsync(int id)
        {
            var entity = await _data.Contacts.FindAsync(id);

            if (entity != null)
            {
                ContactViewModel model = new()
                {
                    FirstName = entity.FirstName,
                    LastName = entity.LastName,
                    Email = entity.Email,
                    PhoneNumber = entity.PhoneNumber,
                    Address = entity.Address,
                    Website = entity.Website
                };

                return model;
            }

            return null;
        }

        public async Task<IEnumerable<ContactViewModel>> GetAllMineContactsAsync(string userId)
        {
            var contactEntities = await _data.ApplicationUsersContacts
                .Where(u => u.ApplicationUserId == userId)
                .Select(c => c.Contact)
                .ToArrayAsync();

            var contactModels = contactEntities.Select(c => new ContactViewModel()
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber,
                Address = c.Address,
                Website = c.Website
            }).ToArray();

            return contactModels;
        }

        public async Task AddContactToUserCollectionAsync(string userId, int contactId)
        {
            ApplicationUserContact entity = new()
            {
                ApplicationUserId = userId,
                ContactId = contactId
            };

            if (!_data.ApplicationUsersContacts.Contains(entity))
            {
                _data.ApplicationUsersContacts.Add(entity);
                await _data.SaveChangesAsync();
            }
        }

        public async Task DeleteContactAsync(int id)
        {
            var entity = await _data.ApplicationUsersContacts
                .FirstOrDefaultAsync(u => u.ContactId == id);

            _data.ApplicationUsersContacts.Remove(entity);
            await _data.SaveChangesAsync();
        }
    }
}
