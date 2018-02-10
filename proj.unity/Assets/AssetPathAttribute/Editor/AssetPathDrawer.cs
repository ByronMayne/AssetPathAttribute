using UnityEngine;
using UnityEditor;
using Object = UnityEngine.Object;
using System.Collections.Generic;
using System;
using System.Reflection;

[CustomPropertyDrawer(typeof(AssetPath.Attribute))]
public class AssetPathDrawer : PropertyDrawer
{
    // A helper warning label when the user puts the attribute above a non string type.
    private const string m_InvalidTypeLabel = "Attribute invalid for type ";
    private const float m_ButtonWidth = 80f;
    private static int s_PPtrHash = "s_PPtrHash".GetHashCode();
    private string m_ActivePickerPropertyPath;
    private int m_PickerControlID = -1;
    private static GUIContent m_MissingAssetLabel = new GUIContent("Missing");

    // A shared array of references to the objects we have loaded
    private IDictionary<string, Object> m_References;


    /// <summary>
    /// Invoked when unity creates our drawer. 
    /// </summary>
    public AssetPathDrawer()
    {
        m_References = new Dictionary<string, Object>();
    }

    /// <summary>
    /// Invoked when we want to try our property. 
    /// </summary>
    /// <param name="position">The position we have allocated on screen</param>
    /// <param name="property">The field our attribute is over</param>
    /// <param name="label">The nice display label it has</param>
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        property = GetProperty(property);
        if (property.propertyType != SerializedPropertyType.String)
        {
            // Create a rect for our label
            Rect labelPosition = position;
            // Set it's width 
            labelPosition.width = EditorGUIUtility.labelWidth;
            // Draw it
            GUI.Label(labelPosition, label);
            // Create a rect for our content
            Rect contentPosition = position;
            // Move it over by the x
            contentPosition.x += labelPosition.width;
            // Shrink it in width since we moved it over
            contentPosition.width -= labelPosition.width;
            // Draw our content warning;
            EditorGUI.HelpBox(contentPosition, m_InvalidTypeLabel + this.fieldInfo.FieldType.Name, MessageType.Error);
        }
        else
        {
            HandleObjectReference(position, property, label);
        }

    }

    /// <summary>
    /// Due to the fact that ShowObjectPicker does not have a none generic version we
    /// have to use reflection to create and invoke it.
    /// </summary>
    /// <param name="type"></param>
    private void ShowObjectPicker(Type type, Rect position)
    {
        // Get the type
        Type classType = typeof(EditorGUIUtility);
        // Get the method
        MethodInfo showObjectPickerMethod = classType.GetMethod("ShowObjectPicker", BindingFlags.Public | BindingFlags.Static);
        // Make the generic version
        MethodInfo genericObjectPickerMethod = showObjectPickerMethod.MakeGenericMethod(type);
        // We have no starting target
        Object target = null;
        // We are not allowing scene objects 
        bool allowSceneObjects = false;
        // An empty filter
        string searchFilter = string.Empty;
        // Make a control ID
        m_PickerControlID = GUIUtility.GetControlID(s_PPtrHash, FocusType.Passive, position);
        // Save our property path
        // Invoke it (We have to do this step since there is only a generic version for showing the asset picker.
        genericObjectPickerMethod.Invoke(null, new object[] { target, allowSceneObjects, searchFilter, m_PickerControlID });
    }

    protected virtual SerializedProperty GetProperty(SerializedProperty rootProperty)
    {
        return rootProperty;
    }


    protected virtual Type ObjectType()
    {
        // Get our attribute
        AssetPath.Attribute attribute = this.attribute as AssetPath.Attribute;
        // Return back the type.
        return attribute.type;
    }

    private void HandleObjectReference(Rect position, SerializedProperty property, GUIContent label)
    {

        Type objectType = ObjectType();
        // First get our value
        Object propertyValue = null;
        // Save our path
        string assetPath = property.stringValue;
        // Have a label to say it's missing
        //bool isMissing = false;
        // Check if we have a key
        if (m_References.ContainsKey(property.propertyPath))
        {
            // Get the value. 
            propertyValue = m_References[property.propertyPath];
        }
        // Now if its null we try to load it
        if (propertyValue == null && !string.IsNullOrEmpty(assetPath))
        {
            // Try to load our asset
            propertyValue = AssetDatabase.LoadAssetAtPath(assetPath, objectType);

            if (propertyValue == null)
            {
                //isMissing = true;
            }
            else
            {
                m_References[property.propertyPath] = propertyValue;
            }
        }

        EditorGUI.BeginChangeCheck();
        {
            // Draw our object field.
            propertyValue = EditorGUI.ObjectField(position, label, propertyValue, objectType, false);
        }
        if (EditorGUI.EndChangeCheck())
        {
            OnSelectionMade(propertyValue, property);
        }
    }

    protected virtual void OnSelectionMade(Object newSelection, SerializedProperty property)
    {
        string assetPath = string.Empty;

        if (newSelection != null)
        {
            // Get our path
            assetPath = AssetDatabase.GetAssetPath(newSelection);
        }

        // Save our value.
        m_References[property.propertyPath] = newSelection;
        // Save it back
        property.stringValue = assetPath;
    }
}
