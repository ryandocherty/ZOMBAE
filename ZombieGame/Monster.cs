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
    class Monster
    {


        public static Texture2D monsterTexture;
        public Vector2 monsterPosition;
        public Vector2 monsterDirection = Vector2.Zero;
        public Vector2[] monsterBBox = new Vector2[2];
        public bool isAlive = true;
        public float monsterSpeed = 1.0005f;



        public Monster() //constructor; what the monster does when it's initialised
        {

            //gives the monster a random spawn position
            Random rnd = new Random();
            int spawnRef = rnd.Next(0, 8);
            switch (spawnRef)
            {

                //need to by -y or +x (a lot) if I want them to appear from left or right 

                case 0:
                    monsterPosition = new Vector2(1200, 200);
                    monsterDirection += new Vector2(-1, 0);
                    break;

                case 1:
                    monsterPosition = new Vector2(1200, 120);
                    monsterDirection += new Vector2(-1, 0);
                    break;

                case 2:
                    monsterPosition = new Vector2(1300, 220);
                    monsterDirection += new Vector2(-1, 0);
                    break;

                case 3:
                    monsterPosition = new Vector2(1400, 320);
                    monsterDirection += new Vector2(-1, 0);
                    break;

                case 4:
                    monsterPosition = new Vector2(1500, 420);
                    monsterDirection += new Vector2(-1, 0);
                    break;

                case 5:
                    monsterPosition = new Vector2(1300, 100);
                    monsterDirection += new Vector2(-1, 0);
                    break;

                case 6:
                    monsterPosition = new Vector2(1250, 200);
                    monsterDirection += new Vector2(-1, 0);
                    break;

                case 7:
                    monsterPosition = new Vector2(1350, 400);
                    monsterDirection += new Vector2(-1, 0);
                    break;
            }
        }



        public void Update()
        {


            monsterDirection *= monsterSpeed;
            monsterPosition += monsterDirection;


            //bounding boxes of monsters 
            monsterBBox[0].X = monsterPosition.X;
            monsterBBox[1].X = (monsterPosition.X) + (monsterTexture.Width);
            monsterBBox[0].Y = monsterPosition.Y;
            monsterBBox[1].Y = (monsterPosition.Y) + (monsterTexture.Height);



            if (monsterPosition.X + monsterTexture.Width < 0)
            {
                monsterPosition.X = 1200;
            }
        }




       public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
           

            spriteBatch.Begin();


            if (isAlive)
            {
                spriteBatch.Draw(monsterTexture, monsterPosition, Color.White);

            }

            else
            {
                spriteBatch.Draw(monsterTexture, monsterPosition, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            }

            spriteBatch.End();
        }
    }
}

