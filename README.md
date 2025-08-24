# 🏠 Real Estate Search Portal

A full-stack web application for browsing and managing real estate properties with user authentication, advanced search filters, and favorites functionality.

## 📋 Project Overview

This is a complete real estate search portal built with modern web technologies:

- **Backend**: .NET 8 Web API with Entity Framework Core and SQL Server
- **Frontend**: React 18 with TypeScript, Bootstrap 5, and Axios
- **Database**: SQL Server with LocalDB for development
- **Authentication**: JWT token-based authentication

## 🏗️ Repository Structure

```
Real Estate Search Portal/
├── backend/                    # .NET 8 Web API
│   ├── RealEstateAPI/         # Main API project
│   │   ├── Controllers/       # API endpoints
│   │   ├── Models/           # Entity models
│   │   ├── Services/         # Business logic
│   │   ├── Data/            # Database context
│   │   └── DTOs/            # Data transfer objects
│   └── README.md            # Backend documentation
├── frontend/                  # React TypeScript application
│   ├── src/                 # Source code
│   │   ├── components/      # Reusable UI components
│   │   ├── pages/          # Main application pages
│   │   ├── contexts/       # React Context providers
│   │   ├── services/       # API service layer
│   │   └── types/          # TypeScript definitions
│   └── README.md           # Frontend documentation
├── STARTUP_GUIDE.md         # Complete setup instructions
├── start-frontend.bat       # Windows batch script
├── start-frontend.ps1       # PowerShell script
└── README.md               # This file
```

## 🚀 Quick Start

### Prerequisites
- **.NET 8 SDK** - [Download here](https://dotnet.microsoft.com/download/dotnet/8.0)
- **Node.js** (v16+) - [Download here](https://nodejs.org/)
- **SQL Server** (LocalDB, Express, or Developer Edition)
- **Visual Studio 2022** or **Visual Studio Code**

### 1. Start the Backend
```bash
cd backend/RealEstateAPI
dotnet restore
dotnet ef database update
dotnet run
```
**Backend will be available at:** `http://localhost:5019`
**Swagger UI:** `http://localhost:5019/swagger`

### 2. Start the Frontend
```bash
cd frontend
npm install
npm start
```
**Frontend will be available at:** `http://localhost:3000`

### 3. Alternative Startup
Use the provided scripts:
- **Windows:** `start-frontend.bat`
- **PowerShell:** `.\start-frontend.ps1`

## 🎯 Features

### 🔐 Authentication
- User registration and login
- JWT token-based authentication
- Protected routes and endpoints
- Automatic token management

### 🏠 Property Management
- Browse properties with advanced filters
- Search by price, bedrooms, location, and listing type
- Property details with image carousel
- Responsive property cards

### ❤️ Favorites System
- Save and manage favorite properties
- Toggle favorites with heart icons
- View all saved properties
- Authentication required for favorites

### 🔍 Advanced Search
- Price range filtering
- Bedroom count filtering
- Location/suburb search
- Listing type filtering (Rent/Sale)
- Pagination support

### 📱 Responsive Design
- Mobile-first approach
- Bootstrap 5 components
- Cross-device compatibility
- Modern UI/UX design

## 🛠️ Technology Stack

### Backend
- **.NET 8** - Latest .NET framework
- **Entity Framework Core** - ORM for database operations
- **SQL Server** - Database
- **JWT Authentication** - Secure token-based auth
- **AutoMapper** - Object mapping
- **Swagger** - API documentation

### Frontend
- **React 18** - Latest React with hooks
- **TypeScript** - Type safety
- **Bootstrap 5** - UI framework
- **React Bootstrap** - Bootstrap React components
- **Axios** - HTTP client
- **React Router** - Navigation
- **Context API** - State management

## 📚 Documentation

- **[STARTUP_GUIDE.md](STARTUP_GUIDE.md)** - Complete setup and usage guide
- **[Backend README](backend/README.md)** - Backend-specific documentation
- **[Frontend README](frontend/README.md)** - Frontend-specific documentation

## 🔧 Configuration

### Backend Configuration
- **Port**: `http://localhost:5019`
- **Database**: SQL Server LocalDB
- **JWT**: Configured in `appsettings.json`

### Frontend Configuration
- **Port**: `http://localhost:3000`
- **API Endpoint**: `http://localhost:5019/api`
- **Proxy**: Configured for CORS handling

## 🧪 Testing

### Backend Testing
- Use Swagger UI at `http://localhost:5019/swagger`
- Test all API endpoints
- Verify authentication flow

### Frontend Testing
- Test user registration and login
- Browse properties and use filters
- Test favorites functionality
- Verify responsive design

## 🚀 Deployment

### Backend Deployment
- Deploy to Azure App Service
- Configure production database
- Set environment variables

### Frontend Deployment
- Build: `npm run build`
- Deploy to Netlify, Vercel, or Azure Static Web Apps

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Test thoroughly
5. Submit a pull request

## 📄 License

This project is open source and available under the MIT License.

## 🆘 Support

If you encounter issues:

1. Check the [STARTUP_GUIDE.md](STARTUP_GUIDE.md) for troubleshooting
2. Verify all prerequisites are installed
3. Ensure both backend and frontend are running
4. Check console logs for error messages

## 🔒 Security & Configuration

### Environment Setup
- **Backend**: Copy `backend/RealEstateAPI/appsettings.template.json` to `appsettings.json` and configure your settings
- **Frontend**: No sensitive configuration required
- **Database**: Uses LocalDB by default, can be configured in appsettings.json

### Git Ignore Files
The repository includes comprehensive `.gitignore` files:
- **Root**: General files, IDE settings, OS files
- **Backend**: .NET specific files, build outputs, sensitive configs
- **Frontend**: Node.js files, build outputs, dependencies

## 🎉 Getting Started

1. **Clone the repository**
2. **Setup environment configuration** (see Security section above)
3. **Follow the [STARTUP_GUIDE.md](STARTUP_GUIDE.md)**
4. **Start both backend and frontend**
5. **Open `http://localhost:3000` in your browser**
6. **Register an account and start exploring!**

---

**Happy coding! 🏠✨**

*Built with ❤️ using .NET 8 and React 18*
