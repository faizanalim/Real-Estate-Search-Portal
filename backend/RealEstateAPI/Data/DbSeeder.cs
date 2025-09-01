using Microsoft.EntityFrameworkCore;
using RealEstateAPI.Models;
using System.Text.Json;

namespace RealEstateAPI.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(RealEstateDbContext context)
        {
            // Check if data already exists
            if (await context.Properties.AnyAsync())
            {
                return;
            }

            var properties = new List<Property>
            {
                new Property
                {
                    Title = "Modern 3-Bedroom Apartment in CBD",
                    Address = "123 Collins Street, Melbourne CBD",
                    Price = 850000,
                    ListingType = ListingType.Sale,
                    Bedrooms = 3,
                    Bathrooms = 2,
                    CarSpots = 1,
                    Description = "Beautiful modern apartment in the heart of Melbourne CBD. Features include open plan living, modern kitchen, and stunning city views.",
                    ImageUrls = JsonSerializer.Serialize(new List<string> 
                    { 
                        "https://images.unsplash.com/photo-1545324418-cc1a3fa10c00?w=800",
                        "https://images.unsplash.com/photo-1560448204-e02f11c3d0e2?w=800"
                    }),


                },
                new Property
                {
                    Title = "Cozy 2-Bedroom Unit in St Kilda",
                    Address = "456 Acland Street, St Kilda",
                    Price = 2200,
                    ListingType = ListingType.Rent,
                    Bedrooms = 2,
                    Bathrooms = 1,
                    CarSpots = 0,
                    Description = "Charming unit in the vibrant St Kilda area. Close to beach, restaurants, and public transport.",
                    ImageUrls = JsonSerializer.Serialize(new List<string> 
                    { 
                        "https://images.unsplash.com/photo-1522708323590-d24dbb6b0267?w=800",
                        "https://images.unsplash.com/photo-1502672260266-1c1ef2d93688?w=800"
                    }),

                },
                new Property
                {
                    Title = "Family Home in Brighton",
                    Address = "789 Bay Street, Brighton",
                    Price = 1200000,
                    ListingType = ListingType.Sale,
                    Bedrooms = 4,
                    Bathrooms = 3,
                    CarSpots = 2,
                    Description = "Spacious family home in the prestigious Brighton area. Features large backyard, modern kitchen, and multiple living areas.",
                    ImageUrls = JsonSerializer.Serialize(new List<string> 
                    { 
                        "https://images.unsplash.com/photo-1564013799919-ab600027ffc6?w=800",
                        "https://images.unsplash.com/photo-1570129477492-45c003edd2be?w=800"
                    }),

                },
                new Property
                {
                    Title = "Luxury Penthouse in South Yarra",
                    Address = "321 Toorak Road, South Yarra",
                    Price = 3500,
                    ListingType = ListingType.Rent,
                    Bedrooms = 3,
                    Bathrooms = 2,
                    CarSpots = 2,
                    Description = "Exclusive penthouse with panoramic city views. Features high-end finishes, private balcony, and secure parking.",
                    ImageUrls = JsonSerializer.Serialize(new List<string> 
                    { 
                        "https://images.unsplash.com/photo-1502005229762-cf1b2da7c5d6?w=800",
                        "https://images.unsplash.com/photo-1486406146926-c627a92ad1ab?w=800"
                    }),

                },
                new Property
                {
                    Title = "Investment Property in Footscray",
                    Address = "654 Nicholson Street, Footscray",
                    Price = 650000,
                    ListingType = ListingType.Sale,
                    Bedrooms = 2,
                    Bathrooms = 1,
                    CarSpots = 1,
                    Description = "Great investment opportunity in the up-and-coming Footscray area. Close to universities and public transport.",
                    ImageUrls = JsonSerializer.Serialize(new List<string> 
                    { 
                        "https://images.unsplash.com/photo-1560448204-603b3fc33ddc?w=800",
                        "https://images.unsplash.com/photo-1560448075-bb485b067938?w=800"
                    }),

                },
                new Property
                {
                    Title = "Studio Apartment in Fitzroy",
                    Address = "987 Brunswick Street, Fitzroy",
                    Price = 1800,
                    ListingType = ListingType.Rent,
                    Bedrooms = 1,
                    Bathrooms = 1,
                    CarSpots = 0,
                    Description = "Charming studio apartment in the trendy Fitzroy area. Perfect for young professionals.",
                    ImageUrls = JsonSerializer.Serialize(new List<string> 
                    { 
                        "https://images.unsplash.com/photo-1522708323590-d24dbb6b0267?w=800",
                        "https://images.unsplash.com/photo-1502672260266-1c1ef2d93688?w=800"
                    }),

                }
            };

            await context.Properties.AddRangeAsync(properties);
            await context.SaveChangesAsync();
        }
    }
}
