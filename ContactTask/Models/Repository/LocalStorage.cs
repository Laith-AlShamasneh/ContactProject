using Microsoft.JSInterop;
using System.Text.Json;

namespace ContactTask.Models.Repository
{
    public class LocalStorage
    {
        private readonly IJSRuntime _jsRuntime; // Using JavaSript to interact with local storage Api.

        public LocalStorage(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task AddContact(Contact contact) // Add new contact to the list.
        {
            var contacts = await GetAllContacts();
            contact.Id = contacts.Count > 0 ? contacts.Max(c => c.Id) + 1 : 1;
            contacts.Add(contact);
            await SaveContacts(contacts);
        }

        public async Task<List<Contact>> GetAllContacts() // Get all contact from list that stored in local storage.
        {
            var contactsJson = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "contacts");
            return contactsJson != null ? JsonSerializer.Deserialize<List<Contact>>(contactsJson) : new List<Contact>();
        }

        public async Task<Contact> GetContactById(int id) // Find contact using it's Id.
        {
            var contacts = await GetAllContacts();
            return contacts.FirstOrDefault(c => c.Id == id);
        }

        public async Task UpdateContact(Contact contact) // Update the existing contact.
        {
            var contacts = await GetAllContacts();
            var existingContact = contacts.FirstOrDefault(c => c.Id == contact.Id);
            if (existingContact != null)
            {
                existingContact.FirstName = contact.FirstName;
                existingContact.LastName = contact.LastName;
                existingContact.Email = contact.Email;
                existingContact.PhoneNumber = contact.PhoneNumber;
                await SaveContacts(contacts);
            }
        }

        public async Task DeleteContact(int id) // Delete contact.
        {
            var contacts = await GetAllContacts();
            contacts.RemoveAll(c => c.Id == id);
            await SaveContacts(contacts);
        }

        private async Task SaveContacts(List<Contact> contacts) // Save contact to local storagr after create, update and delete.
        {
            var contactsJson = JsonSerializer.Serialize(contacts);
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "contacts", contactsJson);
        }
    }
}
