﻿Shader "ToonBase" // Nach dem Tutorial von Erik Roystan
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Main Texture", 2D) = "white" {}

		// Das Umgebungslicht wird gleichmäßig auf alle Oberflächen des Objekts verteilt

		[HDR]
		_AmbientColor("Ambient Color", Color) = (0.4,0.4,0.4,1)
		[HDR]
		_SpecularColor("Specular Color", Color) = (0.9,0.9,0.9,1)

		// Steuert die Größe der spiegelnden Reflexion

		_Glossiness("Glossiness", Float) = 32
		[HDR]
		_RimColor("Rim Color", Color) = (1,1,1,1)
		_RimAmount("Rim Amount", Range(0, 1)) = 0.716

		// Mithilfe des Thresholds kann die Größe des hellen Rands angepasst werden

		_RimThreshold("Rim Threshold", Range(0, 1)) = 0.1		
	}

	SubShader
	{
		Pass
		{
			// Es soll nur das Licht EINES Hauptlichts akzeptiert werden

			Tags
			{
				"LightMode" = "ForwardBase"
				"PassFlags" = "OnlyDirectional"
			}

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			// Mehrere Einstellungen für verschiedene Lichtsettings

			#pragma multi_compile_fwdbase
			
			#include "UnityCG.cginc"

			// Vorgefertigte Makros um den Umgang mit Licht und Schatten einfach zu gestalten

			#include "Lighting.cginc"
			#include "AutoLight.cginc"

			struct appdata
			{
				float4 vertex : POSITION;				
				float4 uv : TEXCOORD0;
				float3 normal : NORMAL;
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
				float3 worldNormal : NORMAL;
				float2 uv : TEXCOORD0;
				float3 viewDir : TEXCOORD1;

				SHADOW_COORDS(2)
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.worldNormal = UnityObjectToWorldNormal(v.normal);		
				o.viewDir = WorldSpaceViewDir(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);

				// World Space zu Shadow-Map Space 

				TRANSFER_SHADOW(o)
				return o;
			}
			
			float4 _Color;

			float4 _AmbientColor;

			float4 _SpecularColor;
			float _Glossiness;		

			float4 _RimColor;
			float _RimAmount;
			float _RimThreshold;	

			float4 frag (v2f i) : SV_Target
			{

				// Toon Look durch clippen der Schatten

				float3 normal = normalize(i.worldNormal);
				float3 viewDir = normalize(i.viewDir);
				float NdotL = dot(_WorldSpaceLightPos0, normal);
				float shadow = SHADOW_ATTENUATION(i);
				float lightIntensity = smoothstep(0, 0.01, NdotL * shadow);	
				float4 light = lightIntensity * _LightColor0;

				// Specular Hightlight

				float3 halfVector = normalize(_WorldSpaceLightPos0 + viewDir);
				float NdotH = dot(normal, halfVector);
				float specularIntensity = pow(NdotH * lightIntensity, _Glossiness * _Glossiness);
				float specularIntensitySmooth = smoothstep(0.005, 0.01, specularIntensity);
				float4 specular = specularIntensitySmooth * _SpecularColor;				

				// Rim Licht nur auf der hellen Seite

				float rimDot = 1 - dot(viewDir, normal);
				float rimIntensity = rimDot * pow(NdotL, _RimThreshold);
				rimIntensity = smoothstep(_RimAmount - 0.01, _RimAmount + 0.01, rimIntensity);
				float4 rim = rimIntensity * _RimColor;
				float4 sample = tex2D(_MainTex, i.uv);
				return (light + _AmbientColor + specular + rim) * _Color * sample;
			}
			ENDCG
		}

        UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
	}
}