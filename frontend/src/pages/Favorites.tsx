import React, { useState, useEffect } from 'react';
import { Container, Row, Col, Alert, Spinner, Card } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import { favoritesAPI } from '../services/api';
import { Property } from '../types';
import { useAuth } from '../contexts/AuthContext';
import PropertyCard from '../components/PropertyCard';

const Favorites: React.FC = () => {
  const [favorites, setFavorites] = useState<Property[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const { isAuthenticated } = useAuth();
  const navigate = useNavigate();

  useEffect(() => {
    if (!isAuthenticated) {
      navigate('/login');
      return;
    }

    loadFavorites();
  }, [isAuthenticated, navigate]);

  const loadFavorites = async () => {
    setLoading(true);
    setError('');

    try {
      const data = await favoritesAPI.getFavorites();
      setFavorites(data);
    } catch (err: any) {
      setError(err.response?.data?.message || 'Failed to load favorites');
    } finally {
      setLoading(false);
    }
  };

  const handleFavoriteToggle = (propertyId: number, isFavorite: boolean) => {
    if (!isFavorite) {
      // Remove from favorites list
      setFavorites(prev => prev.filter(property => property.id !== propertyId));
    }
  };

  if (!isAuthenticated) {
    return null; // Will redirect to login
  }

  if (loading) {
    return (
      <Container>
        <div className="loading-spinner">
          <Spinner animation="border" role="status">
            <span className="visually-hidden">Loading...</span>
          </Spinner>
        </div>
      </Container>
    );
  }

  return (
    <Container>
      <div className="d-flex justify-content-between align-items-center mb-4">
        <h1>My Favorites</h1>
        <span className="text-muted">
          {favorites.length} propert{favorites.length === 1 ? 'y' : 'ies'} saved
        </span>
      </div>

      {error && <Alert variant="danger">{error}</Alert>}

      {favorites.length === 0 ? (
        <Card>
          <Card.Body className="text-center py-5">
            <h4 className="text-muted mb-3">No favorites yet</h4>
            <p className="text-muted mb-4">
              Start exploring properties and save your favorites to see them here.
            </p>
            <button
              className="btn btn-primary"
              onClick={() => navigate('/')}
            >
              Browse Properties
            </button>
          </Card.Body>
        </Card>
      ) : (
        <Row>
          {favorites.map(property => (
            <Col key={property.id} lg={4} md={6} className="mb-4">
              <PropertyCard
                property={property}
                onFavoriteToggle={handleFavoriteToggle}
              />
            </Col>
          ))}
        </Row>
      )}
    </Container>
  );
};

export default Favorites;
