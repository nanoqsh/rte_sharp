#version 330 core

uniform sampler2D tex;

in vec3 FSLightColor;
in vec2 FSTexCoord;

void main()
{
	gl_FragColor = texture2D(tex, FSTexCoord) * vec4(FSLightColor, 1.0);
}
