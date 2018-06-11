using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Interface
{
    public interface Runner<T,V,R>
    {
        R Stop(V id);

        R Run(T config);
    }
}