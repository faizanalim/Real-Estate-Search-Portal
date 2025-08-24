# Real Estate API Backend

A .NET 8 Web API for the Real Estate Search Portal with JWT authentication, Entity Framework Core, and SQL Server.

## 🏗️ Architecture

This project follows a clean layered architecture:

- **Controllers** - Handle HTTP requests and responses
- **Services** - Business logic layer
- **Data** - Entity Framework Core DbContext and data access
- **Models** - Entity models
- **DTOs** - Data Transfer Objects for API requests/responses

## 🚀 Setup Instructions

### Prerequisites
- .NET 8 SDK
- SQL Server (LocalDB or full SQL Server)
- Visual Studio 2022 or VS Code

### Installation

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd backend/RealEstateAPI
   ```

2. **Install dependencies**
   ```bash
   dotnet restore
   ```

3. **Setup environment configuration**
   
   **Important:** The `appsettings.json` file is excluded from git for security reasons. 
   Copy the template file and configure your settings:
   
   ```bash
   cp appsettings.template.json appsettings.json
   ```
   
   Then edit `appsettings.json` and update:
   - **Connection string** (if needed)
   - **JWT Key** (use a strong secret key, minimum 32 characters)
   
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=RealEstateDB;Trusted_Connection=true;MultipleActiveResultSets=true"
     },
     "Jwt": {
       "Key": "YOUR_SUPER_SECRET_KEY_HERE_MINIMUM_32_CHARACTERS",
       "Issuer": "RealEstateAPI",
       "Audience": "RealEstateClient"
     }
   }
   ```

4. **Run database migrations**
   ```bash
   dotnet ef database update
   ```

5. **Run the application**
   ```bash
   dotnet run
   ```

The API will be available at `https://localhost:7001` (or the port shown in the console).

## 📚 API Documentation

Once the application is running, you can access the Swagger UI at:
`https://localhost:7001/swagger`

### Authentication Endpoints

#### POST /api/auth/register
Register a new user account.

**Request Body:**
```json
{
  "email": "user@example.com",
  "password": "password123"
}
```

**Response:**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "email": "user@example.com",
  "userId": 1
}
```

#### POST /api/auth/login
Login with existing credentials.

**Request Body:**
```json
{
  "email": "user@example.com",
  "password": "password123"
}
```

**Response:**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "email": "user@example.com",
  "userId": 1
}
```

### Property Endpoints

#### GET /api/properties
Get a list of properties with optional filtering.

**Query Parameters:**
- `minPrice` (decimal) - Minimum price filter
- `maxPrice` (decimal) - Maximum price filter
- `minBedrooms` (int) - Minimum number of bedrooms
- `maxBedrooms` (int) - Maximum number of bedrooms
- `suburb` (string) - Suburb/location filter
- `listingType` (int) - 0 for Rent, 1 for Sale
- `page` (int) - Page number (default: 1)
- `pageSize` (int) - Items per page (default: 10)

**Response:**
```json
{
  "properties": [
    {
      "id": 1,
      "title": "Modern 3-Bedroom Apartment in CBD",
      "address": "123 Collins Street, Melbourne CBD",
      "price": 850000,
      "listingType": 1,
      "bedrooms": 3,
      "bathrooms": 2,
      "carSpots": 1,
      "description": "Beautiful modern apartment...",
      "imageUrls": ["https://example.com/image1.jpg"],
      "isFavorite": false
    }
  ],
  "totalCount": 6,
  "page": 1,
  "pageSize": 10,
  "totalPages": 1
}
```

#### GET /api/properties/{id}
Get a specific property by ID.

**Response:**
```json
{
  "id": 1,
  "title": "Modern 3-Bedroom Apartment in CBD",
  "address": "123 Collins Street, Melbourne CBD",
  "price": 850000,
  "listingType": 1,
  "bedrooms": 3,
  "bathrooms": 2,
  "carSpots": 1,
  "description": "Beautiful modern apartment...",
  "imageUrls": ["https://example.com/image1.jpg"],
  "isFavorite": false
}
```

### Favorites Endpoints (Requires Authentication)

#### POST /api/favorites/{propertyId}
Toggle favorite status for a property.

**Headers:**
```
Authorization: Bearer <jwt-token>
```

**Response:**
```json
{
  "isFavorite": true
}
```

#### GET /api/favorites
Get user's favorite properties.

**Headers:**
```
Authorization: Bearer <jwt-token>
```

**Response:**
```json
[
  {
    "id": 1,
    "title": "Modern 3-Bedroom Apartment in CBD",
    "address": "123 Collins Street, Melbourne CBD",
    "price": 850000,
    "listingType": 1,
    "bedrooms": 3,
    "bathrooms": 2,
    "carSpots": 1,
    "description": "Beautiful modern apartment...",
    "imageUrls": ["https://example.com/image1.jpg"],
    "isFavorite": true
  }
]
```

## 🔐 Authentication

The API uses JWT (JSON Web Tokens) for authentication. To access protected endpoints:

1. Register or login to get a token
2. Include the token in the Authorization header:
   ```
   Authorization: Bearer <your-jwt-token>
   ```

## 🗄️ Database Schema

### Users Table
- `Id` (int, PK) - User ID
- `Email` (string) - User email (unique)
- `PasswordHash` (string) - Hashed password

### Properties Table
- `Id` (int, PK) - Property ID
- `Title` (string) - Property title
- `Address` (string) - Property address
- `Price` (decimal) - Property price
- `ListingType` (int) - 0 for Rent, 1 for Sale
- `Bedrooms` (int) - Number of bedrooms
- `Bathrooms` (int) - Number of bathrooms
- `CarSpots` (int) - Number of car spots
- `Description` (string) - Property description
- `ImageUrls` (string) - JSON array of image URLs

### Favorites Table
- `Id` (int, PK) - Favorite ID
- `UserId` (int, FK) - User ID
- `PropertyId` (int, FK) - Property ID

## 🛠️ Development

### Adding New Endpoints

1. Create DTOs in the `DTOs` folder
2. Add business logic in the `Services` folder
3. Create controllers in the `Controllers` folder
4. Update the database context if needed

### Database Changes

1. Update entity models in the `Models` folder
2. Create a new migration:
   ```bash
   dotnet ef migrations add MigrationName
   ```
3. Update the database:
   ```bash
   dotnet ef database update
   ```

## 📦 Dependencies

- **Microsoft.EntityFrameworkCore.SqlServer** - SQL Server provider
- **Microsoft.AspNetCore.Authentication.JwtBearer** - JWT authentication
- **AutoMapper.Extensions.Microsoft.DependencyInjection** - Object mapping
- **Swashbuckle.AspNetCore** - API documentation

## 🚀 Deployment

The application can be deployed to:
- Azure App Service
- IIS
- Docker containers
- Any platform supporting .NET 8

Make sure to update the connection string and JWT settings for production environments.
