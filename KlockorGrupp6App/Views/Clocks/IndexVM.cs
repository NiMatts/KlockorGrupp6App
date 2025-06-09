namespace KlockorGrupp6App.Web.Views.Klockor
{
    public class IndexVM
    {
        public ClocksDataVM[] ClocksItems { get; set; }
        public class ClocksDataVM
        {
            public required string Brand { get; set; }
            public required string Model { get; set; }
        }
    }
}
