Shader "HaloCE/HaloMultipurpose" {
	Properties {
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB), Alpha (A)", 2D) = "white" {}
		_LightPower("Light Power", float) = 1

		_MultipurposeMap("Multipurpose (RGBA)", 2D) = "grey" {}
		_SpecularColor("Specular Color", Color) = (1,1,1,1)
		_SpecularPower("Specular Power",  Range(0.01, 1)) = 0.075

		_DetailMap("Detail Map", 2D) = "white" {}

		_CubeMap("Cube Map", CUBE) = "white" {}
		_CubeMapPower("Cube Map Power", range(0,4)) = 1

		_EmissionColor("Emission Color", Color) = (1,1,1,1)
		
		_NormalMap("Normal Map", 2D) = "grey" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf HalfLambert fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		fixed4 _Color;
		sampler2D _MainTex;
		half _LightPower;

		sampler2D _MultipurposeMap;
		half _SpecularPower;
		fixed4 _SpecularColor;
		fixed4 _EmissionColor;

		sampler2D _DetailMap;

		samplerCUBE _CubeMap;
		half _CubeMapPower;
		
		sampler2D _NormalMap;

		struct Input {
			float2 uv_MainTex;
			float2 uv_DetailMap;
			
			float2 uv_NormalMap;

			INTERNAL_DATA
			float3 viewDir;
		};

		// need our own struct because surface shader doesn't have all of this
		struct NewSurfaceOutput
		{
			half3 Albedo;
			half3 Normal;
			half3 Emission;
			half Specular;
			half Alpha;
			half3 SpecularColor;
		};

		half halfDot(half3 a, half3 b)
		{
			return dot(normalize(a), normalize(b)) * 0.5f + 0.5f;
		}

		half4 LightingHalfLambert(NewSurfaceOutput o, half3 lightDir, half3 viewDir, half atten)
		{
			half shadingValue = halfDot(o.Normal, lightDir);
			half3 diffuseLighting = shadingValue * o.Albedo * _LightColor0;

			// specular
			half specularValue = o.Specular * _SpecularPower;
			half3 specularLighting = specularValue * o.SpecularColor * _LightColor0;

			half3 returnColor = (diffuseLighting + specularLighting) * atten * _LightPower;

			return half4(returnColor, o.Alpha);
		}

		void surf (Input IN, inout NewSurfaceOutput o) { // SurfaceOutput o) { //
			fixed4 mainTex = tex2D(_MainTex, IN.uv_MainTex);			

			o.Normal = UnpackNormal (tex2D (_NormalMap, IN.uv_NormalMap));
			
			// multiply the color based on the mask (multipurpose alpha channel)			
			fixed4 multiMap = tex2D(_MultipurposeMap, IN.uv_MainTex);
			if (multiMap.a > 0) mainTex.rgb *= _Color.rgb;

			// detail
			fixed4 detailMap = tex2D(_DetailMap, IN.uv_DetailMap);

			o.Albedo = mainTex.rgb * (detailMap.rbg * 2);
			o.Alpha = mainTex.a;

			// specular
			half specularValue = tex2D(_MultipurposeMap, IN.uv_MainTex).b;
			o.SpecularColor = specularValue * _SpecularColor;
			o.Specular = tex2D(_MultipurposeMap, IN.uv_MainTex).b;

			// glow
			half3 addEmission = half3(multiMap.g, multiMap.g, multiMap.g) * _EmissionColor;
			// cubemap
			half3 reflectedViewDir = reflect(-IN.viewDir, o.Normal);
			half3 cubeMap = texCUBE(_CubeMap, reflectedViewDir) * _CubeMapPower * o.Specular;
			o.Emission = 1 - (1 - cubeMap) * (1 - addEmission);			
		}
		ENDCG
	}
	FallBack "Diffuse"
}
