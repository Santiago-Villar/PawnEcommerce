using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Product;
using Service.Exception;


namespace Service.Filter.ConcreteFilter
{
    using Service.Product;

    public class PriceFilter : FilterTemplate
    {
        public override bool Match(Product product, IFilterCriteria filterCriteria)
        {
            if (!(filterCriteria is PriceFilterCriteria criteria))
            {
                throw new ModelException("Invalid Criteria type. Expected PriceFilterCriteria.");
            }

            return product.Price >= criteria.MinPrice && product.Price <= criteria.MaxPrice;
        }
    }
}

