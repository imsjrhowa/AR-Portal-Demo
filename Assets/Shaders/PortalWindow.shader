Shader "Custom/PortalWindow"
{
	SubShader
	{	
		//render objects behind the portal
		ZWrite off
		//absolutely transparent
		ColorMask 0
		//Bidirectional behaviour
		Cull off

		Stencil
		{
			Ref 1
			//set all pixels in the portal to 1
			Pass replace
		}

		Pass
		{
		}
	}
}
