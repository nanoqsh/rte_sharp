
uniform sampler2D tex;
uniform vec4 ambient;

in vec3 FSNormal;
in vec2 FSTexCoord;

void main()
{
	gl_FragColor = texture2D(tex, FSTexCoord) * ambient;
}
