using Rito.CAT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rito.CAT.Demo
{
    // 2023-07-15
    // 작성자 : Rito
    public class Demo_EditorDIGetComChild_NC : MonoBehaviour
    {
        [EditorDI(DiMethod.GetComponentInChildren, nameIncludes: "NONO", evenDisabled: false)]
        public CapsuleCollider _children_Fail1;

        [EditorDI(DiMethod.GetComponentInChildren, nameIncludes: "DI", evenDisabled: false)]
        public CapsuleCollider _children_Suc1;

        [EditorDI(DiMethod.GetComponentInChildrenOnly, nameIncludes: "DI", evenDisabled: false)]
        public CapsuleCollider _children_Fail2;

        [EditorDI(DiMethod.GetComponentInChildren, nameIncludes: "Cap", evenDisabled: false)]
        public CapsuleCollider _children_Suc2;

        [EditorDI(DiMethod.GetComponentInChildrenOnly, nameIncludes: "Cap", evenDisabled: false)]
        public CapsuleCollider _children_Suc3;



        [EditorDI(DiMethod.GetComponentInChildren, nameIncludes: "DI", evenDisabled:true)]
        public CapsuleCollider _children_dsv_Suc1;

        [EditorDI(DiMethod.GetComponentInChildrenOnly, nameIncludes: "DI", evenDisabled: true)]
        public CapsuleCollider _children_dsv_Fail1;

        [EditorDI(DiMethod.GetComponentInChildren, nameIncludes: "Cap", evenDisabled: true)]
        public CapsuleCollider _children_dsv_Suc2;

        [EditorDI(DiMethod.GetComponentInChildrenOnly, nameIncludes: "Cap", evenDisabled: true)]
        public CapsuleCollider _children_dsv_Suc3;
    }
}
