namespace TeduNetcore.Utilities.Dtos
{
    public abstract class PageResultBase
    {
        public int TotalPage { get; set; } = 0;

        public int TotalRow { get; set; } = 0;
    }
}
