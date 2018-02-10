using System;
using UnityEngine;



public partial class AssetPath
{
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



        public string SuperProperty
        {
            get
            {
                /* whole pile of work done here */
                return "Complex string example"; 
            }
        }

        public void Evulate()
        {

            string value = SuperProperty; 
        }
    }
}
