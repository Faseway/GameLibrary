using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Faseway.GameLibrary.Extra;
using Faseway.GameLibrary.Game.Handlers;
using Faseway.GameLibrary.UI;
using Faseway.GameLibrary.UI.Base;
using Faseway.GameLibrary.UI.Events;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

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

        private MouseState _currentMouse;
        private MouseState _lastMouse;

        private KeyboardState _currentKeyboard;
        private KeyboardState _lastKeyboard;

        private bool _containsMouse;

        private bool _mouseDown;
        private bool _mouseUp;

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
        
        /// <summary>
        /// Gets or sets the width.
        /// </summary>
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
        /// <summary>
        /// Gets or sets the height.
        /// </summary>
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
        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        public Vector2 Position { get; set; }
        /// <summary>
        /// Gets or sets the screen position.
        /// </summary>
        public override Vector2 ScreenPosition
        {
            get { return Container.ScreenPosition + Position; }
        }
        /// <summary>
        /// Gets or sets the size of the widget.
        /// </summary>
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
        /// <summary>
        /// Gets the boundaries.
        /// </summary>
        public Rectangle Bounds
        {
            get { return new Rectangle((int)ScreenPosition.X, (int)ScreenPosition.Y, (int)Width, (int)Height); }
        }

        /// <summary>
        /// Gets or sets the widget state.
        /// </summary>
        public WidgetState State { get; set; }

        /// <summary>
        /// Gets or sets the parent widget container.
        /// </summary>
        public WidgetContainer Container { get; set; }

        // Events
        public event EventHandler Load;
        public event EventHandler Unload;

        public event EventHandler GotFocus;
        public event EventHandler LostFocus;

        public event EventHandler EnablementChanged;
        public event EventHandler VisibilityChanged;

        public event EventHandler<MouseEventArgs> Click;

        public event EventHandler<MouseEventArgs> MouseEnter;
        public event EventHandler<MouseEventArgs> MouseLeave;
        public event EventHandler<MouseEventArgs> MouseDown;
        public event EventHandler<MouseEventArgs> MouseUp;
        public event EventHandler<MouseEventArgs> MouseMove;

        public event EventHandler<KeyEventArgs> KeyPress;

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

            State = WidgetState.Normal;

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
        /// Updates the widget.
        /// </summary>
        public override void Update(float elapsed)
        {
            if (Enabled)
            {
                //if (_dirty)
                //{
                //    OnCreateWidget();
                //    _dirty = false;
                //}

                _currentMouse = Mouse.GetState();
                _currentKeyboard = Keyboard.GetState();

                MouseEventArgs mouseEvent = new MouseEventArgs(new Vector2(_currentMouse.X, _currentMouse.Y), _currentMouse);

                bool mouseIn = Bounds.Contains(_currentMouse.X, _currentMouse.Y);

                if (!_containsMouse && mouseIn)
                {
                    OnMouseEnter(mouseEvent);
                }
                if (_containsMouse && !mouseIn)
                {
                    OnMouseLeave(mouseEvent);

                    _mouseDown = false;
                    _mouseUp = false;
                }

                _containsMouse = mouseIn;

                if (_containsMouse && _currentMouse.LeftButton == ButtonState.Pressed)
                {
                    State = WidgetState.Clicked;
                }
                else if (_containsMouse && _currentMouse.LeftButton == ButtonState.Released)
                {
                    State = WidgetState.Hovered;
                }
                else if (Focused)
                {
                    State = WidgetState.Focused;
                }
                else if (Enabled)
                {
                    State = WidgetState.Normal;
                }
                else
                {
                    State = WidgetState.Disabled;
                }

                if (_currentMouse.LeftButton == ButtonState.Pressed &&
                    _lastMouse.LeftButton == ButtonState.Released && _containsMouse)
                {
                    OnMouseDown(mouseEvent);
                    _mouseDown = true;
                }
                if (_currentMouse.LeftButton == ButtonState.Released &&
                    _lastMouse.LeftButton == ButtonState.Pressed && _containsMouse)
                {
                    OnMouseUp(mouseEvent);
                    _mouseUp = true;
                }
                if (_mouseDown && _mouseUp)
                {
                    OnClick(mouseEvent);

                    Focused = true;

                    _mouseDown = false;
                    _mouseUp = false;
                }

                _lastMouse = _currentMouse;
                _lastKeyboard = _currentKeyboard;

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

        protected virtual void OnClick(MouseEventArgs e)
        {
            Click.SafeInvoke(this, e);
        }

        protected virtual void OnMouseEnter(MouseEventArgs e)
        {
            MouseEnter.SafeInvoke(this, e);
        }

        protected virtual void OnMouseLeave(MouseEventArgs e)
        {
            MouseLeave.SafeInvoke(this, e);
        }

        protected virtual void OnMouseDown(MouseEventArgs e)
        {
            MouseDown.SafeInvoke(this, e);
        }

        protected virtual void OnMouseUp(MouseEventArgs e)
        {
            MouseUp.SafeInvoke(this, e);
        }

        protected virtual void OnMouseMove(MouseEventArgs e)
        {
            MouseMove.SafeInvoke(this, e);
        }

        protected virtual void OnKeyPress(KeyEventArgs e)
        {
            KeyPress.SafeInvoke(this, e);
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
