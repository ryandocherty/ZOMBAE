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
    class classButton
    {
        //button for the initial game state
        Texture2D buttonTexture;
        Vector2 buttonPosition;
        Rectangle buttonRectangle;

        Color colour = new Color(255, 255, 255, 255);



        public Vector2 buttonSize;
        public classButton(Texture2D newTexture, GraphicsDevice graphics)
        {
            buttonTexture = newTexture;

            buttonSize = new Vector2(graphics.Viewport.Width / 6, graphics.Viewport.Height / 10 );


        }

        bool down;
        public bool isClicked;
        public void Update(MouseState mouse)
        {
            buttonRectangle = new Rectangle((int)buttonPosition.X, (int)buttonPosition.Y, (int)buttonSize.X, (int)buttonSize.Y);

            Rectangle mouseRectangle = new Rectangle(mouse.X, mouse.Y, 1,1);

            if (mouseRectangle.Intersects(buttonRectangle))
            {
                if (colour.A == 255) down = false;  //if it's visible
                if (colour.A ==0) down = true; 
                if (down) colour.A +=3; else colour.A -= 3; 
                if (mouse.LeftButton == ButtonState.Pressed) isClicked = true; 
            }
            else if (colour.A < 255)
            {
                colour.A += 3;
                isClicked = false; 
            }
               

        }



        public void SetPosition(Vector2 newPosition)
        {
            buttonPosition = newPosition;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(buttonTexture, buttonRectangle, colour);
        }

    }
}

