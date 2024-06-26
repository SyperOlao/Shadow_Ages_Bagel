using System;
using Enemy.FOV;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(FieldOfView))]
    public class FieldOfViewEditor : UnityEditor.Editor
    {
        private void OnSceneGUI()
        {
            var fov = (FieldOfView)target;
            var position = fov.transform.position;
            var radius = fov.Radius;
            Handles.color = Color.white;
            Handles.DrawWireArc(position, Vector3.up, Vector3.forward, 360, radius);
            var eulerY = fov.transform.eulerAngles.y;
            var angleHalf = fov.Angle / 2;
            var viewAngle01 = DirectionFromAngle(eulerY, angleHalf);
            var viewAngle02 = DirectionFromAngle(eulerY, -angleHalf);
            Handles.color = Color.yellow;
            Handles.DrawLine(position, position + viewAngle01 * radius);
            Handles.DrawLine(position, position + viewAngle02 * radius);
            
            if (!fov.CanSeePlayer) return;
            
            Handles.color = Color.green;
            Handles.DrawLine(position, fov.PlayerRef.transform.position);
        }


        private static Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
        {
            angleInDegrees += eulerY;
            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }
    }
}