#if UNITY_EDITOR
using UnityEditor;

// 날짜 : 2023-10-22 오후 4:44:04
// 작성자 : Rito15

namespace Rito.CAT.EditorDrawer
{
    public class RitoPropertyDrawer<T> : PropertyDrawer where T : System.Attribute
    {
        protected T Atr
        {
            get
            {
                if (_atr == null)
                    _atr = attribute as T;
                return _atr;
            }
        }
        private T _atr;
    }
}
#endif