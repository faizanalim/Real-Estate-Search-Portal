import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { Container, Row, Col, Card, Badge, Button, Alert, Spinner, Carousel } from 'react-bootstrap';
import { propertiesAPI, favoritesAPI } from '../services/api';
import { Property, ListingType } from '../types';
import { useAuth } from '../contexts/AuthContext';

const PropertyDetail: React.FC = () => {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const { isAuthenticated } = useAuth();

  const [property, setProperty] = useState<Property | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const [favoriteLoading, setFavoriteLoading] = useState(false);

  useEffect(() => {
    if (id) {
      loadProperty(parseInt(id));
    }
  }, [id]);

  const loadProperty = async (propertyId: number) => {
    setLoading(true);
    setError('');

    try {
      const data = await propertiesAPI.getProperty(propertyId);
      setProperty(data);
    } catch (err: any) {
      setError(err.response?.data?.message || 'Failed to load property details');
    } finally {
      setLoading(false);
    }
  };

  const handleFavoriteToggle = async () => {
    if (!isAuthenticated) {
      navigate('/login');
      return;
    }

    if (!property) return;

    setFavoriteLoading(true);
    try {
      const response = await favoritesAPI.toggleFavorite(property.id);
      setProperty(prev => prev ? { ...prev, isFavorite: response.isFavorite } : null);
    } catch (error) {
      console.error('Error toggling favorite:', error);
    } finally {
      setFavoriteLoading(false);
    }
  };

  const formatPrice = (price: number) => {
    return new Intl.NumberFormat('en-US', {
      style: 'currency',
      currency: 'USD',
      minimumFractionDigits: 0,
      maximumFractionDigits: 0,
    }).format(price);
  };

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

  if (error || !property) {
    return (
      <Container>
        <Alert variant="danger">
          {error || 'Property not found'}
        </Alert>
        <Button variant="primary" onClick={() => navigate('/')}>
          Back to Properties
        </Button>
      </Container>
    );
  }

  return (
    <Container>
      <Button
        variant="outline-secondary"
        onClick={() => navigate('/')}
        className="mb-3"
      >
        ← Back to Properties
      </Button>

      <Row>
        <Col lg={8}>
          <Card className="mb-4">
            <div className="position-relative">
              {property.imageUrls.length > 0 ? (
                <Carousel>
                  {property.imageUrls.map((imageUrl, index) => (
                    <Carousel.Item key={index}>
                      <img
                        className="d-block w-100 property-detail-image"
                        src={imageUrl}
                        alt={`Property ${index + 1}`}
                      />
                    </Carousel.Item>
                  ))}
                </Carousel>
              ) : (
                <img
                  className="w-100 property-detail-image"
                  src="https://via.placeholder.com/800x400?text=No+Images+Available"
                  alt="No images available"
                />
              )}

              {isAuthenticated && (
                <Button
                  variant={property.isFavorite ? 'danger' : 'outline-danger'}
                  size="lg"
                  className="favorite-btn"
                  onClick={handleFavoriteToggle}
                  disabled={favoriteLoading}
                >
                  {favoriteLoading ? (
                    <Spinner animation="border" size="sm" />
                  ) : (
                    property.isFavorite ? '❤️' : '🤍'
                  )}
                </Button>
              )}
            </div>

            <Card.Body>
              <div className="d-flex justify-content-between align-items-start mb-3">
                <div>
                  <h1>{property.title}</h1>
                  <p className="text-muted mb-2">{property.address}</p>
                  <Badge bg={property.listingType === ListingType.Sale ? 'success' : 'info'} className="mb-2">
                    {property.listingType === ListingType.Sale ? 'For Sale' : 'For Rent'}
                  </Badge>
                </div>
                <div className="text-end">
                  <h2 className="text-primary mb-0">{formatPrice(property.price)}</h2>
                </div>
              </div>

              <Row className="mb-4">
                <Col md={3} className="text-center">
                  <div className="border rounded p-3">
                    <h4>🛏️</h4>
                    <h5>{property.bedrooms}</h5>
                    <small className="text-muted">Bedrooms</small>
                  </div>
                </Col>
                <Col md={3} className="text-center">
                  <div className="border rounded p-3">
                    <h4>🚿</h4>
                    <h5>{property.bathrooms}</h5>
                    <small className="text-muted">Bathrooms</small>
                  </div>
                </Col>
                <Col md={3} className="text-center">
                  <div className="border rounded p-3">
                    <h4>🚗</h4>
                    <h5>{property.carSpots}</h5>
                    <small className="text-muted">Car Spots</small>
                  </div>
                </Col>
                <Col md={3} className="text-center">
                  <div className="border rounded p-3">
                    <h4>💰</h4>
                    <h5>{formatPrice(property.price)}</h5>
                    <small className="text-muted">Price</small>
                  </div>
                </Col>
              </Row>

              <h4>Description</h4>
              <p>{property.description || 'No description available.'}</p>
            </Card.Body>
          </Card>
        </Col>

        <Col lg={4}>
          <Card>
            <Card.Body>
              <h5>Property Summary</h5>
              <hr />
              <div className="d-flex justify-content-between mb-2">
                <span>Type:</span>
                <span>{property.listingType === ListingType.Sale ? 'Sale' : 'Rent'}</span>
              </div>
              <div className="d-flex justify-content-between mb-2">
                <span>Bedrooms:</span>
                <span>{property.bedrooms}</span>
              </div>
              <div className="d-flex justify-content-between mb-2">
                <span>Bathrooms:</span>
                <span>{property.bathrooms}</span>
              </div>
              <div className="d-flex justify-content-between mb-2">
                <span>Car Spots:</span>
                <span>{property.carSpots}</span>
              </div>
              <hr />
              <div className="d-flex justify-content-between">
                <strong>Price:</strong>
                <strong className="text-primary">{formatPrice(property.price)}</strong>
              </div>
            </Card.Body>
          </Card>
        </Col>
      </Row>
    </Container>
  );
};

export default PropertyDetail;
