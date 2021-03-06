// =====================================================================
// Copyright 2013-2015 Fluffy Underware
// All rights reserved
// 
// http://www.fluffyunderware.com
// =====================================================================

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace FluffyUnderware.DevToolsEditor
{
    public static class DTHandles
    {
        public static class SnapSettings
        {
            private static float s_MoveSnapX;
		    private static float s_MoveSnapY;
		    private static float s_MoveSnapZ;
		    private static float s_ScaleSnap;
		    private static float s_RotationSnap;
		    private static bool s_Initialized;

            private static void Initialize()
		    {
			    if (!s_Initialized)
			    {
				    s_MoveSnapX = EditorPrefs.GetFloat("MoveSnapX", 1f);
				    s_MoveSnapY = EditorPrefs.GetFloat("MoveSnapY", 1f);
				    s_MoveSnapZ = EditorPrefs.GetFloat("MoveSnapZ", 1f);
				    s_ScaleSnap = EditorPrefs.GetFloat("ScaleSnap", 0.1f);
				    s_RotationSnap = EditorPrefs.GetFloat("RotationSnap", 15f);
				    s_Initialized = true;
			    }
		    }

            public static Vector3 Move
            {
                get { return new Vector3(MoveX, MoveY, MoveZ); }
            }
            public static float MoveX
            {
                get { Initialize();return s_MoveSnapX; }
            }
            public static float MoveY
            {
                get { Initialize();return s_MoveSnapY; }
            }
            public static float MoveZ
            {
                get { Initialize();return s_MoveSnapZ; }
            }
            public static float Rotation
            {
                get { Initialize();return s_RotationSnap; }
            }
            public static float ScaleSnap
            {
                get { Initialize(); return s_ScaleSnap; }
            }
        }

        public static bool MouseOverSceneView
        {
            get
            {
                return (SceneView.currentDrawingSceneView != null && Event.current != null) ?
                    SceneView.currentDrawingSceneView.position.Contains(GUIUtility.GUIToScreenPoint(Event.current.mousePosition)) : false;
            }
        }

        public static bool SceneViewIsSelected
        {
            get
            {
                return SceneView.focusedWindow == SceneView.currentDrawingSceneView;
            }
        }

        static Stack<Color> mHandlesColorstack = new Stack<Color>();

        public static void PushHandlesColor(Color col)
        {
            mHandlesColorstack.Push(GUI.color);
            Handles.color = col;
        }

        public static void PopHandlesColor()
        {
            Handles.color = mHandlesColorstack.Pop();
        }
        
        public static Quaternion RotationHandle(Quaternion rotation, Vector3 position, float size)
        {
            Color staticColor = new Color(0.5f, 0.5f, 0.5f, 0f);
		    float staticBlend = 0.6f;
            float handleSize = HandleUtility.GetHandleSize(position)*size;
            Color color = Handles.color;
            bool flag = !Tools.hidden && EditorApplication.isPlaying && ContainsStatic(Selection.gameObjects);
            Handles.color = ((!flag) ? Handles.xAxisColor : Color.Lerp(Handles.xAxisColor, staticColor, staticBlend));
            rotation = Handles.Disc(rotation, position, rotation * Vector3.right, handleSize, true, SnapSettings.Rotation);
            Handles.color = ((!flag) ? Handles.yAxisColor : Color.Lerp(Handles.yAxisColor, staticColor, staticBlend));
            rotation = Handles.Disc(rotation, position, rotation * Vector3.up, handleSize, true, SnapSettings.Rotation);
            Handles.color = ((!flag) ? Handles.zAxisColor : Color.Lerp(Handles.zAxisColor, staticColor, staticBlend));
            rotation = Handles.Disc(rotation, position, rotation * Vector3.forward, handleSize, true, SnapSettings.Rotation);
            if (!flag)
            {
                Handles.color = Handles.centerColor;
                rotation = Handles.Disc(rotation, position, Camera.current.transform.forward, handleSize * 1.1f, false, 0f);
                rotation = Handles.FreeRotateHandle(rotation, position, handleSize);
            }
            Handles.color = color;
            return rotation;
        }

        public static Vector3 PositionHandle2D(int id, Vector3 position, Quaternion rotation, float size)
        {
            float rectSize = HandleUtility.GetHandleSize(position) * size * 0.15f;
            float sliderSize = HandleUtility.GetHandleSize(position) * size;

            Vector3 snap = SnapSettings.Move;

            Vector3 newPos = position;
            Handles.color = new Color(0.9f, 0.3f, 0.1f);
            newPos += Handles.Slider(position, rotation * Vector3.right, sliderSize, Handles.ArrowCap, snap.x) - position;

            Handles.color = new Color(0.6f, 0.9f, 0.3f);
            newPos += Handles.Slider(position, rotation * Vector3.up, sliderSize, Handles.ArrowCap, snap.y) - position;

            Handles.color = new Color(0.2f, 0.4f, 0.9f);
            newPos += Handles.Slider2D(id, position, rotation * new Vector3(rectSize, rectSize, 0), rotation * Vector3.forward, rotation * Vector3.up, rotation * Vector3.right, rectSize, Handles.RectangleCap, new Vector2(snap.x, snap.y)) - position;

            return newPos;
        }

        public static Vector3 TinyHandle2D(int id, Vector3 position, Quaternion rotation, float size, Handles.DrawCapFunction func = null)
        {
            return TinyHandle2D(id, position, rotation * Vector3.forward, rotation * Vector3.up, rotation * Vector3.right, size, func);
        }

        public static Vector3 TinyHandle2D(int id, Transform transform, float size, Handles.DrawCapFunction func = null)
        {
            return TinyHandle2D(id, transform.position, transform.forward, transform.up, transform.right, size, func);
        }

        public static Vector3 TinyHandle2D(int id, Vector3 pos, Vector3 forward, Vector3 up, Vector3 right, float size, Handles.DrawCapFunction func = null)
        {
            return Handles.Slider2D(id, pos, forward, up, right, HandleUtility.GetHandleSize(pos) * size, func, SnapSettings.Move);
        }

        public static void DrawSolidRectangleWithOutline(Vector2 center, Vector2 extends, Color backgroundColor, Color outlineColor)
        {
            DrawSolidRectangleWithOutline(new Rect(center.x-extends.x/2,center.y-extends.y/2,extends.x,extends.y), backgroundColor, outlineColor);
        }

        public static void DrawSolidRectangleWithOutline(Rect r, Color backgroundColor, Color outlineColor)
        {
            Vector3[] verts=new Vector3[4]
            {
                new Vector3(r.xMin,r.yMin,0),
                new Vector3(r.xMax,r.yMin,0),
                new Vector3(r.xMax,r.yMax,0),
                new Vector3(r.xMin,r.yMax,0)
            };
            Handles.DrawSolidRectangleWithOutline(verts, backgroundColor, outlineColor);
        }

        public static void DrawOutline(Rect r, Color outlineColor)
        {
            DrawSolidRectangleWithOutline(r, new Color(0, 0, 0, 0), outlineColor);
        }

        public static void WireCubeCap(Vector3 position, Vector3 size)
        {
            var half = size / 2;
            // draw front
            Handles.DrawLine(position + new Vector3(-half.x, -half.y, half.z), position + new Vector3(half.x, -half.y, half.z));
            Handles.DrawLine(position + new Vector3(-half.x, -half.y, half.z), position + new Vector3(-half.x, half.y, half.z));
            Handles.DrawLine(position + new Vector3(half.x, half.y, half.z), position + new Vector3(half.x, -half.y, half.z));
            Handles.DrawLine(position + new Vector3(half.x, half.y, half.z), position + new Vector3(-half.x, half.y, half.z));
            // draw back
            Handles.DrawLine(position + new Vector3(-half.x, -half.y, -half.z), position + new Vector3(half.x, -half.y, -half.z));
            Handles.DrawLine(position + new Vector3(-half.x, -half.y, -half.z), position + new Vector3(-half.x, half.y, -half.z));
            Handles.DrawLine(position + new Vector3(half.x, half.y, -half.z), position + new Vector3(half.x, -half.y, -half.z));
            Handles.DrawLine(position + new Vector3(half.x, half.y, -half.z), position + new Vector3(-half.x, half.y, -half.z));
            // draw corners
            Handles.DrawLine(position + new Vector3(-half.x, -half.y, -half.z), position + new Vector3(-half.x, -half.y, half.z));
            Handles.DrawLine(position + new Vector3(half.x, -half.y, -half.z), position + new Vector3(half.x, -half.y, half.z));
            Handles.DrawLine(position + new Vector3(-half.x, half.y, -half.z), position + new Vector3(-half.x, half.y, half.z));
            Handles.DrawLine(position + new Vector3(half.x, half.y, -half.z), position + new Vector3(half.x, half.y, half.z));
        }

        public static void BoundsCap(Bounds b)
        {
            WireCubeCap(b.center, b.size);
        }

        public static Vector3 TranslateByPixel(Vector3 position, float x, float y)
        {
            return TranslateByPixel(position, new Vector3(x, y));
        }
        public static Vector3 TranslateByPixel(Vector3 position, Vector3 translateBy)
        {
            Camera cam = SceneView.currentDrawingSceneView.camera;
            if (cam)
                return cam.ScreenToWorldPoint(cam.WorldToScreenPoint(position) + translateBy);
            else
                return position;
        }

        static bool ContainsStatic(GameObject[] objects)
        {
            if (objects == null || objects.Length == 0)
            {
                return false;
            }
            for (int i = 0; i < objects.Length; i++)
            {
                if (objects[i] != null && objects[i].isStatic)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
