using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Globalization;

using Faseway.GameLibrary.Game.Scenes;
using Faseway.GameLibrary.Logging;
using Faseway.GameLibrary.UI;
using Faseway.GameLibrary.UI.Widgets;

namespace Faseway.GameLibrary.Serialization
{
    /// <summary>
    /// Provides a serializer for seralizing graphical user interface elements.
    /// </summary>
    public class UiSerializer : ISerializer
    {
        // Constructor
        /// <summary>
        /// Gets the file version.
        /// </summary>
        public const float FileVersion = 1.0f;

        // Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Faseway.GameLibrary.Serialization.UiSerializer"/> class.
        /// </summary>
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

                var document = new XDocument(new XElement("UIDocument", new XAttribute("Version", FileVersion.ToString("F", CultureInfo.InvariantCulture))));

                foreach (var widget in container.Widgets)
                {
                    var element = widget.GetSerializedData();

                    document.Root.Add(element);
                }

                var text = document.ToString(SaveOptions.None);
                var buffer = Encoding.Default.GetBytes(text);
                
                stream.Write(buffer, 0, buffer.Length);
                stream.Flush();
                stream.Close();
                stream.Dispose();

                if (stream.GetType() == typeof(FileStream))
                {
                    Logger.Log("Serializing {0} to {1} done ...", instance.GetType(), ((FileStream)stream).Name);
                }
                else
                {
                    Logger.Log("Serializing {0} done ...", instance.GetType());
                }
            }
        }

        public object Deserialize(Type type, Stream stream)
        {
            if (stream == null)
            {
                if (stream.GetType() == typeof(FileStream))
                {
                    Logger.Log("Deserializing UIDocument from {0} failed ...", ((FileStream)stream).Name);
                }
                else
                {
                    Logger.Log("Deserializing UIDocument failed ...");
                }
                return null;
            }

            var document = XDocument.Load(stream);

            if (document.Root.Attribute("Version").Value != FileVersion.ToString())
            {
                Logger.Log("File Version Missmatch - UIDocument");
                return null;
            }

            return document;
        }
    }

    public static class UiSerializationExtension
    {
        /// <summary>
        /// Returns a serialized version of a widget.
        /// </summary>
        /// <param name="widget">The widget.</param>
        /// <returns>An <see cref="System.Xml.Linq.XElement"/>.</returns>
        public static XElement GetSerializedData(this Widget widget)
        {
            var element = new XElement("Widget", new XAttribute("Type", widget.GetType().Name));
            var basic = new XElement("Base");
            basic.Add(new XElement("Name", widget.Name));
            basic.Add(new XElement("Tag", widget.Tag));
            basic.Add(new XElement("Text", widget.Text));
            basic.Add(new XElement("Enabled", widget.Enabled));
            basic.Add(new XElement("Visible", widget.Visible));
            basic.Add(new XElement("Movable", widget.Moveable));
            basic.Add(new XElement("Focusable", widget.Focusable));
            basic.Add(new XElement("Focused", widget.Focused));
            basic.Add(new XElement("Opacity", widget.Opacity.ToString("F", CultureInfo.InvariantCulture)));
            basic.Add(new XElement("Position", new XAttribute("X", widget.Position.X), new XAttribute("Y", widget.Position.Y), new XAttribute("W", widget.Width), new XAttribute("H", widget.Height)));
            element.Add(basic);

            var custom = new XElement("Custom");
            element.Add(custom);

            var children = new XElement("Children");
            foreach (var child in widget.Widgets)
            {
                children.Add(child.GetSerializedData());
            }
            element.Add(children);

            return element;
        }

        public static void LoadFromFile(this Scene scene, ref WidgetContainer container, string path)
        {
            var serializer = new UiSerializer();
            var deserialized = (XDocument)serializer.Deserialize(typeof(WidgetContainer), Content.ResourceSystem.OpenFile(path));
            var widget = (Widget)null;

            foreach (var element in deserialized.Descendants())
            {
                if (element.Name == "Widget")
                {
                    var type = element.Attribute("Type").Value;
                    if (type == "Box")
                    {
                        widget = new Box(container);
                    }
                }
                else if (element.Name == "Base")
                {
                    if (element.Name == "Enabled")
                    {
                        widget.Enabled = bool.Parse(element.Value);
                    }
                    else if (element.Name == "Visible")
                    {
                        widget.Visible = bool.Parse(element.Value);
                    }
                    else if (element.Name == "Moveable")
                    {
                        widget.Moveable = bool.Parse(element.Value);
                    }
                    else if (element.Name == "Focusable")
                    {
                        //widget.Focusable = bool.Parse(element.Value);
                    }
                    else if (element.Name == "Visible")
                    {
                        widget.Visible = bool.Parse(element.Value);
                    }
                    else if (element.Name == "Opacity")
                    {
                        widget.Opacity = float.Parse(element.Value);
                    }
                }
            }
        }
    }
}
