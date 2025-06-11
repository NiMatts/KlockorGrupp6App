namespace KlockorGrupp6App.Web.Views.Account;

public class MembersVM
{
    public ClocksDataVM[] ClocksItems { get; set; }
    public class ClocksDataVM
    {
        public required string Brand { get; set; }
        public required string Model { get; set; }
        public required int Id { get; set; }
    }
}
