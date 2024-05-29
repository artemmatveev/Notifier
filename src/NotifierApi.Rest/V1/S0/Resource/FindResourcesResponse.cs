namespace NotifierApi.Rest.V1.S0
{
    public sealed record FindResourcesResponse
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
