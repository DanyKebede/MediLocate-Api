namespace mediAPI.Dtos.Medicine
{
    public class MedicineDto
    {
        public Guid MedicineId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string SideEffects { get; set; } = string.Empty;
        public string Precautions { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
    }
}
