Shader "Metaball/MetaballParticle" {
Properties
{
    // _Color ("Color", Color) = (1,1,1,1)
    _Scale ("Scale", Range(0,0.05)) = 0.01
    _Cutoff ("Cutoff", Range(0,05)) = 0.01
    _MainTex ("Texture", 2D) = "white" {}
}

SubShader
{
    Tags
    {
        "Queue"="Transparent"
        "IgnoreProjector"="True"
        "RenderType"="Transparent"
        "PreviewType"="Plane"
    }

    Cull Off//ó†Ç‡ï`âÊÇ∑ÇÈ
    Lighting Off
    ZWrite Off//transparentÇ≈égópÇ∑ÇÈïîï™
    Blend One OneMinusSrcAlpha

    Pass
    {
        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag
        #pragma multi_compile_fog

        #include "UnityCG.cginc"

        struct appdata_t
        {
            float4 vertex   : POSITION;
            // float4 color    : COLOR;
            float2 texcoord : TEXCOORD0;
        };

        struct v2f
        {
            float4 vertex   : SV_POSITION;
            // fixed4 color    : COLOR;
            float2 texcoord : TEXCOORD0;
        };

        // fixed4 _Color;
        fixed _Scale;
        fixed _Cutoff;

        v2f vert(appdata_t IN)
        {
                v2f OUT;
                OUT.vertex = UnityObjectToClipPos(IN.vertex);
                OUT.texcoord = IN.texcoord;
                // OUT.color = IN.color * _Color;
                return OUT;
        }

        fixed4 frag (v2f i) : SV_Target {
            fixed2 uv = i.texcoord - 0.5;
            fixed a = 1 / (uv.x * uv.x + uv.y * uv.y);
            a *= _Scale;
            fixed4 color = 1 * a;
            // color *= a;
            // fixed4 color = (1,1,1,1) * a;
            // fixed4 color = (1,1,1,1) * a;
            clip(color.a - _Cutoff);//à¯êîÇ™0à»â∫ÇÃÇ∆Ç´äÆëSìßñæ
            return color;
        }
     ENDCG
     }
}
}