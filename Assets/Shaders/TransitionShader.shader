Shader "Unlit/TransitionShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            

            #include "UnityCG.cginc"

            uniform sampler2D _MainTex;
            uniform float _ShearValue;
            uniform float2 _ShearCentre;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {   
                float2 dir;
                dir = i.uv - _ShearCentre.xy;
                float3 dirFinal = cross(float3(dir.x,dir.y,0.0f),float3(0.0f,0.0f,1.0f));
                dir = dirFinal.xy;
                
                
                float2 offset = _ShearValue * normalize(dir) ;

                // sample the texture
                float4 col = tex2D(_MainTex, i.uv + offset);
        
                return col;
            }
            ENDCG
        }
    }
}
