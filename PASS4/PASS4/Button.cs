//Author: Adar Kahiri
//File Name: Button.cs
//Project Name: PASS4
//Creation Date: Jan 27, 2021
//Modified Date: Jan 27, 2021
/* Description: This is the button class. It is used to display buttons that execute functions when pressed
 */
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace PASS4
{
    public class Button
    {
        Texture2D buttonTexture;
        Rectangle destRec;
        ButtonAction buttonAction;
        MouseState mouseState;
        Point mouseCoords;
        SpriteFont btnFont;
        string buttonText;
        Vector2 textPos;
        Color textColor;


        public delegate void ButtonAction();

        public Button(string buttonText, Vector2 textPos, ButtonAction buttonAction, Rectangle destRec, Color btnColor, Color textColor, SpriteFont btnFont)
        {
            this.destRec = destRec;
            this.buttonAction = buttonAction;
            buttonTexture = Helper.GetColouredRec(destRec, btnColor);

            this.buttonText = buttonText;
            this.textPos = textPos;
            this.textColor = textColor;
            this.btnFont = btnFont;
            
        }

        public void Update()
        {
            mouseState = Mouse.GetState();
            mouseCoords.X = mouseState.X;
            mouseCoords.Y = mouseState.Y;

            if (destRec.Contains(mouseCoords) && mouseState.LeftButton == ButtonState.Pressed)
            {
                buttonAction();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(buttonTexture, destRec, Color.White);
            spriteBatch.DrawString(btnFont, buttonText, textPos, textColor);
        }
        
    }
}
