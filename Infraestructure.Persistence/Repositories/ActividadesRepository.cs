using Core.Application.Interface.Repository;
using Core.Domain.Entities;
using Infraestructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Persistence.Repositories
{
    public class ActividadesRepository : GenericRepository<Actividades>, IActividadesRepository
    {
        private readonly PersistenceContext _dbContext;

        public ActividadesRepository(PersistenceContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
