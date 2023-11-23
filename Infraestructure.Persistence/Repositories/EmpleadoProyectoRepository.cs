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
    public class EmpleadoProyectoRepository : GenericRepository<EmpleadoProyecto>, IEmpleadoProyectoRepository
    {
        private readonly PersistenceContext _dbContext;

        public EmpleadoProyectoRepository(PersistenceContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
