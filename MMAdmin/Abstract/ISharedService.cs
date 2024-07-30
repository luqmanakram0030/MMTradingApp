using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMAdmin.Abstract
{
    public interface ISharedService
    {
        void Add<T>(string key, T value) where T : class;
        T GetValue<T>(string key) where T : class;
    }
}
