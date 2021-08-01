using Invoice.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Core.Services
{
    public class ProductionService : BaseService<Production>
    {
        private readonly IRepository<Item> _itemService;

        public ProductionService(IRepository<Production> repository, IMapperExtension mapperExtension,
            IRepository<Item> itemService) : base(repository, mapperExtension)
        {
            this._itemService = itemService;
        }

        public async override Task<int> Add(Production data)
        {
            if (data.ItemID < 1)
            {
                throw new AppException(MessageCode.RequieredField, "Debe seleccionar un cliente.");
            }

            var PrincipalItem = _itemService.Get(x => x.ID.Equals(data.ItemID)).FirstOrDefault();
            PrincipalItem.Stock = PrincipalItem.Stock + data.Quantity;
            _itemService.Update(PrincipalItem);

            foreach (var productionDetail in data.ProductionDetails)
            {
                var item = _itemService.Get(x => x.ID.Equals(productionDetail.ItemID)).FirstOrDefault();

                if (item is null)
                {
                    throw new AppException(MessageCode.GeneralException, $"EL articulo ID {productionDetail.ItemID} no Existe");
                }

                if (item.Stock < productionDetail.Quantity)
                {
                    throw new AppException(MessageCode.GeneralException, $"EL articulo {item.Name} no posee cantidad suficiente");
                }

                item.Stock = item.Stock - productionDetail.Quantity;

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

        public override IEnumerable<Production> GetAll()
        {
            return _repository.Get(x => x.Active, x => x.OrderBy(x => x.ID), x => x.Include(x => x.Item).Include(x => x.ProductionDetails).ThenInclude(x => x.Item).ThenInclude(x => x.Unit));
        }

        public override Production GetByID(object id)
        {
            var result = _repository.Get(x => x.ID.Equals(id),null, x => x.Include(x => x.Item).Include(x => x.ProductionDetails).ThenInclude(x => x.Item).ThenInclude(x => x.Unit)).FirstOrDefault();

            if (result is null)
            {
                throw new AppException(MessageCode.ResourceNotFound, $"No existe un registro con el ID {id}");
            }

            return result;
        }

        public override Task<int> Update(Production data)
        {
            if (data.ItemID < 1)
            {
                throw new AppException(MessageCode.RequieredField, "Debe seleccionar un producto.");
            }

            _repository.Update(data);
            return _repository.Commit();
        }
    }
}
