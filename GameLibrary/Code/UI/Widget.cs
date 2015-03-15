using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Faseway.GameLibrary.Extra;
using Faseway.GameLibrary.Game.Handlers;
using Faseway.GameLibrary.UI;
using Faseway.GameLibrary.UI.Base;

using Microsoft.Xna.Framework;

namespace Faseway.GameLibrary.UI
{
    /// <summary>
    /// Provides a widget for user interfaces.
    /// </summary>
    public abstract class Widget : WidgetContainer
    {
        // Variables
        private bool _focused;
        private bool _enabled;
        private bool _visible;
        private bool _dirty;

        private float _width;
        private float _height;

        // Properties
        /// <summary>
        /// Gets or sets a value indicating whether the widget is focusable.
        /// </summary>
        public bool Focusable { get; protected set; }
        /// <summary>
        /// Gets or sets a value indicating whether the widget is focused.
        /// </summary>
        public bool Focused
        {
            get { return _focused; }
            set
            {
                if (Focusable)
                {
                    _focused = value;
                    if (_focused)
                    {
                        OnGotFocus();
                    }
                    else
                    {
                        OnLostFocus();
                    }
                }
            }
        }
        /// <summary>
        /// Gets or sets a value indicating whether the widget is enabled.
        /// </summary>
        public bool Enabled
        {
            get { return _enabled; }
            set
            {
                _enabled = value;
                OnEnablementChanged();
            }
        }
        /// <summary>
        /// Gets or sets a value indicating whether the widget is moveable.
        /// </summary>
        public bool Moveable { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether the widget is visible.
        /// </summary>
        public bool Visible
        {
            get { return _visible; }
            set
            {
                _visible = value;
                _enabled = _visible;

                OnVisibilityChanged();
            }
        }

        /// <summary>
        /// Gets a value indicating whether the widget has children.
        /// </summary>
        public bool HasChildren
        {
            get { return Widgets.Count > 0; }
        }

        /// <summary>
        /// Gets or sets the opacity of the widget.
        /// </summary>
        public float Opacity { get; set; }

        //public Rectangle Bounds
        //{
        //    get { return new Rectangle((int)ScreenPosition.X, (int)ScreenPosition.Y, Width, Height); }
        //}
        public float Width
        {
            get { return _width; }
            set
            {
                _width = value;
                _dirty = true;

                //OnBoundsChanged();
            }
        }
        public float Height
        {
            get { return _height; }
            set
            {
                _height = value;
                _dirty = true;

                //OnBoundsChanged();
            }
        }
        public Vector2 Position { get; set; }
        //public Vector2 ScreenPosition
        //{
        //    get { return Container.ScreenPosition + Position; }
        //}
        public Vector2 Size
        {
            get
            {
                return new Vector2(Width, Height);
            }
            set
            {
                Width = value.X;
                Height = value.Y;
            }
        }

        public WidgetContainer Container { get; set; }

        // Events
        public event EventHandler Load;
        public event EventHandler Unload;
        public event EventHandler ContentLoad;

        public event EventHandler GotFocus;
        public event EventHandler LostFocus;

        public event EventHandler EnablementChanged;
        public event EventHandler VisibilityChanged;

        public event EventHandler Refresh;
        public event EventHandler Paint;

        // Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Faseway.GameLibrary.UI.Widget"/> class.
        /// </summary>
        public Widget(WidgetContainer container)
        {
            Container = container;
            Container.Widgets.Add(this);

            Enabled = true;
            Visible = true;

            Opacity = 1.0f;

            OnLoad();
        }

        // Deconstructor
        ~Widget()
        {
            OnUnload();
        }

        // Methods
        /// <summary>
        /// Shows the widget.
        /// </summary>
        public void Show()
        {
            Visible = true;
        }

        /// <summary>
        /// Hides the widget.
        /// </summary>
        public void Hide()
        {
            Visible = false;
        }

        /// <summary>
        /// Toggles the visiblity of the widget.
        /// </summary>
        public void ToggleVisibility()
        {
            Visible = !Visible;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        public void LoadContent()
        {
            OnContentLoad();
        }

        /// <summary>
        /// Updates the widget.
        /// </summary>
        public override void Update(float elapsed)
        {
            if (Enabled)
            {
                OnRefresh();
                Widgets.ForEach(widget => widget.Update(elapsed));
            }
        }

        /// <summary>
        /// Draws the widget.
        /// </summary>
        public override void Draw()
        {
            if (Visible)
            {
                OnPaint();
                Widgets.ForEach(widget => widget.Draw());
            }
        }

        // Event Methods
        protected virtual void OnLoad()
        {
            Load.SafeInvoke(this, EventArgs.Empty);
        }

        protected virtual void OnUnload()
        {
            Unload.SafeInvoke(this, EventArgs.Empty);
        }

        protected virtual void OnContentLoad()
        {
            ContentLoad.SafeInvoke(this, EventArgs.Empty);
        }

        protected virtual void OnGotFocus()
        {
            GotFocus.SafeInvoke(this, EventArgs.Empty);
        }

        protected virtual void OnLostFocus()
        {
            LostFocus.SafeInvoke(this, EventArgs.Empty);
        }

        protected virtual void OnEnablementChanged()
        {
            EnablementChanged.SafeInvoke(this, EventArgs.Empty);
        }

        protected virtual void OnVisibilityChanged()
        {
            VisibilityChanged.SafeInvoke(this, EventArgs.Empty);
        }

        protected virtual void OnRefresh()
        {
            Refresh.SafeInvoke(this, EventArgs.Empty);
        }

        protected virtual void OnPaint()
        {
            Paint.SafeInvoke(this, EventArgs.Empty);
        }
    }
}
