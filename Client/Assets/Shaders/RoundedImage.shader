Shader "UI/RoundedImage"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)

        _StencilComp ("Stencil Comparison", Float) = 8
        _Stencil ("Stencil ID", Float) = 0
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilReadMask ("Stencil Read Mask", Float) = 255
        _ColorMask ("Color Mask", Float) = 15

        _RectParams ("Rect Params", Vector) = (100,100,0,0)
        _CornerRadii ("Corner Radii", Vector) = (20,20,20,20)
        _BorderWidth ("Border Width", Float) = 0
        _BorderColor ("Border Color", Color) = (1,1,1,1)
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Stencil
        {
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp]
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
        }

        Cull Off
        Lighting Off
        ZWrite Off
        ZTest [unity_GUIZTestMode]
        Blend SrcAlpha OneMinusSrcAlpha
        ColorMask [_ColorMask]

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "UnityUI.cginc"

            #pragma multi_compile_local _ UNITY_UI_CLIP_RECT
            #pragma multi_compile_local _ UNITY_UI_ALPHACLIP

            struct appdata
            {
                float4 vertex : POSITION;
                float4 color : COLOR;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                fixed4 color : COLOR;
                float2 uv : TEXCOORD0;
                float4 worldPos : TEXCOORD1;
            };

            sampler2D _MainTex;
            fixed4 _Color;
            float4 _ClipRect;
            float4 _RectParams;
            float4 _CornerRadii;
            float _BorderWidth;
            fixed4 _BorderColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.worldPos = v.vertex;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.color = v.color * _Color;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 size = _RectParams.xy;
                float2 p = (i.uv - 0.5) * size; // piksel cinsinden merkezden uzaklık

                // x: TL, y: TR, z: BR, w: BL
                float r = p.x < 0
                    ? (p.y > 0 ? _CornerRadii.x : _CornerRadii.w)
                    : (p.y > 0 ? _CornerRadii.y : _CornerRadii.z);
                r = min(r, min(size.x, size.y) * 0.5);

                float2 q = abs(p) - size * 0.5 + r;
                float d = length(max(q, 0.0)) + min(max(q.x, q.y), 0.0) - r;

                float aa = max(fwidth(d), 0.0001);
                float alpha = saturate(0.5 - d / aa);

                fixed4 col = tex2D(_MainTex, i.uv) * i.color;

                if (_BorderWidth > 0.001)
                {
                    float borderT = saturate(0.5 + (d + _BorderWidth) / aa);
                    col = lerp(col, _BorderColor, borderT);
                }

                col.a *= alpha;

                #ifdef UNITY_UI_CLIP_RECT
                col.a *= UnityGet2DClipRect(i.worldPos.xy, _ClipRect);
                #endif
                #ifdef UNITY_UI_ALPHACLIP
                clip(col.a - 0.001);
                #endif

                return col;
            }
            ENDCG
        }
    }
}