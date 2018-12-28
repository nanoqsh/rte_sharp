
attribute vec2 coord;
attribute vec2 tex_coord;

out vec2 fr_tex_coord;

void main()
{
	fr_tex_coord = tex_coord;
	float size = 8.0;
	gl_Position = vec4(coord - (1.0 - 1.0 / size), 0.0, 1.0);
}
