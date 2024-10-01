# Conference Room Booking API

This project is a web API for managing conference room bookings, including services offered by the rooms. Built using ASP.NET Core and Entity Framework Core, the API allows creating, editing, and viewing halls and their services, as well as making bookings.

## Prerequisites

- .NET 6.0 SDK
- SQL Server or a compatible database.
- Entity Framework Core global tool.

## Setup

### 1. Clone the repository

```bash
git clone https://github.com/DimenDie/ConferenceRoomsBooking
```

### 2. Install dependencies

Restore the .NET packages:

```bash
dotnet restore
```

### 3. Update the database connection string (Optional)

In `appsettings.json`, update the connection string to match your database configuration:

```json
    "ConnectionStrings": {
      "DefaultConnection" : "server=.,2433;Database=ConferenceRooms;User Id=sa;Password=P@ssw0rd;Encrypt=False;"
    }
```

### 4. Apply migrations and seed the database

To initialize the database with sample data, the project is configured to run migrations and seed sample data at startup.

```bash
dotnet ef database update
```

### 5. Run the application

Start the API locally:

```bash
dotnet watch
```

