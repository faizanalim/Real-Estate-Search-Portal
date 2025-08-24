# Real Estate Search Portal - Frontend

A modern React TypeScript application for browsing and managing real estate properties with user authentication and favorites functionality.

## 🚀 Features

- **Property Search & Filtering**: Search properties with advanced filters (price, bedrooms, location, listing type)
- **User Authentication**: Register and login with JWT token management
- **Property Details**: Comprehensive property information with image carousel
- **Favorites Management**: Save and manage favorite properties (authenticated users only)
- **Responsive Design**: Mobile-friendly interface using Bootstrap
- **Real-time Updates**: Instant favorite toggling and state management

## 🛠️ Tech Stack

- **React 18** with TypeScript
- **React Router** for navigation
- **Axios** for API communication
- **Bootstrap 5** for UI components
- **React Bootstrap** for Bootstrap React components
- **Context API** for state management
- **Local Storage** for JWT token persistence

## 📦 Installation

### Prerequisites
- Node.js (v16 or higher)
- npm or yarn
- Backend API running on `http://localhost:5019`

### Setup Instructions

1. **Install dependencies**
   ```bash
   cd frontend
   npm install
   ```

2. **Start the development server**
   ```bash
   npm start
   ```

3. **Open your browser**
   Navigate to `http://localhost:3000`

## 🏗️ Project Structure

```
src/
├── components/          # Reusable UI components
│   ├── Navigation.tsx   # Main navigation bar
│   ├── Login.tsx        # Login form
│   ├── Register.tsx     # Registration form
│   ├── PropertyCard.tsx # Property display card
│   └── PropertyFilter.tsx # Search and filter form
├── pages/              # Main application pages
│   ├── PropertyList.tsx    # Property listing with search
│   ├── PropertyDetail.tsx  # Detailed property view
│   └── Favorites.tsx       # User's favorite properties
├── contexts/           # React Context providers
│   └── AuthContext.tsx # Authentication state management
├── services/           # API service layer
│   └── api.ts         # Axios configuration and API calls
├── types/             # TypeScript type definitions
│   └── index.ts       # Application interfaces and types
├── App.tsx            # Main application component
├── index.tsx          # Application entry point
└── index.css          # Global styles
```

## 🔐 Authentication

The application uses JWT tokens for authentication:

- **Registration**: Create new user account
- **Login**: Authenticate existing users
- **Token Storage**: JWT tokens stored in localStorage
- **Auto-logout**: Automatic logout on token expiration
- **Protected Routes**: Favorites page requires authentication

## 🏠 Property Features

### Property Listing
- Grid layout with property cards
- Advanced filtering options:
  - Price range (min/max)
  - Number of bedrooms
  - Listing type (Rent/Sale)
  - Location/suburb search
- Pagination support
- Real-time search results

### Property Details
- Image carousel for multiple property photos
- Comprehensive property information
- Interactive favorite button
- Property specifications display
- Responsive layout

### Favorites Management
- Save/unsave properties
- View all saved properties
- Remove properties from favorites
- Authentication required

## 🎨 UI Components

### Navigation
- Responsive navigation bar
- Dynamic menu based on authentication status
- User email display
- Logout functionality

### Property Cards
- Property image with fallback
- Price formatting
- Property specifications
- Favorite button (authenticated users)
- Hover effects and animations

### Forms
- Login and registration forms
- Form validation
- Error handling
- Loading states

## 🔧 Configuration

### API Configuration
The frontend is configured to connect to the backend API at `http://localhost:5019`. Update the API base URL in `src/services/api.ts` if needed:

```typescript
const API_BASE_URL = 'http://localhost:5019/api';
```

### Proxy Configuration
The development server is configured with a proxy to avoid CORS issues:

```json
{
  "proxy": "http://localhost:5019"
}
```

## 🚀 Available Scripts

- `npm start` - Start development server
- `npm build` - Build for production
- `npm test` - Run tests
- `npm eject` - Eject from Create React App

## 📱 Responsive Design

The application is fully responsive and works on:
- Desktop computers
- Tablets
- Mobile phones

Bootstrap 5 provides responsive grid system and components.

## 🔒 Security Features

- JWT token authentication
- Automatic token refresh handling
- Protected routes
- Secure API communication
- Input validation and sanitization

## 🎯 User Experience

- **Intuitive Navigation**: Easy-to-use navigation with clear labels
- **Fast Loading**: Optimized components and efficient state management
- **Error Handling**: User-friendly error messages and loading states
- **Accessibility**: Semantic HTML and ARIA labels
- **Mobile First**: Responsive design optimized for mobile devices

## 🔄 State Management

The application uses React Context API for state management:

- **Authentication State**: User login status and information
- **Property Data**: Property listings and details
- **Favorites**: User's saved properties
- **UI State**: Loading states and error messages

## 🧪 Testing

The application includes:
- TypeScript for type safety
- PropTypes for component validation
- Error boundaries for error handling
- Console logging for debugging

## 🚀 Deployment

### Build for Production
```bash
npm run build
```

### Deploy Options
- **Netlify**: Drag and drop the `build` folder
- **Vercel**: Connect GitHub repository
- **AWS S3**: Upload build files to S3 bucket
- **Azure Static Web Apps**: Deploy via Azure CLI

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Test thoroughly
5. Submit a pull request

## 📄 License

This project is part of the Real Estate Search Portal application.

## 🆘 Support

For issues and questions:
1. Check the backend API is running
2. Verify API endpoints are accessible
3. Check browser console for errors
4. Ensure all dependencies are installed

---

**Note**: Make sure the backend API is running on `http://localhost:5019` before starting the frontend application.
