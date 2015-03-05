using System;
using System.IO;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Text;

using Faseway.GameLibrary.Logging;

namespace Faseway.GameLibrary.Content
{
    /// <summary>
    /// Implementation of the default resource and file system.
    /// </summary>
    public static class ResourceSystem
    {
        // Properties
        /// <summary>
        /// Gets a value inticating whether the resource system has been initialized.
        /// </summary>
        public static bool Initialized { get; private set; }
        /// <summary>
        /// Gets the base path.
        /// </summary>
        public static string BasePath { get; private set; }

        // Constructor
        /// <summary>
        /// Initializes the <see cref="Faseway.GameLibrary.Content.ResourceSystem"/> class.
        /// </summary>
        static ResourceSystem()
        {
        }

        // Methods
        /// <summary>
        /// Initializes the <see cref="Rewtek.GameLibrary.Common.ResourceSystem"/>.
        /// </summary>
        public static void Initialize()
        {
            if (Initialized) return;

            Logger.Log("Initializing {0} ...", "ResourceSystem");

            BasePath = Environment.CurrentDirectory + "\\";

            Initialized = true;
        }

        /// <summary>
        /// Gets a full path using the library file system.
        /// </summary>
        /// <param name="part">The part.</param>
        public static string GetFullPath(string part)
        {
            return Path.Combine(BasePath, part);
        }

        #region Directories

        /// <summary>
        /// Creates all directories and subdirectories as specified by path.
        /// </summary>
        /// <param name="path">The directory to create.</param>
        public static void CreateDirectory(string path)
        {
            Directory.CreateDirectory(GetFullPath(path));
            Logger.Log("Directory {0} has been created", path);
        }

        /// <summary>
        /// Creates all the directories in the specified path, applying the specified Windows security.
        /// </summary>
        /// <param name="path">The directory to create.</param>
        /// <param name="directorySecurity">The access control to apply to the directory.</param>
        public static void CreateDirectory(string path, DirectorySecurity directorySecurity)
        {
            Directory.CreateDirectory(GetFullPath(path), directorySecurity);
            Logger.Log("Directory {0} has been created", path);
        }

        /// <summary>
        /// Determines whether the given path refers to an existing directory on disk.
        /// </summary>
        /// <param name="path">The path to test.</param>
        /// <returns>true if path refers to an existing directory; otherwise, false.</returns>
        public static bool ExistsDirectory(string path)
        {
            var directory = GetFullPath(path); // Path.GetDirectoryName(GetFullPath(path));
            if (directory == null) return false;
            if (directory.Contains("/") || directory.Contains("\\"))
            {
                if (!Directory.Exists(directory)) return false;
            }

            return true;
        }

        /// <summary>
        /// Deletes an empty directory from a specified path.
        /// </summary>
        /// <param name="path">The name of the directory to remove.</param>
        public static void DeleteDirectory(string path)
        {
            Directory.Delete(GetFullPath(path));
        }

        /// <summary>
        /// Deletes an empty directory and, if indicated, any subdirectories and files in the directory.
        /// </summary>
        /// <param name="path">The name of the directory to remove.</param>
        /// <param name="recursive">true to remove directories, subdirectories, and files in path; otherwise, false.</param>
        public static void DeleteDirectory(string path, bool recursive)
        {
            Directory.Delete(GetFullPath(path), recursive);
        }

        #endregion
    }
}
