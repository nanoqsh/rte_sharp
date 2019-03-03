
attribute vec3 coord;
attribute vec2 texCoord;

uniform int pixelSize;
uniform mat4 model;
uniform mat4 projection;
uniform mat4 view;

out vec2 FSTexCoord;

void main()
{
	FSTexCoord = vec2(texCoord.x, 1.0 - texCoord.y);
	gl_Position = projection * view * model * vec4(coord, 1.0);
}
