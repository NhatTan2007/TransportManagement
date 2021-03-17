using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportManagement.Entities;
using TransportManagement.DbContexts;
using TransportManagement.Models.VehicleBrand;
using TransportManagement.Services.IServices;

namespace TransportManagement.Services.ImplementServices
{
    public class VehicleBrandServices : IVehicleBrandServices
    {
        private readonly TransportDbContext _context;

        public VehicleBrandServices(TransportDbContext context)
        {
            _context = context;
        }

        public int CountBrands()
        {
            return _context.VehicleBrands.Count();
        }

        public async Task<bool> CreateBrand(VehicleBrand newBrand)
        {
            try
            {
                _context.VehicleBrands.Add(newBrand);
                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteBrand(VehicleBrand brand)
        {
            try
            {
                _context.VehicleBrands.Remove(brand);
                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> EditBrand(EditVehicleBrandViewModel model)
        {
            var brand = GetBrand(model.BrandId);
            if (brand != null)
            {
                try
                {
                    _context.VehicleBrands.Attach(brand);
                    brand.BrandName = model.BrandName;
                    var result = await _context.SaveChangesAsync();
                    return result > 0;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }

        public ICollection<VehicleBrandViewModel> GetAllBrands()
        {
            return _context.VehicleBrands.Select(b => new VehicleBrandViewModel
                                                    {
                                                        BrandId = b.BrandId,
                                                        BrandName = b.BrandName
                                                    }).ToList();
        }

        public ICollection<VehicleBrandViewModel> GetAllBrands(int page, int pageSize, string search)
        {
            return _context.VehicleBrands.Where(b => b.BrandName.Contains(search))
                                            .Skip((page - 1) * pageSize)
                                            .Take(pageSize)
                                            .Select(b => new VehicleBrandViewModel
                                                {
                                                    BrandId = b.BrandId,
                                                    BrandName = b.BrandName
                                                }).ToList();
        }

        public ICollection<VehicleBrandViewModel> GetAllBrands(int page, int pageSize)
        {
            return _context.VehicleBrands.Skip((page - 1) * pageSize)
                                            .Take(pageSize)
                                            .Select(b => new VehicleBrandViewModel
                                            {
                                                BrandId = b.BrandId,
                                                BrandName = b.BrandName
                                            }).ToList();
        }

        public VehicleBrand GetBrand(string brandId)
        {
            return _context.VehicleBrands.Where(b => b.BrandId == brandId).SingleOrDefault();
        }
    }
}
