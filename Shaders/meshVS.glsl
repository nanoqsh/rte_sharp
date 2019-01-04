
attribute vec3 coord;
attribute vec2 texCoord;

uniform int pixelSize;
uniform mat4 model;
uniform mat4 projView;

out vec2 FSTexCoord;

void main()
{
	FSTexCoord = texCoord;
	vec3 sized = coord - (1.0 - 1.0 / float(pixelSize));
	gl_Position = projView * model * vec4(sized, 1.0);
}
