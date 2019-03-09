
uniform sampler2D tex;
uniform vec4 lightColor;

in vec2 FSTexCoord;

void main()
{
	gl_FragColor = texture2D(tex, FSTexCoord) * lightColor;
}
