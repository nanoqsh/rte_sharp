
uniform vec4 color;
uniform sampler2D tex;

in vec2 FSTexCoord;

void main()
{
	gl_FragColor = texture2D(tex, FSTexCoord);
}
