namespace Assets.Scripts.Enemies.BazaJednostek
{
    public enum EnemyState
    {
        WalkingUp,
        GettingIntoCastle,
        OnTheWizardsWall,
        Atacking,
        Dead
    }

    public enum LongTermEffect
    {
        Zero,
        Podpalenie,
        Zamrozenie,
        Otrucie
    }

    public enum MovementSpeedEnum
    {
        Normalnie,
        Spowolniony,
        BardzoSpowolniony,
        Zatrzymany
    }

    public enum RenderTypes
    {
        JustSingleDirection
    }
}