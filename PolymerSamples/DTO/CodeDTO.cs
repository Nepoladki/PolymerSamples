namespace PolymerSamples.DTO
{
    public record class CodeDTO
        (
            Guid Id,
            string CodeIndex,
            string CodeName,
            string? LegacyCodeName,
            string? StockLevel,
            string? Note,
            int TypeId,
            int Layers,
            float Thickness
        );
}
