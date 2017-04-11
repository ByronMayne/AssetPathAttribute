using UnityEngine;
using System.Collections;

[CreateAssetMenu]
public class InvalidTypeExample : ScriptableObject
{

    [AssetPath.Attribute(typeof(Object))]
    public Object m_ObjectValue;

    [AssetPath.Attribute(typeof(GameObject))]
    public GameObject m_GameObjectValue;

    [AssetPath.Attribute(typeof(AudioClip))]
    public AudioClip m_AudioClipValue;

    [AssetPath.Attribute(typeof(ScriptableObject))]
    public ScriptableObject m_ScriptableObject;

    [AssetPath.Attribute(typeof(Texture2D))]
    public Texture2D m_Texutre2D;

    [AssetPath.Attribute(typeof(Animator))]
    public Animator m_Animator;

    [AssetPath.Attribute(typeof(AudioSource))]
    public AudioSource m_AudioSource;

    [AssetPath.Attribute(typeof(Transform))]
    public Transform m_Transform;

    [AssetPath.Attribute(typeof(UnityEngine.UI.Image))]
    public UnityEngine.UI.Image m_UIImage;
}
