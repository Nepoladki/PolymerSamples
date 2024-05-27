namespace PolymerSamples.DTO
{
    public record class CodeDTO
        (
            Guid id,
            string short_code_name,
            string code_name,
            string? supplier_code_name,
            string? stock_level,
            string? note,
            int? type_id,
            int? layers,
            float? thickness
        );
}
