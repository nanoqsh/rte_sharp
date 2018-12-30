
attribute vec3 coord;
attribute vec2 tex_coord;

uniform int pixelSize;
uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

out vec2 fr_tex_coord;

void main()
{
	fr_tex_coord = tex_coord;
	vec3 sized = coord - (1.0 - 1.0 / float(pixelSize));
	gl_Position = projection * view * model * vec4(sized, 1.0);
}
