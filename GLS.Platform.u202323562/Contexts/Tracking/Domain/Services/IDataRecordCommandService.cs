using GLS.Platform.u202323562.Contexts.Tracking.Domain.Commands;
using GLS.Platform.u202323562.Contexts.Tracking.Domain.Model.Aggregates;

namespace GLS.Platform.u202323562.Contexts.Tracking.Domain.Services;

public interface IDataRecordCommandService
{
    Task<DataRecord> Handle(CreateDataRecordCommand command);
}