using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Sections;

namespace PM3Events.Web.Components.Shared.LayoutTitle
{
    public sealed class LayoutTitleContent : ComponentBase
    {
        /// <summary>
        /// Gets or sets the content to be rendered in <see cref="LayoutTitleOutlet"/> instances.
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        /// <inheritdoc/>
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenComponent<SectionContent>(0);
            builder.AddComponentParameter(1, nameof(SectionContent.SectionId), LayoutTitleOutlet.LayoutTitleSectionId);
            builder.AddComponentParameter(2, nameof(SectionContent.ChildContent), ChildContent);
            builder.CloseComponent();
        }
    }
}
