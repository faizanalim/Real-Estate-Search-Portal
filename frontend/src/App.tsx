import React from 'react';
import { Routes, Route, Navigate } from 'react-router-dom';
import { AuthProvider } from './contexts/AuthContext';
import Navigation from './components/Navbar';
import PropertyList from './pages/PropertyList';
import PropertyDetail from './pages/PropertyDetail';
import Favorites from './pages/Favorites';
import Login from './components/Login';
import Register from './components/Register';

const App: React.FC = () => {
  return (
    <AuthProvider>
      <div className="App">
        <Navigation />
        <main className="py-4">
          <Routes>
            <Route path="/" element={<PropertyList />} />
            <Route path="/property/:id" element={<PropertyDetail />} />
            <Route path="/favorites" element={<Favorites />} />
            <Route path="/login" element={<Login />} />
            <Route path="/register" element={<Register />} />
            <Route path="*" element={<Navigate to="/" replace />} />
          </Routes>
        </main>
      </div>
    </AuthProvider>
  );
};

export default App;
