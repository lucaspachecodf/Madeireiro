using System.ComponentModel;

namespace Assets.Scripts.Enum
{
    public enum ECena
    {
        [Description("GameScene")]
        PrimeiraFase,
        [Description("GameScenaTwo")]
        SegundaFase,
        [Description("GameSceneThree")]
        TerceiraFase,
        [Description("GameOverScene")]
        GameOverScene
    }
}
