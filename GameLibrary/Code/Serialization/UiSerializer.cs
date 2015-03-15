using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

using Faseway.GameLibrary.Logging;
using Faseway.GameLibrary.UI;

namespace Faseway.GameLibrary.Serialization
{
    public class UiSerializer : ISerializer
    {
        // Constructor
        public UiSerializer()
        {

        }

        // Methods
        public void Serialize(object instance, Stream stream)
        {
            var container = instance as WidgetContainer;
            if (container == null)
            {
                Logger.Log("Serializing {0} failed ...", instance.GetType());
            }
            else
            {
                Logger.Log("Serializing {0} ...", instance.GetType());
                
                var document = new XDocument(new XElement("uidocument", new XAttribute("version", 1.0f)));

                foreach (var widget in container.Widgets)
                {
                    var element = widget.GetSerializedData();

                    document.Root.Add(element);
                }

                document.Save("dummy.xml");

                Logger.Log("Serializing {0} done ...", instance.GetType());
            }
        }

        public object Deserialize(Type type, Stream stream)
        {
            throw new NotImplementedException();
        }
    }

    public static class UiSerializationExtension
    {
        public static XElement GetSerializedData(this Widget widget)
        {
            var element = new XElement("widget", new XAttribute("type", widget.GetType().Name));
            var basic = new XElement("base");
            basic.Add(new XElement("enabled", widget.Enabled));
            basic.Add(new XElement("visible", widget.Visible));
            basic.Add(new XElement("movable", widget.Moveable));
            basic.Add(new XElement("focusable", widget.Focusable));
            basic.Add(new XElement("focused", widget.Focused));
            basic.Add(new XElement("opacity", widget.Opacity));
            element.Add(basic);

            var custom = new XElement("custom");
            element.Add(custom);

            var children = new XElement("children");
            foreach (var child in widget.Widgets)
            {
                children.Add(child.GetSerializedData());
            }
            element.Add(children);

            return element;
        }
    }
}
