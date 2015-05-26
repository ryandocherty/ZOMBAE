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
  
    public class GameForm : Game 
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        enum GameState
        {
            MainMenu,
            Playing, 
        }

        GameState CurrentGameState = GameState.MainMenu; 

        //screen adjustments
        int screenWidth = 1200, screenHeight = 700;
        classButton buttonPlay; 


        Player player;
        Zombie zombie;
        Monster monster;
        Slender slender; 
       
   
        //enemy variables
        int maxZombies = 20, zombieSpawn = 0, zombieNext = 50;
        int maxMonsters = 13, monsterSpawn = 0, monsterNext = 50;
        int maxSlenders = 6, slenderSpawn = 0, slenderNext = 50; 
        bool isZombie = false;
        bool isMonster = false; 
        bool isSlender = false; 
        

        private SpriteFont scoreFont;
        public SpriteFont healthFont;
        public SpriteFont timerFont; 
        private int score = 0;
        private int health = 100;
        

        public Texture2D spriteTexture;
        
        Vector2 spriteDirection = Vector2.Zero;
        public Vector2 spritePosition;

        List<Bullet> bullets; //list of bullets to be used
        List<Zombie> zombies = new List<Zombie>(); //list of zombies to be used 
        List<Monster> monsters = new List<Monster>();
        List<Slender> slenders = new List<Slender>(); 

        float gameFrameTime = 0, gameSecondTime = 0;


        public GameForm()
            : base()
        {

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        
        protected override void Initialize()
        {
            
            base.Initialize();
        }
     


        protected override void LoadContent()
        {
           
            spriteBatch = new SpriteBatch(GraphicsDevice);
           
            spriteTexture = Content.Load<Texture2D>("Character1");
            Bullet.bulletTexture = Content.Load<Texture2D>("bullet");
            Player.crosshairsTexture = Content.Load<Texture2D>("crosshairs");
            Zombie.zombieTexture = Content.Load<Texture2D>("EnemyZombie");
            Monster.monsterTexture = Content.Load<Texture2D>("EnemyMonster");
            Slender.slenderTexture = Content.Load<Texture2D>("EnemySlender"); 



            //screen things 
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.ApplyChanges();
            

            buttonPlay = new classButton(Content.Load<Texture2D>("PlayButton"), graphics.GraphicsDevice); //sets the button 
            buttonPlay.SetPosition(new Vector2(510, 350)); 
 

            spritePosition = new Vector2(300, 250); 

            player = new Player(new Vector2(screenWidth/2,screenHeight/2)); //setting the initial sprite position
            player.LoadContent(Content);

            //loads the spritefont content
            scoreFont = Content.Load<SpriteFont>("ScoreFont"); 
            healthFont = Content.Load<SpriteFont>("HealthFont");
            timerFont = Content.Load<SpriteFont>("TimerFont"); 

        }



        protected override void UnloadContent()
        {
            Content.Unload();
        }



        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();

            MouseState mouse = Mouse.GetState(); 

            switch(CurrentGameState) //game state updates 
            {
                case GameState.MainMenu:
                    if (buttonPlay.isClicked == true) CurrentGameState = GameState.Playing;
                    buttonPlay.Update(mouse); 
                    break; 

                case GameState.Playing:
                    break; 
            }


            zombie = new Zombie();
            monster = new Monster();
            slender = new Slender(); 


            if (zombieSpawn >= zombieNext) //has a zombie been spawned recently? 
            {
                zombieSpawn = 0;
                isZombie = false;
            }


            if (monsterSpawn >= monsterNext)
            {
                monsterSpawn = 0;
                isMonster = false;
            }

            if (slenderSpawn >= slenderNext)
            {
                slenderSpawn = 0;
                isSlender = false; 
            }

            if (zombieSpawn == 0 && maxZombies > zombies.Count()) //has the max amount of zombies been reached? 
            {

                //if not, add more zombies
                zombies.Add(zombie);
                isZombie = true;
            }

            if (monsterSpawn == 0 && maxMonsters > monsters.Count())
            {
                monsters.Add(monster);
                isMonster = true;
            }

            if (slenderSpawn == 0 && maxSlenders > slenders.Count())
            {
                slenders.Add(slender);
                isSlender = true; 
            }


            if (isZombie == true)
            {
                zombieSpawn++;
            }



            if (isMonster == true)
            {
                monsterSpawn++;
            }

            if (isSlender == true)
            {
                slenderSpawn++; 
            }

            bullets = player.Load(); //Load the bullets from the list in Cursor
            player.Update(gameTime); //Updates the player 




            foreach (Zombie zombieItem in zombies) //has the player been hit by a zombie? 
            {
                zombieItem.Update();

                

                if (player.isHit(zombieItem.zombieBBox[0], zombieItem.zombieBBox[1]) && health != 0)
                {

                    zombieItem.isAlive = false;
                    health -= 2; //if so, take 2 away from the player's health 
                    score -= 100; //take away 100 from the score 



                    if (health <= 0f) //is the player dead? 
                    {
                        player.isAlive = false; //set player to dead
                        Exit(); 

                    }
                }
            }



            foreach (Monster monsterItem in monsters)
            {


                monsterItem.Update();

                if (player.isHit(monsterItem.monsterBBox[0], monsterItem.monsterBBox[1]) && health != 0)
                {
                    monsterItem.isAlive = false;
                    health -= 5;
                    score -= 200;


                    if (health <= 0)
                    {
                        player.isAlive = false;
                    }
                }
            }

            foreach (Slender slenderItem in slenders)
            {

                slenderItem.Update();

                {
                    if (player.isHit(slenderItem.slenderBBox[0], slenderItem.slenderBBox[1]) && health !=0)
                    {
                        slenderItem.isAlive = false;
                        health -= 30;
                        score -= 1000; 

                        if (health <= 0)
                        {
                            player.isAlive = false;
                            Exit(); 
                        }
                    }
                }
            }


            


            foreach (Bullet bull in bullets) //handles when bullets hit enemies
            {
                foreach (Zombie zom in zombies)
                {

                    foreach (Monster mon in monsters)
                    {

                        foreach (Slender slend in slenders)
                        {

                            if (bull.inBoundingBox(zom.zombieBBox[0], zom.zombieBBox[1])) //has a bullet hit an enemy? 
                            {
                                zom.isAlive = false; //zombie is now dead 
                                bull.isActive = false; //if bullet hits enemy, make it inactive
                                score += 1; //add 1 to the score 

                            }

                            if (bull.inBoundingBox(mon.monsterBBox[0], mon.monsterBBox[1]))
                            {
                                mon.isAlive = false;
                                bull.isActive = false;
                                score += 2;
                            }

                            if (bull.inBoundingBox(slend.slenderBBox[0], slend.slenderBBox[1]))
                            {
                                slend.isAlive = false;
                                bull.isActive = false;
                                score += 3; 
                            }
                        }
                    }
                }
            }






            for (int i = 0; i < zombies.Count(); i++) //checks each enemy in the list
            {

                if (!zombies[i].isAlive) //have they been hit? 
                {
                    zombies.Remove(zombies[i]); //if so, remove them
                }
            }



            for (int i = 0; i < monsters.Count(); i++)
            {


                if (!monsters[i].isAlive)
                {
                    monsters.Remove(monsters[i]);
                }
            }



            for (int i = 0; i < slenders.Count(); i++)
            {
                if (!slenders[i].isAlive)
                {
                    slenders.Remove(slenders[i]); 
                }
            }



            double deltaTime = gameTime.ElapsedGameTime.TotalSeconds;
            gameFrameTime++;

            if (gameFrameTime % 60 == 0 /*add variable for game being active (unless you're using states)*/)
            {
                gameSecondTime++;
            }


            if(gameSecondTime == 60f)
            {
                Exit();
            }

                base.Update(gameTime);
        }
        





        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(); ////
            player.Draw(spriteBatch);
            spriteBatch.DrawString(scoreFont, "SCORE: " + score, new Vector2(10, 680), Color.Black); //draws the score text
            spriteBatch.DrawString(healthFont, "HEALTH: " + health, new Vector2(1080, 680), Color.Black); //draws the health text 
            spriteBatch.DrawString(timerFont, "TIME: " + (int)(60 - gameTime.TotalGameTime.TotalSeconds), new Vector2(570, 680), Color.Black);

            switch (CurrentGameState)
            {
                case GameState.MainMenu:
                    spriteBatch.Draw(Content.Load<Texture2D>("background"), new Rectangle(0, 0, screenWidth, screenHeight), Color.White); 
                    buttonPlay.Draw(spriteBatch);
                    IsMouseVisible = true; 
                    break;

                case GameState.Playing:
                    IsMouseVisible = false; 
                    break;
            }


            spriteBatch.End(); ////


            foreach (Bullet bulletItem in bullets)
            {
                bulletItem.Draw(gameTime, spriteBatch); 
            }


            foreach (Zombie zombieItem in zombies)
            {
 
                zombieItem.Draw(gameTime, spriteBatch); 
            }

            foreach (Monster monsterItem in monsters)
            {
                monsterItem.Draw(gameTime, spriteBatch);
            }

            foreach (Slender slenderItem in slenders)
            {
                slenderItem.Draw(gameTime, spriteBatch); 
            }


            base.Draw(gameTime);
        }
    }
}
