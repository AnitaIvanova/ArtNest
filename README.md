# ARTNEST

ARTNEST is a web application that allows users to explore, collect, remember, and organize meaningful artworks in a personal digital space. The project is developed as part of the course **IN-SDE-EN-CMK**.

## Project Idea

The goal of ARTNEST is to create a personal museum memory and wishlist platform where users can:
- explore artworks
- save artworks to a wishlist
- keep personal memories or reflections related to artworks
- build a more personal connection with art

## Current Features

At the current stage, the project includes:
- homepage with artwork display
- explore page
- wishlist page
- journal page
- artwork data model and data handling
- initial SQL Server connection work
- user authentication and authorization work in progress

## Tech Stack

- **Frontend:** HTML, CSS, Razor Pages
- **Backend:** C#
- **Framework:** ASP.NET Core Razor Pages
- **Database:** SQL Server
- **Tools:** Git, GitHub, Visual Studio / VS Code

## Project Structure

Main folders in the project:
- `Pages/` – Razor Pages and page models
- `Models/` – data models such as Artwork and User
- `Data/` – artwork-related data logic
- `wwwroot/` – static files such as CSS, JavaScript, and images

## How to Run the Project

1. Clone the repository
2. Open the project folder
3. Restore dependencies with `dotnet restore`
4. Make sure the connection string in `appsettings.json` is correct
5. Run the project with `dotnet run`

## Important Note

The SQL Server connection may require access to the Fontys network or VPN, depending on the database server configuration.

## Status

This project is currently under development.  
The structure, pages, and main functionality are being implemented iteratively.

## Author

Anita Ivanova
