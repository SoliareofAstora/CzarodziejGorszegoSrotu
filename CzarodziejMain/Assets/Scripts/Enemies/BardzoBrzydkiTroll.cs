using BaseUnit;

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