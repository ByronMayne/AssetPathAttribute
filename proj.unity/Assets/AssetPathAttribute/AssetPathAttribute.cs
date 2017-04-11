using System;
using UnityEngine;



public class AssetPath
{
    /// <summary>
    /// A enum containing all the types of paths we can watch
    /// </summary>
    public enum Types
    {
        /// <summary>
        /// The path will be contained within the 'Asset/*' directory.
        /// </summary>
        Project,
        /// <summary>
        /// The path will be contained within a resources folder.
        /// </summary>
        Resources,
    }

    /// <summary>
    /// We limit this attributes to fields and only allow one. Should
    /// only be applied to string types. 
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class Attribute : PropertyAttribute
    {
        private Types m_PathType;
        private Type m_Type;

        /// <summary>
        /// Gets the type of asset path this attribute is watching.
        /// </summary>
        public Types pathType
        {
            get { return m_PathType; }
        }

        /// <summary>
        /// Gets the type of asset this attribute is expecting.
        /// </summary>
        public Type type
        {
            get { return m_Type; }
        }

        /// <summary>
        /// Creates the default instance of AssetPathAttribute
        /// </summary>
        public Attribute(Type type)
        {
            m_Type = type; 
            m_PathType = Types.Project;
        }
    }

    /// <summary>
    /// Takes the string from the Asset Path Attribute and converts it into
    /// a usable path.
    /// </summary>
    /// <param name="assetPath"></param>
    /// <returns></returns>
    public string Convert(string assetPath)
    {
        return string.Empty;
    }
}
