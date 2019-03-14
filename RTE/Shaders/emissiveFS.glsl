#version 330 core

uniform sampler2D tex;
uniform vec4 lightColor;

in vec3 FSPosition;
in vec3 FSNormal;
in vec2 FSTexCoord;

out vec4 color;

void main()
{
	color = texture2D(tex, FSTexCoord) * lightColor;
}
