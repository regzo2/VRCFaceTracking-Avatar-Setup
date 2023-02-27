using UnityEngine;

namespace VRCFaceTracking.Tools.Avatar_Setup
{
    public static class GameObjectExtensions
    {
        /// <summary>
        /// Returns the full hierarchy name of the game object.
        /// </summary>
        /// <param name="go">The game object.</param>
        public static string GetHeirarchy(this GameObject go)
        {
            string name = go.name;
            string parentName = "";
            while (go.transform.parent != null)
            {
                go = go.transform.parent.gameObject;
                name = parentName + name;
                parentName = go.name + "/";
            }
            return name;
        }
    }
}
