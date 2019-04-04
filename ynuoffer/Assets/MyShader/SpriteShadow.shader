Shader "My/SpriteShadow"
{
    Properties
    {
        _MainTex("Albedo (RGB)", 2D) = "white" {}
		_Color("Color Tint",Color)=(1,1,1,1)
		_AlphaCut("AlphaCut",Range(0,1))=1
     }
 
    SubShader
    {
        Tags { "RenderType"="TransparentCutOut" "Queue"="Geometry" }
        LOD 200
 
        CGPROGRAM
        #pragma surface surf Lambert addShadow alphatest:_AlphaCut
 
        sampler2D _MainTex;
        fixed4 _Color;
		
 
 
        struct Input
        {
            float2 uv_MainTex;
			fixed4 color:COLOR;
        };
 
        void surf(Input IN, inout SurfaceOutput o)
        {
            half4 c = tex2D(_MainTex, IN.uv_MainTex)*IN.color*_Color;
 
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
