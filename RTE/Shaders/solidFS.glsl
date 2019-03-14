#version 330 core

// material
uniform vec3 matColor;
uniform vec3 specularColor;
uniform float shininess;

// ambient
uniform vec3 ambient;
uniform vec3 viewPosition;

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

	// specular
	vec3 viewDir = normalize(viewPosition - FSPosition);
	vec3 reflectDir = reflect(-lightDir, normal);
	float specPower = pow(max(dot(viewDir, reflectDir), 0.0), shininess);
	vec3 specular = specPower * specularColor;

	vec3 result = ambient + diffuse + specular;
	color = vec4(matColor * result, 1.0);
}
