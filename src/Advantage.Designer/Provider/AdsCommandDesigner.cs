using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace Advantage.Data.Provider
{
    public class AdsCommandDesigner : ComponentDesigner
    {
        protected override void PreFilterAttributes(IDictionary attributes)
        {
            var component = Component as AdsCommand;
            base.PreFilterAttributes(attributes);
            var visibleAttribute =
                new DesignTimeVisibleAttribute(component.DesignTimeVisible);
            attributes.Add(typeof(DesignTimeVisibleAttribute), visibleAttribute);
        }
    }
}