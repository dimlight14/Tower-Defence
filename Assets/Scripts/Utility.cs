using UnityEngine;

namespace TowerDefence
{
    public static class Utility
    {
        public static T GetComponentSafely<T>(GameObject _object, bool includingInactive = true) where T : Component {
            T returnObject = _object.GetComponent<T>();
            if (returnObject == null) {
                returnObject = _object.GetComponentInChildren<T>(includingInactive);
                if (returnObject == null) {
                    returnObject = _object.GetComponentsInParent<T>(includingInactive)[0];
                    if (returnObject == null) {
                        Debug.LogError($"Can't find an object of type {typeof(T)}.");
                    }
                }
            }

            return returnObject;
        }
    }

    public static class GameLayers
    {
        public static int IgnoreRaycast = 2;
        public static int InputRecieverLayer = 8;
    }
}