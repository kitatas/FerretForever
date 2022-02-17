Shader "Custom/ResultBackground"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }

    CGINCLUDE
    #include "UnityCG.cginc"
    #define PI 3.14159265359

    float2 rotate(float2 ist)
    {
        float angle = _Time.y * 1.5;
        float2x2 rotate = float2x2(cos(angle), -sin(angle), sin(angle), cos(angle));
        float scale = 0.5;
        float2 pivot_uv = float2(0.5, 0.5);
        float2 r = (ist - pivot_uv) * (1 / scale);
        return mul(rotate, r) + pivot_uv;
    }

    float4 frag(v2f_img i) : SV_Target
    {
        float2 ist = rotate(i.uv);

        float2 st = 0.5 - ist;

        float a = atan2(st.y, st.x);

        float n = 8;
        float d = abs(sin(a * n));

        float4 s = step(0.7, d);

        return lerp(
            float4(1.0, 1.0, 0.5, 1.0),
            float4(1.0, 1.0, 0.7, 1.0),
            s);
    }

    ENDCG

    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
            ENDCG
        }
    }
}