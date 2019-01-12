
attribute vec3 coord;
attribute vec2 texCoord;

uniform int pixelSize;
uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

out vec2 FSTexCoord;

void main()
{
	FSTexCoord = texCoord;
	vec3 sized = coord - (1.0 - 1.0 / float(pixelSize));
	gl_Position = projection * view * model * vec4(sized, 1.0);
}
