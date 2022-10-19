using UnityEngine;
using UnityEditor;
using UnityEditor.Rendering;


public class M3DecalsPanelEditor : ShaderGUI {
        MaterialProperty aoCurvHeightSubsetMap;
        MaterialProperty normalAlphaMap;

        MaterialProperty ao;

        MaterialProperty blendSurfaceNormals;
        MaterialProperty offset;

        GUIStyle titleStyle;
        GUIStyle boxStyle;
        Texture2D boxTexture;
        Texture2D MACHIN3logo;

        // set to 'true' to have the 'Advanced Options' foldout open by default
        bool advFold = false;

        public override void OnGUI (MaterialEditor materialEditor, MaterialProperty[] properties) {
                // uncomment the following line to see default Material Inspector
                //base.OnGUI (materialEditor, properties);

                Init(materialEditor, properties);
                DeferredCheck();
                ColorSpaceCheck();
                Draw(materialEditor);
        }

        void Init (MaterialEditor materialEditor, MaterialProperty[] properties) {
                MACHIN3logo = (Texture2D)EditorGUIUtility.Load ("Assets/MACHIN3/Textures/Editor/MACHIN3logo.png");

                aoCurvHeightSubsetMap = FindProperty("_AoCurvHeightSubset", properties);
                normalAlphaMap = FindProperty("_NormalAlpha", properties);

                ao = FindProperty("_AOStrength", properties);

                blendSurfaceNormals = FindProperty("_BlendSurfaceNormals", properties);
                offset = FindProperty("_Offset", properties);

                titleStyle = new GUIStyle();
                titleStyle.fontSize = 12;
                titleStyle.alignment = TextAnchor.MiddleCenter;

                boxTexture = new Texture2D(1, 1);
                boxTexture.SetPixel(1, 1, new Color(0, 0, 0, 0.05f));
                boxTexture.Apply();

                boxStyle = new GUIStyle();
                boxStyle.normal.background = boxTexture;
                boxStyle.margin = new RectOffset(4,4,8,4);
        }

        void DeferredCheck () {
                TierSettings tier = EditorGraphicsSettings.GetTierSettings(EditorUserBuildSettings.selectedBuildTargetGroup, Graphics.activeTier);

                if (tier.renderingPath != RenderingPath.DeferredShading) {
                        EditorGUILayout.HelpBox("This shader only works with Deferred Rendering. Adjust your Project Settings accordingly.", MessageType.Error);
                }
        }

        void ColorSpaceCheck () {
                if (PlayerSettings.colorSpace != ColorSpace.Linear) {
                        EditorGUILayout.HelpBox("This shader is supposed to be used in Linear Color Space. Change your Project Settings accordingly.", MessageType.Warning);
                }
        }

        void Draw (MaterialEditor materialEditor) {
                DrawLogo();

                DrawMaps(materialEditor);

                DrawAO(materialEditor);
                DrawDepth(materialEditor);

                DrawAdvanced(materialEditor);
        }

        void DrawLogo () {
                Rect r = EditorGUILayout.GetControlRect();
                r.x += Screen.width / 2 - 50;
                r.width = 100;
                r.height = 14;

                GUI.DrawTexture(r, MACHIN3logo);
        }

        void DrawMaps (MaterialEditor materialEditor) {
                Rect r = EditorGUILayout.GetControlRect();
                r.width = 50;
                r.x += Screen.width / 4 - r.width / 2;

                r.width = 100;
                r.x -= 20;

                GUI.Label(r, "(R) AO");
                r.y += 12;
                GUI.Label(r, "(G) Curvature");
                r.y += 12;
                GUI.Label(r, "(B) Height");
                r.y += 12;
                GUI.Label(r, "(A) Subset Mask");
                r.y += 20;
                r.width = 50;
                r.x += 20;
                materialEditor.TextureProperty(r, aoCurvHeightSubsetMap, "", false);

                r.x += Screen.width / 2;
                r.width = 100;
                r.x -= 20;
                r.y -= 32;
                GUI.Label(r, "(RGB) Normal");
                r.y += 12;
                GUI.Label(r, "(A) Decal Alpha");
                r.y += 20;
                r.width = 50;
                r.x += 20;
                materialEditor.TextureProperty(r, normalAlphaMap, "", false);

                GUILayout.Space(110);
        }

        void DrawAO(MaterialEditor materialEditor) {
                GUILayout.BeginVertical(boxStyle);
                GUILayout.Label("AO", titleStyle);

                materialEditor.ShaderProperty(ao, "Ambient Occlusion");

                GUILayout.EndVertical();
        }

        void DrawDepth(MaterialEditor materialEditor) {
                GUILayout.BeginVertical(boxStyle);
                GUILayout.Label("Depth", titleStyle);

                materialEditor.ShaderProperty(blendSurfaceNormals, blendSurfaceNormals.displayName);
                materialEditor.ShaderProperty(offset, offset.displayName);

                GUILayout.EndVertical();
        }

        void DrawAdvanced(MaterialEditor materialEditor) { 
                GUILayout.BeginVertical(boxStyle);
                EditorGUI.indentLevel++;

                Rect r = EditorGUILayout.GetControlRect();
                advFold = EditorGUI.Foldout(r, advFold, "Advanced Options", true);

                if (advFold) {
                        EditorGUI.indentLevel++;
                        materialEditor.RenderQueueField();
                        materialEditor.EnableInstancingField();
                        materialEditor.DoubleSidedGIField();
                        EditorGUI.indentLevel--;
                }
                EditorGUI.indentLevel--;
                GUILayout.EndVertical();
        }
}
