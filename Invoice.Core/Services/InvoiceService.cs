using Invoice.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invoice.Core.Services
{
    public class InvoiceService : BaseService<InvoiceHeader>
    {
        private readonly IRepository<Item> _itemService;

        public InvoiceService(IRepository<InvoiceHeader> repository, IMapperExtension mapperExtension,
            IRepository<Item> itemService) : base(repository, mapperExtension)
        {
            this._itemService = itemService;
        }

        public async override Task<int> Add(InvoiceHeader data)
        {
                if (data.CustomerID < 1)
                {
                    throw new AppException(MessageCode.RequieredField, "Debe seleccionar un cliente.");
                }

                foreach (var InvoiceDetail in data.InvoiceDetails)
                {
                    var item = _itemService.Get(x => x.ID.Equals(InvoiceDetail.ItemID)).FirstOrDefault();

                    if (item is null)
                    {
                        throw new AppException(MessageCode.GeneralException, $"EL articulo ID {InvoiceDetail.ItemID} no Existe");
                    }

                    if (item.Stock < InvoiceDetail.Quantity)
                    {
                        throw new AppException(MessageCode.GeneralException, $"EL articulo {item.Name} no posee cantidad suficiente");
                    }

                    item.Stock = item.Stock - InvoiceDetail.Quantity;

                    _itemService.Update(item);
                }

               await _itemService.Commit();

                _repository.Insert(data);
                return await _repository.Commit();
           
        }

        public override Task<int> Delete(object id)
        {
            var result = GetByID(id);
            result.Active = !result.Active;
            return Update(result);
        }

        public override IEnumerable<InvoiceHeader> GetAll()
        {
            return _repository.Get(x => x.Active, x => x.OrderBy(x => x.ID),x=> x.Include(x=>x.Customer).Include(x=>x.InvoiceDetails).ThenInclude(x=>x.Item).ThenInclude(x=>x.Unit));
        }

        public override InvoiceHeader GetByID(object id)
        {
            var result = _repository.Get(x => x.ID.Equals(id)).FirstOrDefault();

            if (result is null)
            {
                throw new AppException(MessageCode.ResourceNotFound, $"No existe un registro con el ID {id}");
            }

            return result;
        }

        public override Task<int> Update(InvoiceHeader data)
        {
            if (data.CustomerID < 1)
            {
                throw new AppException(MessageCode.RequieredField, "Debe seleccionar un cliente.");
            }

            _repository.Update(data);
            return _repository.Commit();
        }
    }
}
