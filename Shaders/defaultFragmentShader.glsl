
uniform sampler2D tex;

in vec2 fr_tex_coords;

void main()
{
    gl_FragColor = texture2D(tex, fr_tex_coords);
}
