using System;
using System.Collections.Generic;
using System.Text;

namespace Invoice.Core
{
    public interface IMapperExtension
    {
        D Map<S, D>(S data) where S : class where D : class;
        D Map<S, D>(S data, D dataDestination) where S : class where D : class;
    }
}
