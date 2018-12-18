using System.Collections.Generic;

namespace TeduNetcore.Utilities.Dtos
{
    public class PageResult<T> : PageResultBase where T : class
    {
        public IList<T> Result { get; set; }
        public PageResult()
        {
            Result = new List<T>();
        }

    }
}
