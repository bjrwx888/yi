using System;
using Volo.Abp.Auditing;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;
using Yi.Framework.AuditLogging.Domain.Shared.Consts;

namespace Yi.Framework.AuditLogging.Domain.Entities;

[DisableAuditing]
public class AuditLogActionEntity : Entity<Guid>, IMultiTenant
{
    public virtual Guid? TenantId { get; protected set; }

    public virtual Guid AuditLogId { get; protected set; }

    public virtual string ServiceName { get; protected set; }

    public virtual string MethodName { get; protected set; }

    public virtual string Parameters { get; protected set; }

    public virtual DateTime ExecutionTime { get; protected set; }

    public virtual int ExecutionDuration { get; protected set; }


    protected AuditLogActionEntity()
    {
    }

    public AuditLogActionEntity(Guid id, Guid auditLogId, AuditLogActionInfo actionInfo, Guid? tenantId = null)
    {

        Id = id;
        TenantId = tenantId;
        AuditLogId = auditLogId;
        ExecutionTime = actionInfo.ExecutionTime;
        ExecutionDuration = actionInfo.ExecutionDuration;

        ServiceName = actionInfo.ServiceName.TruncateFromBeginning(AuditLogActionConsts.MaxServiceNameLength);
        MethodName = actionInfo.MethodName.TruncateFromBeginning(AuditLogActionConsts.MaxMethodNameLength);
        Parameters = actionInfo.Parameters.Length > AuditLogActionConsts.MaxParametersLength ? "" : actionInfo.Parameters;
    }
}
