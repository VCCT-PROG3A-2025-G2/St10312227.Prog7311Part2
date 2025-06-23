Agri-Energy Connect
Agri-Energy Connect is a web-based platform designed to facilitate the management and connection between agricultural employees and farmers, promoting sustainable and energy-efficient farming practices. Built using ASP.NET Core MVC, the application supports employee-farmer collaboration, product management, and goal tracking for greener farming.
Features
Farmer Management
•	Add, edit, and manage farmer profiles.
•	View associated products per farmer.
•	Secure login credentials are issued by employees.

Employee Access
•	Employee registration and login via Identity.
•	Dashboard for viewing and managing all farmers and their products.

Product Management
•	Employees can add/view/search farming products.
•	Products are associated with specific farmer profiles.

Authentication & Authorization
•	Role-based access using ASP.NET Core Identity.
•	Separate views and access paths for Employees and Farmers.

UI Theme
•	Earth-tone colour palette with a green and organic look.
•	Mobile-responsive design using Flexbox and Bootstrap.
•	Subtle animations for enhanced UX.



Project Structure

AgriEnergyConnect/
├── Controllers/
 │ ├── HomeController.cs
 │ ├── AccountController.cs
 │ ├── EmployeeController.cs
 │ └── FarmerController.cs
├── Models/
 │ ├── Farmer.cs
 │ ├── Product.cs
 │ └── ApplicationUser.cs
├── Views/
 │ ├── Home/
 │ ├── Employee/
 │ ├── Farmer/
 │ ├── Shared/
├── Data/
 │ └── ApplicationDbContext.cs
├── wwwroot/
 │ ├── css/
 │ ├── images/
 │ └── js/
├── appsettings.json
├── Program.cs
 └── Startup.cs

Technologies Used

•	ASP.NET Core MVC
o	Backend web framework
•	Entity Framework Core
o	ORM for database access
•	ASP.NET Identity
o	User authentication and roles
•	Bootstrap 5
o	Responsive design
•	Razor Views
o	Templated frontend rendering
•	SQL Server
o	Database
•	C#
o	Server-side language
Setup Instructions
1.	Clone Repository on github:
https://github.com/ST10312227/St10312227.Prog7311Part2.git

2. Configure the Database
•	Update the connection string in appsettings.json:
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=AgriEnergyConnectDb;Trusted_Connection=True;"
}


3. Run Migrations
dotnet ef migrations add InitialCreate
dotnet ef database update


4. Build and Run the Project 
dotnet build
dotnet run

5. Access the App
Open your browser and go to https://localhost:5001

User Roles
Employee
•	Created by system or seeded.
•	Has access to all farmer profiles and product management.
•	Can create one-time credentials for Farmers.
Farmer
•	Logs in with provided credentials.
•	Can view assigned products and manage own profile (future).
________________________________________
Future Improvements
•	Add product filtering and sorting by crop type or location.
•	Integrate charts for product yields and progress (using Chart.js or Recharts).
•	Notification system for updates and weather alerts.
•	REST API endpoints for mobile integration.

 Screenshots
Initial Screen
 

Employee Register Screen
 

Employee Login Screen
 


Employee Dashboard Screen
 
Search All Products Screen
 
Add New Farmer Screen
 
View Product by Farmer Screen
 

Farmer Dashboard Screen
 
Add Product Screen
 

References:
https://www.w3schools.com/html/html_css.asp
https://www.youtube.com/watch?v=6AQRfaQqHk0&pp=0gcJCdgAo7VqN5tD
https://www.twilio.com/docs/usage/tutorials/how-to-set-up-your-csharp-and-asp-net-mvc-development-environment
https://www.codecademy.com/article/mvc









