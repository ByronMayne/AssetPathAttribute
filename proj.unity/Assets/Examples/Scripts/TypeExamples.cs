using UnityEngine;
using System.Collections;

[CreateAssetMenu]
public class TypeExamples : ScriptableObject
{

    [AssetPath.Attribute(typeof(Object))]
    public string m_ObjectValue;

    [AssetPath.Attribute(typeof(GameObject))]
    public string m_GameObjectValue;

    [AssetPath.Attribute(typeof(AudioClip))]
    public string m_AudioClipValue;

    [AssetPath.Attribute(typeof(ScriptableObject))]
    public string m_ScriptableObject;

    [AssetPath.Attribute(typeof(Texture2D))]
    public string m_Texutre2D;

    [AssetPath.Attribute(typeof(Animator))]
    public string m_Animator;

    [AssetPath.Attribute(typeof(AudioSource))]
    public string m_AudioSource;

    [AssetPath.Attribute(typeof(Transform))]
    public string m_Transform;

    [AssetPath.Attribute(typeof(UnityEngine.UI.Image))]
    public string m_UIImage;

    public void ConvertExample()
    {
        string resourcesPath = AssetPath.ConvertToResourcesPath(m_UIImage);
    }

    [AssetPath.Attribute(typeof(GameObject))]
    public string m_PlayerPrefab;

    public void CreatePlayer()
    {
        // Converts our path to a resources path and then loads the object.
        GameObject player = AssetPath.Load<GameObject>(m_PlayerPrefab);
    }
}
