namespace HaikuLab3.Models
{
    public class ViewModelHaikuList
       
    {
        public ViewModelHaikuList() { }
        public IEnumerable<HaikuListDetail>HaikuListDetailList { get; set; }
        public IEnumerable<GenreDetail> GenreDetailList { get; set; }
    }
}
