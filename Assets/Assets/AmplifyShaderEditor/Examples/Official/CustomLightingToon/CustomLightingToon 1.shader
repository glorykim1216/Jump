// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "LBCWingToon2"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_TextureSample1("Texture Sample 1", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" }
		Cull Off
		Blend One One , SrcAlpha OneMinusSrcAlpha
		BlendOp Add , Add
		AlphaToMask On
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Lambert keepalpha noshadow 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _TextureSample1;
		uniform float4 _TextureSample1_ST;
		uniform sampler2D _TextureSample0;
		uniform float4 _TextureSample0_ST;
		uniform float _Cutoff = 0.5;

		void surf( Input i , inout SurfaceOutput o )
		{
			float2 uv_TextureSample1 = i.uv_texcoord * _TextureSample1_ST.xy + _TextureSample1_ST.zw;
			float4 tex2DNode55 = tex2D( _TextureSample1, uv_TextureSample1 );
			o.Albedo = tex2DNode55.rgb;
			o.Specular = tex2DNode55.r;
			float2 uv_TextureSample0 = i.uv_texcoord * _TextureSample0_ST.xy + _TextureSample0_ST.zw;
			float4 tex2DNode53 = tex2D( _TextureSample0, uv_TextureSample0 );
			o.Alpha = tex2DNode53.r;
			clip( tex2DNode53.r - _Cutoff );
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15600
1927;1;1906;1010;241.8066;15.59735;1;True;True
Node;AmplifyShaderEditor.SamplerNode;53;578.0759,754.9481;Float;True;Property;_TextureSample0;Texture Sample 0;1;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;55;590.1934,335.4026;Float;True;Property;_TextureSample1;Texture Sample 1;2;0;Create;True;0;0;False;0;None;e4c04f362fa1cc84dbf21d831431e501;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1077.3,350.7;Float;False;True;2;Float;ASEMaterialInspector;0;0;Lambert;LBCWingToon2;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;False;0;True;TransparentCutout;;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;7;False;-1;0;False;-1;0;False;-1;0;False;-1;1;False;-1;1;False;-1;1;False;-1;1;False;-1;False;0;4;10;25;False;0.5;False;4;1;False;-1;1;False;-1;2;5;False;-1;10;False;-1;1;False;-1;1;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;True;0;0;False;-1;-1;0;False;-1;0;0;0;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;0;0;55;0
WireConnection;0;3;55;0
WireConnection;0;9;53;0
WireConnection;0;10;53;0
ASEEND*/
//CHKSM=B47A1AB6CFBD39B161378EC0469C913D069A49C7