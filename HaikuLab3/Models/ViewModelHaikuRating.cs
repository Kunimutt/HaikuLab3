namespace HaikuLab3.Models
{
    public class ViewModelHaikuRating
    {
        public ViewModelHaikuRating() { }
        public IEnumerable<HaikuDetail> HaikuDetailList { get; set; }
        public IEnumerable<RatingDetail> RatingDetailList { get; set; }
    }
}
