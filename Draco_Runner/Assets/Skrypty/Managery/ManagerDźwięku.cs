using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class ManagerDźwięku : MonoBehaviour
{
    public static ManagerDźwięku managerDźwięku = null;
    public AudioMixer master;
    public AudioClip[] klipyMenu;
    public AudioClip[] klipyAmbient;
    private Queue<AudioSource> wasGeneratedAudioSource = null;
    void Awake()
    {
        if (managerDźwięku == null)
        {
            managerDźwięku = this;
        }
        else
        {
            Destroy(this);
        }
    }
    ///<summary>Metoda generuje obiekt z AudioSource ustawia klip do odtworzenia. Po zakończeniu odtwarzania zostanie on usunięty</summary>
    ///<param name="type">Jaki typ klipu ma zostać wylosowany do przypisania (0-Menu, 1-Ambient gry...)</param>
    public void WygenerujAudioSourcePodClip(byte type)
    {
        AudioSource tSource = null;
        if (wasGeneratedAudioSource == null || wasGeneratedAudioSource.Count == 0)
        {
            GameObject go = new GameObject("Temporary_Audio_Source");
            tSource = go.AddComponent<AudioSource>();
        }
        else
        {
            tSource = wasGeneratedAudioSource.Dequeue();
            tSource.gameObject.SetActive(true);
        }
        if (type < 2)
        {
            tSource.outputAudioMixerGroup = master.FindMatchingGroups("Master/MusicV")[0];
        }
        else
        {
            tSource.outputAudioMixerGroup = master.FindMatchingGroups("Master/SoundV")[0];
        }
        SetAudio(ref tSource, type);
        StartCoroutine(WaitForEndMusic(() =>
        {
            if (wasGeneratedAudioSource == null)
            {
                wasGeneratedAudioSource = new Queue<AudioSource>();
            }
            tSource.Stop();
            wasGeneratedAudioSource.Enqueue(tSource);
            tSource.gameObject.SetActive(false);
        }, tSource));
    }
    ///<summary>Metoda ustawia klip do odtworzenia na istniejącym już AudioSource</summary>
    ///<param name="source">Komponent do którego ma zostać przypisany klip</param>
    ///<param name="type">Jaki typ klipu ma zostać wylosowany do przypisania (0-Menu, 1-Ambient gry...)</param>
    ///<param name="isLoop">Czy metoda ma na audioSource ustawić loop?</param>
    ///<param name="whatToDo">Co metoda ma zrobić po przypisaniu klipu -1 odtwórz, 0-poczekaj, aż zakończy się aktualna piosenka(nie może być loop)</param>
    public void SetAudio(ref AudioSource source, byte type, bool isLoop = false, sbyte whatToDo = -1)
    {
        SetClipFouAudioSource(ref source, type);
        source.loop = isLoop;
        switch (whatToDo)
        {
            case -1:
                source.Play();
                break;
            case 0:
                if (source.loop)
                {
                    source.Play();
                }
                else
                {
                    AudioSource tSource = source;
                    StartCoroutine(WaitForEndMusic(() =>
                    {
                        tSource.Play();
                    }, tSource));
                }
                break;
        }
    }
    private void SetClipFouAudioSource(ref AudioSource source, byte type)
    {
        source.clip = GetClipForAudioSource(type);
    }
    private ref AudioClip GetClipForAudioSource(byte type)
    {
        switch (type)
        {
            case 0: //Ambient menu
                return ref klipyMenu[Random.Range(0, klipyMenu.Length)];
            case 1: //Ambient gry
                return ref klipyAmbient[Random.Range(0, klipyAmbient.Length)];
        }
        return ref klipyMenu[0];
    }
    public void SetMixerMusic(bool value)
    {
        if (master == null) return;
        master.SetFloat("MusicV", (value) ? 0.0f : -80.0f);
    }
    public void SetMixerSound(bool value)
    {
        if (master == null) return;
        master.SetFloat("SoundV", (value) ? 0.0f : -80.0f);
    }
    private IEnumerator WaitForEndMusic(System.Action action, AudioSource source)
    {
        yield return new WaitUntil(() => source.isPlaying);
        action.Invoke();
    }
}
