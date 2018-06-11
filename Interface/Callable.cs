using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Interface
{
    public interface Callable<T,R>
    {
        R call(T args);
    }
}