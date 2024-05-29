﻿namespace NotifierApi.Rest.V1.S0
{
    public sealed record GetChannelResponse
    (
        long Id,
        string Name,
        Transport Transport,
        string Data,        
        Status Status,
        DateTime CreationTime,
        DateTime ModificationTime
    );
}
