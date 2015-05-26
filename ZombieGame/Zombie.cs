#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
#endregion

namespace ZombieGame
{
    class Zombie
    {
        public static Texture2D zombieTexture;
        public Vector2 zombiePosition;
        public Vector2 zombieDirection = Vector2.Zero;
        public Vector2[] zombieBBox = new Vector2[2];
        public bool isAlive = true;
        public float zombieSpeed = 1.001f;





        public Zombie() //constructor; what the zombie does when it's initialised
        {

            //gives the zombie a random spawn position
            Random rnd = new Random();
         
            int spawnRef = rnd.Next(0, 9);
            switch (spawnRef)
            {

                //need to by -y or +x (a lot) if I want them to appear from left or right 

                case 0:
                    zombiePosition = new Vector2(-20, 20);
                    zombieDirection += new Vector2(1, 0);
                    break;

                case 1:
                    zombiePosition = new Vector2(-40, 120);
                    zombieDirection += new Vector2(1, 0);
                    break;

                case 2:
                    zombiePosition = new Vector2(-60, 220);
                    zombieDirection += new Vector2(1, 0);
                    break;

                case 3:
                    zombiePosition = new Vector2(-80, 320);
                    zombieDirection += new Vector2(1, 0);
                    break;

                case 4:
                    zombiePosition = new Vector2(-100, 400);
                    zombieDirection += new Vector2(1, 0);
                    break;

                case 5:
                    zombiePosition = new Vector2(-120, 630);
                    zombieDirection += new Vector2(1, 0);
                    break;

                case 6:
                    zombiePosition = new Vector2(-140, 200);
                    zombieDirection += new Vector2(1, 0);
                    break;

                case 7:
                    zombiePosition = new Vector2(-160, 400);
                    zombieDirection += new Vector2(1, 0);
                    break;

                case 8:
                    zombiePosition = new Vector2(-20, 45);
                    zombieDirection += new Vector2(1, 0);
                    break;
            }
        }





        public void Update()
        {

            zombieDirection *= zombieSpeed;
            zombiePosition += zombieDirection;


            //bounding boxes of zombies 
            zombieBBox[0].X = zombiePosition.X;
            zombieBBox[1].X = (zombiePosition.X) + (zombieTexture.Width);
            zombieBBox[0].Y = zombiePosition.Y;
            zombieBBox[1].Y = (zombiePosition.Y) + (zombieTexture.Height);


            if (zombiePosition.X + zombieTexture.Width > 1200)
            {
                zombiePosition.X = 0;
            }
        }






        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
           
            spriteBatch.Begin();


            if (isAlive)
            {
                spriteBatch.Draw(zombieTexture, zombiePosition, Color.White);

            }

            else
            {
                spriteBatch.Draw(zombieTexture, zombiePosition, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            }

            spriteBatch.End();
        }
    }
}

