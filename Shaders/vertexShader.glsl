
attribute vec2 coord;
attribute vec2 tex_coord;

out vec2 frag_tex_coord;

void main()
{
	frag_tex_coord = tex_coord;
	gl_Position = vec4(coord, 0.0, 1.0);
}
