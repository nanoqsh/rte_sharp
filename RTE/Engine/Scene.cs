using System.Collections.Generic;
using System;
using System.Linq;
using OpenTK.Graphics;
using OpenTK;

namespace RTE.Engine
{
    class Scene
    {
        public readonly string Name;
        public readonly Dictionary<string, Actor> Actors;

        private readonly SceneRenderer sceneRenderer;

        public Vector3 AmbientColor;

        private Color4 backgroundColor;
        public Color4 BackgroundColor
        {
            get => backgroundColor;
            set
            {
                backgroundColor = value;

                sceneRenderer.BackgroundColor = backgroundColor;
            }
        }

        public PointLight[] Lights;

        public Scene(string name)
        {
            Name = name;
            Actors = new Dictionary<string, Actor>();

            AmbientColor = Color4.White.ToVector3();
            backgroundColor = Color4.Black;

            sceneRenderer = new SceneRenderer(this)
            {
                BackgroundColor = BackgroundColor
            };
        }

        public void Draw()
        {
            sceneRenderer.Draw(Actors.Values.ToArray());
        }

        public Scene AddActor(Actor actor)
        {
            Actors.Add(actor.Name, actor);

            return this;
        }

        public bool RemoveActor(string name)
        {
            return Actors.Remove(name);
        }

        public Actor GetActor(string name)
        {
            if (!Actors.TryGetValue(name, out Actor result))
                throw new Exception($"Scene does not contain actor '{name}'");

            return result;
        }
    }
}
