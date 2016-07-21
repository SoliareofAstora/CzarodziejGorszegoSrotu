using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Spells
{
    public class Bańka : MonoBehaviour
    {
        public int Wytrzumałość = 200;
        private Animator anim;
        public Text Hapeki;
        private Quaternion rotation;
        // Use this for initialization
        void Start()
        {
            anim = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            rotation = new Quaternion(0, transform.rotation.y - 90, 0, 0);

            if (Wytrzumałość > 0)
            {
                Hapeki.text = "Bańka's hp = " + Wytrzumałość;
            } else
            {
                Hapeki.text = "Bańka's hp = 0";
                anim.SetBool("Zepsute", true);
            }
        }

        public bool Hitit(int dmg)
        {
            Wytrzumałość -= dmg;
            return Wytrzumałość > 0;
        }

        void LateUpdate()
        {
            transform.rotation = rotation;
        }
    }
}