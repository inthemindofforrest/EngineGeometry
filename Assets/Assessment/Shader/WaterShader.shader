Shader "Custom/WaterShader"
{
    Properties
    {
		_XScrollSpeed ("Scroll Speed X", Float) = 0.5
		_YScrollSpeed ("Scroll Speed Y", Float) = 0.5
        _Color ("Color", Color) = (1,1,0,.25)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Fade" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _SecondaryTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        fixed4 _Color;
		fixed _XScrollSpeed;
		fixed _YScrollSpeed;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
			fixed _OffsetX = _XScrollSpeed * _Time;//Takes the Speed and Time and makes a X offset
			fixed _OffsetY = _YScrollSpeed * _Time;//Takes the Speed and Time and makes a Y offset
			fixed2 _OffsetUV = fixed2(_OffsetX, _OffsetY);//Takes in both the x and y offset and puts them in a "Vector2"

            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, (IN.uv_MainTex + _OffsetUV)) * _Color;//Modifys the textures UV with the offset
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = .5f;
            o.Smoothness = .5f;
			o.Alpha = .25f;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
