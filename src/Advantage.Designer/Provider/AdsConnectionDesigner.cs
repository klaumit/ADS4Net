using System.ComponentModel;
using System.ComponentModel.Design;

namespace Advantage.Data.Provider
{
    public class AdsConnectionDesigner : ComponentDesigner
    {
        public override void DoDefaultAction()
        {
            var service1 = (IEventBindingService)GetService(typeof(IEventBindingService));
            var service2 = (IDesignerHost)GetService(typeof(IDesignerHost));
            DesignerTransaction designerTransaction = null;
            EventDescriptor e = null;
            string str = null;
            try
            {
                e = TypeDescriptor.GetEvents(Component)["InfoMessage"];
                var eventProperty = service1.GetEventProperty(e);
                if (service2 != null && designerTransaction == null)
                    designerTransaction = service2.CreateTransaction(e.Name);
                str = (string)eventProperty.GetValue(Component);
                if (str == null)
                {
                    str = service1.CreateUniqueMethodName(Component, e);
                    eventProperty.SetValue(Component, str);
                }
            }
            finally
            {
                designerTransaction?.Commit();
            }

            if (e == null || str == null)
                return;
            service1.ShowCode(Component, e);
        }
    }
}