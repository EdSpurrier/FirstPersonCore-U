// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "MACHIN3/Surface/Textured"
{
	Properties
	{
		_GrungeMap("Grunge Map", 2D) = "white" {}
		_MasksMap("MasksMap", 2D) = "black" {}
		_ColorMap("Color Map", 2D) = "white" {}
		_NormalMap("Normal Map", 2D) = "bump" {}
		_MetallicRoughnessMap("Metallic Roughness Map", 2D) = "white" {}
		_Tile("Tile", Range( 0 , 1000)) = 40
		_Rotate("Rotate", Range( 0 , 1)) = 0
		_Color("Color", Color) = (1,1,1,0)
		_Metallic("Metallic", Range( 0 , 1)) = 1
		_Roughness("Roughness", Range( 0 , 1)) = 0.5
		_AmbientOcclusion("Ambient Occlusion", Range( 0 , 4)) = 1
		_NormalIntensity("Normal Intensity", Range( 0 , 2)) = 1
		_NormalWearAmount("Normal Wear Amount", Range( 0 , 1)) = 0
		_GrungeIntensity("Grunge Intensity", Range( 0 , 0.3)) = 0.2907866
		_GrungeMaskTiling("Grunge Mask Tiling", Range( 0 , 20)) = 4
		_Grunge1Tiling("Grunge 1 Tiling", Range( 0 , 20)) = 10
		_Grunge2Tiling("Grunge 2 Tiling", Range( 0 , 20)) = 6
		_WearColor("Wear Color", Color) = (0,0,0,0)
		_WearMetallic("Wear Metallic", Range( 0 , 1)) = 1
		_WearRoughness("Wear Roughness", Range( 0 , 1)) = 0
		_WearAmount("Wear Amount", Range( 0 , 10)) = 10
		_WearGrungeTiling("Wear Grunge Tiling", Range( 0 , 20)) = 13.17647
		_WearHardness("Wear Hardness", Range( 0 , 10)) = 1
		_WearGeometry("Wear Geometry", Range( 0 , 1)) = 1
		_WearDecals("Wear Decals", Range( 0 , 1)) = 1
		_DirtColor("Dirt Color", Color) = (0.1397059,0.134265,0.1273789,0)
		_DirtMetallic("Dirt Metallic", Range( 0 , 1)) = 0
		_DirtRoughness("Dirt Roughness", Range( 0 , 1)) = 1
		_DirtAmount("Dirt Amount", Range( 0 , 10)) = 1
		_DirtHardness("Dirt Hardness", Range( 0 , 10)) = 10
		_DirtGeometry("Dirt Geometry", Range( 0 , 1)) = 1
		_DirtDecals("Dirt Decals", Range( 0 , 1)) = 1
		_GrungeToDirt("GrungeToDirt", Range( 0 , 1)) = 0
		_VertexColorWear("Vertex Color Wear", Range( 0 , 10)) = 0
		_VertexColorDirt("Vertex Color Dirt", Range( 0 , 10)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#include "UnityStandardUtils.cginc"
		#pragma target 4.6
		#pragma multi_compile_instancing
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
			float4 vertexColor : COLOR;
		};

		uniform float _NormalIntensity;
		uniform sampler2D _NormalMap;
		uniform float _Rotate;
		uniform float _Tile;
		uniform float _NormalWearAmount;
		uniform sampler2D _MasksMap;
		uniform float4 _MasksMap_ST;
		uniform float _WearGeometry;
		uniform float _WearDecals;
		uniform float _WearAmount;
		uniform sampler2D _GrungeMap;
		uniform float _WearGrungeTiling;
		uniform float _WearHardness;
		uniform float4 _Color;
		uniform sampler2D _ColorMap;
		uniform float _AmbientOcclusion;
		uniform float4 _WearColor;
		uniform float _VertexColorWear;
		uniform float4 _DirtColor;
		uniform float _GrungeToDirt;
		uniform float _Grunge1Tiling;
		uniform float _Grunge2Tiling;
		uniform float _GrungeMaskTiling;
		uniform float _DirtGeometry;
		uniform float _DirtDecals;
		uniform float _DirtAmount;
		uniform float _DirtHardness;
		uniform float _VertexColorDirt;
		uniform float _Metallic;
		uniform sampler2D _MetallicRoughnessMap;
		uniform float _WearMetallic;
		uniform float _DirtMetallic;
		uniform float _Roughness;
		uniform float _GrungeIntensity;
		uniform float _WearRoughness;
		uniform float _DirtRoughness;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_TexCoord3_g31 = i.uv_texcoord * float2( 1,1 ) + float2( 0,0 );
			float cos5_g31 = cos( ( _Rotate * 6.28318548202515 ) );
			float sin5_g31 = sin( ( _Rotate * 6.28318548202515 ) );
			float2 rotator5_g31 = mul( uv_TexCoord3_g31 - float2( 0,0 ) , float2x2( cos5_g31 , -sin5_g31 , sin5_g31 , cos5_g31 )) + float2( 0,0 );
			float2 temp_output_253_0 = ( rotator5_g31 * _Tile );
			float2 uv_MasksMap = i.uv_texcoord * _MasksMap_ST.xy + _MasksMap_ST.zw;
			float4 tex2DNode76 = tex2D( _MasksMap, uv_MasksMap );
			float blendOpSrc3_g32 = ( tex2DNode76.r * _WearGeometry );
			float blendOpDest3_g32 = ( tex2DNode76.g * _WearDecals );
			float2 uv_TexCoord2_g22 = i.uv_texcoord * float2( 1,1 ) + float2( 0,0 );
			float4 tex2DNode8_g22 = tex2D( _GrungeMap, ( uv_TexCoord2_g22 * _WearGrungeTiling ) );
			float temp_output_179_0 = saturate( ((0.0 + (_WearHardness - 0.0) * (-1.0 - 0.0) / (1.0 - 0.0)) + (( ( saturate( ( 1.0 - ( 1.0 - blendOpSrc3_g32 ) * ( 1.0 - blendOpDest3_g32 ) ) )) * _WearAmount * tex2DNode8_g22.a ) - 0.0) * (1.0 - (0.0 + (_WearHardness - 0.0) * (-1.0 - 0.0) / (1.0 - 0.0))) / (1.0 - 0.0)) );
			float4 lerpResult228 = lerp( float4( UnpackScaleNormal( tex2D( _NormalMap, temp_output_253_0 ) ,_NormalIntensity ) , 0.0 ) , float4(0,0,1,0) , ( ( 1.0 - _NormalWearAmount ) * temp_output_179_0 ));
			o.Normal = lerpResult228.rgb;
			float4 temp_cast_2 = (( 1.0 - ( tex2DNode76.b * _AmbientOcclusion ) )).xxxx;
			float4 blendOpSrc80 = ( _Color * tex2D( _ColorMap, temp_output_253_0 ) );
			float4 blendOpDest80 = temp_cast_2;
			float blendOpSrc183 = temp_output_179_0;
			float blendOpDest183 = ( i.vertexColor.r * _VertexColorWear );
			float temp_output_8_0_g34 = ( saturate( ( 1.0 - ( 1.0 - blendOpSrc183 ) * ( 1.0 - blendOpDest183 ) ) ));
			float4 lerpResult3_g34 = lerp( ( saturate( ( blendOpSrc80 * blendOpDest80 ) )) , _WearColor , temp_output_8_0_g34);
			float4 tex2DNode7_g22 = tex2D( _GrungeMap, ( uv_TexCoord2_g22 * _Grunge1Tiling ) );
			float4 tex2DNode9_g22 = tex2D( _GrungeMap, ( uv_TexCoord2_g22 * _Grunge2Tiling ) );
			float temp_output_177_3 = tex2DNode9_g22.b;
			float4 tex2DNode1_g22 = tex2D( _GrungeMap, ( uv_TexCoord2_g22 * _GrungeMaskTiling ) );
			float lerpResult67 = lerp( tex2DNode7_g22.g , temp_output_177_3 , tex2DNode1_g22.r);
			float blendOpSrc3_g33 = ( tex2DNode76.b * _DirtGeometry );
			float blendOpDest3_g33 = ( tex2DNode76.a * _DirtDecals );
			float blendOpSrc282 = ( _GrungeToDirt * lerpResult67 );
			float blendOpDest282 = saturate( ((0.0 + (_DirtHardness - 0.0) * (-1.0 - 0.0) / (1.0 - 0.0)) + (( ( saturate( ( 1.0 - ( 1.0 - blendOpSrc3_g33 ) * ( 1.0 - blendOpDest3_g33 ) ) )) * _DirtAmount * temp_output_177_3 ) - 0.0) * (1.0 - (0.0 + (_DirtHardness - 0.0) * (-1.0 - 0.0) / (1.0 - 0.0))) / (1.0 - 0.0)) );
			float blendOpSrc189 = ( saturate( ( 1.0 - ( 1.0 - blendOpSrc282 ) * ( 1.0 - blendOpDest282 ) ) ));
			float blendOpDest189 = ( i.vertexColor.g * _VertexColorDirt );
			float temp_output_8_0_g35 = ( saturate( ( 1.0 - ( 1.0 - blendOpSrc189 ) * ( 1.0 - blendOpDest189 ) ) ));
			float4 lerpResult3_g35 = lerp( lerpResult3_g34 , _DirtColor , temp_output_8_0_g35);
			o.Albedo = lerpResult3_g35.rgb;
			float4 tex2DNode262 = tex2D( _MetallicRoughnessMap, temp_output_253_0 );
			float lerpResult4_g34 = lerp( ( _Metallic * tex2DNode262.a ) , _WearMetallic , temp_output_8_0_g34);
			float lerpResult4_g35 = lerp( lerpResult4_g34 , _DirtMetallic , temp_output_8_0_g35);
			o.Metallic = lerpResult4_g35;
			float blendOpSrc61 = ( _Roughness * tex2DNode262.r );
			float blendOpDest61 = ( _GrungeIntensity * lerpResult67 );
			float lerpResult5_g34 = lerp( ( saturate( ( 1.0 - ( 1.0 - blendOpSrc61 ) * ( 1.0 - blendOpDest61 ) ) )) , _WearRoughness , temp_output_8_0_g34);
			float lerpResult5_g35 = lerp( lerpResult5_g34 , _DirtRoughness , temp_output_8_0_g35);
			o.Smoothness = ( 1.0 - lerpResult5_g35 );
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "M3SurfaceEditor"
}
/*ASEBEGIN
Version=13801
1;291;1407;766;3797.539;4049.842;5.959423;True;False
Node;AmplifyShaderEditor.RangedFloatNode;51;-1370.753,-540.2631;Float;False;Property;_Grunge1Tiling;Grunge 1 Tiling;15;0;10;0;20;0;1;FLOAT
Node;AmplifyShaderEditor.TexturePropertyNode;4;-1368.087,-932.8621;Float;True;Property;_GrungeMap;Grunge Map;0;0;None;False;white;Auto;0;1;SAMPLER2D
Node;AmplifyShaderEditor.RangedFloatNode;52;-1369.701,-426.6539;Float;False;Property;_Grunge2Tiling;Grunge 2 Tiling;16;0;6;0;20;0;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;53;-1367.436,-334.9304;Float;False;Property;_WearGrungeTiling;Wear Grunge Tiling;21;0;13.17647;0;20;0;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;50;-1368.735,-662.8145;Float;False;Property;_GrungeMaskTiling;Grunge Mask Tiling;14;0;4;0;20;0;1;FLOAT
Node;AmplifyShaderEditor.CommentaryNode;68;-231.3268,-872.0201;Float;False;1291.104;648.5857;and Grunge;9;262;263;55;63;61;64;67;54;264;Metallic/Roughness;1,1,1,1;0;0
Node;AmplifyShaderEditor.FunctionNode;177;-882.3764,-676.3428;Float;False;M3_channel_tile;-1;;22;69f3eadd556f046638c82dde50724e62;5;0;SAMPLER2D;;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.LerpOp;67;-150.5448,-356.4099;Float;False;3;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.WireNode;285;1132.865,-353.3979;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.WireNode;174;-363.9228,-914.9445;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.WireNode;284;1213.905,-402.2789;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.WireNode;200;-277.8128,-963.5402;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.WireNode;194;-96.63184,-1022.221;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SamplerNode;76;-1411.644,-2033.026;Float;True;Property;_MasksMap;MasksMap;1;0;None;True;0;False;black;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.WireNode;212;46.10711,-2048.964;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.WireNode;287;1269.96,-1833.789;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.WireNode;277;-39.03442,-1051.885;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.CommentaryNode;127;76.23659,-1901.107;Float;False;905.7062;814.9794;;12;132;131;133;158;157;98;104;116;96;179;271;272;Wear;0.5147059,0.7188641,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;92;-1477.388,-2464.145;Float;False;Property;_Rotate;Rotate;6;0;0;0;1;0;1;FLOAT
Node;AmplifyShaderEditor.WireNode;173;982.1699,-974.1265;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.WireNode;213;44.42081,-2005.121;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;87;-1471.342,-2587.965;Float;False;Property;_Tile;Tile;5;0;40;0;1000;0;1;FLOAT
Node;AmplifyShaderEditor.WireNode;202;1058.216,-1036.349;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.WireNode;286;1314.555,-1875.09;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.WireNode;272;335.6946,-1616.93;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.WireNode;278;396.4615,-1056.328;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.WireNode;271;357.6946,-1680.93;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.FunctionNode;253;-1002.577,-2546.826;Float;False;M3_tile_and_rotate;-1;;31;2750d3b43b6844c28b7ac2a2a1f2b94d;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT2
Node;AmplifyShaderEditor.WireNode;273;103.8129,-2051.925;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.WireNode;274;106.8129,-2006.925;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.CommentaryNode;176;1391.182,-2836.196;Float;False;1451.593;746.3911;;14;144;145;146;178;201;161;162;163;164;172;280;281;282;289;Dirt;0.8014706,0.6375999,0.4302011,1;0;0
Node;AmplifyShaderEditor.WireNode;172;1778.075,-2142.397;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.WireNode;276;1704.46,-2004.037;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.WireNode;288;2234.46,-1971.327;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.WireNode;275;1704.46,-2044.037;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;116;120.2598,-1179.956;Float;False;Property;_WearHardness;Wear Hardness;22;0;1;0;10;0;1;FLOAT
Node;AmplifyShaderEditor.WireNode;267;-608.2823,-2427.619;Float;False;1;0;FLOAT2;0,0;False;1;FLOAT2
Node;AmplifyShaderEditor.RangedFloatNode;104;118.2598,-1289.956;Float;False;Property;_WearAmount;Wear Amount;20;0;10;0;10;0;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;96;120.6638,-1404.134;Float;False;Property;_WearDecals;Wear Decals;24;0;1;0;1;0;1;FLOAT
Node;AmplifyShaderEditor.CommentaryNode;75;-184.2839,-2849.382;Float;False;1154.684;704.3084;and Ambient Occlusion;7;81;77;80;74;72;69;78;Color;1,1,1,1;0;0
Node;AmplifyShaderEditor.WireNode;158;408.7881,-1567.975;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.WireNode;157;414.7881,-1639.975;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.WireNode;279;458.6752,-1076.326;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.WireNode;141;-372.9383,-2275.487;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;98;109.6637,-1513.134;Float;False;Property;_WearGeometry;Wear Geometry;23;0;1;0;1;0;1;FLOAT
Node;AmplifyShaderEditor.WireNode;283;2297.688,-2015.303;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.WireNode;168;1777.657,-2071.787;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;280;1993.284,-2439.041;Float;False;Property;_GrungeToDirt;GrungeToDirt;32;0;0;0;1;0;1;FLOAT
Node;AmplifyShaderEditor.WireNode;169;1783.194,-2035.276;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;161;1489.786,-2543.367;Float;False;Property;_DirtGeometry;Dirt Geometry;30;0;1;0;1;0;1;FLOAT
Node;AmplifyShaderEditor.WireNode;268;-555.4967,-2355.039;Float;False;1;0;FLOAT2;0,0;False;1;FLOAT2
Node;AmplifyShaderEditor.RangedFloatNode;162;1497.382,-2319.189;Float;False;Property;_DirtAmount;Dirt Amount;28;0;1;0;10;0;1;FLOAT
Node;AmplifyShaderEditor.WireNode;214;-306.5425,-2295.234;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;78;-148.7856,-2241.994;Float;False;Property;_AmbientOcclusion;Ambient Occlusion;10;0;1;0;4;0;1;FLOAT
Node;AmplifyShaderEditor.WireNode;201;1812.203,-2158.516;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;163;1493.742,-2221.053;Float;False;Property;_DirtHardness;Dirt Hardness;29;0;10;0;10;0;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;164;1499.786,-2433.367;Float;False;Property;_DirtDecals;Dirt Decals;31;0;1;0;1;0;1;FLOAT
Node;AmplifyShaderEditor.FunctionNode;179;667.5446,-1363.312;Float;False;M3_weathering;-1;;32;034fa4b3330ee467fa13dcf81e7c71c6;7;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;281;2373.655,-2356.383;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.FunctionNode;178;1991.737,-2328.401;Float;False;M3_weathering;-1;;33;034fa4b3330ee467fa13dcf81e7c71c6;7;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.ColorNode;72;-104.0058,-2797.83;Float;False;Property;_Color;Color;7;0;1,1,1,0;0;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SamplerNode;69;-147.2675,-2569.183;Float;True;Property;_ColorMap;Color Map;2;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.CommentaryNode;190;1501.892,-1452.684;Float;False;1589.072;603.3558;;7;189;183;184;185;187;188;142;Vertex Color;0.3970588,1,0.4839238,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;81;261.6059,-2338.608;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.WireNode;269;-516.9775,-712.899;Float;False;1;0;FLOAT2;0,0;False;1;FLOAT2
Node;AmplifyShaderEditor.WireNode;220;1247.04,-1272.19;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.WireNode;254;-513.9564,-2477.275;Float;False;1;0;FLOAT2;0,0;False;1;FLOAT2
Node;AmplifyShaderEditor.WireNode;270;-475.0022,-664.4783;Float;False;1;0;FLOAT2;0,0;False;1;FLOAT2
Node;AmplifyShaderEditor.OneMinusNode;77;477.2056,-2343.207;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;74;358.7631,-2664.668;Float;False;2;2;0;COLOR;0.0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.WireNode;221;1291.026,-1232.005;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;188;1552.477,-936.358;Float;False;Property;_VertexColorDirt;Vertex Color Dirt;34;0;0;0;10;0;1;FLOAT
Node;AmplifyShaderEditor.BlendOpsNode;282;2574.688,-2353.303;Float;False;Screen;True;2;0;FLOAT;0,0,0,0;False;1;FLOAT;0;False;1;FLOAT
Node;AmplifyShaderEditor.VertexColorNode;142;1552.576,-1260.299;Float;False;0;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.WireNode;255;-464.5127,-2409.016;Float;False;1;0;FLOAT2;0,0;False;1;FLOAT2
Node;AmplifyShaderEditor.WireNode;289;2777.154,-2263.102;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;55;-213.0777,-816.6312;Float;False;Property;_Roughness;Roughness;9;0;0.5;0;1;0;1;FLOAT
Node;AmplifyShaderEditor.WireNode;256;-416.0131,-262.3874;Float;False;1;0;FLOAT2;0,0;False;1;FLOAT2
Node;AmplifyShaderEditor.WireNode;223;1439.264,-775.712;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.BlendOpsNode;80;732.3004,-2437.195;Float;False;Multiply;True;2;0;COLOR;0,0,0,0;False;1;FLOAT;0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.RangedFloatNode;63;-217.5839,-487.4328;Float;False;Property;_GrungeIntensity;Grunge Intensity;13;0;0.2907866;0;0.3;0;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;185;1551.892,-1063.673;Float;False;Property;_VertexColorWear;Vertex Color Wear;33;0;0;0;10;0;1;FLOAT
Node;AmplifyShaderEditor.SamplerNode;262;-210.7738,-711.3226;Float;True;Property;_MetallicRoughnessMap;Metallic Roughness Map;4;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;187;2244.834,-1046.699;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.WireNode;222;1511.291,-717.0825;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.WireNode;257;-314.8371,-143.1446;Float;False;1;0;FLOAT2;0,0;False;1;FLOAT2
Node;AmplifyShaderEditor.WireNode;215;1089.146,-2388.22;Float;False;1;0;COLOR;0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;263;202.4497,-756.147;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.CommentaryNode;90;3377.354,-1184.787;Float;False;1054.712;606.3671;;13;224;227;226;225;86;228;219;85;233;234;218;260;261;Normal;0.5019608,0.5019608,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;64;168.4487,-401.0689;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.BlendOpsNode;189;2853.317,-1391.267;Float;False;Screen;True;2;0;FLOAT;0,0,0,0;False;1;FLOAT;0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;184;1942.244,-1246.012;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;54;373.9148,-820.5822;Float;False;Property;_Metallic;Metallic;8;0;1;0;1;0;1;FLOAT
Node;AmplifyShaderEditor.WireNode;216;1153.625,-2355.246;Float;False;1;0;COLOR;0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.BlendOpsNode;183;2202.036,-1402.684;Float;False;Screen;True;2;0;FLOAT;0,0,0,0;False;1;FLOAT;0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;264;776.5297,-755.025;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.WireNode;224;3494.911,-721.4813;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.WireNode;258;2775.228,-140.4896;Float;False;1;0;FLOAT2;0,0;False;1;FLOAT2
Node;AmplifyShaderEditor.WireNode;203;3359.328,-1373.995;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.BlendOpsNode;61;745.3342,-607.3279;Float;True;Screen;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT
Node;AmplifyShaderEditor.WireNode;259;2886.29,-165.7645;Float;False;1;0;FLOAT2;0,0;False;1;FLOAT2
Node;AmplifyShaderEditor.WireNode;192;3442.132,-1418.414;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.WireNode;225;3580.513,-747.1084;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.WireNode;265;1508.706,-1636.067;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.WireNode;217;1403.965,-1975.361;Float;False;1;0;COLOR;0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.WireNode;209;2837.738,-1729.332;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.WireNode;180;1471.428,-1495.336;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;218;3516.221,-1134.14;Float;False;Property;_NormalWearAmount;Normal Wear Amount;12;0;0;0;1;0;1;FLOAT
Node;AmplifyShaderEditor.WireNode;186;2883.451,-1749.902;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.WireNode;226;3806.47,-1039.232;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.WireNode;260;3445.592,-855.4907;Float;False;1;0;FLOAT2;0,0;False;1;FLOAT2
Node;AmplifyShaderEditor.WireNode;208;3612.199,-1720.574;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;132;670.5654,-1621.725;Float;False;Property;_WearMetallic;Wear Metallic;18;0;1;0;1;0;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;133;670.4871,-1517.661;Float;False;Property;_WearRoughness;Wear Roughness;19;0;0;0;1;0;1;FLOAT
Node;AmplifyShaderEditor.WireNode;199;1546.96,-1565.898;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.WireNode;193;1478.735,-1936.089;Float;False;1;0;COLOR;0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.WireNode;266;1576.516,-1678.914;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.ColorNode;131;713.2109,-1825.338;Float;False;Property;_WearColor;Wear Color;17;0;0,0,0,0;0;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;146;1998.543,-2529.308;Float;False;Property;_DirtRoughness;Dirt Roughness;27;0;1;0;1;0;1;FLOAT
Node;AmplifyShaderEditor.ColorNode;144;2049.705,-2789.566;Float;False;Property;_DirtColor;Dirt Color;25;0;0.1397059,0.134265,0.1273789,0;0;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.WireNode;227;3846.04,-1060.739;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;145;1994.837,-2613.148;Float;False;Property;_DirtMetallic;Dirt Metallic;26;0;0;0;1;0;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;86;3452.624,-663.5078;Float;False;Property;_NormalIntensity;Normal Intensity;11;0;1;0;2;0;1;FLOAT
Node;AmplifyShaderEditor.FunctionNode;167;3085.988,-1744.233;Float;False;M3_lerp;-1;;34;81c2a129e4b934bf6b643187a44c03d3;7;0;FLOAT;0.0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT;0.0;False;3;COLOR;FLOAT;FLOAT
Node;AmplifyShaderEditor.WireNode;191;3659.9,-1758.631;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.OneMinusNode;234;3837.773,-1128.357;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.WireNode;261;3515.57,-882.9797;Float;False;1;0;FLOAT2;0,0;False;1;FLOAT2
Node;AmplifyShaderEditor.FunctionNode;175;4082.387,-1781.525;Float;False;M3_lerp;-1;;35;81c2a129e4b934bf6b643187a44c03d3;7;0;FLOAT;0.0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT;0.0;False;3;COLOR;FLOAT;FLOAT
Node;AmplifyShaderEditor.ColorNode;233;3879.535,-757.6793;Float;False;Constant;_NormalColor;NormalColor;33;0;0,0,1,0;0;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SamplerNode;85;3806.744,-945.296;Float;True;Property;_NormalMap;Normal Map;3;0;None;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;FLOAT3;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;219;4006.731,-1117.851;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.OneMinusNode;139;4545.586,-1509.004;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.LerpOp;228;4243.522,-1058.662;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0;False;2;FLOAT;0.0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;4811.599,-1598.485;Float;False;True;6;Float;M3SurfaceEditor;0;0;Standard;MACHIN3/Surface/Textured;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;Back;0;0;False;0;0;Opaque;0.5;True;True;0;False;Opaque;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;2;15;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;OFF;OFF;0;False;0;0,0,0,0;VertexOffset;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;0;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;FLOAT;0.0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;177;0;4;0
WireConnection;177;1;50;0
WireConnection;177;2;51;0
WireConnection;177;3;52;0
WireConnection;177;4;53;0
WireConnection;67;0;177;2
WireConnection;67;1;177;3
WireConnection;67;2;177;1
WireConnection;285;0;67;0
WireConnection;174;0;177;3
WireConnection;284;0;285;0
WireConnection;200;0;174;0
WireConnection;194;0;177;4
WireConnection;212;0;76;3
WireConnection;287;0;284;0
WireConnection;277;0;194;0
WireConnection;173;0;200;0
WireConnection;213;0;76;4
WireConnection;202;0;173;0
WireConnection;286;0;287;0
WireConnection;272;0;76;2
WireConnection;278;0;277;0
WireConnection;271;0;76;1
WireConnection;253;0;87;0
WireConnection;253;1;92;0
WireConnection;273;0;212;0
WireConnection;274;0;213;0
WireConnection;172;0;202;0
WireConnection;276;0;274;0
WireConnection;288;0;286;0
WireConnection;275;0;273;0
WireConnection;267;0;253;0
WireConnection;158;0;272;0
WireConnection;157;0;271;0
WireConnection;279;0;278;0
WireConnection;141;0;76;3
WireConnection;283;0;288;0
WireConnection;168;0;275;0
WireConnection;169;0;276;0
WireConnection;268;0;267;0
WireConnection;214;0;141;0
WireConnection;201;0;172;0
WireConnection;179;0;157;0
WireConnection;179;1;158;0
WireConnection;179;2;98;0
WireConnection;179;3;96;0
WireConnection;179;4;104;0
WireConnection;179;5;116;0
WireConnection;179;6;279;0
WireConnection;281;0;280;0
WireConnection;281;1;283;0
WireConnection;178;0;168;0
WireConnection;178;1;169;0
WireConnection;178;2;161;0
WireConnection;178;3;164;0
WireConnection;178;4;162;0
WireConnection;178;5;163;0
WireConnection;178;6;201;0
WireConnection;69;1;253;0
WireConnection;81;0;214;0
WireConnection;81;1;78;0
WireConnection;269;0;268;0
WireConnection;220;0;179;0
WireConnection;254;0;253;0
WireConnection;270;0;269;0
WireConnection;77;0;81;0
WireConnection;74;0;72;0
WireConnection;74;1;69;0
WireConnection;221;0;220;0
WireConnection;282;0;281;0
WireConnection;282;1;178;0
WireConnection;255;0;254;0
WireConnection;289;0;282;0
WireConnection;256;0;255;0
WireConnection;223;0;221;0
WireConnection;80;0;74;0
WireConnection;80;1;77;0
WireConnection;262;1;270;0
WireConnection;187;0;142;2
WireConnection;187;1;188;0
WireConnection;222;0;223;0
WireConnection;257;0;256;0
WireConnection;215;0;80;0
WireConnection;263;0;55;0
WireConnection;263;1;262;1
WireConnection;64;0;63;0
WireConnection;64;1;67;0
WireConnection;189;0;289;0
WireConnection;189;1;187;0
WireConnection;184;0;142;1
WireConnection;184;1;185;0
WireConnection;216;0;215;0
WireConnection;183;0;179;0
WireConnection;183;1;184;0
WireConnection;264;0;54;0
WireConnection;264;1;262;4
WireConnection;224;0;222;0
WireConnection;258;0;257;0
WireConnection;203;0;189;0
WireConnection;61;0;263;0
WireConnection;61;1;64;0
WireConnection;259;0;258;0
WireConnection;192;0;203;0
WireConnection;225;0;224;0
WireConnection;265;0;264;0
WireConnection;217;0;216;0
WireConnection;209;0;183;0
WireConnection;180;0;61;0
WireConnection;186;0;209;0
WireConnection;226;0;225;0
WireConnection;260;0;259;0
WireConnection;208;0;192;0
WireConnection;199;0;180;0
WireConnection;193;0;217;0
WireConnection;266;0;265;0
WireConnection;227;0;226;0
WireConnection;167;0;186;0
WireConnection;167;1;193;0
WireConnection;167;2;131;0
WireConnection;167;3;266;0
WireConnection;167;4;132;0
WireConnection;167;5;199;0
WireConnection;167;6;133;0
WireConnection;191;0;208;0
WireConnection;234;0;218;0
WireConnection;261;0;260;0
WireConnection;175;0;191;0
WireConnection;175;1;167;0
WireConnection;175;2;144;0
WireConnection;175;3;167;1
WireConnection;175;4;145;0
WireConnection;175;5;167;2
WireConnection;175;6;146;0
WireConnection;85;1;261;0
WireConnection;85;5;86;0
WireConnection;219;0;234;0
WireConnection;219;1;227;0
WireConnection;139;0;175;2
WireConnection;228;0;85;0
WireConnection;228;1;233;0
WireConnection;228;2;219;0
WireConnection;0;0;175;0
WireConnection;0;1;228;0
WireConnection;0;3;175;1
WireConnection;0;4;139;0
ASEEND*/
//CHKSM=6AD1EBF378A0FE057EBD5DF1E09867C130A54238