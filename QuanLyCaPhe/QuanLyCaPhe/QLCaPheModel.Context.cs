﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLyCaPhe
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class QuanLyCaPheEntities : DbContext
    {
        public QuanLyCaPheEntities()
            : base("name=QuanLyCaPheEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<BanAn> BanAns { get; set; }
        public DbSet<ChamCong> ChamCongs { get; set; }
        public DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        public DbSet<DangNhap> DangNhaps { get; set; }
        public DbSet<HoaDon> HoaDons { get; set; }
        public DbSet<KhachHang> KhachHangs { get; set; }
        public DbSet<LoaiThucAn> LoaiThucAns { get; set; }
        public DbSet<NhanVien> NhanViens { get; set; }
        public DbSet<ThanhPho> ThanhPhoes { get; set; }
        public DbSet<ThucAn> ThucAns { get; set; }
        public DbSet<TinhLuong> TinhLuongs { get; set; }
    }
}