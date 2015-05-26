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
    class Player : AnimatedSprite
    {
       
        public static Texture2D crosshairsTexture; //static, oh well 
        public Vector2 playerCentre; //centre of the player sprite 
        public Vector2[] playerBBox = new Vector2[2];
        public float playerSpeed = 200f;
        public bool shooting = false;
        public Vector2 cursorPos; //position of cursor 
        public bool isAlive = true; 
        bool countShots;
        Bullet bullet;
        

        List<Bullet> bullets = new List<Bullet>();

        public int timeBetweenShots = 20;
        int shotTimer = 0;


        public Player(Vector2 position)
            : base(position)
        {
            FPS = 10;
            
            //each sqaure (frame) on the sprite sheet is 64x64
            //this is where each animation is created using the sprite sheet
            //the location of the individual frame strip had to be calculated by counting in increments of 64

            AddAnimation(9, 512, 0, "Up", 64, 64);
            AddAnimation(1, 512, 0, "UpIdle", 64, 64);
            AddAnimation(9, 640, 0, "Down", 64, 64);
            AddAnimation(1, 640, 0, "DownIdle", 64, 64);
            AddAnimation(9, 576, 0, "Left", 64, 64);
            AddAnimation(1, 576, 0, "LeftIdle", 64, 64);
            AddAnimation(9, 704, 0, "Right", 64, 64);
            AddAnimation(1, 704, 0, "RightIdle", 64, 64);
            AddAnimation(6, 1092, 0, "Death", 64, 64);
            PlayAnimation("DownIdle"); //initial starting animation 

        }


        public void LoadContent(ContentManager content)
        {
            playerTexture = content.Load<Texture2D>("Character1");
            Bullet.bulletTexture = content.Load<Texture2D>("bullet");

        }

       

        public List<Bullet> Load() //loads all the bullets to handle 
        {
            return bullets; 
        }



        public void Reload(List<Bullet> bull)
        {
            bullets = bull; 
        }

   



        public override void Update(GameTime gameTime)
        {
        
            spriteDirection = Vector2.Zero;
            HandleInput(Keyboard.GetState());

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds; //make sure the game runs at the same speed on different machines


            //setting the positition of the centre of the player
            //this will be used for colision detection and projectile firing
            playerCentre.X = playerPosition.X + 32;
            playerCentre.Y = playerPosition.Y + 32;               

            spriteDirection *= playerSpeed;
            playerPosition += (spriteDirection * deltaTime); //deltaTime makes the movement framerate independant 
            
            cursorPos = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);

     
            if (shotTimer >= timeBetweenShots)
            {
                shotTimer = 0;
                countShots = false;
            }

            MouseState mouse = Mouse.GetState();

            if (mouse.LeftButton == ButtonState.Pressed) //if left mouse button is pressed 
            {
                if (shotTimer == 0) //can a shot be created? 
                {

                    //create new bullet 
                    bullet = new Bullet();
                    bullet.Shoot(playerCentre, cursorPos);
                    bullets.Add(bullet);
                    countShots = true;
                }

            }

            if (countShots)
            {
                shotTimer++;
            }

            //update each bullet that this clas handles 
            foreach (Bullet bulletItem in bullets)
            {
                bulletItem.Update(gameTime);
            }


            //remove any inactive bullets 
            for (int i = 0; i < bullets.Count; i++)
            {
                if (!bullets[i].isActive)
                {
                    bullets.Remove(bullets[i]);
                }
            }
            
            
            base.Update(gameTime);
        }

        public bool isHit(Vector2 min, Vector2 max) //checks to see if player has been hit 
        {
            if ((playerCentre.X >= min.X && playerCentre.X <= max.X) && (playerCentre.Y >= min.Y && playerCentre.Y <= max.Y))
            
                return true;
                else 
                return false; 
        }





        private void HandleInput(KeyboardState keyState) //keyboard input handeling 
        {

            if (!shooting)
            {
                if (keyState.IsKeyDown(Keys.W))
                {
                    spriteDirection += new Vector2(0, -1); //up
                    PlayAnimation("Up");
                    currentDirection = direction.up;

                }

                if (keyState.IsKeyDown(Keys.A))
                {
                    spriteDirection += new Vector2(-1, 0); //left
                    PlayAnimation("Left");
                    currentDirection = direction.left;
                }

                if (keyState.IsKeyDown(Keys.S))
                {
                    spriteDirection += new Vector2(0, 1); //down
                    PlayAnimation("Down");
                    currentDirection = direction.down;
                }

                if (keyState.IsKeyDown(Keys.D))
                {
                    spriteDirection += new Vector2(1, 0); //right
                    PlayAnimation("Right");
                    currentDirection = direction.right;
                }

                if (keyState.IsKeyDown(Keys.Space))
                {
                    //shooting code
                }

                else if (!shooting)
                {
                    if (currentAnimation.Contains("Up"))
                    {
                        PlayAnimation("UpIdle");
                    }
                    if (currentAnimation.Contains("Down"))
                    {
                        PlayAnimation("DownIdle");
                    }
                    if (currentAnimation.Contains("Left"))
                    {
                        PlayAnimation("LeftIdle");
                    }
                    if (currentAnimation.Contains("Right"))
                    {
                        PlayAnimation("RightIdle");
                    }

                }

                currentDirection = direction.none;
            }
        }




      
        
        public void Draw(SpriteBatch spriteBatch) //draw bullets on screen 
        {
            spriteBatch.Draw(crosshairsTexture, cursorPos, Color.White);
            spriteBatch.Draw(playerTexture, playerPosition, spriteAnimations[currentAnimation][frameIndex], Color.White);
        }
    }
}
