#version 330 core

in vec3 position;
in vec3 normal;
in vec2 texCoord;

uniform mat4 model;
uniform mat4 projView;
uniform mat3 normalMatrix;

uniform vec3 ambient;
uniform vec3 viewPosition;

uniform vec3 specularColor;
uniform vec3 lightPosition;
uniform vec3 lightColor;

out vec3 FSLightColor;
out vec2 FSTexCoord;

void main()
{
	vec3 modelPosition = vec3(model * vec4(position, 1.0));

	// diffuse
	vec3 normal = normalize(normalMatrix * normal);
	vec3 lightDir = normalize(lightPosition - modelPosition);
	vec3 diffuse = max(dot(normal, lightDir), 0.0) * lightColor;

	// specular
	float specularStrength = 0.6;
	vec3 viewDir = normalize(viewPosition - modelPosition);
	vec3 reflectDir = reflect(-lightDir, normal);
	float specPower = pow(max(dot(viewDir, reflectDir), 0.0), 16);
	vec3 specular = specularStrength * specPower * specularColor;

	FSLightColor = ambient + diffuse + specular;

	FSTexCoord = vec2(texCoord.x, 1.0 - texCoord.y);
	gl_Position = projView * model * vec4(position, 1.0);
}
