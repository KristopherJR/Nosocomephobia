using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nosocomephobia.Engine_Code.Components;
using Nosocomephobia.Engine_Code.Interfaces;


namespace Nosocomephobia.Game_Code.Screens
{
    public class GameScreen : Screen
    {
        private IEngineManager _engineManager;
        private ISceneManager _sceneManager;
        private Camera _camera;
        public GameScreen(IEngineManager engineManager, ISceneManager sceneManager, Camera camera)
        {
            _engineManager = engineManager;
            _sceneManager = sceneManager;
            _camera = camera;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            // SET the transform of the Penumbra engine to the Cameras Transform:
            Kernel.PENUMBRA.Transform = (_camera as Camera).Transform;
            // BEGIN penumbras drawing cycle:
            Kernel.PENUMBRA.BeginDraw();
            // SET the window to dark gray:
            graphicsDevice.Clear(Color.DarkGray);
            spriteBatch.Begin(samplerState: SamplerState.PointClamp,
                               transformMatrix: (_camera as Camera).Transform);

            _sceneManager.DrawSceneGraphs(spriteBatch);

            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            // UPDATE the EngineManager:
            _engineManager.Update(gameTime);
            // UPDATE the Camera:
            _camera.Update(gameTime);
        }
    }
}
