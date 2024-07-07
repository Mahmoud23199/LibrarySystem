Library System Application
Overview
This is a .NET 8-based library management system designed to handle book inventory, borrowing, and user management. Below are the steps to set up and run the application on your local machine.

Prerequisites
.NET 8 SDK
Visual Studio Code or any other code editor
SQL Server or any compatible database system
Setup Instructions
1. Clone the Repository
bash
Copy code
git clone https://github.com/yourusername/systemLibrary.git

2. Update Connection String
Open appsettings.json and update the connection string to match your SQL Server configuration:

json
Copy code
{
  "ConnectionStrings": {
    "LibraryContext": "Server=localhost;Database=LibraryDb;Trusted_Connection=True;"
  }
}
3. Update Database
Run the following commands in the terminal to update the database schema:

bash
Copy code
dotnet ef database update
4. Run the Application
Use the following command to start the application:

bash
Copy code
dotnet run
The application should now be running on https://localhost:7251.

Features
Book Management: Add, edit, and delete books.
User Management: Add users and manage borrowed books.
Borrowing System: Users can borrow books, and the system tracks the borrowing status.
Additional Information
Project Structure:

Controllers: Handle HTTP requests and responses.
Models: Define the database schema.
Services: Business logic and data access layer.
Views: Razor views for the UI.
Database Seeding:

The database is seeded with sample data. To add more data, modify the OnModelCreating method in LibraryContext.cs.
Troubleshooting
Connection Issues: Ensure the database server is running and the connection string is correct.
Build Errors: Ensure all dependencies are restored by running dotnet restore.
