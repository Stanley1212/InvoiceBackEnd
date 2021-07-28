using AutoMapper;
using Invoice.Core.Dtos;
using Invoice.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Invoice.Data
{
   public class MapperProfiles: Profile
    {
        public MapperProfiles()
        {
            #region Unit
            CreateMap<UnitCreateDto, Unit>()
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
                .ForMember(d => d.UserCreated, o => o.MapFrom(s => ""))
                .ForMember(d => d.CreatedDate, o => o.MapFrom(s => DateTime.UtcNow))
                .ForMember(d => d.Active, o => o.MapFrom(s => true));

            CreateMap<UnitUpdateDto, Unit>()
              .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
              .ForMember(d => d.UserUpdated, o => o.MapFrom(s => ""))
              .ForMember(d => d.UpdatedDate, o => o.MapFrom(s => DateTime.UtcNow))
              .ForMember(d => d.Active, o => o.MapFrom(s => true));

            CreateMap<UnitListDto, Unit>().ReverseMap();
            #endregion

            #region Supplier
            CreateMap<SupplierCreateDto, Supplier>()
                .ForMember(s => s.Name, f => f.MapFrom(o => o.Name))
                .ForMember(s => s.Phone, f => f.MapFrom(o => o.Phone))
                .ForMember(s => s.Address, f => f.MapFrom(o => o.Address))
                .ForMember(s => s.UserCreated, f => f.MapFrom(o => ""))
                .ForMember(s => s.CreatedDate, f => f.MapFrom(o => DateTime.UtcNow))
                .ForMember(s => s.Active, f => f.MapFrom(o => true));

            CreateMap<SupplierUpdateDto, Supplier>()
                .ForMember(s => s.Name, f => f.MapFrom(o => o.Name))
                .ForMember(s => s.Phone, f => f.MapFrom(o => o.Phone))
                .ForMember(s => s.Address, f => f.MapFrom(o => o.Address))
                .ForMember(s => s.UserUpdated, f => f.MapFrom(o => ""))
                .ForMember(s => s.UpdatedDate, f => f.MapFrom(o => DateTime.UtcNow))
                .ForMember(s => s.Active, f => f.MapFrom(o => true));

            #endregion

            #region Customer
            CreateMap<CustomerCreateDto, Customer>()
                .ForMember(s => s.Name, f => f.MapFrom(o => o.Name))
                .ForMember(s => s.Phone, f => f.MapFrom(o => o.Phone))
                .ForMember(s => s.Address, f => f.MapFrom(o => o.Address))
                .ForMember(s => s.UserCreated, f => f.MapFrom(o => ""))
                .ForMember(s => s.CreatedDate, f => f.MapFrom(o => DateTime.UtcNow))
                .ForMember(s => s.Active, f => f.MapFrom(o => true));

            CreateMap<CustomerUpdateDto, Customer>()
                .ForMember(s => s.Name, f => f.MapFrom(o => o.Name))
                .ForMember(s => s.Phone, f => f.MapFrom(o => o.Phone))
                .ForMember(s => s.Address, f => f.MapFrom(o => o.Address))
                .ForMember(s => s.UserUpdated, f => f.MapFrom(o => ""))
                .ForMember(s => s.UpdatedDate, f => f.MapFrom(o => DateTime.UtcNow))
                .ForMember(s => s.Active, f => f.MapFrom(o => true));
            #endregion

            #region Item
            CreateMap<ItemCreateDto, Item>()
                .ForMember(s => s.Name, f => f.MapFrom(o => o.Name))
                .ForMember(s => s.Stock, f => f.MapFrom(o => o.Stock))
                .ForMember(s => s.SalePrice, f => f.MapFrom(o => o.SalePrice))
                .ForMember(s => s.PurchasePrice, f => f.MapFrom(o => o.PurchasePrice))
                .ForMember(s => s.UnitID, f => f.MapFrom(o => o.UnitID))
                .ForMember(s => s.Type, f => f.MapFrom(o => o.Type))
                .ForMember(s => s.UserCreated, f => f.MapFrom(o => ""))
                .ForMember(s => s.CreatedDate, f => f.MapFrom(o => DateTime.UtcNow))
                .ForMember(s => s.Active, f => f.MapFrom(o => true));

            CreateMap<ItemUpdateDto, Item>()
                .ForMember(s => s.Name, f => f.MapFrom(o => o.Name))
                .ForMember(s => s.Stock, f => f.MapFrom(o => o.Stock))
                .ForMember(s => s.SalePrice, f => f.MapFrom(o => o.SalePrice))
                .ForMember(s => s.PurchasePrice, f => f.MapFrom(o => o.PurchasePrice))
                .ForMember(s => s.UnitID, f => f.MapFrom(o => o.UnitID))
                .ForMember(s => s.Type, f => f.MapFrom(o => o.Type))
                .ForMember(s => s.UserUpdated, f => f.MapFrom(o => ""))
                .ForMember(s => s.UpdatedDate, f => f.MapFrom(o => DateTime.UtcNow))
                .ForMember(s => s.Active, f => f.MapFrom(o => true));
            #endregion

            #region Invoice
            CreateMap<InvoiceHeaderCreateDto, InvoiceHeader>()
                .ForMember(s => s.CustomerID, f => f.MapFrom(o => o.CustomerID))
                .ForMember(s => s.Description, f => f.MapFrom(o => o.Description))
                .ForMember(s => s.Discount, f => f.MapFrom(o => o.Discount))
                .ForMember(s => s.Total, f => f.MapFrom(o => o.Total))
                .ForMember(s => s.UserCreated, f => f.MapFrom(o => ""))
                .ForMember(s => s.CreatedDate, f => f.MapFrom(o => DateTime.UtcNow))
                .ForMember(s => s.Active, f => f.MapFrom(o => true));

            CreateMap<InvoiceHeaderUpdateDto, InvoiceHeader>()
                .ForMember(s => s.CustomerID, f => f.MapFrom(o => o.CustomerID))
                .ForMember(s => s.Description, f => f.MapFrom(o => o.Description))
                .ForMember(s => s.Discount, f => f.MapFrom(o => o.Discount))
                .ForMember(s => s.Total, f => f.MapFrom(o => o.Total))
                .ForMember(s => s.UserUpdated, f => f.MapFrom(o => ""))
                .ForMember(s => s.UpdatedDate, f => f.MapFrom(o => DateTime.UtcNow))
                .ForMember(s => s.Active, f => f.MapFrom(o => true));

            CreateMap<InvoiceDetailCreateDto, InvoiceDetail>();
            #endregion

            #region Bill
            CreateMap<BillCreateDto, Bill>()
                .ForMember(s => s.SupplierID, f => f.MapFrom(o => o.SupplierID))
                .ForMember(s => s.Description, f => f.MapFrom(o => o.Description))
                .ForMember(s => s.Discount, f => f.MapFrom(o => o.Discount))
                .ForMember(s => s.Total, f => f.MapFrom(o => o.Total))
                .ForMember(s => s.UserCreated, f => f.MapFrom(o => ""))
                .ForMember(s => s.CreatedDate, f => f.MapFrom(o => DateTime.UtcNow))
                .ForMember(s => s.Active, f => f.MapFrom(o => true));

            CreateMap<BillUpdateDto, Bill>()
                .ForMember(s => s.SupplierID, f => f.MapFrom(o => o.SupplierID))
                .ForMember(s => s.Description, f => f.MapFrom(o => o.Description))
                .ForMember(s => s.Discount, f => f.MapFrom(o => o.Discount))
                .ForMember(s => s.Total, f => f.MapFrom(o => o.Total))
                .ForMember(s => s.UserUpdated, f => f.MapFrom(o => ""))
                .ForMember(s => s.UpdatedDate, f => f.MapFrom(o => DateTime.UtcNow))
                .ForMember(s => s.Active, f => f.MapFrom(o => true));

            CreateMap<BillDetailDto, BillDetail>();
            #endregion
        }

    }
}
