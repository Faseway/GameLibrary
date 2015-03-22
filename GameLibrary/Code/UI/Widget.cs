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
    /// Provides the base class for widgets, which are components for user interfaces.
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
        /// Infrastructure. This property is not relevant for this class.
        /// </summary>
        public virtual bool AutoSize { get; set; }

        /// <summary>
        /// Gets or sets the name of the widget.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the object that contains data about the widget.
        /// </summary>
        public object Tag { get; set; }
        /// <summary>
        /// Gets or sets the text associated with this widget.
        /// </summary>
        public string Text { get; set; }

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

                OnBoundsChanged();
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

                OnBoundsChanged();
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
        /// Gets or sets the background color for the widget.
        /// </summary>
        public Color BackColor { get; set; }

        /// <summary>
        /// Gets or sets the widget state.
        /// </summary>
        public WidgetState State { get; set; }

        /// <summary>
        /// Gets or sets the parent widget container.
        /// </summary>
        public WidgetContainer Container { get; set; }

        // Events
        /// <summary>
        /// Occurs when the widget is created.
        /// </summary>
        public event EventHandler Load;
        /// <summary>
        /// Occurs when the widget is destroyed.
        /// </summary>
        public event EventHandler Unload;

        /// <summary>
        /// Occurs when the widget got focused.
        /// </summary>
        public event EventHandler GotFocus;
        /// <summary>
        /// Occurs when the widget lost focus.
        /// </summary>
        public event EventHandler LostFocus;

        /// <summary>
        /// Occurs when the enablement of widget changed.
        /// </summary>
        public event EventHandler EnablementChanged;
        /// <summary>
        /// Occurs when the visibility of the widget changed.
        /// </summary>
        public event EventHandler VisibilityChanged;
        /// <summary>
        /// Occurs when the bounds of the widget changed.
        /// </summary>
        public event EventHandler BoundsChanged;

        /// <summary>
        /// Occurs when the mouse pointer is over the widget and a mouse button was pressed.
        /// </summary>
        public event EventHandler<MouseEventArgs> Click;

        /// <summary>
        /// Occurs when the mouse pointer enters the widget.
        /// </summary>
        public event EventHandler<MouseEventArgs> MouseEnter;
        /// <summary>
        /// Occurs when the mouse pointer is moved over the widget.
        /// </summary>
        public event EventHandler<MouseEventArgs> MouseMove;
        /// <summary>
        /// Occurs when the mouse pointer rests on the widget.
        /// </summary>
        public event EventHandler<MouseEventArgs> MouseHover;
        /// <summary>
        /// Occurs when the mouse pointer is over the widget and a mouse button is pressed.
        /// </summary>
        public event EventHandler<MouseEventArgs> MouseDown;
        /// <summary>
        /// Occurs when the mouse wheel moves while the widget has focus.
        /// </summary>
        public event EventHandler<MouseEventArgs> MouseWheel;
        /// <summary>
        /// Occurs when the mouse pointer is over the widget and a mouse button is released.
        /// </summary>
        public event EventHandler<MouseEventArgs> MouseUp;
        /// <summary>
        /// Occurs when the mouse pointer leaves the widget.
        /// </summary>
        public event EventHandler<MouseEventArgs> MouseLeave;

        /// <summary>
        /// Occurs when a key is pressed while the widget has focus.
        /// </summary>
        public event EventHandler<KeyEventArgs> KeyPress;

        /// <summary>
        /// Occurs when the widget is updated.
        /// </summary>
        public event EventHandler Refresh;
        /// <summary>
        /// Occurs when the widget is redrawn.
        /// </summary>
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
                
                Widgets.ForEach(widget => widget.Update(elapsed));
            }
            else
            {
                State = WidgetState.Disabled;
            }
            OnRefresh();
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

        protected virtual void OnBoundsChanged()
        {
            BoundsChanged.SafeInvoke(this, EventArgs.Empty);
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
