namespace Rope2d
{
    using UnityEngine;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using pancake.Rope2DEditor;
#if UNITY_EDITOR
    using UnityEditor;

#endif

    public class Chain : MonoBehaviour
    {
        public LineRenderer lr;


        private void Reset() { this.AutoGet(out lr); }

        public RopeNode[] GetNodes() { return GetComponentsInChildren<RopeNode>(); }
    }


#if UNITY_EDITOR
    [CustomEditor(typeof(Chain))]
    [CanEditMultipleObjects]
    public class ChainEditor : Editor
    {
        bool editingRope;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var chain = target as Chain;

            EditorUtilEx.SubSection("Rope editor");

            var col = GUI.color;
            if (editingRope) GUI.color = new Color(0.35f, 0.55f, 0.95f);
            if (GUILayout.Button("Edit chain", EditorStyles.toolbarButton)) editingRope = !editingRope;
            GUI.color = col;
            EditorUtilEx.MiniBoxedSection("Edit chain helps",
                () =>
                {
                    if (editingRope)
                    {
                        EditorGUILayout.LabelField("Shift to add node");
                        EditorGUILayout.LabelField("Ctrl to delete node");
                    }
                });
        }

        private void OnSceneGUI()
        {
            if (!editingRope) return;
            if (!SceneViewUtils.Get2DMouseScenePosition(out var pos)) return;

            var chain = target as Chain;
            var nodes = chain.GetNodes();
            var ec = Event.current;
            bool isClick = ec.type == EventType.MouseUp && ec.button == 0;

            if (nodes?.Length > 0)
            {
                Handles.color = new Color(1, 1, 1, 0.25f);
                Handles.DrawPolyLine(nodes.Select(n => n.transform.position).ToArray());
            }

            if (ec.shift)
            {
            }
            else if (ec.control)
            {
            }

            if (ec.type == EventType.MouseDown || ec.type == EventType.MouseUp) ec.Use();
        }
    }

#endif
}