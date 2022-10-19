using UnityEngine;
using UnityEditor;


public class M3SurfaceEditor : ShaderGUI {
        MaterialProperty grungeMap;
        MaterialProperty masksMap;
        MaterialProperty colorMap;
        MaterialProperty normalMap;
        MaterialProperty metallicRoughnessMap;

        MaterialProperty Tile;
        MaterialProperty Rotate;

        MaterialProperty color;
        MaterialProperty metallic;
        MaterialProperty roughness;
        MaterialProperty ao;

        MaterialProperty normalIntensity;
        MaterialProperty normalWearAmount;
                                         
        MaterialProperty grungeIntensity;
        MaterialProperty grungeMaskTiling;
        MaterialProperty grunge1Tiling;
        MaterialProperty grunge2Tiling;

        MaterialProperty wearColor;
        MaterialProperty wearMetallic;
        MaterialProperty wearRoughness;
        MaterialProperty wearAmount;
        MaterialProperty wearGrungeTiling;
        MaterialProperty wearHardness;
        MaterialProperty wearDecals;
        MaterialProperty wearGeometry;
                                         
        MaterialProperty dirtColor;
        MaterialProperty dirtMetallic;
        MaterialProperty dirtRoughness;
        MaterialProperty dirtAmount;
        MaterialProperty dirtHardness;
        MaterialProperty dirtDecals;
        MaterialProperty dirtGeometry;
        MaterialProperty grungeToDirt;

