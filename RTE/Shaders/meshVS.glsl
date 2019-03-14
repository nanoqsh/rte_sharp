#version 330 core

in vec3 position;
in vec3 normal;
in vec2 texCoord;

uniform mat4 model;
uniform mat4 projView;
uniform mat3 normalMatrix;

out vec3 FSPosition;
out vec3 FSNormal;
out vec2 FSTexCoord;

void main()
{
	FSNormal = normalMatrix * normal;
	FSPosition = vec3(model * vec4(position, 1.0));

	FSTexCoord = vec2(texCoord.x, 1.0 - texCoord.y);
	gl_Position = projView * model * vec4(position, 1.0);
}
