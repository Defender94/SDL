using SDL2;
using static SDL2.SDL;

namespace Runtime
{
    public class GameRenderEventArgs : EventArgs
    {
        public readonly IntPtr renderer;
        public GameRenderEventArgs(IntPtr renderer)
        {
            this.renderer = renderer;
        }
    }
    public class GameUpdateEventArgs : EventArgs
    {
        public readonly IntPtr renderer;
        public GameUpdateEventArgs(IntPtr renderer)
        {
            this.renderer = renderer;
        }
    }
    public class GameHandleEventArgs : EventArgs
    {
        public readonly IntPtr renderer;
        public readonly Game game;
        public GameHandleEventArgs(IntPtr renderer, Game game)
        {
            this.renderer = renderer;
            this.game = game;
        }
    }
    public class Game : Program
    {
        public override void Start(string title, int xpos, int ypos, int width, int heigth, bool fullscreen)
        {
            //int flags = 0;
            
            if(SDL_Init(SDL_INIT_EVERYTHING) == 0)
            {
                window = SDL_CreateWindow(title, xpos, ypos, width, heigth, 0); //todo implement fullscreen
                renderer = SDL_CreateRenderer(window, -1, 0);
                isAlive = true;
            }
            while(isAlive)
            {
                HandleEvents();
                Update();
                Render();
            }
        }
        public event EventHandler<GameHandleEventArgs> GameHandle = null;
        protected virtual void OnHandle(GameHandleEventArgs e) => GameHandle?.Invoke(this, e);
        public override void HandleEvents()
        {
            OnHandle(new GameHandleEventArgs(renderer, this));
        }
        public event EventHandler<GameUpdateEventArgs> GameUpdate = null;
        protected virtual void OnUpdate(GameUpdateEventArgs e) => GameUpdate?.Invoke(this, e);
        public override void Update()
        {
            OnUpdate(new GameUpdateEventArgs(renderer));
        }
        public event EventHandler<GameRenderEventArgs> GameRender = null;
        protected virtual void OnRender(GameRenderEventArgs e) => GameRender?.Invoke(this, e);
        public override void Render()
        {
            SDL_RenderClear(renderer);
            OnRender(new GameRenderEventArgs(renderer));
            SDL_RenderPresent(renderer);
        }
        public override void Quit()
        {
            isAlive = false;
            SDL_DestroyWindow(window);
            SDL_DestroyRenderer(renderer);
            SDL_Quit();
        }
    }
}