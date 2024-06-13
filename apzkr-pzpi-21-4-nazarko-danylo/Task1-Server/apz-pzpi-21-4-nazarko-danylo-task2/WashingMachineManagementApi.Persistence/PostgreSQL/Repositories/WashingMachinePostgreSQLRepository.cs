using WashingMachineManagementApi.Application.Common.IRepositories;
using WashingMachineManagementApi.Domain.Entities;

namespace WashingMachineManagementApi.Persistence.PostgreSQL.Repositories;

public sealed class WashingMachinePostgreSQLRepository : BasePostgreSQLRepository<WashingMachine, string>, IWashingMachineRepository
{
    public WashingMachinePostgreSQLRepository(ApplicationDbContext dbContext)
        : base(dbContext) { }
}
