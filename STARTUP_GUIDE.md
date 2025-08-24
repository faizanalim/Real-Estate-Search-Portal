# Real Estate Search Portal - Startup Guide

This guide will help you get both the backend API and frontend application running on your local machine.

## 🚀 Quick Start

### Prerequisites
- **.NET 8 SDK** - Download from [Microsoft](https://dotnet.microsoft.com/download/dotnet/8.0)
- **Node.js** (v16 or higher) - Download from [Node.js](https://nodejs.org/)
- **SQL Server** (LocalDB, Express, or Developer Edition)
- **Visual Studio 2022** or **Visual Studio Code** (recommended)

### Step 1: Start the Backend API

1. **Open a terminal/command prompt**
2. **Navigate to the backend directory:**
   ```bash
   cd backend/RealEstateAPI
   ```

3. **Restore NuGet packages:**
   ```bash
   dotnet restore
   ```

4. **Run database migrations:**
   ```bash
   dotnet ef database update
   ```

5. **Start the API:**
   ```bash
   dotnet run
   ```

6. **Verify the API is running:**
   - Open your browser and go to: `http://localhost:5019/swagger`
   - You should see the Swagger UI with all available endpoints

### Step 2: Start the Frontend Application

1. **Open a new terminal/command prompt**
2. **Navigate to the frontend directory:**
   ```bash
   cd frontend
   ```

3. **Install dependencies (if not already done):**
   ```bash
   npm install
   ```

4. **Start the development server:**
   ```bash
   npm start
   ```

5. **Verify the frontend is running:**
   - Open your browser and go to: `http://localhost:3000`
   - You should see the Real Estate Search Portal homepage

## 🔧 Alternative Startup Methods

### Using the Provided Scripts

#### Windows Batch Script
```bash
# Start frontend only
start-frontend.bat
```

#### PowerShell Script
```powershell
# Start frontend only
.\start-frontend.ps1
```

### Using Visual Studio

1. **Open the solution file:**
   - Navigate to `backend/RealEstateAPI/RealEstateAPI.sln`
   - Open with Visual Studio 2022

2. **Set startup project:**
   - Right-click on `RealEstateAPI` project
   - Select "Set as Startup Project"

3. **Run the project:**
   - Press `F5` or click the "Start" button

## 🌐 Accessing the Application

### Backend API
- **Swagger UI:** `http://localhost:5019/swagger`
- **API Base URL:** `http://localhost:5019/api`

### Frontend Application
- **Homepage:** `http://localhost:3000`
- **Properties:** `http://localhost:3000/`
- **Login:** `http://localhost:3000/login`
- **Register:** `http://localhost:3000/register`
- **Favorites:** `http://localhost:3000/favorites` (requires login)

## 🗄️ Database Configuration

### Default Connection String
The application uses the following connection string (configured in `appsettings.json`):
```
"DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=RealEstateDB;Trusted_Connection=true;MultipleActiveResultSets=true"
```

### Database Setup
The application will automatically:
1. Create the database if it doesn't exist
2. Run all migrations
3. Seed the database with sample data

### Sample Data
The database is seeded with:
- Sample properties with images
- Test user accounts (if needed)

## 🔐 Authentication

### Default Test Account
You can register a new account or use the following test credentials:
- **Email:** `test@example.com`
- **Password:** `password123`

### JWT Configuration
JWT tokens are configured in `appsettings.json`:
```json
{
  "Jwt": {
    "Key": "YourSuperSecretKeyHere",
    "Issuer": "RealEstateAPI",
    "Audience": "RealEstateClient"
  }
}
```

## 🛠️ Troubleshooting

### Common Issues

#### Backend Issues

1. **Port already in use:**
   - Change the port in `Properties/launchSettings.json`
   - Or kill the process using the port

2. **Database connection failed:**
   - Ensure SQL Server is running
   - Check connection string in `appsettings.json`
   - Verify LocalDB is installed

3. **Migration errors:**
   ```bash
   dotnet ef database drop
   dotnet ef database update
   ```

#### Frontend Issues

1. **Port 3000 already in use:**
   - The development server will automatically suggest an alternative port
   - Or manually change the port in `package.json`

2. **API connection failed:**
   - Ensure the backend is running on `http://localhost:5019`
   - Check CORS configuration
   - Verify the backend is accessible

3. **Dependencies not found:**
   ```bash
   npm install
   ```

### SSL Certificate Issues

If you encounter SSL certificate warnings:

1. **For Backend:**
   - Accept the self-signed certificate in your browser
   - Or run: `dotnet dev-certs https --trust`

2. **For Frontend:**
   - Accept the certificate warning in your browser
   - Or use HTTP instead of HTTPS for development

## 📱 Testing the Application

### Basic Functionality Test

1. **Browse Properties:**
   - Visit `http://localhost:3000`
   - Use the filter options to search properties

2. **User Registration:**
   - Click "Register" in the navigation
   - Create a new account

3. **User Login:**
   - Click "Login" in the navigation
   - Sign in with your credentials

4. **Favorites:**
   - Browse properties
   - Click the heart icon to save favorites
   - View your favorites in the "My Favorites" section

5. **Property Details:**
   - Click on any property card
   - View detailed information and images

## 🔄 Development Workflow

### Making Changes

1. **Backend Changes:**
   - Modify C# code in the backend
   - The API will automatically restart

2. **Frontend Changes:**
   - Modify React components
   - The development server will hot-reload

3. **Database Changes:**
   - Create new migrations: `dotnet ef migrations add MigrationName`
   - Apply migrations: `dotnet ef database update`

### Debugging

1. **Backend Debugging:**
   - Use Visual Studio debugger
   - Check Swagger UI for API testing
   - View logs in the console

2. **Frontend Debugging:**
   - Use browser developer tools
   - Check React Developer Tools extension
   - View console logs

## 🚀 Production Deployment

### Backend Deployment
- Deploy to Azure App Service
- Configure production database
- Set environment variables

### Frontend Deployment
- Build: `npm run build`
- Deploy to Netlify, Vercel, or Azure Static Web Apps

## 📞 Support

If you encounter issues:

1. Check the console output for error messages
2. Verify all prerequisites are installed
3. Ensure both backend and frontend are running
4. Check the troubleshooting section above

## 🎯 Next Steps

Once the application is running:

1. Explore the property listings
2. Test user registration and login
3. Try the favorites functionality
4. Test the search and filter features
5. Review the API documentation in Swagger

---

**Happy coding! 🏠✨**
