using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab5.Models;

namespace Lab5
{
    public class AreaValid
    {
        MakeUpContext _context;
        Area area;
        public AreaValid(MakeUpContext context, Area area)
        {
            _context = context;
            this.area = area;
        }

        public bool Valid()
        {
            var areas = _context.Areas.Where(a => a.ApplicationArea == area.ApplicationArea).ToList();
            if (areas.Count != 0) return false;
            else return true;
        }
    }

    public class CountryValid
    {
        MakeUpContext _context;
        Country country;
        public CountryValid(MakeUpContext context, Country country)
        {
            _context = context;
            this.country  = country;
        }

        public bool Valid()
        {
            var countries = _context.Countries.Where(a => a.Name == country.Name).ToList();
            if (countries.Count != 0) if (countries.Count == 1) { if (countries[0].Id == country.Id) return true; else return false; } else return false;
            else return true;
        }
    }

    public class ColorValid
    {
        MakeUpContext _context;
        _Color color;
        public ColorValid(MakeUpContext context, _Color color)
        {
            _context = context;
            this.color = color;
        }

        public bool Valid()
        {
            var colors = _context.Colors.Where(a => a.Name == color.Name).ToList();
            if (colors.Count != 0) if (colors.Count == 1) { if (colors[0].Id == color.Id) return true; else return false; } else return false;
            else return true;
        }
    }

    public class CosmeticValid
    {
        MakeUpContext _context;
        Cosmetic cosmetic;
        public CosmeticValid(MakeUpContext context, Cosmetic cosmetic)
        {
            _context = context;
            this.cosmetic = cosmetic;
        }

        public bool Valid()
        {
            var cosmetics = _context.Cosmetics.Where(a => a.Name == cosmetic.Name).ToList();
            if (cosmetics.Count != 0) if (cosmetics.Count == 1) { if (cosmetics[0].Id == cosmetic.Id) return true; else return false; } else return false;
            else return true;
        }
    }

    public class FirmValid
    {
        MakeUpContext _context;
        Firm color;
        public FirmValid(MakeUpContext context, Firm color)
        {
            _context = context;
            this.color = color;
        }

        public bool Valid()
        {
            var colors = _context.Firms.Where(a => a.Name == color.Name).ToList();
            if (colors.Count != 0) if (colors.Count == 1) { if (colors[0].Id == color.Id) return true; else return false; } else return false;
            else return true;
        }
    }

    public class ProductValid
    {
        MakeUpContext _context;
        Product color;
        public ProductValid(MakeUpContext context, Product color)
        {
            _context = context;
            this.color = color;
        }

        public bool Valid()
        {
            var colors = _context.Products.Where(a => a.Id_Cosmetics == color.Id_Cosmetics && a.Id_Firm == color.Id_Firm).ToList();
            if (colors.Count != 0) if (colors.Count == 1) { if (colors[0].Id == color.Id) return true; else return false; } else return false;
            else return true;
        }
    }

    public class ProductColorValid
    {
        MakeUpContext _context;
        ProductColor color;
        public ProductColorValid(MakeUpContext context, ProductColor color)
        {
            _context = context;
            this.color = color;
        }

        public bool Valid()
        {
            var colors = _context.ProductColors.Where(a => a.Id_Color == color.Id_Color && a.Id_Product == color.Id_Color).ToList();
            if (colors.Count != 0) if (colors.Count == 1) { if (colors[0].Id == color.Id) return true; else return false; } else return false;
            else return true;
        }
    }
}
