
uniform sampler2D tex;

in vec2 fr_tex_coords;

void main()
{
    gl_FragColor = mix(texture2D(tex, fr_tex_coords), vec4(1.0, 0.7, 0.3, 1.0), 0.6);
}
