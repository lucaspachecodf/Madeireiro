using Assets.Scripts.Enum;
using Assets.Scripts.Util;
using UnityEngine;

public class Personagem : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private string ladoAtual = ELadoPersonagem.Esquerdo.ToDescription();

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.gameOver)
        {
            spriteRenderer.flipX = false;
            animator.Play(EAnimacao.Morto.ToDescription());
        }
    }

    void MudarPosicao(string novaPosicao)
    {
        if (ladoAtual != novaPosicao)
        {
            transform.position = new Vector3(-transform.position.x, transform.position.y, transform.position.z);
            spriteRenderer.flipX = !spriteRenderer.flipX;

            ladoAtual = novaPosicao;

            if (GameManager.instance.fase == (int)EFase.PrimeiraFase)
                TroncoBase.instance.spriteRenderer.flipX = spriteRenderer.flipX;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        GameManager.instance.gameOver = true;
        GameManager.instance.SalvarPontuacao();
    }

    public void Toque(string lado)
    {
        if (!GameManager.instance.gameOver)
        {
            MudarPosicao(lado);
            animator.Play(EAnimacao.Cortar.ToDescription());
        }
    }
}
