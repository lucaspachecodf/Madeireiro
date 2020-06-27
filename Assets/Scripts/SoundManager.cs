using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    
    public AudioSource efeitos;
    public AudioClip audioCorte;
    public AudioClip audioMorte;
    public AudioClip audioPlay;
        
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(instance);

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InicializarSom(AudioClip audio)
    {
        efeitos.clip = audio;
        efeitos.Play();
    }
}
