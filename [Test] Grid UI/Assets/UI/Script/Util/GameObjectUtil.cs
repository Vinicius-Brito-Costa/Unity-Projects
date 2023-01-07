using UnityEngine;

namespace Util
{
    public class GameObjectUtil
    {
        private static GameObjectUtil _instance;
        private GameObjectUtil() { }

        public static GameObjectUtil Instance()
        {
            if (_instance == null)
            {
                _instance = new GameObjectUtil();
            }
            return _instance;
        }

        public GameObject getChildGameObject(GameObject fromGameObject, string withName)
        {
            int childCount = fromGameObject.transform.childCount;
            for (int index = 0; index < childCount; index++)
            {
                GameObject child = fromGameObject.transform.GetChild(index).gameObject;
                if (child.name == withName)
                {
                    return child;
                }
            }
            return null;
        }
    }
}