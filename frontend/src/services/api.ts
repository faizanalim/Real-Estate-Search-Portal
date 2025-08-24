import axios from 'axios';
import { AuthResponse, LoginRequest, RegisterRequest, PropertyListResponse, Property, FavoriteResponse, PropertyFilter } from '../types';

const API_BASE_URL = 'http://localhost:5019/api';

// Create axios instance
const api = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});

// Add request interceptor to include JWT token
api.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem('token');
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

// Add response interceptor to handle errors
api.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error.response?.status === 401) {
      localStorage.removeItem('token');
      localStorage.removeItem('user');
      window.location.href = '/login';
    }
    return Promise.reject(error);
  }
);

// Auth API
export const authAPI = {
  register: async (data: RegisterRequest): Promise<AuthResponse> => {
    const response = await api.post('/auth/register', data);
    return response.data;
  },

  login: async (data: LoginRequest): Promise<AuthResponse> => {
    const response = await api.post('/auth/login', data);
    return response.data;
  },
};

// Properties API
export const propertiesAPI = {
  getProperties: async (filters: PropertyFilter): Promise<PropertyListResponse> => {
    const params = new URLSearchParams();

    if (filters.minPrice !== undefined) params.append('minPrice', filters.minPrice.toString());
    if (filters.maxPrice !== undefined) params.append('maxPrice', filters.maxPrice.toString());
    if (filters.minBedrooms !== undefined) params.append('minBedrooms', filters.minBedrooms.toString());
    if (filters.maxBedrooms !== undefined) params.append('maxBedrooms', filters.maxBedrooms.toString());
    if (filters.suburb) params.append('suburb', filters.suburb);
    if (filters.listingType !== undefined) params.append('listingType', filters.listingType.toString());
    params.append('page', filters.page.toString());
    params.append('pageSize', filters.pageSize.toString());

    const response = await api.get(`/properties?${params.toString()}`);
    return response.data;
  },

  getProperty: async (id: number): Promise<Property> => {
    const response = await api.get(`/properties/${id}`);
    return response.data;
  },
};

// Favorites API
export const favoritesAPI = {
  toggleFavorite: async (propertyId: number): Promise<FavoriteResponse> => {
    const response = await api.post(`/favorites/${propertyId}`);
    return response.data;
  },

  getFavorites: async (): Promise<Property[]> => {
    const response = await api.get('/favorites');
    return response.data;
  },
};

export default api;
