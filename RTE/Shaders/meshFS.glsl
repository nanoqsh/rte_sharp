
uniform sampler2D tex;
uniform vec4 ambient;

in vec2 FSTexCoord;

void main()
{
	gl_FragColor = texture2D(tex, FSTexCoord) * ambient;
}
