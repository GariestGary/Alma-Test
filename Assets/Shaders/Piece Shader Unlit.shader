Shader "Unlit/Piece Shader Unlit"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _PatternTex ("Pattern", 2D) = "white" {}
        _PieceIndex ("Piece Index", Vector) = (0, 0, 0, 0)
        _PatternResolution ("Pieces Count", Vector) = (2, 2, 0, 0)
        _StencilComp ("Stencil Comparison", Float) = 8
        _Stencil ("Stencil ID", Float) = 0
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilReadMask ("Stencil Read Mask", Float) = 255
        _ColorMask("Color Mask", Float) = 15
    }
    
    SubShader
    {
        Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        
        Stencil
        {
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp]
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
        }
        
        ColorMask[_ColorMask]

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            sampler2D _PatternTex;
            uniform float4 _PatternTex_TexelSize;
            float4 _MainTex_ST;
            float4 _PieceIndex;
            float4 _PatternResolution;
            float4 _TestOffset;
            
            v2f vert (appdata_base v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.texcoord;
                
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                //uv starts from bottom left
                float4 actualPieceIndex = _PieceIndex;
                //actualPieceIndex.y = (_PatternResolution.y - 1) - _PieceIndex.y;
                
                float pieceSizeX = 1 / _PatternResolution.x;
                float pieceSizeY = 1 / _PatternResolution.y;
                
                float2 indexUVOffset = float2 ((1 / _PatternResolution.x) * actualPieceIndex.x, (1 / _PatternResolution.y) * actualPieceIndex.y);
                
                //current index color in pattern
                float2 patternColorUV = float2 (pieceSizeX * actualPieceIndex.x + (pieceSizeX / 2), pieceSizeY * actualPieceIndex.y + (pieceSizeY / 2));
                fixed4 patternColor = tex2D(_PatternTex, patternColorUV);
                
                float2 mainTexUV = i.uv;
                mainTexUV.x = mainTexUV.x / _PatternResolution.x + indexUVOffset.x;
                mainTexUV.y = mainTexUV.y / _PatternResolution.y + indexUVOffset.y;

                //scaling in half
                mainTexUV.x = mainTexUV.x / 0.5;
                mainTexUV.y = mainTexUV.y / 0.5;

                //adjusting position
                float offsetX = 0.5 / _PatternResolution;
                float offsetY = 0.5 / _PatternResolution;

                offsetX = offsetX + offsetX * (actualPieceIndex.x * 2);
                offsetY = offsetY + offsetY * (actualPieceIndex.y * 2);
                
                mainTexUV.x = mainTexUV.x - offsetX;
                mainTexUV.y = mainTexUV.y - offsetY;

                fixed4 col = tex2D(_MainTex, mainTexUV);
                fixed4 currentPatternColor = tex2D(_PatternTex, mainTexUV);

                if(any(currentPatternColor.rgb != patternColor.rgb))
                {
                    col.a = 0;
                }
                
                return col;
            }
            ENDCG
        }
    }
}
