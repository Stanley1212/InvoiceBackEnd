using Invoice.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Core.Services
{
   public class CustomerService : BaseService<Customer>
    {
        public CustomerService(IRepository<Customer> repository, IMapperExtension mapperExtension) : base(repository, mapperExtension)
        {
        }

        public override Task<int> Add(Customer data)
        {

            if (string.IsNullOrEmpty(data.Name))
            {
                throw new AppException(MessageCode.RequieredField, "El nombre es obligatorio.");
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

        public override IEnumerable<Customer> GetAll()
        {
            return _repository.Get(x=>x.Active,x=>x.OrderBy(x=>x.ID));
        }

        public override Customer GetByID(object id)
        {
            var result = _repository.Get(x => x.ID.Equals(id)).FirstOrDefault();

            if (result is null)
            {
                throw new AppException(MessageCode.ResourceNotFound, $"No existe un registro con el ID {id}");
            }

            return result;
        }

        public override Task<int> Update(Customer data)
        {
            if (string.IsNullOrEmpty(data.Name))
            {
                throw new AppException(MessageCode.RequieredField, "El nombre es obligatorio.");
            }

            _repository.Update(data);
            return _repository.Commit();
        }
    }
}
