texture particleTexture;

half4x4 WorldViewProj : WorldViewProjection;
half4x4 Projection : Projection;

half ParticleSize = 200.0f;
half ViewportHeight;

struct PS_INPUT
{
	half4 Position : POSITION0;
	#ifdef XBOX
		half4 TexCoord : SPRITETEXCOORD;
    #else
		half2 TexCoord : TEXCOORD0;
    #endif

	half4 Color : COLOR0;
	half psize : PSIZE0;
};
struct VS_OUTPUT
{
	half4 Position : POSITION0;
	#ifdef XBOX	
		half4 TexCoord : TEXCOORD0;	
    #else
		half2 TexCoord : TEXCOORD0;
    #endif

	half4 Color : COLOR0;
	half psize : PSIZE0;
};
sampler Sampler = sampler_state
{
	Texture = <particleTexture>;
	MinFilter = Linear;
    MagFilter = Linear;
    MipFilter = Linear;
};
#ifdef XBOX
VS_OUTPUT VertexShader(half4 pos : POSITION0, half4 color : COLOR0)
#else
VS_OUTPUT VertexShader(half4 pos : POSITION0, half4 color : COLOR0, half2 texCoord : TEXCOORD0)
#endif
{
	PS_INPUT Output = (PS_INPUT)0;
	
	Output.Position = mul(pos, WorldViewProj);		

	#ifdef XBOX	
	Output.TexCoord = 0;
	#else
	Output.TexCoord = texCoord;
	#endif
	
	Output.Color = color;
	
	Output.psize = ParticleSize * Projection._m11 / Output.Position.w * ViewportHeight / 2;
	
	return Output;
}
float4 PixelShader(PS_INPUT input) : COLOR0
{
	half2 texCoord;
	#ifdef XBOX
        texCoord = abs(input.TexCoord.zw);
    #else
        texCoord = input.TexCoord.xy;
    #endif
	half4 Color = tex2D(Sampler, texCoord);
	Color *= (input.Color*2);
	return Color;	
}
technique PointSpriteTechnique
{
    pass P0
    {   
        vertexShader = compile vs_2_0 VertexShader();
        pixelShader = compile ps_2_0 PixelShader();
    }
}