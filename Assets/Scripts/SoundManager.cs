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
            //Se já existir destrói;
            Destroy(instance);

        //Toda vez que carregar uma cena esse game object não será destruido;
        DontDestroyOnLoad(gameObject);
    }

    public void InicializarSom(AudioClip audio)
    {
        efeitos.clip = audio;
        efeitos.Play();
    }
}
