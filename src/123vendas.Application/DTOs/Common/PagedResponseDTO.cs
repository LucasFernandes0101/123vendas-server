namespace _123vendas.Domain.Base;

public record PagedResponseDTO<T> where T : class
{
    public PagedResponseDTO(IEnumerable<T> data, int totalItems, int currentPage, int maxResult)
    {
        Data = data;
        TotalItems = totalItems;
        CurrentPage = currentPage;

        TotalPages = (int)Math.Ceiling(totalItems / (double)maxResult);
    }

    public IEnumerable<T>? Data { get; init; }
    public int TotalItems { get; init; }
    public int CurrentPage { get; init; }
    public int TotalPages { get; init; }
}