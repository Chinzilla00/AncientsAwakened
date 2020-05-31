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

float4 TooltipShader(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
    float4 colourSample = tex2D(uImage0, float2(coords.x, coords.y + (sin((coords.x * uOpacity) + (uTime * 4)) * uSaturation)));
    float wave = 1 - frac(coords.x + uTime);
    if (any(colourSample))
    {
        float3 additiveColor = (float3(103, 0, 158) / 255.0f) - colourSample.rgb;
        colourSample.rgb += (additiveColor * 0.75f * wave);
    }
    return colourSample * sampleColor;
}

technique Technique1
{
    pass ShadeTooltip
    {
        PixelShader = compile ps_2_0 TooltipShader();
    }
}