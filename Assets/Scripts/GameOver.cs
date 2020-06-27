using Assets.Scripts.Enum;
using Assets.Scripts.Util;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Text melhorPontuacao;
    public Text ponto;

    void Start()
    {
        melhorPontuacao.text = PlayerPrefs.GetInt(EPrefs.MelhorPontuacao.ToDescription()).ToString();
        ponto.text = PlayerPrefs.GetInt(EPrefs.Ponto.ToDescription()).ToString();
    }

    public void ReiniciarPartida()
    {
        PlayerPrefs.SetInt(EPrefs.Ponto.ToDescription(), 0);
        SoundManager.instance.InicializarSom(SoundManager.instance.audioPlay);
        SceneManager.LoadScene(ECena.PrimeiraFase.ToDescription());
    }
}
