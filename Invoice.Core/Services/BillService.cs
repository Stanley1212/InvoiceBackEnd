using Invoice.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Core.Services
{
    public class BillService : BaseService<Bill>
    {

        private readonly IRepository<Item> _itemService;

        public BillService(IRepository<Bill> repository, IMapperExtension mapperExtension,
            IRepository<Item> itemService) : base(repository, mapperExtension)
        {
            this._itemService = itemService;
        }

        public async override Task<int> Add(Bill data)
        {

            if (data.SupplierID < 1)
            {
                throw new AppException(MessageCode.RequieredField, "Debe seleccionar un suplidor.");
            }

            foreach (var BillDetail in data.BillDetails)
            {
                var item = _itemService.Get(x => x.ID.Equals(BillDetail.ItemID)).FirstOrDefault();

                if (item is null)
                {
                    throw new AppException(MessageCode.GeneralException, $"EL articulo ID {BillDetail.ItemID} no Existe");
                }

                item.Stock = item.Stock + BillDetail.Quantity;

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

        public override IEnumerable<Bill> GetAll()
        {
            return _repository.Get(x => x.Active, x => x.OrderBy(x => x.ID), x => x.Include(x => x.Supplier).Include(x => x.BillDetails).ThenInclude(x => x.Item).ThenInclude(x => x.Unit));
        }

        public override Bill GetByID(object id)
        {
            var result = _repository.Get(x => x.ID.Equals(id),null, x => x.Include(x => x.Supplier).Include(x => x.BillDetails).ThenInclude(x => x.Item).ThenInclude(x => x.Unit)).FirstOrDefault();

            if (result is null)
            {
                throw new AppException(MessageCode.ResourceNotFound, $"No existe un registro con el ID {id}");
            }

            return result;
        }

        public override Task<int> Update(Bill data)
        {
            if (data.SupplierID < 1)
            {
                throw new AppException(MessageCode.RequieredField, "Debe seleccionar un suplidor.");
            }

            _repository.Update(data);
            return _repository.Commit();
        }
    }
}
