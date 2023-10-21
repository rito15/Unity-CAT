using UnityEngine;

namespace Rito.CAT.Demo
{
    // 날짜 : 2021-01-17 AM 12:52:11
    public class Demo_Readonly : MonoBehaviour
    {
        [Readonly]
        public GameObject alwaysReadonly;

        [Readonly(ReadOnlyOption.EditMode)]
        public float readOnlyInEditMode;

        [Readonly(ReadOnlyOption.PlayMode)]
        public Rigidbody readOnlyInPlayMode;
    }
}