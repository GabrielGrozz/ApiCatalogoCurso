using System.Net.NetworkInformation;

namespace ApiCatalogo.Pagination
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }

        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;

        public PagedList(List<T> items, int currentPage, int pageSize, int totalCount)
        {
            TotalCount = totalCount;
            PageSize = pageSize;
            CurrentPage = currentPage;
            TotalPages = (int)Math.Ceiling(totalCount / (double)PageSize);

            AddRange(items);
        }

        //retorna uma instancia de pagedlist
        public static PagedList<T> ToPagedList(IQueryable<T> source, int pageNumber, int pageSize) 
        {
            var count = source.Count();
            //pula os items anteriores e pega os próximos
            var items = source.Skip((pageNumber - 1 ) * pageSize).Take(pageSize).ToList();
            
            //retorna uma instacia de pagedlist com os valores setados
            return new PagedList<T>(items, pageNumber, pageSize, count);
        }
    }
}
