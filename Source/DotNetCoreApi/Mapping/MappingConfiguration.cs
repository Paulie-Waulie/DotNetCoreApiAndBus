namespace DotNetCoreApi.Mapping
{
    using AutoMapper;

    internal static class MappingConfiguration
    {
        internal static void Configure()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<DotNetCore.Contracts.Rest.Payment, Model.Payment>());
        }
    }
}