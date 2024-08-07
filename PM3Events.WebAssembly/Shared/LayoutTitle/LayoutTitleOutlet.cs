using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Sections;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace PM3Events.WebAssembly.Shared.LayoutTitle
{
    public sealed class LayoutTitleOutlet : ComponentBase
    {
        private const string GetAndRemoveExistingTitle = "Blazor._internal.PageTitle.getAndRemoveExistingTitle";

        internal static readonly object TitleSectionId = new();
        internal static readonly object DefaultTitleSectionId = new();

        private string? _defaultTitle;

        [Inject]
        private IJSRuntime JSRuntime { get; set; } = default!;

        /// <inheritdoc/>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _defaultTitle = await JSRuntime.InvokeAsync<string>("getPageTitle");
                StateHasChanged();
            }
        }

        /// <inheritdoc/>
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            // Render the title content
            builder.OpenComponent<SectionOutlet>(0);
            builder.AddComponentParameter(1, nameof(SectionOutlet.SectionId), TitleSectionId);
            builder.CloseComponent();

            // Render the default title content
            builder.OpenComponent<SectionOutlet>(0);
            builder.AddComponentParameter(1, nameof(SectionOutlet.SectionId), DefaultTitleSectionId);
            builder.CloseComponent();

            // Render the default title if it exists
            if (!string.IsNullOrEmpty(_defaultTitle))
            {
                builder.OpenComponent<SectionContent>(2);
                builder.AddComponentParameter(3, nameof(SectionContent.SectionId), DefaultTitleSectionId);
                builder.AddComponentParameter(5, nameof(SectionContent.ChildContent), (RenderFragment)BuildDefaultTitleRenderTree);
                builder.CloseComponent();
            }
        }

        private void BuildDefaultTitleRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, "h2");
            builder.AddAttribute(1, "class", "layout-default-page-title");
            builder.AddContent(2, _defaultTitle);
            builder.CloseElement();
        }
    }
}
