using BonsaiShop_API.DALL.Repositories;

namespace BonsaiShop_API.DALL.RepositoriesImplement
{
    public class OrderReponsitory 
    {
       public BonsaiDbcontext dbcontext;
        public OrderReponsitory(BonsaiDbcontext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

    }
}
