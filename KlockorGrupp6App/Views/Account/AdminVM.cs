namespace KlockorGrupp6App.Web.Views.Account
{
    public class AdminVM
    {
        public string UserId { get; set; } = null!;
        public string UserEmail { get; set; } = null!;
        public List<ClocksDataVM> ClocksItems { get; set; } = new();

        public class ClocksDataVM
        {
            public int Id { get; set; }
            public string Brand { get; set; } = null!;
            public string Model { get; set; } = null!;
            public string Owner { get; set; } = null!;
        }
    }
}
