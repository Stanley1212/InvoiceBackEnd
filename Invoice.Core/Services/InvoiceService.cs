using Invoice.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invoice.Core.Services
{
    public class InvoiceService : BaseService<InvoiceHeader>
    {
        public InvoiceService(IRepository<InvoiceHeader> repository, IMapperExtension mapperExtension) : base(repository, mapperExtension)
        {
        }

        public override Task<int> Add(InvoiceHeader data)
        {

            if (data.CustomerID < 1)
            {
                throw new AppException(MessageCode.RequieredField, "Debe seleccionar un cliente.");
            }

            foreach (var InvoiceDetail in data.InvoiceDetails)
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
