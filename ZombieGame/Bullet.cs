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
    class Bullet 
    {

        public static Texture2D bulletTexture;
        public Vector2 bulletPosition; 
        public Vector2 bulletVelocity;
        public static float bulletSpeed = 20f;
        public double bulletFiringAngle; 
        public bool isActive = true;
        public Player player;
        
      

        public void Shoot(Vector2 playerOrigin, Vector2 mouseOrigin) 
        {
            //this method decides where to shoot from and the angle the bullet travels on 
            //this method could've been written better to be honest
            
            player = new Player(playerOrigin);
            
            //get the player centre at the time of shooting 
            player.playerCentre.X = player.playerPosition.X + 32;
            player.playerCentre.Y = player.playerPosition.Y + 32;

            player.cursorPos = mouseOrigin;
            bulletPosition = player.playerPosition; //where the bullet fires from 

            //now that I have the playerCentre and mouseOrigin, I can do some trig 
            //the angle the bullet moves along wasn't entirely accurate, so I added 32 pixels which seemed to fix it
            bulletFiringAngle = Math.Atan2((mouseOrigin.Y+32 - player.playerCentre.Y), (mouseOrigin.X+32 - player.playerCentre.X)); 
        }


        public void Update(GameTime gameTime)
        {
            bulletVelocity = new Vector2((float)Math.Cos(bulletFiringAngle) * bulletSpeed, (float)Math.Sin(bulletFiringAngle) * bulletSpeed);
            bulletPosition += bulletVelocity;
        }


        public bool inBoundingBox(Vector2 Min, Vector2 Max)
        {
            if ((bulletPosition.X >= Min.X && bulletPosition.X <= Max.X) && (bulletPosition.Y >= Min.Y && bulletPosition.Y <= Max.Y)) return true;
            else return false; 
        }

       


        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(bulletTexture, bulletPosition, Color.White);
            spriteBatch.End(); 
        }


        public void RemoveBullet()
        {
            isActive = false; 
        }


    }
}
