using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Sections;

namespace PM3Events.Web.Components.Shared.LayoutTitle
{
    public sealed class LayoutTitle : ComponentBase
    {
        /// <summary>
        /// Gets or sets the content to be rendered as the document title.
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenComponent<SectionContent>(0);
            builder.AddComponentParameter(1, nameof(SectionContent.SectionId), LayoutTitleOutlet.TitleSectionId);
            builder.AddComponentParameter(2, nameof(SectionContent.ChildContent), (RenderFragment)BuildTitleRenderTree);
            builder.CloseComponent();
        }

        private void BuildTitleRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, "h2");
            builder.AddAttribute(1, "class", "layout-page-title");
            builder.AddContent(2, ChildContent);
            builder.CloseElement();
        }
    }
}
