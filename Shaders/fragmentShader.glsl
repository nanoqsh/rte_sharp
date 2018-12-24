
uniform vec4 color;
uniform sampler2D tex;

in vec2 frag_tex_coord;

void main()
{
	gl_FragColor = texture2D(tex, frag_tex_coord) * color;
}
