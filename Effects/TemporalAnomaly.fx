sampler uImage0 : register(s0);
sampler uImage1 : register(s1);
sampler uImage2 : register(s2);
sampler uImage3 : register(s3);
float3 uColor;
float3 uSecondaryColor;
float2 uScreenResolution;
float2 uScreenPosition;
float2 uTargetPosition;
float2 uDirection;
float uOpacity;
float uTime;
float uIntensity;
float uProgress;
float2 uImageSize1;
float2 uImageSize2;
float2 uImageSize3;
float2 uImageOffset;
float uSaturation;
float4 uSourceRect;
float2 uZoom;

float smoothness;

float4 AnomalyShader(float2 coords : TEXCOORD0) : COLOR0
{
    float2 oldCoords = coords;
    float2 targetCoords = (uTargetPosition - uScreenPosition) / uScreenResolution;
    float2 centreCoords = (coords - targetCoords) * (uScreenResolution / uScreenResolution.y);
    float dotField = saturate(dot(centreCoords, centreCoords) * 4);

    coords.x -= ((coords.x * cos(dotField)) - (coords.y * sin(dotField))) * (1 - dotField) * uOpacity;
    coords.y -= ((coords.x * sin(dotField)) + (coords.y * cos(dotField))) * (1 - dotField) * uOpacity;
    
    float4 color = tex2D(uImage0, coords);

    float progressField = dotField;
    
    float threshold = abs(oldCoords.y - targetCoords.y);
    
    if (threshold <= uIntensity)
    {
        dotField -= (((1 - saturate(abs(oldCoords.x - targetCoords.x))) * ((threshold / pow(threshold, uDirection.x)) / 40)) / 420);
    }
    
    dotField += sin(uTime * uImageOffset.x) * uImageOffset.y;
    
    float3 additiveColor = uColor - color.rgb * uSecondaryColor.r;
    
    color.rgb += (additiveColor * saturate((1 - (dotField * uSecondaryColor.g))) * uOpacity);
    
    if (abs(progressField - uProgress * uProgress) < smoothness)
    {
        float smness = (progressField - uProgress * uProgress) / smoothness;
        smness = 0.5f * (smness + 1);
        color.rgb = smoothstep(0, 1, smness);
    }
    
    else if (progressField < uProgress * uProgress)
    {
        color.rgb = 0;
    }
    
    return color;
}

technique Technique1
{
    pass CreateAnomaly
    {
        PixelShader = compile ps_2_0 AnomalyShader();
    }
}