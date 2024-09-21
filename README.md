# Welcome to the repo! ✌️

Hi! Welcome to the Convenience Store repo.

## Tech stack in the project

 - C#
 - ASP.NET 8 (minimal API)
 - .NET 8
 - Entity Framework
 - FluentValidation
 - Automapper
 - Docker

## Database

Since I am using EF to create the models, I decided to go with MSSQL 2022.
The schema of the database can be created on a SQL Server with the update-database command. Just make sure to change the connection string to the correct one.

## Pattern used 👌

I could just put everything inside the API endpoints and called it a day. However, I decided to use the repository pattern, just to make the code a little bit cleaner.

If the project were to grow to 2 ore more tables but still being a RESTful API, I would change it to Generic Repository pattern with Unit Of Work pattern.

## How to run the project 🏃‍♂️

Once the project is open and load, it can be debug with the docker-compose.yml file, since the orchestration is enabled in this project visual studio will run the file and preload the image for the project and the SQL Server 2022. If you do not want this or do not have docker installed in your machine, you can always select IISExpress, please be sure to select the ASP.NET API project and make it the start project.

If you want to run the project without open it with Visual Studio or VS Code, and you do have docker. You can run the compose file with the command: 

    docker compose up -d

> Make sure to be inside the solution folder were the compose file is!

If you have decided to run the containers the swagger page should be available at:

    https://localhost:8081/swagger/index.html

### How docker works

With the docker compose file, everything is preconfigured... it even creates a network inside docker, that allows the web api and SQL to communicate between DNS names.

## Secrets

I decided not to use secrets, and instead I have embedded the database connection string inside the Json configuration file.  This was only made in order to make it easy to change the connection string if necessary.

> This type of information should never be upload in any source control! 🚫
