using CleanArch.Application.Common.Mappings;

namespace CleanArch.Application.Common.Model
{
    public class PaginatedResultVm<T> : IMapFrom<PaginatedResultVm<T>>
    {
        public PaginatedResultVm()
        {
            TotalCount = default(int);
        }

        public IEnumerable<T> Items { get; set; } = null!;
        public int TotalCount { get; set; }
    }
}
