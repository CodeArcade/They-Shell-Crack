using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LessRoomyMoreShooty.Component;

namespace LessRoomyMoreShooty.Manager
{
    public class ParticleManager
    {
        private readonly Random Random;
        public Vector2 EmitterLocation { get; set; }

        private readonly List<Particle> Particles;
        public List<Texture2D> Textures { get; set; }

        public ParticleManager()
        {
            Random = new Random();
            Particles = new List<Particle>();
        }

        public ParticleManager(Vector2 emitterLocation, List<Texture2D> textures) : this()
        {
            EmitterLocation = emitterLocation;
            Textures = textures;
        }

        public void GenerateNewParticle(Color color, int count = 1, int baseTtl = 20)
        {
            if (count <= 0) count = 1;

            for (int i = 0; i < count; i++)
            {
                Texture2D texture = Textures[Random.Next(Textures.Count)];
                Vector2 velocity = new Vector2(
                    1f * (float)(Random.NextDouble() * 2 - 1),
                    1f * (float)(Random.NextDouble() * 2 - 1)
                );

                float angle = 0;
                float angularVelocity = 0.1f * (float)(Random.NextDouble() * 2 - 1);
                float size = (float)Random.NextDouble();
                int ttl = baseTtl + Random.Next(baseTtl * 2);

                Particle p = new Particle(velocity, EmitterLocation, angle, angularVelocity, size, ttl, color, texture);
                Particles.Add(p);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch) => Particles.ForEach(p => p.Draw(gameTime, spriteBatch));

        public void Update(GameTime gameTime)
        {
            for (int i = Particles.Count - 1; i >= 0; i--)
            {
                Particles[i].Update(gameTime);
                if (Particles[i].TTL <= 0)
                {
                    Particles.RemoveAt(i);
                }
            }
        }

    }
}
