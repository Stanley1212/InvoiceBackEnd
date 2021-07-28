using Invoice.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Core.Services
{
    public class ItemService : BaseService<Item>
    {
        public ItemService(IRepository<Item> repository, IMapperExtension mapperExtension)
            : base(repository, mapperExtension)
        {
        }

        public override Task<int> Add(Item data)
        {

            var validate = _repository.Get(x => x.Name.Equals(data.Name)).FirstOrDefault();

            if (!(validate is null))
            {
                throw new AppException(MessageCode.GeneralException, $"Ya existe un registro con el nombre {data.Name}");
            }

            if (data.UnitID < 1)
            {
                throw new AppException(MessageCode.RequieredField, $"Debe seleccionar una unidad de medida");
            }

            if (data.Type < 1 || data.Type > 2)
            {
                throw new AppException(MessageCode.RequieredField, $"Debe seleccionar un tipo de producto");
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

        public override IEnumerable<Item> GetAll()
        {
            return _repository.Get(x=>x.Active,x=>x.OrderBy(o=>o.ID),x=>x.Include(x=>x.Unit));
        }

        public override Item GetByID(object id)
        {
            var result = _repository.Get(x => x.ID.Equals(id)).FirstOrDefault();

            if (result is null)
            {
                throw new AppException(MessageCode.ResourceNotFound, $"No existe un registro con el ID {id}");
            }

            return result;
        }

        public override Task<int> Update(Item data)
        {
            var validate = _repository.Get(x => x.Name.Equals(data)).FirstOrDefault();

            if (!(validate is null) && data.ID != validate.ID)
            {
                throw new AppException(MessageCode.GeneralException, $"Ya existe un registro con el nombre {data.Name}");
            }

            _repository.Update(data);
            return _repository.Commit();
        }
    }
}