        MaterialProperty vertexColorWear;
        MaterialProperty vertexColorDirt;

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
                ColorSpaceCheck();
                Draw(materialEditor);
        }

        void Init (MaterialEditor materialEditor, MaterialProperty[] properties) {
                MACHIN3logo = (Texture2D)EditorGUIUtility.Load ("Assets/MACHIN3/Textures/Editor/MACHIN3logo.png");

                grungeMap = FindProperty("_GrungeMap", properties);
                masksMap = FindProperty("_MasksMap", properties);
                colorMap = FindProperty("_ColorMap", properties);
                normalMap = FindProperty("_NormalMap", properties);
                metallicRoughnessMap = FindProperty("_MetallicRoughnessMap", properties);

                Tile = FindProperty("_Tile", properties);
                Rotate = FindProperty("_Rotate", properties);

                color = FindProperty("_Color", properties);
                metallic = FindProperty("_Metallic", properties);
                roughness = FindProperty("_Roughness", properties);
                ao = FindProperty("_AmbientOcclusion", properties);

                normalIntensity = FindProperty("_NormalIntensity", properties);
                normalWearAmount = FindProperty("_NormalWearAmount", properties);

                grungeIntensity = FindProperty("_GrungeIntensity", properties);
                grungeMaskTiling = FindProperty("_GrungeMaskTiling", properties);
                grunge1Tiling = FindProperty("_Grunge1Tiling", properties);
                grunge2Tiling = FindProperty("_Grunge2Tiling", properties);

                wearColor = FindProperty("_WearColor", properties);
                wearMetallic = FindProperty("_WearMetallic", properties);
                wearRoughness = FindProperty("_WearRoughness", properties);
                wearAmount = FindProperty("_WearAmount", properties);
                wearGrungeTiling = FindProperty("_WearGrungeTiling", properties);
                wearHardness = FindProperty("_WearHardness", properties);
                wearDecals = FindProperty("_WearDecals", properties);
                wearGeometry = FindProperty("_WearGeometry", properties);

                dirtColor = FindProperty("_DirtColor", properties);
                dirtMetallic = FindProperty("_DirtMetallic", properties);
                dirtRoughness = FindProperty("_DirtRoughness", properties);
                dirtAmount = FindProperty("_DirtAmount", properties);
                dirtHardness = FindProperty("_DirtHardness", properties);
                dirtDecals = FindProperty("_DirtDecals", properties);
                dirtGeometry = FindProperty("_DirtGeometry", properties);
                grungeToDirt = FindProperty("_GrungeToDirt", properties);

                vertexColorWear = FindProperty("_VertexColorWear", properties);
                vertexColorDirt = FindProperty("_VertexColorDirt", properties);

                titleStyle = new GUIStyle();
                titleStyle.fontSize = 12;
                titleStyle.alignment = TextAnchor.MiddleCenter;

                boxTexture = new Texture2D(1, 1);
                boxTexture.SetPixel(1, 1, new Color(0, 0, 0, 0.05f));
                boxTexture.Apply();

                boxStyle = new GUIStyle();
                boxStyle.normal.background = boxTexture;
                boxStyle.margin = new RectOffset(4,4,8,4);

                if ( !grungeMap.textureValue) {
                        dirtAmount.floatValue = 0;
                }

                if ( metallicRoughnessMap.textureValue ) {
                        metallic.floatValue = 1;
                        roughness.floatValue = 1;
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

                if ( colorMap.textureValue || normalMap.textureValue || metallicRoughnessMap.textureValue ) {
                        DrawUVs(materialEditor);
                }

                DrawPBR(materialEditor);

                if ( normalMap.textureValue) {
                        DrawNormal(materialEditor);
                }


                if ( grungeMap.textureValue) {
                        DrawGrunge(materialEditor);
                }

                if ( masksMap.textureValue ) { 
                        DrawWear(materialEditor);
                }

                if ( (grungeMap.textureValue && masksMap.textureValue) || (grungeMap.textureValue && grungeToDirt.floatValue > 0)) { 
                        DrawDirt(materialEditor);
                }

                DrawVertexColors(materialEditor);

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

                GUI.Label(r, "Grunge");
                r.y += 20;
                materialEditor.TextureProperty(r, grungeMap, "", false);

                r.x += Screen.width / 5.4f;
                r.y -= 20;
                GUI.Label(r, "Masks");
                r.y += 20;
                materialEditor.TextureProperty(r, masksMap, "", false);

                r.x += Screen.width / 5.4f;
                r.y -= 20;
                GUI.Label(r, "Color");
                r.y += 20;
                materialEditor.TextureProperty(r, colorMap, "", false);

                r.x += Screen.width / 5.4f;
                r.y -= 20;
                GUI.Label(r, "Normal");
                r.y += 20;
                materialEditor.TextureProperty(r, normalMap, "", false);

                r.x += Screen.width / 5.4f;
                r.y -= 32;
                r.width = 80;
                GUI.Label(r, "(R)Metallic");
                r.y += 12;
                GUI.Label(r, "(A)Roughness");
                r.y += 20;
                r.width = 50;
                materialEditor.TextureProperty(r, metallicRoughnessMap, "", false);

                GUILayout.Space(70);
        }

        void DrawPBR(MaterialEditor materialEditor) {
                GUILayout.BeginVertical(boxStyle);
                GUILayout.Label("PBR", titleStyle);

                materialEditor.ShaderProperty(color, color.displayName);
                if ( colorMap.textureValue && color.colorValue != new Color(1, 1, 1, 1) ) {
                        EditorGUILayout.HelpBox("Non-white color values will tint the color map!", MessageType.Info);
                }

                if ( !metallicRoughnessMap.textureValue ) {
                        materialEditor.ShaderProperty(metallic, metallic.displayName);
                        materialEditor.ShaderProperty(roughness, roughness.displayName);
                }
                if ( masksMap.textureValue) {
                        materialEditor.ShaderProperty(ao, ao.displayName);
                }

                GUILayout.EndVertical();
        }

        void DrawUVs(MaterialEditor materialEditor) {
                GUILayout.BeginVertical(boxStyle);
                GUILayout.Label("UVs", titleStyle);

                materialEditor.ShaderProperty(Tile, "Tile");
                materialEditor.ShaderProperty(Rotate, "Rotate");

                GUILayout.EndVertical();
        }

        void DrawNormal(MaterialEditor materialEditor) {
                GUILayout.BeginVertical(boxStyle);
                GUILayout.Label("Normal", titleStyle);

                materialEditor.ShaderProperty(normalIntensity, "Intensity");
                if ( normalIntensity.floatValue > 0) {
                        EditorGUI.indentLevel++;
                        //materialEditor.ShaderProperty(Rotate, "Rotate");
                        //materialEditor.ShaderProperty(Tile, "Tile");
                        if ( masksMap.textureValue && wearAmount.floatValue > 0) {
                                materialEditor.ShaderProperty(normalWearAmount, "Wear Amount");
                        }
                        EditorGUI.indentLevel--;
                }

                GUILayout.EndVertical();
        }

        void DrawGrunge(MaterialEditor materialEditor) {
                GUILayout.BeginVertical(boxStyle);
                GUILayout.Label("Grunge", titleStyle);

                materialEditor.ShaderProperty(grungeIntensity, "Intensity");

                if ( grungeIntensity.floatValue > 0) {
                        EditorGUI.indentLevel++;
                        materialEditor.ShaderProperty(grungeMaskTiling, "Mask Tiling");
                        materialEditor.ShaderProperty(grunge1Tiling, "1 Tiling");
                        materialEditor.ShaderProperty(grunge2Tiling, "2 Tiling");
                        materialEditor.ShaderProperty(grungeToDirt, "Grunge To Dirt");
                        EditorGUI.indentLevel--;
                }

                GUILayout.EndVertical();
        }

        void DrawWear(MaterialEditor materialEditor) {
                GUILayout.BeginVertical(boxStyle);
                GUILayout.Label("Wear", titleStyle);

                if ( !grungeMap.textureValue && wearAmount.floatValue > 0) {

                            EditorGUILayout.HelpBox("Wear is best used in combination with a Grunge Map!", MessageType.Info);
                }

                materialEditor.ShaderProperty(wearAmount, "Intensity");

                if ( wearAmount.floatValue > 0) {
                        EditorGUI.indentLevel++;
                        materialEditor.ShaderProperty(wearDecals, "Decals");
                        materialEditor.ShaderProperty(wearGeometry, "Geometry");

                        GUILayout.Space(20);
                        materialEditor.ShaderProperty(wearHardness, "Hardness");
                        materialEditor.ShaderProperty(wearGrungeTiling, "Tiling");

                        GUILayout.Space(20);
                        materialEditor.ShaderProperty(wearColor, "Color");
                        materialEditor.ShaderProperty(wearMetallic, "Metallic");
                        materialEditor.ShaderProperty(wearRoughness, "Roughness");
                        EditorGUI.indentLevel--;
                }

                GUILayout.EndVertical();
        }

        void DrawDirt(MaterialEditor materialEditor) {
                GUILayout.BeginVertical(boxStyle);
                GUILayout.Label("Dirt", titleStyle);

                materialEditor.ShaderProperty(dirtAmount, "Intensity");

                if ( dirtAmount.floatValue > 0 || grungeToDirt.floatValue > 0 ) {
                        EditorGUI.indentLevel++;
                        materialEditor.ShaderProperty(dirtDecals, "Decals");
                        materialEditor.ShaderProperty(dirtGeometry, "Geometry");

                        GUILayout.Space(20);
                        materialEditor.ShaderProperty(dirtHardness, "Hardness");
                        materialEditor.ShaderProperty(grunge2Tiling, "Tiling");

                        GUILayout.Space(20);
                        materialEditor.ShaderProperty(dirtColor, "Color");
                        materialEditor.ShaderProperty(dirtMetallic, "Metallic");
                        materialEditor.ShaderProperty(dirtRoughness, "Roughness");
                        EditorGUI.indentLevel--;

                }
                GUILayout.EndVertical();
        }

        void DrawVertexColors(MaterialEditor materialEditor) {
                GUILayout.BeginVertical(boxStyle);
                GUILayout.Label("Vertex Colors", titleStyle);

                materialEditor.ShaderProperty(vertexColorWear, "(R) Wear");
                materialEditor.ShaderProperty(vertexColorDirt, "(G) Dirt");

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
