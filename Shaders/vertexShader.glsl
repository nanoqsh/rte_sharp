
attribute vec2 coord;
attribute vec2 tex_coord;

uniform int pixelSize;

out vec2 fr_tex_coord;

void main()
{
	fr_tex_coord = tex_coord;
	gl_Position = vec4(coord - (1.0 - 1.0 / float(pixelSize)), 0.0, 1.0);
}
