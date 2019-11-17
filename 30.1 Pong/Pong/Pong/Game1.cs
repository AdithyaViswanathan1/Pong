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

namespace Pong
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;
        Texture2D box;

        Texture2D ballTex;
        Rectangle ballRect;
        double ballSpeedX;
        double ballSpeedY;

        Rectangle top;
        Rectangle bottom;
        Rectangle left;
        Rectangle right;

        Texture2D all;
        Rectangle allRec;

        Rectangle green1RecSource;
        Rectangle green2RecSource;
        Rectangle paddleBlueRecSource;
        Rectangle paddleGreenRecSource;
        Rectangle paddleGrayRecSource;
        Rectangle ballRecSource;
        Rectangle squareRecSource;

        Rectangle green1RecDest;
        Rectangle green2RecDest;
        Rectangle paddleBlueRecDest;
        Rectangle paddleGreenRecDest;
        Rectangle paddleGrayRecDest;
        Rectangle ballRecDest;
        Rectangle squareRecDest;

        int p1Score;
        int p2Score;

        int p1GameScore;
        int p2GameScore;

        Random rand;

        float ballRotation;
        double spin;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            int screenWidth = graphics.GraphicsDevice.Viewport.Width;
            int screenHeight = graphics.GraphicsDevice.Viewport.Height;
            top = new Rectangle(0, 0, screenWidth, 5);
            bottom = new Rectangle(0, screenHeight, screenWidth, 20);
            left = new Rectangle(0, 0, 0, screenHeight);
            right = new Rectangle(screenWidth, 0, 0, screenHeight);
            ballRect = new Rectangle(50, 50, 20, 20);
            ballSpeedX = 2;
            ballSpeedY = 3;
            //ballSpeedX = 1;
            //ballSpeedY = 0;

            green1RecSource = new Rectangle(0, 0, 800, 480);
            green2RecSource = new Rectangle(0, 481, 800, 480);
            paddleBlueRecSource = new Rectangle(801, 714, 32, 128);
            //paddleGreenRecSource = new Rectangle(1515, 0, 32, 128);
            paddleGrayRecSource = new Rectangle(834, 714, 32, 128);
            ballRecSource = new Rectangle(801, 0, 713, 713);
            squareRecSource = new Rectangle(867, 714, 16, 16);

            green1RecDest = new Rectangle(0, 0, 800, 480);
            green2RecDest = new Rectangle(0, 481, 800, 480);
            paddleBlueRecDest = new Rectangle(-18, 10, 32, 128);
            //paddleGreenRecDest = new Rectangle(0, 0, 32, 128);
            paddleGrayRecDest = new Rectangle(graphics.PreferredBackBufferWidth-14, 10, 32, 128);
            //ballRecDest = new Rectangle(150, 120, 20, 20);
            ballRecDest = new Rectangle(screenWidth/2, screenHeight/2, 20, 20);
            squareRecDest = new Rectangle(graphics.PreferredBackBufferWidth/2, graphics.PreferredBackBufferHeight/2, 16, 16);

            p1Score = 0;
            p2Score = 0;
            p1GameScore = 0;
            p2GameScore = 0;
            rand = new Random();
            ballRotation = 0;
            spin = 1;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            ballTex = Content.Load<Texture2D>("orange ping pong ball");
            all = Content.Load<Texture2D>("Pong Sprite Sheet");
            font = Content.Load<SpriteFont>("SpriteFont1");
            box = Content.Load<Texture2D>("box");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            KeyboardState kb = Keyboard.GetState();
            // Allows the game to exit
            if (kb.IsKeyDown(Keys.Escape))
                this.Exit();
            if (kb.IsKeyDown(Keys.R))
                Initialize();

            // TODO: Add your update logic here
            ballRecDest.X += (int)ballSpeedX;
            ballRecDest.Y += (int)ballSpeedY;

            if (ballRecDest.Intersects(top))
            {
                ballSpeedY *= -1;
            }
            if (ballRecDest.Intersects(bottom))
            {
                ballSpeedY *= -1;
            }
            if (ballRecDest.Intersects(left))
            {
                ballSpeedX *= -1;
                p2Score++;
                ballRecDest.X = graphics.PreferredBackBufferWidth / 2;
                ballRecDest.Y = graphics.PreferredBackBufferHeight / 2;
                ballSpeedX = rand.Next(-4, -2);
                ballSpeedY = rand.Next(-3, 3);
                spin = 0;
            }
            if (ballRecDest.Intersects(right))
            {
                ballSpeedX *= -1;
                p1Score++;
                ballRecDest.X = graphics.PreferredBackBufferWidth / 2;
                ballRecDest.Y = graphics.PreferredBackBufferHeight / 2;
                ballSpeedX = rand.Next(2, 4);
                ballSpeedY = rand.Next(-3, 3);
                //ballSpeedY = rand.Next(-3, 3);
                spin = 0;
            }

            
            if(kb.IsKeyDown(Keys.W) && paddleBlueRecDest.Y >= 0)
            {
                paddleBlueRecDest.Y-=10;
            }
            if (kb.IsKeyDown(Keys.S) && paddleBlueRecDest.Y <= graphics.PreferredBackBufferHeight-128)
            {
                paddleBlueRecDest.Y += 10;
            }

            if (kb.IsKeyDown(Keys.Up) && paddleGrayRecDest.Y >= 0)
            {
                paddleGrayRecDest.Y -= 10;
            }
            if (kb.IsKeyDown(Keys.Down) && paddleGrayRecDest.Y <= graphics.PreferredBackBufferHeight-128)
            {
                paddleGrayRecDest.Y += 10;
            }

            if (ballRecDest.Intersects(paddleGrayRecDest))
            {
                
                //ballSpeedX *= -1;

                if (spin > 0) //CW
                {
                    if (ballSpeedX > 0 && ballSpeedY < 0) // up and to the right
                    {
                        //ballSpeedX *= -1;
                        ballSpeedY *= 2;
                    }
                    else if (ballSpeedX > 0 && ballSpeedY > 0 && ballSpeedY != 0) // down and to the right
                    {
                        //ballSpeedX *= -1;
                        ballSpeedY *= .5;
                    }
                    else if (ballSpeedY - .5 < 0 && ballSpeedY + .5 > 0)//straight right
                    {
                        //ballSpeedX *= -1;
                        ballSpeedY += -2;
                    }
                }
                else if (spin < 0)//CCW
                {
                    if (ballSpeedX > 0 && ballSpeedY < 0) // up and to the right
                    {
                        //ballSpeedX *= -1;
                        ballSpeedY *= 0.5;
                    }
                    else if (ballSpeedX > 0 && ballSpeedY > 0 && ballSpeedY != 0) // down and to the right
                    {
                        //ballSpeedX *= -1;
                        ballSpeedY *= 2;
                    }
                    else if (ballSpeedY - .5 < 0 && ballSpeedY + .5 > 0)//straight right
                    {
                        //ballSpeedX *= -1;
                        ballSpeedY += 2;
                    }
                }
                ballSpeedX *= -1.1;
                spin = rand.Next(-3, 3);
            }
                
            if (ballRecDest.Intersects(paddleBlueRecDest))
            {
                
                //ballSpeedX *= -1;
                if (spin > 0) //CW
                {
                    if (ballSpeedY < 0 && ballSpeedX < 0) // up and to the left
                    {
                        //ballSpeedX *= -1;
                        ballSpeedY *= .5;
                    }
                    else if (ballSpeedY > 0 && ballSpeedX < 0 && ballSpeedY != 0) // down and to the left
                    {
                        //ballSpeedX *= -1;
                        ballSpeedY *= 2;
                    }
                    else if (ballSpeedY - .5 < 0 && ballSpeedY + .5 > 0)//straight right
                    {
                        //ballSpeedX *= -1;
                        ballSpeedY += 2;
                    }
                }
                else if (spin < 0)//CCW
                {
                    if (ballSpeedX < 0 && ballSpeedY<0) // up left
                    {
                        //ballSpeedX *= -1;
                        ballSpeedY *= 2;
                    }
                    else if (ballSpeedX < 0 && ballSpeedY > 0 && ballSpeedY != 0) // down left
                    {
                        //ballSpeedX *= -1;
                        ballSpeedY *= .5;
                    }
                    else if (ballSpeedY - .5 < 0 && ballSpeedY + .5 > 0)//straight left
                    {
                        //ballSpeedX *= -1;
                        ballSpeedY -= 2;
                    }
                }
                ballSpeedX *= -1.1;
                spin = rand.Next(-3, 3);
            }
                

            if((p1Score == 11 || p2Score == 11) && Math.Abs(p1Score-p2Score) >= 2)
            {
                if (p1Score > p2Score)
                    p1GameScore++;
                else
                    p2GameScore++;
                p1Score = 0;
                p2Score = 0;
            }


            ballRotation += (float)spin;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            
            spriteBatch.Draw(all, green1RecDest, green1RecSource, Color.White);
            for (int i = 0; i < 10; i++)
            {
                spriteBatch.Draw(all, new Rectangle(graphics.PreferredBackBufferWidth/2, i * graphics.PreferredBackBufferHeight/10 + 10, 16,16), squareRecSource, Color.White);
            }
            spriteBatch.Draw(all, paddleBlueRecDest, paddleBlueRecSource, Color.Cyan, 0, new Vector2(0, 0), new SpriteEffects(), 0);
            //spriteBatch.Draw(box, paddleBlueRecDest, paddleBlueRecSource, Color.White, 0, new Vector2(paddleBlueRecDest.Width, paddleBlueRecDest.Height / 2), new SpriteEffects(), 0);



            spriteBatch.Draw(all, paddleGrayRecDest, paddleGrayRecSource, Color.WhiteSmoke, 0, new Vector2(0, 0), new SpriteEffects(), 0);
            //spriteBatch.Draw(ballTex, top, Color.White);
            spriteBatch.DrawString(font, "" + p1Score, new Vector2(graphics.PreferredBackBufferWidth / 2 - 50, graphics.PreferredBackBufferHeight - 50), Color.Cyan);
            spriteBatch.DrawString(font, "" + p2Score, new Vector2(graphics.PreferredBackBufferWidth / 2 + 50, graphics.PreferredBackBufferHeight - 50), Color.White);
            spriteBatch.DrawString(font, "" + p1GameScore, new Vector2(graphics.PreferredBackBufferWidth / 2 - 50, 20), Color.Cyan);
            spriteBatch.DrawString(font, "" + p2GameScore, new Vector2(graphics.PreferredBackBufferWidth / 2 + 50, 20), Color.White);

            spriteBatch.Draw(all, ballRecDest, ballRecSource, Color.White, ballRotation, new Vector2(ballRecSource.Width/2,ballRecSource.Height/2), new SpriteEffects(), 0);
            //spriteBatch.Draw()
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
