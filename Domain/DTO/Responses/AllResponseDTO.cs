namespace UserService.Domain.DTOs.Responses
{
    public class AllResponseDTO<T>(List<T> values)
    {
        public IEnumerable<T> Users { get; set; } = values;
        public int TotalItems { get; set; } = values.Count;
    }
}
