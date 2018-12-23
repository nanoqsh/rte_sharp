
attribute vec2 coord;
attribute vec3 ver_color;

out vec3 frag_ver_color;

void main()
{
	frag_ver_color = ver_color;
	gl_Position = vec4(coord, 0.0, 1.0);
}
