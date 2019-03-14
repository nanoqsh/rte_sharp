#version 330 core

// material
uniform sampler2D tex;
uniform float opacity;

// ambient
uniform vec3 ambient;

// light
uniform vec3 lightPosition;
uniform vec3 lightColor;

in vec3 FSPosition;
in vec3 FSNormal;
in vec2 FSTexCoord;

out vec4 color;

void main()
{
	// diffuse
	vec3 normal = normalize(FSNormal);
	vec3 lightDir = normalize(lightPosition - FSPosition);
	vec3 diffuse = max(dot(normal, lightDir), 0.0) * lightColor;

	color = texture2D(tex, FSTexCoord) * vec4(ambient + diffuse, opacity);
}
