﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Core.Helper
{
    public class PageResult<T>
    {
        public Pagination Pagination { get; set; }
        public IEnumerable<T> Data { get; set; }
        public PageResult() { }
        public PageResult(Pagination pagination, IEnumerable<T> data)
        {
            Pagination = pagination;
            Data = data;
        }
        public static IQueryable<T> ToPageResult(Pagination pagination, IQueryable<T> query)
        {
            pagination.PageNumber = pagination.PageNumber < 1 ? 1 : pagination.PageNumber;
            if (pagination.PageSize > 0)
            {
                query = query.Skip(pagination.PageSize * (pagination.PageNumber - 1))
                        .Take(pagination.PageSize);
            }
            return query;
        }
    }
}
