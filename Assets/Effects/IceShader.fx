sampler uImage0 : register(s0);
sampler uImage1 : register(s1);
float3 uColor;
float3 uSecondaryColor;
float uOpacity;
float uSaturation;
float uRotation;
float uTime;
float4 uSourceRect;
float2 uWorldPosition;
float uDirection;
float3 uLightSource;
float2 uImageSize0;
float2 uImageSize1;

float4 IceShader(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
    float4 tex = tex2D(uImage0, coords);
    if(!any(tex))
        return tex;
    
    float3 ice = (tex.r + tex.g + tex.b) / 3 / tex.a;
    float2 offset = coords + uWorldPosition * 0.0001;
    ice += sin((offset.x * 800) + (offset.y * 300)) * 0.25 + 0.25;
    ice = lerp(float3(0.1,0.3,0.6), float3(0.5,0.7,1), ice * ice);
    return float4(ice.r, ice.g, ice.b, 0.5) * 0.6;
}

technique Technique1
{
    pass IceShader
    {
        PixelShader = compile ps_2_0 IceShader();
    }
}