
attribute vec2 position;
attribute vec2 texCoords;

out vec2 FSTexCoord;

void main()
{
	FSTexCoord = texCoords;
    gl_Position = vec4(position, 0.0, 1.0);
}
