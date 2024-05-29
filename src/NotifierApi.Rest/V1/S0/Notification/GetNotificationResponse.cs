﻿namespace NotifierApi.Rest.V1.S0
{
    public sealed record GetNotificationResponse
    (
        long Id,
        long ApplicationId,
        Guid Constant,
        Priority Priority,
        string Name,
        string? Comment,        
        Status Status,
        DateTime CreationTime,
        DateTime ModificationTime
    );
}
