namespace Wikia.Mercury
{
    public class Adscontext
    {
        public Opts opts { get; set; }
        public Targeting targeting { get; set; }
        public object[] providers { get; set; }
        public object[] slots { get; set; }
        public object[] forceProviders { get; set; }
    }
}