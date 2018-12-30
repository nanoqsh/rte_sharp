
uniform vec4 color;

uniform sampler2D tex;
uniform sampler2D tex2;

in vec2 fr_tex_coord;

void main()
{
	// gl_FragColor = mix(texture2D(tex, fr_tex_coord), texture2D(tex2, fr_tex_coord), 0.5);
	gl_FragColor = color;
}
