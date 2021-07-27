using AutoMapper;
using Invoice.Core;

namespace Invoice.Data
{
    public class MapperExtension : IMapperExtension
    {
        private readonly IMapper _mapper;

        public MapperExtension(IMapper mapper)
        {
            this._mapper = mapper;
        }
        public D Map<S, D>(S data)
            where S : class
            where D : class
        {
            return _mapper.Map<S, D>(data);
        }
        public D Map<S, D>(S data, D dataDestination)
            where S : class
            where D : class
        {
            return _mapper.Map(data, dataDestination);
        }
    }
}
