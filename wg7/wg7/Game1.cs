using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;

namespace wg7
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        Texture2D   circle;
        Vector2     step    = new Vector2(1, 1);
        Vector2     pos     = new Vector2(0, 0);
        Vector2 tp = new Vector2();

        int         frame, fps;
        float       te, tpf;

       bool        touching = false;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Frame rate is 30 fps by default for Windows Phone.
            TargetElapsedTime = TimeSpan.FromTicks(333333);
            graphics.PreferredBackBufferWidth   = 480;
            graphics.PreferredBackBufferHeight  = 728;
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
            circle = this.Content.Load<Texture2D>("circle");
            //circle  = this.Content.Load<Texture2D>("sprite");
            fps     = 4;
            tpf     = 1/(float) fps;
            frame   = 0;
            te      = 0;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        void UpdateFrame(float tElapse)
        {
            te += tElapse;
            if (te > tpf)
            {
                frame = (frame + 1) % 4;
                te -= tpf;
            }
        }

        private void HandleTouches()
        {
            TouchCollection touches = TouchPanel.GetState();

            if (!touching && touches.Count > 0)
            {
                touching = true;
                foreach (TouchLocation t in touches)
                {
                    if ((t.State == TouchLocationState.Pressed) ||
                        (t.State == TouchLocationState.Moved))
                    {
                        tp = t.Position;
                        
                        
                    }
                }
            }
            else if (touches.Count == 0)
                touching = false;
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            
            // TODO: Add your update logic here
            HandleTouches();
            go();
            UpdateFrame((float)gameTime.ElapsedGameTime.TotalSeconds);

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
            spriteBatch.Draw(circle, pos, Color.White);
            //spriteBatch.Draw(circle, pos, new Rectangle(frame * 50, 0, 50, 50), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
        protected void go()
        {
            if (tp.X > pos.X+30 )pos.X += 1;
            if (tp.Y > pos.Y+30) pos.Y += 1;
            if (tp.X < pos.X+30) pos.X -= 1;
            if (tp.Y > pos.Y+30) pos.Y += 1;
            if (tp.X > pos.X+30) pos.X += 1;
            if (tp.Y < pos.Y+30) pos.Y -= 1;
            if (tp.X < pos.X+30) pos.X -= 1;
            if (tp.Y < pos.Y+30) pos.Y -= 1;
            
        }
    }
}
