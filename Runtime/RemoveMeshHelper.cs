using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Anatawa12.AvatarOptimizer;
using UnityEngine.EventSystems;
using VRC.SDKBase;

namespace nyakomake.ModularLegAndArm
{
    [ExecuteAlways]
    public class RemoveMeshHelper : MonoBehaviour, VRC.SDKBase.IEditorOnly
    {
        public GameObject attachObject;
        public RemoveMeshInBox removeMeshInBox;
        public int selectNum;
        public bool deleteMeshBoxOnDestroy = true;
        public GameObject pivotObj;

        [System.Serializable]
        public struct RemoveMeshBox
        {
            public Vector3 Center;
            public Vector3 Size;
            public Vector3 Rotation;
        }

        public RemoveMeshBox removeMeshBox;

        void Reset()
        {
            removeMeshBox.Center = new Vector3(0, 1f, 0);
            removeMeshBox.Size = Vector3.one;
            removeMeshBox.Rotation = Vector3.zero;
            deleteMeshBoxOnDestroy = true;

        }

#if UNITY_EDITOR
        void OnEnable()
        {

            // Hierarchy変更イベント購読
            EditorApplication.hierarchyChanged += OnHierarchyChanged;
            deleteMeshBoxOnDestroy = true;
            foreach (Transform t in this.transform)
            {
                if (t.name == "removeMeshHelperPivot_Do not delete!") pivotObj = t.gameObject;
            }
            if (pivotObj == null)
            {
                var prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Packages/com.nyakomake.modular-leg-and-arm/pivot.prefab");
                pivotObj = UnityEditor.PrefabUtility.InstantiatePrefab(prefab) as GameObject;
                pivotObj.name = "removeMeshHelperPivot_Do not delete!";
                pivotObj.transform.SetParent(this.transform);
                pivotObj.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
                pivotObj.transform.localScale = Vector3.one;
            }

        }

        void OnDisable()
        {
            // イベント購読解除
            EditorApplication.hierarchyChanged -= OnHierarchyChanged;

        }

        private void OnHierarchyChanged()
        {
            if (!Application.isPlaying)
            {
                var rootObj = GetRootTransform();
                if (rootObj == null)
                {
                    if (removeMeshInBox != null) Undo.DestroyObjectImmediate(removeMeshInBox);
                }
            }
        }
#endif
        Transform GetRootTransform()
        {
            var rootObject = transform;
            while (rootObject != null)
            {
                if (rootObject.GetComponent<VRC_AvatarDescriptor>() != null) break;
                rootObject = rootObject.transform.parent;
            }
            return rootObject;
        }
#if UNITY_EDITOR
        void OnDestroy()
        {
            if (!deleteMeshBoxOnDestroy) return;
            if (removeMeshInBox != null) Undo.DestroyObjectImmediate(removeMeshInBox);
            if (pivotObj != null) Undo.DestroyObjectImmediate(pivotObj);
        }
#endif


    }
}