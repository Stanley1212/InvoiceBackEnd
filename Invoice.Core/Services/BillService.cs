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
        public BillService(IRepository<Bill> repository, IMapperExtension mapperExtension) : base(repository, mapperExtension)
        {
        }

        public override Task<int> Add(Bill data)
        {

            if (data.SupplierID < 1)
            {
                throw new AppException(MessageCode.RequieredField, "Debe seleccionar un suplidor.");
            }

            foreach (var BillDetail in data.BillDetails)
            {
                //InvoiceDetail.InvoiceID = data.ID;
            }

            _repository.Insert(data);
            return _repository.Commit();
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
            var result = _repository.Get(x => x.ID.Equals(id)).FirstOrDefault();

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
