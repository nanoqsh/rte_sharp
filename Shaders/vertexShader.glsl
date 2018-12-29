
attribute vec3 coord;
attribute vec2 tex_coord;

uniform int pixelSize;
uniform mat4 transform;

out vec2 fr_tex_coord;

void main()
{
	fr_tex_coord = tex_coord;
	gl_Position = transform * vec4(coord - (1.0 - 1.0 / float(pixelSize)), 1.0);
}
