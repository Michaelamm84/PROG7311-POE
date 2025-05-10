﻿using Microsoft.EntityFrameworkCore;
using PROG_WEB_APP.DATA;
using PROG_WEB_APP.Models;


namespace PROG_WEB_APP.Services
{
    public class ProductService
    {
  
            private readonly AppDbContext _context;
            public ProductService (AppDbContext context)
            {
                _context = context;
            }

            public async Task<List<Product>> GetAllAsync() => await _context.Products.ToListAsync();

            public async Task AddAsync(Product product)
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
            }

            public async Task<Product?> GetByIdAsync(int id) => await _context.Products.FindAsync(id);

            public async Task UpdateAsync(Product product)
            {
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
            }

            public async Task DeleteAsync(int id)
            {
                var product = await _context.Products.FindAsync(id);
                if (product != null)
                {
                    _context.Products.Remove(product);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }

