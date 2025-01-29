using BudgetManager.Domain.Abstractions;
using BudgetManager.Domain.Models.ValueObjects;

namespace BudgetManager.Domain.Models;

public class User : Aggregate<UserId>
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }

    private User(UserId id, string firstName, string lastName, string email, string password)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
        CreatedOn = DateTime.UtcNow;
        CreatedBy = "System";
    }

    public static User Create(UserId id, string firstName, string lastName, string email, string password)
    {
        if (string.IsNullOrWhiteSpace(firstName)) throw new ArgumentException("First name is required.");
        if (string.IsNullOrWhiteSpace(lastName)) throw new ArgumentException("Last name is required.");
        if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email is required.");
        if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Password is required.");

        return new User(id, firstName, lastName, email, password);
    }

    public void Update(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName)) throw new ArgumentException("First name is required.");
        if (string.IsNullOrWhiteSpace(lastName)) throw new ArgumentException("Last name is required.");

        FirstName = firstName;
        LastName = lastName;
        UpdatedOn = DateTime.UtcNow;
        UpdatedBy = "System";
    }
}