using Assets.Scripts.Enemies.BazaJednostek;

public class Rycerzyk : MeleUnit
{
    public Rycerzyk()
    {
        BaseSpeed = 60;
        DeathAnimationDuration = 2;
        maxHP = 200;
        renderType = RenderTypes.JustSingleDirection;
    }
}

/*

float tg = DirectionVector.x / DirectionVector.y;
        if (tg< -1)
        {
            anim.SetBool("WalkingLeft", true);
        }
        else if (tg > 1)
        {
            anim.SetBool("WalkingRight", true);
        }
        else
        {
            anim.SetBool("WalkingDown", true);
        }
        */
/*    void EnemyExplode()
{
Instantiate(enemyExplosion, transform.position, Quaternion.identity);
Instantiate(enemyDestroyShield, transform.position, Quaternion.identity);
}

void ClearHierarchy()
{
foreach (GameObject o in Object.FindObjectsOfType<GameObject>()) 
   if (o.tag == "Clear")
       Destroy(o);
}
*/