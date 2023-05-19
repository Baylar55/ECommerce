namespace EcommerceAPI.Application.RequestParameters
{
    public record Pagination
    {
        public int Page { get; set; }
        public int Size { get; set; }
    }
}
