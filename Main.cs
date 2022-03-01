using SDL2;
using static SDL2.SDL;
namespace Runtime
{
    class Runtime
    {
        public static IntPtr playerTex;
        public static SDL_Rect srcRect = new SDL_Rect(0, 0, 64, 64);
        public static SDL_Rect destRect = new SDL_Rect(0, 0, 64, 64);
        
        static void Main(string[] args)
        {
            var game = new Game();
            game.GameInit += LoadPlayer;
            game.GameRender += Background;
            game.GameRender += RenderPlayer;
            //game.GameRender += RenderFps;
            game.GameHandle += PlayerMovement;
            game.GameHandle += QuitEvent;
            game.Start("Test", SDL_WINDOWPOS_CENTERED, SDL_WINDOWPOS_CENTERED, 600, 400, true);
        }
        static void Background(object? sender, GameEventArgs e)
        {
            SDL_SetRenderDrawColor(e.renderer, 255, 255, 255, 255);
        }
        static void QuitEvent(object? sender, GameEventArgs e)
        {
            SDL_Event ev;
            SDL_PollEvent(out ev);
            switch(ev.type)
            {
                case SDL_EventType.SDL_QUIT:
                    e.game.Quit();
                    break;
                default:
                    break;
            }
        }
        static void LoadPlayer(object? sender, GameEventArgs e)
        {
            var temp = SDL_image.IMG_Load("assets/sprite.png");
            playerTex = SDL_CreateTextureFromSurface(e.renderer, temp);
            SDL_FreeSurface(temp);
        }
        static void RenderPlayer(object? sender, GameEventArgs e)
        { 
            SDL_RenderCopy(e.renderer, playerTex, ref srcRect, ref destRect);
        }
        static void PlayerMovement(object? sender, GameEventArgs e)
        {
            destRect.x++;
        }
        /*static void RenderFps(object? sender, GameEventArgs e)
        {
            SDL_
        }*/
    }
}