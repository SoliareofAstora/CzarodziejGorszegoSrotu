using Assets.Scripts.Enemies.BazaJednostek;

public class BardzoBrzydkiTroll : MeleUnit
{
    public BardzoBrzydkiTroll()
    {
        BaseSpeed = 40;
        DeathAnimationDuration = 5;
        maxHP = 200;
        renderType = RenderTypes.JustSingleDirection;
    }
}