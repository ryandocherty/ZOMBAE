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

    public abstract class AnimatedSprite 
    {

      
        protected Texture2D playerTexture;
        protected Vector2 spriteDirection = Vector2.Zero; 
        public Vector2 playerPosition;
        public int frameIndex;
        private double timeElapsed; 
        private double timeToUpdate;
        public string currentAnimation;
       
        public Vector2 frameCentre; //need the centre of each sprite frame, will be used with projectiles and collision detection 


        public Dictionary<string, Rectangle[]> spriteAnimations = new Dictionary<string, Rectangle[]>();

        public enum direction { none, up, down, left, right }; //helps with the sprite idle states 
        protected direction currentDirection; 
        
        public int FPS
        {
            set { timeToUpdate = (1f / value); }
        }


        public AnimatedSprite(Vector2 position)
        {
            //this is where all the bad things happen 
            playerPosition = position;
        }


        public void AddAnimation(int frames, int yPos, int xInitialFrame, string name, int width, int height)
        {

            Rectangle[] Rectangles = new Rectangle[frames]; //creates an array of rectangles which will be used when playing an animation 

            for (int i = 0; i < frames; i++)
            {
                Rectangles[i] = new Rectangle((i + xInitialFrame) * width, yPos, width, height);
    
            }
            spriteAnimations.Add(name, Rectangles); //add name to dictionary (run 'right' for example) 
        }




        public virtual void Update(GameTime gameTime) //updates the animation 
        {
            timeElapsed += gameTime.ElapsedGameTime.TotalSeconds;

            if (timeElapsed > timeToUpdate)
            {
                timeElapsed -= timeToUpdate;

                if (frameIndex < spriteAnimations[currentAnimation].Length-1)
                {
                    
                    frameIndex++; 
                }

                else
                {
                    frameIndex = 0; 
                }
            }
        }



        public void Draw(SpriteBatch spriteBatch) //draws the animation on screen depending on the frame
        {
            spriteBatch.Draw(playerTexture, playerPosition, spriteAnimations[currentAnimation][frameIndex], Color.White);
        }




        public void PlayAnimation(string name)
        {
            if (currentAnimation != name && currentDirection == direction.none)
            {
                currentAnimation = name;
                frameIndex = 0; //makes sure no frameIndex out of bounds exceptions occur
            }
        }

    }
}
