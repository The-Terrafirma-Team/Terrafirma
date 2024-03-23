sampler uImage0 : register(s0);
sampler uImage1 : register(s1);
float3 uColor;
float3 uSecondaryColor;
float uOpacity : register(C0);
float uSaturation;
float uRotation;
float uTime;
float4 uSourceRect;
float2 uWorldPosition;
float uDirection;
float3 uLightSource;
float2 uImageSize0;
float2 uImageSize1;

float4 FilterMyShader(float2 coords : TEXCOORD0) : COLOR0
{
    float wavex = 1 - frac(coords.x + uTime * 0.5f);
    float wavey = 1 - frac(coords.y + uTime * 0.5f);

    float4 screencolor = tex2D(uImage0, coords);
    float4 color = tex2D(uImage1, float2(coords.x, wavey));
    float4 color2 = tex2D(uImage1, float2(wavex, coords.y));
    
    color = screencolor + (color * color2);
    if ((color.r + color.g + color.b) / 3 > 0.2f)
    {
        color.rgb = 0.5f;
        return screencolor + color;
    }
    return screencolor;
}

technique Technique1
{
    pass FilterMyShader
    {
        PixelShader = compile ps_2_0 FilterMyShader();
    }
}