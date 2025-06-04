namespace UserService.Domain.DTOs.Responses
{
    public class PaginatedResponseDTO<T>(
        List<T> values,
        int totalItems,
        int pageNumber,
        int pageSize
    )
    {
        public int PageNumber { get; set; } = pageNumber;
        public int PageSize { get; set; } = pageSize;
        public IEnumerable<T> Users { get; set; } = values;
        public int TotalItems { get; set; } = totalItems;
        public int TotalPages { get; set; } = (int)Math.Ceiling(totalItems / (double)pageSize);
    }
}
