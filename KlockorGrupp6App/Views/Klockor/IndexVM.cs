namespace KlockorGrupp6App.Web.Views.Klockor
{
    public class IndexVM
    {
        public KlockorDataVM[] KlockorItems { get; set; }
        public class KlockorDataVM
        {
            public required string Brand { get; set; }
            public required string Model { get; set; }
        }
    }
}
