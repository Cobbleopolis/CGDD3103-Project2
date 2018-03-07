Shader "Custom/Lattice" {
	Properties {
	    [Header(General Settings)]
		_Color ("Color", Color) = (1, 1, 1, 1)
		
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		
		[Header(Lattice Settings)]
		_LatticeColor("Lattice Color", Color) = (0, 0, 0, 1)
		[MaterialToggle] _LookXAxis("X Axis", Float) = 1
		_SpacingX ("X Axis Lattice Spacing", Float) = 1.0
		[MaterialToggle] _LookYAxis("Y Axis", Float) = 1
		_SpacingY ("Y Lattice Spacing", Float) = 1.0
		[MaterialToggle] _LookZAxis("Z Axis", Float) = 1
		_SpacingZ ("Z Axis Lattice Spacing", Float) = 1.0
		_LineThickness ("Line Thickness", Float) = 0.1
		
		[Header(Color Ramp)]
		[MaterialToggle] _UseColorRamp("Use Color Ramp", Float) = 0
		_RampColor ("Ramp Color", Color) = (1, 1, 1, 1)
		_RampMinHeight("Ramp Min Height", Float) = 0
		_RampMaxHeight("Ramp Max Height", Float) = 10
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0
		
		#include "UnityCG.cginc"
		
		static const float PI = 3.1415926535897932384626433832795;
		static const float TAU = 2 * PI;
		static const float3 UP_VEC = float3(1.0, 0.0, 0.0);

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
			float3 worldPos;
			float3 worldNormal;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		fixed4 _LatticeColor;
		
		bool _LookXAxis;
		float _SpacingX;
		bool _LookYAxis;
		float _SpacingY;
		bool _LookZAxis;
		float _SpacingZ;
		float _LineThickness;
		
		bool _UseColorRamp;
		fixed4 _RampColor;
		float _RampMinHeight;
		float _RampMaxHeight;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)
		
		float step(float a, float res) {
		    return res * (int) (a / res);
		}

		void surf (Input input, inout SurfaceOutputStandard o) {
			float THRESH = _LineThickness / 2.0f;
			fixed4 surfCol;
			if (_UseColorRamp)
//			    surfCol = lerp(_Color, _RampColor, (input.worldPos.y - _RampMinHeight) / (_RampMaxHeight - _RampMinHeight));
			    surfCol = lerp(_Color, _RampColor, abs((step(input.worldPos.y, _SpacingY) - _RampMinHeight) / abs(_RampMaxHeight - _RampMinHeight)));
            else
                surfCol = _Color;
			float modX = fmod(input.worldPos.x, _SpacingX);
			float modY = fmod(input.worldPos.y, _SpacingY);
			float modZ = fmod(input.worldPos.z, _SpacingZ);
			if (
			    (_LookXAxis && (modX < THRESH || modX > _SpacingX - THRESH)) ||
			    (_LookYAxis && ((input.worldPos.y > THRESH || input.worldPos.y < -THRESH) && modY < THRESH || modY > _SpacingY - THRESH)) ||
			    (_LookZAxis && (modZ < THRESH || modZ > _SpacingZ - THRESH))
			)
			    surfCol = _LatticeColor;
			fixed4 c = tex2D (_MainTex, input.uv_MainTex) * surfCol;
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
