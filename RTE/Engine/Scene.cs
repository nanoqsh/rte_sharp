using System.Collections.Generic;

namespace RTE.Engine
{
    class Scene
    {
        public readonly string Name;
        public readonly List<Actor> Actors;

        public Scene(string name)
        {
            Name = name;
            Actors = new List<Actor>();
        }

        public Scene AddActor(Actor actor)
        {
            Actors.Add(actor);

            return this;
        }
    }
}
