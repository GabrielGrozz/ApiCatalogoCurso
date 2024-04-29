﻿namespace ApiCatalogo.Pagination
{
    public class ProductParameters
    {
        const int maxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int _pageSize;
        //propriedade que irá retornar o valor de _pageSize, caso vc tente setar um valor maior que o maximo, ele seta o maximo
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize= (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }
}
