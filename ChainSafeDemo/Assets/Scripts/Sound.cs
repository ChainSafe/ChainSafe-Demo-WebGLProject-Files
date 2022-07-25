using UnityEngine.Audio;
using UnityEngine;


[System.Serializable]
public class Sound
{

    // allows you to call via name
    public string name;

    public AudioClip clip;

    // allows sliders within editor
    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;
    public AudioSource source;
    public bool loop;

}