namespace UnityEngine
{
    public static class ExtensionsUnityObject
    {
        public static bool IsDestroyed(this Object instance)
        {
            // UnityEngine overloads the == opeator for the GameObject type
            // and returns null when the object has been destroyed, but 
            // actually the object is still there but has not been cleaned up yet
            // if we test both we can determine if the object has been destroyed.
            return instance == null && !ReferenceEquals(instance, null);
        }
    }
}