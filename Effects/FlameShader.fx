sampler uImage0 : register(s0);
sampler uImage1 : register(s1);
float3 uColor;
float3 uSecondaryColor;
float uOpacity : register(C0);
float uSaturation;
float uRotation;
float4 uSourceRect;
float2 uWorldPosition;
float uDirection;
float3 uLightSource;
float2 uImageSize0;
float2 uImageSize1;
float4 uShaderSpecificData;
    
float4 Effect(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
    float4 color = tex2D(uImage0, coords);
    if(color.r < uOpacity)
        color.rgb = 0;
    else
        color.rgb *= 2;
    return color * sampleColor * (color.r * 0.5f);
}
    
technique Technique1
{
    pass Effect
    {
        PixelShader = compile ps_2_0 Effect();
    }
}

