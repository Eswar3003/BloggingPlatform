using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Common
{
    public abstract class InputParameters
    {

        private const int _maxPageSize = 100;

        private const int _maxDownloadPageSize = 1000;

        [DefaultValue(1)]
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;

        [DefaultValue(10)]
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > _maxPageSize) ? _maxPageSize : value;
            }
        }

        [JsonIgnore]
        public static int DownloadPageSize => _maxDownloadPageSize;

        [DefaultValue(Sorting.DefaultOrderBy)]
        public string OrderBy { get; set; } = Sorting.DefaultOrderBy;
    }
}
