
attribute vec2 position;
attribute vec2 tex_coords;

out vec2 fr_tex_coords;

void main()
{
	fr_tex_coords = tex_coords;
    gl_Position = vec4(position, 0.0, 1.0);
}
