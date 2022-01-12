using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflection
{
    public interface Media<T>
    {
        public Media<T> MediaWith(T input);
        public string AsString();
    }
}
