export interface User {
  id: number;
  email: string;
}

export interface AuthResponse {
  token: string;
  email: string;
  userId: number;
}

export interface LoginRequest {
  email: string;
  password: string;
}

export interface RegisterRequest {
  email: string;
  password: string;
}

export interface Property {
  id: number;
  title: string;
  address: string;
  price: number;
  listingType: ListingType;
  bedrooms: number;
  bathrooms: number;
  carSpots: number;
  description: string;
  imageUrls: string[];
  isFavorite: boolean;
}

export enum ListingType {
  Rent = 0,
  Sale = 1
}

export interface PropertyFilter {
  minPrice?: number;
  maxPrice?: number;
  minBedrooms?: number;
  maxBedrooms?: number;
  suburb?: string;
  listingType?: ListingType;
  page: number;
  pageSize: number;
}

export interface PropertyListResponse {
  properties: Property[];
  totalCount: number;
  page: number;
  pageSize: number;
  totalPages: number;
}

export interface FavoriteResponse {
  isFavorite: boolean;
}
