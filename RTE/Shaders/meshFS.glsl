
uniform sampler2D tex;
uniform vec4 ambient;

uniform vec3 lightPosition;
uniform vec3 lightColor;

in vec3 FSPosition;
in vec3 FSNormal;
in vec2 FSTexCoord;

void main()
{
	vec3 normal = normalize(FSNormal);
	vec3 lightDir = normalize(lightPosition - FSPosition);
	vec3 diffuse = max(dot(normal, lightDir), 0.0f) * lightColor;

	gl_FragColor = texture2D(tex, FSTexCoord) * (ambient + vec4(diffuse, 1.0f));
}
