using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Faseway.GameLibrary.Logging;

namespace Faseway.GameLibrary.Components
{
    /// <summary>
    /// Provides a class for managing components.
    /// </summary>
    public class ComponentManager
    {
        // Properties
        /// <summary>
        /// Gets the components.
        /// </summary>
        protected List<IComponent> Components { get; private set; }

        // Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Faseway.GameLibrary.Components"/> class.
        /// </summary>
        public ComponentManager()
        {
            Logger.Log("Initializing {0} ...", GetType().Name);

            Components = new List<IComponent>();
        }

        // Destructor
        ~ComponentManager()
        {
            Logger.Log("Destroying {0} ...", GetType().Name);
        }

        // Methods
        /// <summary>
        /// Adds a specified component.
        /// </summary>
        /// <param name="component">The component.</param>
        public void Install(IComponent component)
        {
            if (Components.Find(match => match.GetType().Name == component.GetType().Name) != null)
            {
                Logger.Log("Component {0} has already been installed", component.GetType().Name);
            }
            else
            {
                Components.Add(component);

                Logger.Log("Component {0} has been installed", component.GetType().Name);
            }
        }

        /// <summary>
        /// Removes a specified component.
        /// </summary>
        /// <param name="component">The component.</param>
        public void Remove(IComponent component)
        {
            Components.Remove(component);

            Logger.Log("Component {0} has been removed", component.GetType().Name);
        }

        /// <summary>
        /// Removes a specified component abstraction.
        /// </summary>
        public void Remove<T>() where T : IComponent
        {
            Remove(Get<T>());
        }

        /// <summary>
        /// Gets a component by a specified type.
        /// </summary>
        /// <typeparam name="T">The type of the component.</typeparam>
        public T Get<T>() where T : IComponent
        {
            var component = Components.Find(comp => comp.GetType() == typeof(T));
            if (component != null)
            {
                return (T)component;
            }

            Logger.Log("Component {0} does not exist inside the component registry.", typeof(T).Name);

            return default(T);
        }

        /// <summary>
        /// Requires a specified component.
        /// </summary>
        /// <typeparam name="T">The component type.</typeparam>
        public T GetAndRequire<T>() where T : IComponent
        {
            var component = Get<T>();
            if (object.Equals(component, null))
            {
                throw new MissingComponentException(typeof(T));
            }

            return component;
        }

        #region IDisposable Implementation

        /// <summary>
        /// Disposes the object.
        /// </summary>
        public void Dispose()
        {
            //Components.ForEach(component => component.Dispose());
        }

        #endregion
    }
}
