using SDL2;
using static SDL2.SDL;

namespace Runtime
{
    public class GameEventArgs : EventArgs
    {
        public readonly IntPtr renderer = null;
        public readonly IntPtr game = null;
        public GameEventArgs(IntPtr renderer)
        {
            this.renderer = renderer;
        }
        public GameEventArgs(IntPtr renderer, IntPtr game)
        {
            this.renderer = renderer;
            this.game = game;
        }
    }
    public class Game : Program
    {
        public event EventHandler<GameEventArgs> GameInit = null;
        public event EventHandler<GameEventArgs> GameHandle = null;
        public event EventHandler<GameEventArgs> GameUpdate = null;
        public event EventHandler<GameEventArgs> GameRender = null;

        protected virtual void OnInit(GameEventArgs e) => GameInit?.Invoke(this, e);
        protected virtual void OnHandle(GameEventArgs e) => GameHandle?.Invoke(this, e);
        protected virtual void OnUpdate(GameEventArgs e) => GameUpdate?.Invoke(this, e);
        protected virtual void OnRender(GameEventArgs e) => GameRender?.Invoke(this, e);

        public override void HandleEvents()
        {
            OnHandle(new GameEventArgs(renderer, this));
        }
        public override void Update()
        {
            OnUpdate(new GameEventArgs(renderer));
        }     
        public override void Render()
        {
            SDL_RenderClear(renderer);
            OnRender(new GameEventArgs(renderer));
            SDL_RenderPresent(renderer);
        }
        public override void Quit()
        {
            isAlive = false;
            SDL_DestroyWindow(window);
            SDL_DestroyRenderer(renderer);
            SDL_Quit();
        }
        public override void Start(string title, int xpos, int ypos, int width, int heigth, bool fullscreen)
        {
            //int flags = 0;
            
            if(SDL_Init(SDL_INIT_EVERYTHING) == 0)
            {
                window = SDL_CreateWindow(title, xpos, ypos, width, heigth, 0); //todo implement fullscreen
                renderer = SDL_CreateRenderer(window, -1, 0);
                isAlive = true;
            }
            OnInit(new GameEventArgs(renderer));
            while(isAlive)
            {
                HandleEvents();
                Update();
                Render();
            }
        }
    }
}