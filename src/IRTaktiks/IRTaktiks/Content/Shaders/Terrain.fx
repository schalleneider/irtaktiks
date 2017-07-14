float4x4 World;
float4x4 View;
float4x4 Projection;

float MaxHeight;

texture TerrainHeightmap;
sampler displacementSampler = sampler_state
{
    Texture   = <TerrainHeightmap>;
    MipFilter = Point;
    MinFilter = Point;
    MagFilter = Point;
    AddressU  = Clamp;
    AddressV  = Clamp;
};

texture SandTexture;
sampler sandSampler = sampler_state
{
    Texture   = <SandTexture>;
    MipFilter = Linear;
    MinFilter = Linear;
    MagFilter = Linear;
    AddressU  = Wrap;
    AddressV  = Wrap;
};

texture GrassTexture;
sampler grassSampler = sampler_state
{
    Texture   = <GrassTexture>;
    MipFilter = Linear;
    MinFilter = Linear;
    MagFilter = Linear;
    AddressU  = Wrap;
    AddressV  = Wrap;
};

texture RockTexture;
sampler rockSampler = sampler_state
{
    Texture   = <RockTexture>;
    MipFilter = Linear;
    MinFilter = Linear;
    MagFilter = Linear;
    AddressU  = Wrap;
    AddressV  = Wrap;
};

texture SnowTexture;
sampler snowSampler = sampler_state
{
    Texture   = <SnowTexture>;
    MipFilter = Linear;
    MinFilter = Linear;
    MagFilter = Linear;
    AddressU  = Wrap;
    AddressV  = Wrap;
};

struct VS_INPUT {
    float4 position	: POSITION;
    float4 uv : TEXCOORD0;
};
struct VS_OUTPUT
{
    float4 position  : POSITION;
    float4 uv : TEXCOORD0;
    float4 worldPos : TEXCOORD1;
    float4 textureWeights : TEXCOORD2;
};

float textureSize = 256.0f;
float texelSize =  1.0f / 256.0f; //size of one texel;

float4 tex2Dlod_bilinear( sampler texSam, float4 uv )
{

float4 height00 = tex2Dlod(texSam, uv);
float4 height10 = tex2Dlod(texSam, uv + float4(texelSize, 0, 0, 0)); 
float4 height01 = tex2Dlod(texSam, uv + float4(0, texelSize, 0, 0)); 
float4 height11 = tex2Dlod(texSam, uv + float4(texelSize , texelSize, 0, 0)); 

float2 f = frac( uv.xy * textureSize );

float4 tA = lerp( height00, height10, f.x );
float4 tB = lerp( height01, height11, f.x );

return lerp( tA, tB, f.y );
}

 
VS_OUTPUT Transform(VS_INPUT In)
{
    VS_OUTPUT Out = (VS_OUTPUT)0;
    float4x4 viewProj = mul(View, Projection);
    float4x4 worldViewProj= mul(World, viewProj);
        
    float height = tex2Dlod_bilinear( displacementSampler, float4(In.uv.xy,0,0)).r;

    In.position.y = height * MaxHeight;
    Out.worldPos = mul( In.position, World);
    Out.position = mul( In.position , worldViewProj);
    Out.uv = In.uv;
    float4 TexWeights = 0;
    
    TexWeights.x = saturate( 1.0f - abs(height - 0.0) / 0.15f );
    TexWeights.y = saturate( 1.0f - abs(height - 0.3) / 0.25f );
    TexWeights.z = saturate( 1.0f - abs(height - 0.6) / 0.25f );
    TexWeights.w = saturate( 1.0f - abs(height - 0.9) / 0.25f );
    float totalWeight = TexWeights.x + TexWeights.y + TexWeights.z + TexWeights.w;
    TexWeights /= totalWeight;
	Out.textureWeights = TexWeights;

    return Out;
}

float4 PixelShader(in float4 uv : TEXCOORD0, in float4 weights : TEXCOORD2) : COLOR
{
	float4 sand  = tex2D(sandSampler , uv*8);
	float4 grass = tex2D(grassSampler, uv*8);
	float4 rock  = tex2D(rockSampler , uv*8);
	float4 snow  = tex2D(snowSampler , uv*8);
	
	return sand * weights.x + grass * weights.y + rock * weights.z + snow * weights.w;		
}

technique GridDraw
{
    pass P0
    {
        vertexShader = compile vs_3_0 Transform();
        pixelShader  = compile ps_3_0 PixelShader();
    }
}
