# NotesAPI

NotesAPI is a web API for managing notes, including creating, reading, updating, and deleting notes. This project uses ASP.NET Core with Dapper for data access, JWT Bearer Authentication, and Redis caching.

## Table of Contents

- [Features](#features)
- [Technologies Used](#technologies-used)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
  - [Configuration](#configuration)
  - [Database Migration](#database-migration)
  - [Running the Application](#running-the-application)
- [API Endpoints](#api-endpoints)
  - [Authentication](#authentication)
  - [Notes](#notes)
- [Caching](#caching)
- [Contributing](#contributing)
- [Security](#security)
- [License](#license)

## Features

- **Note Management**: Create, update, delete, and retrieve notes.
- **User Authentication**: JWT-based registration and login.
- **Caching**: Uses Redis for caching frequently accessed data.

## Technologies Used

- ASP.NET Core
- Dapper
- JWT Bearer Authentication
- Redis
- SQL Server
- Swagger
- AutoMapper

## Getting Started

### Prerequisites

- .NET 8 SDK
- SQL Server
- Redis

### Installation

1. Clone the repository:
    ```bash
    git clone https://github.com/GiorgiChekurishvili/NotesAPI.git
    ```

2. Navigate to the project directory:
    ```bash
    cd NotesAPI
    ```

3. Restore dependencies:
    ```bash
    dotnet restore
    ```

### Configuration

Update your `appsettings.json` with the correct connection strings:

```json
"ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=NotesDB;Trusted_Connection=True;"
},
"RedisCache": {
    "ConnectionString": "localhost:6379"
}
```
## Database Migration

#### Apply database migrations:

```bash
dotnet ef database update
```
## Running the Application
### Run the application:
```bash
dotnet run
```
## API Endpoints
### Authentication
- POST `/api/Authentication/register`: Register a new user.
- POST `/api/Authentication/login`: Log in with JWT authentication.
  
### Notes
- GET `/api/Notes/readallnotes`: Retrieve a list of notes.
- GET `/api/Notes/readnotesbyid/{id}`: Retrieve note details by ID.
- POST `/api/Notes/createnote`: Create a new note.
- PUT `/api/Notes/updatenote/{id}`: Update a note by ID.
- DELETE `/api/Notes/deletenote/{id}`: Delete a note by ID.
  
## Caching
The application utilizes Redis for caching note data to improve performance.

## Contributing
Feel free to contribute by opening pull requests or issues. Your contributions are welcome!

## Security
JWT authentication is used for securing the endpoints.

## License
This project is licensed under the MIT License. See the LICENSE file for details.
