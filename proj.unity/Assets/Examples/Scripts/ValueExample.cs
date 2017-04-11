using UnityEngine;
using System.Collections;

[CreateAssetMenu]
public class ValueExample : ScriptableObject
{
    [AssetPath.Attribute(typeof(GameObject))]
    public string m_PlayerPrefab;

    [AssetPath.Attribute(typeof(Transform))]
    public string m_PlayerSpawn;

    [AssetPath.Attribute(typeof(AudioSource))]
    public string m_PlayerAudioSource;
}
