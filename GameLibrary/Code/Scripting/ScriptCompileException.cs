using System;
using System.Linq;
using System.Text;

namespace Faseway.GameLibrary.Scripting
{
    /// <summary>
    /// Represents a script compiler exception.
    /// </summary>
    public class ScriptCompileException : Exception
    {
        // Properties
        /// <summary>
        /// Gets or sets the error number.
        /// </summary>
        public string ErrorNumber { get; set; }
        /// <summary>
        /// Gets or sets the text of the error message.
        /// </summary>
        public string ErrorText { get; set; }
        /// <summary>
        /// Gets or sets the file name of the source file that contains the code which caused the error.
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// Gets or sets the line number where the source of the error occurs.
        /// </summary>
        public int Line { get; set; }

        // Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Faseway.GameLibrary.Scripting.ScriptCompileException"/> class.
        /// </summary>
        public ScriptCompileException()
        {
            ErrorNumber = string.Empty;
            ErrorText = string.Empty;
            FileName = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Faseway.GameLibrary.Scripting.ScriptCompileException"/> class
        /// using the specified file name, line, error number, and error text.
        /// <param name="fileName">The file name of the file that the compiler was compiling when it encountered the error.</param>
        /// <param name="line">The line of the source of the error.</param>
        /// <param name="errorNumber">The error number of the error.</param>
        /// <param name="errorText">The error message text.</param>
        /// </summary>
        public ScriptCompileException(string fileName, int line, string errorNumber, string errorText)
        {
            FileName = fileName;
            Line = line;
            ErrorNumber = errorNumber;
            ErrorText = errorText;
        }
    }
}
