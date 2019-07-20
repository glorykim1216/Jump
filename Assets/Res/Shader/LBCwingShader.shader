// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "LBCwingShader"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.9
		_TextureSample2("Texture Sample 2", 2D) = "white" {}
		_Albedo("Albedo", 2D) = "white" {}
		_TileableFire("TileableFire", 2D) = "white" {}
		_Float1("Float 1", Range( 0 , 5)) = 0
		_Float0("Float 0", Float) = 1
		_Smoothness("Smoothness", Float) = 1
		_TileSpeed("TileSpeed", Vector) = (0,0,0,0)
		_TextureSample1("Texture Sample 1", 2D) = "bump" {}
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_emission("emission", Color) = (1,0.004716992,0.09496178,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "Transparent+0" "IsEmissive" = "true"  }
		Cull Off
		ZTest LEqual
		Blend One One
		BlendOp Add
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#include "UnityStandardUtils.cginc"
		#pragma target 3.0
		#pragma surface surf StandardSpecular keepalpha 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _TileableFire;
		uniform float2 _TileSpeed;
		uniform sampler2D _TextureSample1;
		uniform float _Float0;
		uniform float _Float1;
		uniform sampler2D _Albedo;
		uniform float4 _Albedo_ST;
		uniform float4 _emission;
		uniform sampler2D _TextureSample2;
		uniform float4 _TextureSample2_ST;
		uniform float _Smoothness;
		uniform sampler2D _TextureSample0;
		uniform float4 _TextureSample0_ST;
		uniform float _Cutoff = 0.9;

		void surf( Input i , inout SurfaceOutputStandardSpecular o )
		{
			float2 temp_cast_0 = (_Float0).xx;
			float2 panner23 = ( 1.0 * _Time.y * temp_cast_0 + i.uv_texcoord);
			float2 uv_TexCoord6 = i.uv_texcoord + UnpackNormal( tex2D( _TextureSample1, panner23 ) ).xy;
			float2 panner16 = ( _Time.x * _TileSpeed + uv_TexCoord6);
			float4 tex2DNode1 = tex2D( _TileableFire, panner16 );
			o.Normal = UnpackScaleNormal( (tex2DNode1.r).xxxx, _Float1 );
			float2 uv_Albedo = i.uv_texcoord * _Albedo_ST.xy + _Albedo_ST.zw;
			o.Albedo = tex2D( _Albedo, uv_Albedo ).rgb;
			o.Emission = ( tex2DNode1.r * _emission ).rgb;
			float2 uv_TextureSample2 = i.uv_texcoord * _TextureSample2_ST.xy + _TextureSample2_ST.zw;
			o.Specular = tex2D( _TextureSample2, uv_TextureSample2 ).rgb;
			o.Smoothness = _Smoothness;
			o.Alpha = 1;
			float2 uv_TextureSample0 = i.uv_texcoord * _TextureSample0_ST.xy + _TextureSample0_ST.zw;
			clip( tex2D( _TextureSample0, uv_TextureSample0 ).r - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15600
581;266;1145;663;2487.986;387.9122;1;True;True
Node;AmplifyShaderEditor.RangedFloatNode;24;-2679.336,699.777;Float;False;Property;_Float0;Float 0;5;0;Create;True;0;0;False;0;1;0.2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;22;-2820.096,464.498;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;23;-2472.229,443.1232;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;25;-2446.905,-71.3159;Float;True;Property;_TextureSample1;Texture Sample 1;8;0;Create;True;0;0;False;0;None;f7f322ea849ea7d41adb6fa8a7d8a3e6;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;6;-2107.767,78.3693;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;17;-2188.186,261.4239;Float;False;Property;_TileSpeed;TileSpeed;7;0;Create;True;0;0;False;0;0,0;1,8;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TimeNode;5;-2076.63,434.7289;Float;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;16;-1757.522,145.5305;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;-1,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;1;-2004.902,-205.1222;Float;True;Property;_TileableFire;TileableFire;3;0;Create;True;0;0;False;0;None;175e92f3f304e9041ab3e27616eeed52;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;29;-844.8129,377.1893;Float;False;Property;_emission;emission;10;0;Create;True;0;0;False;0;1,0.004716992,0.09496178,0;0.3652577,0,1,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;28;-1206.326,343.1466;Float;False;Property;_Float1;Float 1;4;0;Create;True;0;0;False;0;0;5;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.SwizzleNode;26;-1010.019,49.98727;Float;False;FLOAT4;0;0;0;0;1;0;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.UnpackScaleNormalNode;27;-782.184,47.24212;Float;False;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;15;-180.9505,256.774;Float;False;Property;_Smoothness;Smoothness;6;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;18;-266.8397,721.4971;Float;True;Property;_TextureSample0;Texture Sample 0;9;0;Create;True;0;0;False;0;None;88afce4f0c2ac3d4b8d6867dd7b17263;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;31;-595.3757,-357.7223;Float;True;Property;_TextureSample2;Texture Sample 2;1;0;Create;True;0;0;False;0;None;36be8d528a4fa024faa4680d7658642c;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;12;-556,-625.5;Float;True;Property;_Albedo;Albedo;2;0;Create;True;0;0;False;0;None;36be8d528a4fa024faa4680d7658642c;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;30;-662.1454,256.0956;Float;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;171,-60;Float;False;True;2;Float;ASEMaterialInspector;0;0;StandardSpecular;LBCwingShader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Off;0;False;-1;3;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.9;True;False;0;True;TransparentCutout;;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;0;4;10;25;False;0.5;True;4;1;False;-1;1;False;-1;0;1;False;-1;1;False;-1;1;False;-1;1;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;23;0;22;0
WireConnection;23;2;24;0
WireConnection;25;1;23;0
WireConnection;6;1;25;0
WireConnection;16;0;6;0
WireConnection;16;2;17;0
WireConnection;16;1;5;1
WireConnection;1;1;16;0
WireConnection;26;0;1;1
WireConnection;27;0;26;0
WireConnection;27;1;28;0
WireConnection;30;0;1;1
WireConnection;30;1;29;0
WireConnection;0;0;12;0
WireConnection;0;1;27;0
WireConnection;0;2;30;0
WireConnection;0;3;31;0
WireConnection;0;4;15;0
WireConnection;0;10;18;0
ASEEND*/
//CHKSM=23FD5B52B8AB5D4A2C6A4B7EC20278E25F3610CE