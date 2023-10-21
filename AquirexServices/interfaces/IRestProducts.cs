using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcquireXModel;

namespace AquirexServices.interfaces
{
    internal interface IRestProducts
    {
        Task<Products> GetProducts(string endpoint);
    }
}
