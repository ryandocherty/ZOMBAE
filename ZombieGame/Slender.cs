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
    class Slender
    {


        public static Texture2D slenderTexture;
        public Vector2 slenderPosition;
        public Vector2 slenderDirection = Vector2.Zero;
        public Vector2[] slenderBBox = new Vector2[2];
        public bool isAlive = true;
        public float slenderSpeed = 1.003f;
       




        public Slender() //constructor; what the slender does when it's initialised
        {

            //gives the slender a random spawn position
            Random rnd = new Random();
            int spawnRef = rnd.Next(0, 8);
            switch (spawnRef)
            {

                //need to by -y or +x (a lot) if I want them to appear from left or right 

                case 0:
                    slenderPosition = new Vector2(1200, 100);
                    slenderDirection += new Vector2(-1, 0);
                    break;

                case 1:
                    slenderPosition = new Vector2(1200, 120);
                    slenderDirection += new Vector2(-1, 0);
                    break;

                case 2:
                    slenderPosition = new Vector2(1300, 220);
                    slenderDirection += new Vector2(-1, 0);
                    break;

                case 3:
                    slenderPosition = new Vector2(1400, 320);
                    slenderDirection += new Vector2(-1, 0);
                    break;

                case 4:
                    slenderPosition = new Vector2(1500, 420);
                    slenderDirection += new Vector2(-1, 0);
                    break;

                case 5:
                    slenderPosition = new Vector2(1300, 520);
                    slenderDirection += new Vector2(-1, 0);
                    break;

                case 6:
                    slenderPosition = new Vector2(1250, 620);
                    slenderDirection += new Vector2(-1, 0);
                    break;

                case 7:
                    slenderPosition = new Vector2(1350, 500);
                    slenderDirection += new Vector2(-1, 0);
                    break;
            }
        }



        public void Update()
        {
            slenderDirection *= slenderSpeed;
            slenderPosition += slenderDirection;

            //bounding boxes of slenders 
            slenderBBox[0].X = slenderPosition.X;
            slenderBBox[1].X = (slenderPosition.X) + (slenderTexture.Width);
            slenderBBox[0].Y = slenderPosition.Y;
            slenderBBox[1].Y = (slenderPosition.Y) + (slenderTexture.Height);



            if (slenderPosition.X + slenderTexture.Width < 0)
            {
                slenderPosition.X = 1200;
            }
        }




        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {


            spriteBatch.Begin();


            if (isAlive)
            {
                spriteBatch.Draw(slenderTexture, slenderPosition, Color.White);

            }

            else
            {
                spriteBatch.Draw(slenderTexture, slenderPosition, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            }

            spriteBatch.End();
        }
    }
}

