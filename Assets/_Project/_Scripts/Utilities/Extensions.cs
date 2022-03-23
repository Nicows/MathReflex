using UnityEngine;

namespace _Scripts
{
    public static class Extensions 
    {
        public static void DestroyChildren(this Transform t) {
            foreach (Transform child in t)
            {
                Object.Destroy(child.gameObject);
            }
        }

    }
}
