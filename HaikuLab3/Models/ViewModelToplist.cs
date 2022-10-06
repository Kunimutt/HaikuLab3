namespace HaikuLab3.Models
{
    public class ViewModelToplist
    {
        public ViewModelToplist() { }
        public IEnumerable<TopRatingDetail> TopRatingDetailList { get; set; }
        public IEnumerable<TopHaikuDetail> TopHaikuDetailList { get; set; }

    }
}
