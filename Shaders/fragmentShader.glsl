
uniform vec4 color;

uniform sampler2D tex;

in vec2 fr_tex_coord;

void main()
{
	gl_FragColor = texture2D(tex, fr_tex_coord);
}
