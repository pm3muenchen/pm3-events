using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PM3Events.Web.ViewModels;
using Radzen.Blazor.Rendering;

namespace PM3Events.Web.Components.Layout
{
    public partial class Sidebar : ComponentBase
    {
        private const string DefaultStyle = "top:0;height:100vh;width:20vw;";
        public const string ExpandIcon = "keyboard_double_arrow_right";
        public const string CollapseIcon = "keyboard_double_arrow_left";

        public IEnumerable<NavLink> NavLinks { get; set; } = new List<NavLink>();

        [Parameter]
        public string AppName { get; set; }

        [Parameter]
        public string AppLogo { get; set; }

        /// <summary>
        /// Gets or sets the style.
        /// </summary>
        /// <value>The style.</value>
        [Parameter]
        public string Style { get; set; } = DefaultStyle;

        /// <summary>
        /// Toggles the responsive mode of the component. If set to <c>true</c> (the default) the component will be 
        /// expanded on larger displays and collapsed on touch devices. Set to <c>false</c> if you want to disable this behavior.
        /// Responsive mode is only available when RadzenSidebar is inside <see cref="RadzenLayout" />.
        /// </summary>
        [Parameter]
        public bool Responsive { get; set; } = true;

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        private bool IsResponsive => Responsive;

        /// <summary>
        /// Gets the style.
        /// </summary>
        /// <returns>System.String.</returns>
        protected string GetStyle()
        {
            var style = Style;

            return $"{style}{(Expanded ? ";transform:translateX(0);" : ";width:64px;")}";
        }

        protected string GetCssClass()
        {
            return ClassList.Create("sidebar").Add("sidebar-expanded", expanded == true)
                                                 .Add("sidebar-collapsed", expanded == false)
                                                 .Add("sidebar-responsive", IsResponsive)
                                                 .ToString();
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="RadzenSidebar"/> is expanded.
        /// </summary>
        /// <value><c>true</c> if expanded; otherwise, <c>false</c>.</value>
        [Parameter]
        public bool Expanded { get; set; } = true;

        /// <summary>
        /// Gets or sets the expanded changed callback.
        /// </summary>
        /// <value>The expanded changed callback.</value>
        [Parameter]
        public EventCallback<bool> ExpandedChanged { get; set; }

        bool? expanded;

        /// <inheritdoc />
        protected override void OnInitialized()
        {
            NavLinks = new List<NavLink>
            {
                new NavLink("Dashboard", "dashboard", "/dashboard")
            };

            if (!Responsive)
            {
                expanded = Expanded;
            }

            base.OnInitialized();
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
        }

        async Task OnChange(bool matches)
        {
            expanded = !matches;
            await ExpandedChanged.InvokeAsync(!matches);
        }

        public async Task ToggleSidebar()
        {
            Expanded = !Expanded;
            expanded = Expanded;
            await ExpandedChanged.InvokeAsync(Expanded);
            await JSRuntime.InvokeVoidAsync("setMainContainerClass", Expanded);
        }

        public void NavigateTo(string url)
        {
            NavigationManager.NavigateTo(url);
        }
    }
}
