using System;
using System.Windows.Forms;

namespace Faseway.GameLibrary
{
    /// <summary>
    /// Displays a message box that can contain text, buttons, and symbols that inform
    /// and instruct the user.
    /// </summary>
    public static class MsgBox
    {
        /// <summary>
        /// Displays a message box with specified text.
        /// </summary>
        /// <param name="text">The text to display in the message box.</param>
        public static void Show(string text)
        {
            MessageBox.Show(text);
        }

        /// <summary>
        /// Displays a message box with specified value.
        /// </summary>
        /// <param name="value">The value to be display as text in the message box.</param>
        public static void Show(object value)
        {
            MessageBox.Show(value.ToString());
        }

        /// <summary>
        ///  Displays a message box with specified text.
        /// </summary>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="text">The text to display in the message box.</param>
        public static void Show(string caption, string text)
        {
            MessageBox.Show(text, caption);
        }

        /// <summary>
        ///  Displays a message box with specified text.
        /// </summary>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="format">The formated text to display in the message box.</param>
        /// <param name="args">An array of objects to display using format.</param>
        public static void Show(string caption, string format, params object[] args)
        {
            System.Console.WriteLine();
            MessageBox.Show(string.Format(format, args), caption);
        }

        /// <summary>
        /// Displays a message box with specified text.
        /// </summary>
        /// <param name="icon">One of the MessageBoxIcon values that specifies which
        /// icon to display in the message box.</param>
        /// <param name="text">The text to display in the title bar of the message box.</param>
        public static void Show(MsgBoxIcon icon, string text)
        {
            MessageBox.Show(text);
        }

        /// <summary>
        /// Displays a message box with specified text.
        /// </summary>
        /// <param name="icon">One of the MessageBoxIcon values that specifies which
        /// icon to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="text">The text to display in the title bar of the message box.</param>
        public static void Show(MsgBoxIcon icon, string caption, string text)
        {
            MessageBox.Show(text, caption, MessageBoxButtons.OK, (MessageBoxIcon)icon);
        }

        /// <summary>
        /// Displays a message box with specified text.
        /// </summary>
        /// <param name="icon">One of the MessageBoxIcon values that specifies which
        /// icon to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="format">The formated text to display in the message box.</param>
        /// <param name="args">An array of objects to display using format.</param>
        public static void Show(MsgBoxIcon icon, string caption, string format, params object[] args)
        {
            MessageBox.Show(string.Format(format, args), caption, MessageBoxButtons.OK, (MessageBoxIcon)icon);
        }
    }
}
