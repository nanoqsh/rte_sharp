
uniform vec4 color;

in vec3 frag_ver_color;

void main()
{
	gl_FragColor = vec4(frag_ver_color, 1.0);
}
