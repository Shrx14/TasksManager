# TasksManager

## Project Description
TasksManager is an ASP.NET Core MVC web application designed to manage tasks within an organization. It provides functionality to create, update, and track tasks with a SQL Server backend database.

## Prerequisites
- [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- SQL Server instance accessible to the application
- Visual Studio or any IDE supporting .NET development (optional)

## Database Setup
The application uses a SQL Server database named `TasksManagerDb`. To set up the database, run the provided SQL script `database-setup.sql` which will:
- Create the `TasksManagerDb` database
- Create the necessary tables: `TaskMaster` and `TaskTransaction`

You can run the script using SQL Server Management Studio (SSMS) or any SQL client connected to your SQL Server instance.

## Configuration
The database connection string is configured in `appsettings.json` under the `ConnectionStrings` section. Update the connection string as needed to match your SQL Server instance credentials and network settings.

Example connection string:
```
"ConnectionStrings": {
  "TasksManagerDb": "Server=YOUR_SERVER;Database=TasksManagerDb;User Id=YOUR_USER;Password=YOUR_PASSWORD;Encrypt=False;TrustServerCertificate=True;MultipleActiveResultSets=true"
}
```

## Running the Application
To run the application, navigate to the project directory and use the following command:
```
dotnet run
```
This will start the web server. By default, the application uses HTTPS and listens on the configured ports.

Open a web browser and navigate to the default route:
```
https://localhost:5001/
```

## Authentication
The application uses Windows Authentication (Negotiate) for user authentication. Ensure your environment supports this authentication scheme.

## Project Structure
- **Controllers/**: Contains MVC controllers handling HTTP requests.
- **Models/**: Contains data models representing the database entities.
- **Views/**: Contains Razor views for the UI.
- **Data/**: Contains the Entity Framework Core DbContext and database-related classes.
- **Middleware/**: Custom middleware components.
- **wwwroot/**: Static files such as CSS, JavaScript, and images.

## Dependencies
- Microsoft.AspNetCore.Authentication.Negotiate (v8.0.17)
- Microsoft.EntityFrameworkCore (v9.0.7)
- Microsoft.EntityFrameworkCore.SqlServer (v9.0.7)
- System.Data.SqlClient (v4.9.0)

## License
This project is provided as-is without any warranty. Modify and use as needed.
