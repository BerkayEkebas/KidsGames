using UnityEngine;

namespace GameScripts.MoleGameScripts
{
    public class DestroyAnimation : MonoBehaviour
    {
        private const float destroyTime = 1f;

        void Start()
        {
            Destroy(gameObject, destroyTime);
        }
    }
}