using System;

namespace BarberShop.MVC.Models.UsersPage
{
    public class PageModel
    {
        public int PageNumber { get; private set; }
        public int TotalPages { get; private set; }

        public PageModel(int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling((double) (count / pageSize));
        }

        public bool HasPreviousPage => (PageNumber > 0);

        public bool HasNextPage => (PageNumber < TotalPages);
    }
}
