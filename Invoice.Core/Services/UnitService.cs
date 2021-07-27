using Invoice.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Core.Services
{
    public class UnitService : BaseService<Unit>
    {
        public UnitService(IRepository<Unit> repository, IMapperExtension mapperExtension) 
            : base(repository, mapperExtension)
        {
        }

        public override Task<int> Add(Unit data)
        {

            var validate = _repository.Get(x => x.Name.Equals(data.Name)).FirstOrDefault();

            if (!(validate is null))
            {
                throw new AppException(MessageCode.GeneralException, $"Ya existe un registro con el nombre {data.Name}");
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

        public override IEnumerable<Unit> GetAll()
        {
            return _repository.Get();
        }

        public override Unit GetByID(object id)
        {
            var result = _repository.Get(x => x.ID.Equals(id)).FirstOrDefault();

            if (result is null)
            {
                throw new AppException(MessageCode.ResourceNotFound, $"No existe un registro con el ID {id}");
            }

            return result;

        }

        public override Task<int> Update(Unit data)
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
