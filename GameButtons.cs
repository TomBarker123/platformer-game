using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GameNew
{
    class GameButtons
    {
    }

    class InstructionButton
    {
        Texture2D texture;
        Vector2 position;
        Rectangle rectangle;

        Color colour = new Color(255, 255, 255, 255);

        public Vector2 size;

        public InstructionButton(Texture2D newTexture, GraphicsDevice graphics)
        {
            texture = newTexture;

            //ScreenWidth = 800, ScreenHeight = 600
            //ImgW = 100(width of image, imgH = 20(height of image)
            //size = new Vector2(graphics.Viewport.Width / 8, graphics.Viewport.Height / 30);  
            size = new Vector2(graphics.Viewport.Width / 4, graphics.Viewport.Height / 12);  //will have to be different for the leaderboard button as its dimensions are different (maybe create new class for it
        }

        bool down;
        public bool isClicked;

        public void Update(MouseState mouse)
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);

            Rectangle mouseRectangle = new Rectangle(mouse.X, mouse.Y, 1, 1);

            if (mouseRectangle.Intersects(rectangle))
            {
                if (colour.A == 255) down = false;
                if (colour.A == 0) down = true;
                if (down) colour.A += 3; else colour.A -= 3;
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
            position = newPosition;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, colour);
        }
    }



    class LeaderboardButton
    {
        Texture2D texture;
        Vector2 position;
        Rectangle rectangle;


        Color colour = new Color(255, 255, 255, 255);

        public Vector2 size;

        public LeaderboardButton(Texture2D newTexture, GraphicsDevice graphics)
        {
            texture = newTexture;

            //ScreenWidth = 800, ScreenHeight = 600
            //ImgW = 100(width of image, imgH = 20(height of image)
            //size = new Vector2(graphics.Viewport.Width / 8, graphics.Viewport.Height / 30);  
            size = new Vector2(graphics.Viewport.Width / 4, graphics.Viewport.Height / 12);  //will have to be different for the leaderboard button as its dimensions are different (maybe create new class for it
        }

        bool down;
        public bool isClicked;

        public void Update(MouseState mouse)
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);

            Rectangle mouseRectangle = new Rectangle(mouse.X, mouse.Y, 1, 1);

            if (mouseRectangle.Intersects(rectangle))
            {
                if (colour.A == 255) down = false;
                if (colour.A == 0) down = true;
                if (down) colour.A += 3; else colour.A -= 3;
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
            position = newPosition;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, colour);
        }
    }



    class Level1Button
    {
        Texture2D texture;
        Vector2 position;
        Rectangle rectangle;

        Color colour = new Color(255, 255, 255, 255);

        public Vector2 size;

        public Level1Button(Texture2D newTexture, GraphicsDevice graphics)
        {
            texture = newTexture;

            //ScreenWidth = 800, ScreenHeight = 600
            //ImgW = 100(width of image, imgH = 20(height of image)
            //size = new Vector2(graphics.Viewport.Width / 8, graphics.Viewport.Height / 30);  
            size = new Vector2(graphics.Viewport.Width / 8, graphics.Viewport.Height / 12);  //will have to be different for the leaderboard button as its dimensions are different (maybe create new class for it
        }

        bool down;
        public bool isClicked;

        public void Update(MouseState mouse)
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);

            Rectangle mouseRectangle = new Rectangle(mouse.X, mouse.Y, 1, 1);

            if (mouseRectangle.Intersects(rectangle))
            {
                if (colour.A == 255) down = false;
                if (colour.A == 0) down = true;
                if (down) colour.A += 3; else colour.A -= 3;
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
            position = newPosition;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, colour);
        }
    }



    class Level2Button
    {
        Texture2D texture;
        Vector2 position;
        Rectangle rectangle;

        Color colour = new Color(255, 255, 255, 255);

        public Vector2 size;

        public Level2Button(Texture2D newTexture, GraphicsDevice graphics)
        {
            texture = newTexture;

            //ScreenWidth = 800, ScreenHeight = 600
            //ImgW = 100(width of image, imgH = 20(height of image)
            //size = new Vector2(graphics.Viewport.Width / 8, graphics.Viewport.Height / 30);  
            size = new Vector2(graphics.Viewport.Width / 8, graphics.Viewport.Height / 12);  //will have to be different for the leaderboard button as its dimensions are different (maybe create new class for it
        }

        bool down;
        public bool isClicked;

        public void Update(MouseState mouse)
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);

            Rectangle mouseRectangle = new Rectangle(mouse.X, mouse.Y, 1, 1);

            if (mouseRectangle.Intersects(rectangle))
            {
                if (colour.A == 255) down = false;
                if (colour.A == 0) down = true;
                if (down) colour.A += 3; else colour.A -= 3;
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
            position = newPosition;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, colour);
        }
    }



    class Level3Button
    {
        Texture2D texture;
        Vector2 position;
        Rectangle rectangle;

        Color colour = new Color(255, 255, 255, 255);

        public Vector2 size;

        public Level3Button(Texture2D newTexture, GraphicsDevice graphics)
        {
            texture = newTexture;

            //ScreenWidth = 800, ScreenHeight = 600
            //ImgW = 100(width of image, imgH = 20(height of image)
            //size = new Vector2(graphics.Viewport.Width / 8, graphics.Viewport.Height / 30);  
            size = new Vector2(graphics.Viewport.Width / 8, graphics.Viewport.Height / 12);  //will have to be different for the leaderboard button as its dimensions are different (maybe create new class for it
        }

        bool down;
        public bool isClicked;

        public void Update(MouseState mouse)
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);

            Rectangle mouseRectangle = new Rectangle(mouse.X, mouse.Y, 1, 1);

            if (mouseRectangle.Intersects(rectangle))
            {
                if (colour.A == 255) down = false;
                if (colour.A == 0) down = true;
                if (down) colour.A += 3; else colour.A -= 3;
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
            position = newPosition;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, colour);
        }
    }


    class LevelSelectButton
    {
        Texture2D texture;
        Vector2 position;
        Rectangle rectangle;

        Color colour = new Color(255, 255, 255, 255);

        public Vector2 size;

        public LevelSelectButton(Texture2D newTexture, GraphicsDevice graphics)
        {
            texture = newTexture;

            //ScreenWidth = 800, ScreenHeight = 600
            //ImgW = 100(width of image, imgH = 20(height of image)
            //size = new Vector2(graphics.Viewport.Width / 8, graphics.Viewport.Height / 30);  
            size = new Vector2(graphics.Viewport.Width / 4, graphics.Viewport.Height / 12);  //will have to be different for the leaderboard button as its dimensions are different (maybe create new class for it
        }

        bool down;
        public bool isClicked;

        public void Update(MouseState mouse)
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);

            Rectangle mouseRectangle = new Rectangle(mouse.X, mouse.Y, 1, 1);

            if (mouseRectangle.Intersects(rectangle))
            {
                if (colour.A == 255) down = false;
                if (colour.A == 0) down = true;
                if (down) colour.A += 3; else colour.A -= 3;
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
            position = newPosition;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, colour);
        }
    }

    class MainMenuButton
    {
        Texture2D texture;
        Vector2 position;
        Rectangle rectangle;

        Color colour = new Color(255, 255, 255, 255);

        public Vector2 size;

        public MainMenuButton(Texture2D newTexture, GraphicsDevice graphics)
        {
            texture = newTexture;

            //ScreenWidth = 800, ScreenHeight = 600
            //ImgW = 100(width of image, imgH = 20(height of image)
            //size = new Vector2(graphics.Viewport.Width / 8, graphics.Viewport.Height / 30);  
            size = new Vector2(graphics.Viewport.Width / 4, graphics.Viewport.Height / 12);  //will have to be different for the leaderboard button as its dimensions are different (maybe create new class for it
        }

        bool down;
        public bool isClicked;

        public void Update(MouseState mouse)
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);

            Rectangle mouseRectangle = new Rectangle(mouse.X, mouse.Y, 1, 1);

            if (mouseRectangle.Intersects(rectangle))
            {
                if (colour.A == 255) down = false;
                if (colour.A == 0) down = true;
                if (down) colour.A += 3; else colour.A -= 3;
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
            position = newPosition;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, colour);
        }
    }

    class PlayButton
    {
        Texture2D texture;
        Vector2 position;
        Rectangle rectangle;

        Color colour = new Color(255, 255, 255, 255);

        public Vector2 size;


        public PlayButton(Texture2D newTexture, GraphicsDevice graphics)
        {
            texture = newTexture;

            //ScreenWidth = 800, ScreenHeight = 600
            //ImgW = 100(width of image, imgH = 20(height of image)
            //size = new Vector2(graphics.Viewport.Width / 8, graphics.Viewport.Height / 30);  
            size = new Vector2(graphics.Viewport.Width / 8, graphics.Viewport.Height / 12);  //will have to be different for the leaderboard button as its dimensions are different (maybe create new class for it
        }

        bool down;
        public bool isClicked;

        public void Update(MouseState mouse)
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);

            Rectangle mouseRectangle = new Rectangle(mouse.X, mouse.Y, 1, 1);

            if (mouseRectangle.Intersects(rectangle))
            {
                if (colour.A == 255) down = false;
                if (colour.A == 0) down = true;
                if (down) colour.A += 3; else colour.A -= 3;
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
            position = newPosition;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, colour);
        }
    }


    class QuitButton
    {
        Texture2D texture;
        Vector2 position;
        Rectangle rectangle;

        Color colour = new Color(255, 255, 255, 255);

        public Vector2 size;

        public QuitButton(Texture2D newTexture, GraphicsDevice graphics)
        {
            texture = newTexture;

            //ScreenWidth = 800, ScreenHeight = 600
            //ImgW = 100(width of image, imgH = 20(height of image)
            //size = new Vector2(graphics.Viewport.Width / 8, graphics.Viewport.Height / 30);  
            size = new Vector2(graphics.Viewport.Width / 8, graphics.Viewport.Height / 12);  //will have to be different for the leaderboard button as its dimensions are different (maybe create new class for it
        }

        bool down;
        public bool isClicked;

        public void Update(MouseState mouse)
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);

            Rectangle mouseRectangle = new Rectangle(mouse.X, mouse.Y, 1, 1);

            if (mouseRectangle.Intersects(rectangle))
            {
                if (colour.A == 255) down = false;
                if (colour.A == 0) down = true;
                if (down) colour.A += 3; else colour.A -= 3;
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
            position = newPosition;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, colour);
        }
    }
}
