namespace NotifierApi.Rest.V1.S0
{
    public sealed record TransformRequest
    (
        Guid Constant,
        Transport Transport,
        Lang Lang,
        object Payload
    );
}
