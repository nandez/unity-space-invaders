using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] sounds;

    private void Awake()
    {
        if (instance != null)
        {
            if (instance != this)
                Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            var soundGo = new GameObject($"Sound_{i}_{sounds[i].name}");
            soundGo.transform.SetParent(transform);
            sounds[i].SetSource(soundGo.AddComponent<AudioSource>());
        }

        PlaySound("music");
    }

    public void PlaySound(string soundName)
    {
        if (string.IsNullOrEmpty(soundName))
            return;

        var sound = sounds.FirstOrDefault(t => t.name.Equals(soundName, System.StringComparison.InvariantCultureIgnoreCase));
        if (sound == null)
        {
            Debug.LogWarning($"AudioManager: {soundName} sound not defined in list.");
            return;
        }

        sound.Play();
    }

    public void StopSound(string soundName)
    {
        if (string.IsNullOrEmpty(soundName))
            return;

        var sound = sounds.FirstOrDefault(t => t.name.Equals(soundName, System.StringComparison.InvariantCultureIgnoreCase));
        if (sound == null)
        {
            Debug.LogWarning($"AudioManager: {soundName} sound not defined in list.");
            return;
        }

        sound.Stop();
    }
}