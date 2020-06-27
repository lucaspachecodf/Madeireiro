using Assets.Scripts.Enum;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Assets.Scripts.Util;

public class GameManager : MonoBehaviour
{
    public GameObject[] troncos;
    public List<GameObject> listaTroncos;

    public int Game { get; set; }
    public float alturaTronco = 2.0f;
    public float posicaoInicialY = -3.95f;
    public static GameManager instance;
    public bool gameOver = false;    
    public Text pontuacao;
    public Image tempoBarra;
    public int fase = (int)EFase.PrimeiraFase;
    public Text descricaoFase;

    private float larguraBarra = 188f;
    private int maximoTronco = 8;
    private bool hasGalho = false;
    private int pontos = 0;
    private float tempoJogo = 10f;
    private float tempoExtra = 0.2f;
    private float tempoAtual;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        descricaoFase.text = ((EFase)fase).ToDescription();
        Invoke(nameof(LimparDescricaoFase), 2f);        

        if (fase == (int)EFase.PrimeiraFase)
        {
            PlayerPrefs.SetInt(EPrefs.Ponto.ToDescription(), 0);
            tempoAtual = tempoJogo;
        }
        else
        {
            pontos = PlayerPrefs.GetInt(EPrefs.Ponto.ToDescription());
            pontuacao.text = pontos.ToString();
            tempoAtual = PlayerPrefs.GetFloat(EPrefs.TempoAtual.ToDescription());
        }

        IniciarTronco();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            DiminuirBarra();
        }
    }

    void CriarTroncos(int posicao)
    {
        GameObject tronco = Instantiate(hasGalho ? troncos[Random.Range(0, 3)] : troncos[0]);
        tronco.transform.localPosition = new Vector3(0, posicaoInicialY + (posicao * alturaTronco), 0);
        listaTroncos.Add(tronco);
        hasGalho = !hasGalho;
    }

    void IniciarTronco()
    {
        for (int i = 0; i < maximoTronco; i++)
        {
            CriarTroncos(i);
        }
    }

    void CortarTronco()
    {
        Destroy(listaTroncos[0]);
        listaTroncos.RemoveAt(0);
        SomarPontuacao();

        SoundManager.instance.InicializarSom(SoundManager.instance.audioCorte);
    }

    void PosicionarTronco()
    {
        for (int i = 0; i < listaTroncos.Count; i++)
        {
            listaTroncos[i].transform.position = new Vector3(0, posicaoInicialY + (i * alturaTronco), 0);
        }

        CriarTroncos(maximoTronco);
    }

    void SomarPontuacao()
    {
        pontos++;
        pontuacao.text = pontos.ToString();

        PlayerPrefs.SetInt(EPrefs.Ponto.ToDescription(), pontos);

        if (tempoAtual + tempoExtra < tempoJogo)
            tempoAtual += tempoExtra;

        if (fase == (int)EFase.PrimeiraFase && pontos == (int)EPontosFase.PontosSegundaFase)
        {
            NavegarProximaFase(ECena.SegundaFase);
        }
        else if (fase == (int)EFase.SegundaFase && pontos == (int)EPontosFase.PontosTerceiraFase)
        {
            NavegarProximaFase(ECena.TerceiraFase);
        }
    }

    void DiminuirBarra()
    {
        tempoAtual -= Time.deltaTime;
        float tempo = tempoAtual / tempoJogo;
        float posicaoX = larguraBarra - (tempo * larguraBarra);

        tempoBarra.transform.localPosition = new Vector2(-posicaoX, tempoBarra.transform.localPosition.y);

        PlayerPrefs.SetFloat(EPrefs.TempoAtual.ToDescription(), tempoAtual);

        if (tempoAtual > 0)        
            return;        

        gameOver = true;
        SalvarPontuacao();
    }

    public void SalvarPontuacao()
    {
        if (PlayerPrefs.GetInt(EPrefs.MelhorPontuacao.ToDescription()) < pontos)
            PlayerPrefs.SetInt(EPrefs.MelhorPontuacao.ToDescription(), pontos);

        PlayerPrefs.SetInt(EPrefs.Ponto.ToDescription(), pontos);

        SoundManager.instance.InicializarSom(SoundManager.instance.audioMorte);

        Invoke(nameof(NavegarGameOver), 2f);
    }

    public void NavegarGameOver() => SceneManager.LoadScene(ECena.GameOverScene.ToDescription());
    private void NavegarProximaFase(ECena cena) => SceneManager.LoadScene(cena.ToDescription());
    void LimparDescricaoFase() => descricaoFase.text = string.Empty;

    public void Toque()
    {
        if (!gameOver)
        {
            CortarTronco();
            PosicionarTronco();
        }
    }
}
