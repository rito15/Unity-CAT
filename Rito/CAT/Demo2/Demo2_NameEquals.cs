using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rito.CAT.Demo
{

    public class Demo2_NameEquals : MonoBehaviour
    {
        [EditorDI(nameIncludes:"Col")]
        public Collider col1;

        [EditorDI(nameIncludes:"Col", NameEquals = "Col9")]
        public Collider col2;

        [EditorDI(nameIncludes:"Col", NameEquals = "col9", IgnoreCase = false)]
        public Collider col3_fail;

        [EditorDI(nameIncludes:"Col", NameEquals = "col9")]
        public Collider col4_succ;
    }
}