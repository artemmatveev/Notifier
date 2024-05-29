namespace NotifierApi.Rest.V1.S0
{
    public sealed record GetResourceResponse
    (
        long Id,
        int StaffId,
        string Name,
        string? Email,
        string? Username,
        long? ChatId,
        DateTime CreationTime,
        DateTime ModificationTime
    );
}
