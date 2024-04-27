namespace Shop.API.MyHelper
{
    public class Pagination<T> where T : class
    {
        public Pagination(int pageSize, int pageNumber, int pageCount, IReadOnlyList<T> data)
        {
            try
            {
                PageSize = pageSize;
                PageNumber = pageNumber;
                PageCount = pageCount;
                Data = data;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al crear una instancia de Paginación: {ex.Message}");
            }
        }

        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int PageCount { get; set; }
        public IReadOnlyList<T> Data { get; set; }
    }
}
