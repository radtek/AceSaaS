namespace Acesoft.Web.UI.Widgets.Html
{
    public class SelectBoxHtmlBuilder : ComboHtmlBuilder<SelectBox>
    {
        public SelectBoxHtmlBuilder(SelectBox component)
            : base(component)
        {
            this.RenderType = "aceui";
        }

        protected override void PreBuild()
        {
            base.PreBuild();

            if (Component.SelectUrl.HasValue())
            {
                Options["url"] = Component.SelectUrl;
            }
            if (Component.Parent.HasValue())
            {
                Options["parent"] = Component.Parent;
            }
            if (Component.Param.HasValue())
            {
                Options["param"] = Component.Param;
            }
        }
    }
}