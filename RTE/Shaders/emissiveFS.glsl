
uniform sampler2D tex;
uniform vec3 lightColor;

in vec2 FSTexCoord;

void main()
{
	gl_FragColor = texture2D(tex, FSTexCoord) * lightColor;
}
