namespace PM3Events.WebAssembly.ViewModels
{
    public class NavLink(string label, string icon, string url)
    {
        public string Label { get; } = label;
        public string Icon { get; } = icon;
        public string Url { get; } = url;

        public NavLink() : this("", "", "") { }
    }
}
