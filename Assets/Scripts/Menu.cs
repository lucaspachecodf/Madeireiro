using Assets.Scripts.Enum;
using Assets.Scripts.Util;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Text texto;

    public void IniciarGame()
    {
        SoundManager.instance.InicializarSom(SoundManager.instance.audioPlay);
        SceneManager.LoadScene(ECena.PrimeiraFase.ToDescription());
    }
}
