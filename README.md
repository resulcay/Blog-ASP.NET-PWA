# Blog-ASP.NET-PWA

Blog-ASP.NET-PWA is a progressive web app (PWA) that allows you to create and manage your own blog using ASP.NET Core and Blazor WebAssembly.

## Features

- Create, edit, and delete blog posts
- Add categories and tags to your posts
- Upload images and files to your posts
- Search and filter posts by keywords, categories, and tags
- View post statistics and analytics
- Customize your blog theme and settings

## Installation

### Prerequisites

To run this project, you need to have the following prerequisites:

- .NET 5 SDK
- Visual Studio 2019 or VS Code
- SQL Server Express

### Steps to Install

1. Clone this repository:
git clone <https://github.com/resulcay/Blog-ASP.NET-PWA.git>
cd Blog-ASP.NET-PWA

2. Update the connection string in the `appsettings.json` file:

```json
{
  "ConnectionStrings": {
      "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=BlogDb;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

or ~/DataAccessLayer/Concrete/Context.cs --> ln.15  optionsBuilder.UseSqlServer("server=(localdb)\\CoreDemo;database=CoreBlogDb; integrated security=true;");

Run the database migrations:

In Package Manager Console:
-dotnet ef add-migration exampleMigName
-dotnet ef update-database

Run the project:
dotnet run
Open <http://localhost:5000x> in your browser.

Usage
To use the blog app, you need to register an account and log in. You can then access the admin panel by clicking on the user icon on the top right corner. From there, you can create and manage your blog posts, categories, tags, files, and settings.

License
This project is not licenced yet.

Acknowledgments
This project is based on the following tutorials and resources:

Blazor PWA by Murat Yücedağ
will be updated
...
