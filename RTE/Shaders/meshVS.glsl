
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
	vec3 sized = coord - (1.0 - 1.0 / float(pixelSize));
	gl_Position = projection * view * model * vec4(sized, 1.0);
}
