using mediAPI.Models;
using Newtonsoft.Json;

namespace mediAPI.Data
{
    public class Seed
    {
        private readonly MediDbContext _context;
        private readonly ILogger<Seed> _logger;

        public Seed(MediDbContext context, ILogger<Seed> logger)
        {
            _context = context;
            _logger = logger;
        }


        public async Task SeedMedicineAsync()
        {
            // Read JSON data
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "data", "medicine_data.json");
            var jsonData = await File.ReadAllTextAsync(filePath);
            var medicines = JsonConvert.DeserializeObject<List<Medicine>>(jsonData);

            // Check if any medicine exists before adding
            if (!_context.Medicines.Any())
            {
                _context.Medicines.AddRange(medicines!);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Successfully seeded medicine data");
            }
            _logger.LogInformation("Medicine db already seeded");
        }
    }
}
