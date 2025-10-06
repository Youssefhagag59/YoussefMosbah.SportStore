namespace SportsStore.Models.ViewModels
{
    public class NavigationMenuViewModel
    {
        public IEnumerable<string> Categories { get; set; } = Enumerable.Empty<string>();

        public string ? CurrentCategory { get; set; }
    }
}
