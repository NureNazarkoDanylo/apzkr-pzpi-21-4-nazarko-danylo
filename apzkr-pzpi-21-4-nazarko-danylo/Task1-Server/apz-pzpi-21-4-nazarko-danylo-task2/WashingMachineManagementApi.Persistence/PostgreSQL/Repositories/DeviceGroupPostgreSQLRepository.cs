using WashingMachineManagementApi.Application.Common.IRepositories;
using WashingMachineManagementApi.Domain.Entities;

namespace WashingMachineManagementApi.Persistence.PostgreSQL.Repositories;

public sealed class DeviceGroupPostgreSQLRepository : BasePostgreSQLRepository<DeviceGroup, string>, IDeviceGroupRepository
{
    public DeviceGroupPostgreSQLRepository(ApplicationDbContext dbContext)
        : base(dbContext) { }
}
