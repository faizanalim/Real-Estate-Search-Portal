import React, { useState } from 'react';
import { Form, Row, Col, Button, Card } from 'react-bootstrap';
import { PropertyFilter as PropertyFilterType, ListingType } from '../types';

interface PropertyFilterProps {
  onFilterChange: (filters: PropertyFilterType) => void;
  loading?: boolean;
}

const PropertyFilter: React.FC<PropertyFilterProps> = ({ onFilterChange, loading = false }) => {
  const [filters, setFilters] = useState<PropertyFilterType>({
    page: 1,
    pageSize: 10,
  });

  const handleInputChange = (field: keyof PropertyFilterType, value: string | number | undefined) => {
    const newFilters = { ...filters, [field]: value };
    setFilters(newFilters);
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    onFilterChange({ ...filters, page: 1 });
  };

  const handleClear = () => {
    const clearedFilters: PropertyFilterType = {
      page: 1,
      pageSize: 10,
    };
    setFilters(clearedFilters);
    onFilterChange(clearedFilters);
  };

  return (
    <Card className="filter-section">
      <Card.Body>
        <Form onSubmit={handleSubmit}>
          <Row>
            <Col md={3}>
              <Form.Group className="mb-3">
                <Form.Label>Min Price</Form.Label>
                <Form.Control
                  type="number"
                  placeholder="Min price"
                  value={filters.minPrice || ''}
                  onChange={(e) => handleInputChange('minPrice', e.target.value ? Number(e.target.value) : undefined)}
                />
              </Form.Group>
            </Col>
            
            <Col md={3}>
              <Form.Group className="mb-3">
                <Form.Label>Max Price</Form.Label>
                <Form.Control
                  type="number"
                  placeholder="Max price"
                  value={filters.maxPrice || ''}
                  onChange={(e) => handleInputChange('maxPrice', e.target.value ? Number(e.target.value) : undefined)}
                />
              </Form.Group>
            </Col>
            
            <Col md={3}>
              <Form.Group className="mb-3">
                <Form.Label>Bedrooms</Form.Label>
                <Form.Select
                  value={filters.minBedrooms || ''}
                  onChange={(e) => handleInputChange('minBedrooms', e.target.value ? Number(e.target.value) : undefined)}
                >
                  <option value="">Any</option>
                  <option value="1">1+</option>
                  <option value="2">2+</option>
                  <option value="3">3+</option>
                  <option value="4">4+</option>
                  <option value="5">5+</option>
                </Form.Select>
              </Form.Group>
            </Col>
            
            <Col md={3}>
              <Form.Group className="mb-3">
                <Form.Label>Listing Type</Form.Label>
                <Form.Select
                  value={filters.listingType !== undefined ? filters.listingType : ''}
                  onChange={(e) => handleInputChange('listingType', e.target.value ? Number(e.target.value) : undefined)}
                >
                  <option value="">All</option>
                  <option value={ListingType.Rent}>For Rent</option>
                  <option value={ListingType.Sale}>For Sale</option>
                </Form.Select>
              </Form.Group>
            </Col>
          </Row>
          
          <Row>
            <Col md={6}>
              <Form.Group className="mb-3">
                <Form.Label>Suburb/Location</Form.Label>
                <Form.Control
                  type="text"
                  placeholder="Enter suburb or location"
                  value={filters.suburb || ''}
                  onChange={(e) => handleInputChange('suburb', e.target.value || undefined)}
                />
              </Form.Group>
            </Col>
            
            <Col md={6} className="d-flex align-items-end">
              <div className="d-flex gap-2 w-100">
                <Button
                  variant="primary"
                  type="submit"
                  disabled={loading}
                  className="flex-fill"
                >
                  {loading ? 'Searching...' : 'Search'}
                </Button>
                <Button
                  variant="outline-secondary"
                  onClick={handleClear}
                  disabled={loading}
                >
                  Clear
                </Button>
              </div>
            </Col>
          </Row>
        </Form>
      </Card.Body>
    </Card>
  );
};

export default PropertyFilter;
