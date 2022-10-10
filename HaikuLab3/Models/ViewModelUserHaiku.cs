namespace HaikuLab3.Models
{
    public class ViewModelUserHaiku
    {

        public ViewModelUserHaiku() { }
        public IEnumerable<HaikuListDetail> HaikuListDetailList { get; set; }
        public IEnumerable<UserDetail> UserDetailList { get; set; }
        public UserDetail userDetail { get; set; }
    }
}
