using UnityEditor;
using UnityEngine;

namespace AssetUsageFinder.Styles {
    class WindowStyleAsset : ScriptableObject {
        public DependencyWindow.Style Pro;
        public DependencyWindow.Style Personal;

#if false
        [CustomEditor(typeof (DependencyStyle))]
        private class Editor : UnityEditor.Editor
        {
            public override void OnInspectorGUI()
            {
                EditorGUI.BeginChangeCheck();
                base.OnInspectorGUI();
                if (EditorGUI.EndChangeCheck())
                    InternalEditorUtility.RepaintAllViews();
            }
        }
#endif
    }
}