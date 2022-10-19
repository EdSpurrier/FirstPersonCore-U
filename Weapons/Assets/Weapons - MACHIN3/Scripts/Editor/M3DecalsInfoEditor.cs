using UnityEngine;
using UnityEditor;
using UnityEditor.Rendering;


public class M3DecalsInfoEditor : ShaderGUI {
        MaterialProperty colorAlphaMap;

        MaterialProperty metallic;
        MaterialProperty smoothness;

        MaterialProperty invert;
        MaterialProperty opacity;

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

                colorAlphaMap = FindProperty("_ColorMap", properties);

                metallic = FindProperty("_Metalness", properties);
                smoothness = FindProperty("_Smoothness", properties);

                invert = FindProperty("_ColorInvert", properties);
                opacity = FindProperty("_Opacity", properties);

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

                DrawPBR(materialEditor);
                DrawSpecial(materialEditor);
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
                r.x += Screen.width / 2 - r.width / 2;

                r.width = 100;
                r.x -= 7;
                GUI.Label(r, "(RGB) Color");
                r.y += 12;
                GUI.Label(r, "(A) Alpha");
                r.x += 7;
                r.y += 20;
                r.width = 50;
                materialEditor.TextureProperty(r, colorAlphaMap, "", false);

                GUILayout.Space(80);
        }

        void DrawPBR(MaterialEditor materialEditor) {
                GUILayout.BeginVertical(boxStyle);
                GUILayout.Label("PBR", titleStyle);

                materialEditor.ShaderProperty(metallic, metallic.displayName);
                materialEditor.ShaderProperty(smoothness, smoothness.displayName);

                GUILayout.EndVertical();
        }

        void DrawSpecial(MaterialEditor materialEditor) {
                GUILayout.BeginVertical(boxStyle);
                GUILayout.Label("Special", titleStyle);

                materialEditor.ShaderProperty(invert, invert.displayName);
                materialEditor.ShaderProperty(opacity, opacity.displayName);

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
