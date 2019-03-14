#version 330 core

// material
uniform sampler2D tex;
uniform vec3 specularColor;
uniform float shininess;

// ambient
uniform vec3 ambient;
uniform vec3 viewPosition;

// light
struct Light
{
	vec3 position;
	vec3 color;
};

#define NLIGHTS 3
uniform Light lights[NLIGHTS];

in vec3 FSPosition;
in vec3 FSNormal;
in vec2 FSTexCoord;

out vec4 color;

vec3 CalcLight(Light light)
{
	// diffuse
	vec3 normal = normalize(FSNormal);
	vec3 lightDir = normalize(light.position - FSPosition);
	vec3 diffuse = max(dot(normal, lightDir), 0.0) * light.color;

	// specular
	vec3 viewDir = normalize(viewPosition - FSPosition);
	vec3 reflectDir = reflect(-lightDir, normal);
	float specPower = pow(max(dot(viewDir, reflectDir), 0.0), shininess);
	vec3 specular = specPower * specularColor;

	float dist = length(light.position - FSPosition);
	float att = 1.0 / (1.0 + 0.09 * dist + 0.0032 * dist * dist);

	return ambient + (diffuse + specular) * att;
}

void main()
{
	vec3 result = vec3(0);

	result += CalcLight(lights[0]);
	result += CalcLight(lights[1]);
	result += CalcLight(lights[2]);

	color = texture2D(tex, FSTexCoord) * vec4(result, 1.0);
}
