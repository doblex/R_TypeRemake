using UnityEngine;

namespace utilities 
{
    public static class GameObjectExtensions
    {
        public static T GetOrAdd<T>(this GameObject gameObject) where T : Component
        {
            T component = gameObject.GetComponent<T>();
            return component != null ? component : gameObject.AddComponent<T>();
        }
    }

    public static class VectorExtension
    {
        public static Vector3 V2ToV3(Vector2 vector, float z = 0)
        { 
            return new Vector3(vector.x, vector.y, z);
        }
    }
}