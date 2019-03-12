#version 330 core

uniform sampler2D tex;
uniform vec3 specularColor;
uniform float shininess;

uniform vec3 ambient;
uniform vec3 viewPosition;

uniform vec3 lightPosition;
uniform vec3 lightColor;

in vec3 FSPosition;
in vec3 FSNormal;
in vec2 FSTexCoord;

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
    vec3 specular = 0.6 * specPower * specularColor;

	vec3 result = ambient + diffuse + specular;
	gl_FragColor = texture2D(tex, FSTexCoord) * vec4(result, 1.0);
}
