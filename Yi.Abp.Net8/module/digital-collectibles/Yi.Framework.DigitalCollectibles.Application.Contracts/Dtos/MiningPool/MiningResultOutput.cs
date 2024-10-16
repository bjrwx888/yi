using Yi.Framework.DigitalCollectibles.Application.Contracts.Dtos.Collectibles;
using Yi.Framework.DigitalCollectibles.Domain.Shared.Enums;

namespace Yi.Framework.DigitalCollectibles.Application.Contracts.Dtos.MiningPool;

public class MiningResultOutput
{
    
    public CollectiblesUserGetOutputDto? Collectibles { get; set; }
}