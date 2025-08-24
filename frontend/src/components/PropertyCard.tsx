import React from 'react';
import { Card, Badge, Button } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import { Property, ListingType } from '../types';
import { useAuth } from '../contexts/AuthContext';
import { favoritesAPI } from '../services/api';

interface PropertyCardProps {
  property: Property;
  onFavoriteToggle?: (propertyId: number, isFavorite: boolean) => void;
}

const PropertyCard: React.FC<PropertyCardProps> = ({ property, onFavoriteToggle }) => {
  const navigate = useNavigate();
  const { isAuthenticated } = useAuth();

  const handleFavoriteClick = async (e: React.MouseEvent) => {
    e.stopPropagation();
    
    if (!isAuthenticated) {
      navigate('/login');
      return;
    }

    try {
      const response = await favoritesAPI.toggleFavorite(property.id);
      if (onFavoriteToggle) {
        onFavoriteToggle(property.id, response.isFavorite);
      }
    } catch (error) {
      console.error('Error toggling favorite:', error);
    }
  };

  const handleCardClick = () => {
    navigate(`/property/${property.id}`);
  };

  const formatPrice = (price: number) => {
    return new Intl.NumberFormat('en-US', {
      style: 'currency',
      currency: 'USD',
      minimumFractionDigits: 0,
      maximumFractionDigits: 0,
    }).format(price);
  };

  return (
    <Card className="property-card h-100" onClick={handleCardClick}>
      <div className="position-relative">
        <Card.Img
          variant="top"
          src={property.imageUrls[0] || 'https://via.placeholder.com/300x200?text=No+Image'}
          className="property-image"
        />
        {isAuthenticated && (
          <Button
            variant={property.isFavorite ? 'danger' : 'outline-danger'}
            size="sm"
            className="favorite-btn"
            onClick={handleFavoriteClick}
          >
            {property.isFavorite ? '❤️' : '🤍'}
          </Button>
        )}
      </div>
      
      <Card.Body>
        <Card.Title className="h6">{property.title}</Card.Title>
        <Card.Text className="text-muted small mb-2">{property.address}</Card.Text>
        
        <div className="d-flex justify-content-between align-items-center mb-2">
          <Badge bg={property.listingType === ListingType.Sale ? 'success' : 'info'}>
            {property.listingType === ListingType.Sale ? 'For Sale' : 'For Rent'}
          </Badge>
          <span className="fw-bold text-primary">{formatPrice(property.price)}</span>
        </div>
        
        <div className="d-flex justify-content-between text-muted small">
          <span>🛏️ {property.bedrooms} beds</span>
          <span>🚿 {property.bathrooms} baths</span>
          <span>🚗 {property.carSpots} cars</span>
        </div>
      </Card.Body>
    </Card>
  );
};

export default PropertyCard;
