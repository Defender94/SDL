using SDL2;
using System;
using static SDL2.SDL;

namespace Runtime
{
    public class GameEventArgs : EventArgs
    {
        public readonly Game game = null;
        public GameEventArgs(Game game)
        {
            this.game = game;
        }
    }
    public class Game : Program
    {
        public SDL_Event ev;
        public List<GameObject> objectsToRender = new List<GameObject>();
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
            SDL_PollEvent(out ev);
            switch(ev.type)
            {
                case SDL_EventType.SDL_QUIT:
                    Quit();
                    break;
                default:
                    OnHandle(new GameEventArgs(this));
                    break;
            }
        }
        public override void Update()
        {
            OnUpdate(new GameEventArgs(this));

            foreach(GameObject o in objectsToRender)
            {
                o.destRect.x += (int)Math.Round(o.velocity*1f);
                if(o.velocity > 0.0f)
                    o.velocity -= 0.1f;
                else
                    o.velocity += 0.1f;
            }
            
        }     
        public override void Render()
        {
            SDL_RenderClear(renderer);
            SDL_SetRenderDrawColor(renderer, 255, 255, 255, 255);
            foreach(GameObject o in objectsToRender)
            {
                SDL_RenderCopy(renderer, o.texture, ref o.srcRect, ref o.destRect);
            }
            //OnRender(new GameEventArgs(renderer));
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
                renderer = SDL_CreateRenderer(window, -1, SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);
                isAlive = true;
            }
            
            OnInit(new GameEventArgs(this));
            while(isAlive)
            {
                UInt64 start = SDL_GetPerformanceCounter();

                HandleEvents();
                Update();
                Render();
                SDL_Delay(3000);
                UInt64 end = SDL_GetPerformanceCounter();
	            float elapsed = (end - start) / (float)SDL_GetPerformanceFrequency();
	            //Console.WriteLine("Current FPS: {0}", (1.0f / elapsed));

            }
        }
    }
}