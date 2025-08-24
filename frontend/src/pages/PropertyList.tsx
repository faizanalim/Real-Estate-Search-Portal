import React, { useState, useEffect } from 'react';
import { Container, Row, Col, Alert, Pagination, Spinner } from 'react-bootstrap';
import { propertiesAPI } from '../services/api';
import { Property, PropertyFilter as PropertyFilterType } from '../types';
import PropertyCard from '../components/PropertyCard';
import PropertyFilter from '../components/PropertyFilter';

const PropertyList: React.FC = () => {
  const [properties, setProperties] = useState<Property[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const [totalPages, setTotalPages] = useState(1);
  const [currentPage, setCurrentPage] = useState(1);
  const [totalCount, setTotalCount] = useState(0);

  const loadProperties = async (filters: PropertyFilterType) => {
    setLoading(true);
    setError('');

    try {
      const response = await propertiesAPI.getProperties(filters);
      setProperties(response.properties);
      setTotalPages(response.totalPages);
      setCurrentPage(response.page);
      setTotalCount(response.totalCount);
    } catch (err: any) {
      setError(err.response?.data?.message || 'Failed to load properties');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    loadProperties({ page: 1, pageSize: 10 });
  }, []);

  const handleFilterChange = (filters: PropertyFilterType) => {
    loadProperties(filters);
  };

  const handlePageChange = (page: number) => {
    const filters: PropertyFilterType = {
      page,
      pageSize: 10,
    };
    loadProperties(filters);
  };

  const handleFavoriteToggle = (propertyId: number, isFavorite: boolean) => {
    setProperties(prev => 
      prev.map(property => 
        property.id === propertyId 
          ? { ...property, isFavorite } 
          : property
      )
    );
  };

  const renderPagination = () => {
    if (totalPages <= 1) return null;

    const items = [];
    const startPage = Math.max(1, currentPage - 2);
    const endPage = Math.min(totalPages, currentPage + 2);

    for (let page = startPage; page <= endPage; page++) {
      items.push(
        <Pagination.Item
          key={page}
          active={page === currentPage}
          onClick={() => handlePageChange(page)}
        >
          {page}
        </Pagination.Item>
      );
    }

    return (
      <div className="d-flex justify-content-center mt-4">
        <Pagination>
          <Pagination.First
            disabled={currentPage === 1}
            onClick={() => handlePageChange(1)}
          />
          <Pagination.Prev
            disabled={currentPage === 1}
            onClick={() => handlePageChange(currentPage - 1)}
          />
          {items}
          <Pagination.Next
            disabled={currentPage === totalPages}
            onClick={() => handlePageChange(currentPage + 1)}
          />
          <Pagination.Last
            disabled={currentPage === totalPages}
            onClick={() => handlePageChange(totalPages)}
          />
        </Pagination>
      </div>
    );
  };

  return (
    <Container>
      <h1 className="mb-4">Find Your Dream Property</h1>
      
      <PropertyFilter onFilterChange={handleFilterChange} loading={loading} />
      
      {error && <Alert variant="danger">{error}</Alert>}
      
      {loading ? (
        <div className="loading-spinner">
          <Spinner animation="border" role="status">
            <span className="visually-hidden">Loading...</span>
          </Spinner>
        </div>
      ) : (
        <>
          <div className="mb-3">
            <p className="text-muted">
              Found {totalCount} property{totalCount !== 1 ? 'ies' : 'y'}
            </p>
          </div>
          
          {properties.length === 0 ? (
            <Alert variant="info">
              No properties found matching your criteria. Try adjusting your filters.
            </Alert>
          ) : (
            <Row>
              {properties.map(property => (
                <Col key={property.id} lg={4} md={6} className="mb-4">
                  <PropertyCard
                    property={property}
                    onFavoriteToggle={handleFavoriteToggle}
                  />
                </Col>
              ))}
            </Row>
          )}
          
          {renderPagination()}
        </>
      )}
    </Container>
  );
};

export default PropertyList;
