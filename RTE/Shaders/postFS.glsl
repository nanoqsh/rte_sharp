#version 330 core

uniform sampler2D tex;

in vec2 FSTexCoord;

out vec4 color;

void main()
{
	color = texture2D(tex, FSTexCoord);
}
