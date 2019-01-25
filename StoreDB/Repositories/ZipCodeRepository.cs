using StoreDB.Interface;
using StoreDB.Model.Partials;

namespace StoreDB.Repositories
{
    public class ZipCodeRepository : Repository<ZipCode>
    {
        private readonly IRepository<ZipCode> _ZipCodeRep;

        public ZipCodeRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _ZipCodeRep = new Repository<ZipCode>(unitOfWork);
        }
    }
}