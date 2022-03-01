using SDL2;
using static SDL2.SDL;
namespace Runtime
{
    class Runtime
    {
        public static IntPtr? playertex = null;
        static void Main(string[] args)
        {
            var game = new Game();
            game.GameInit += LoadPlayer;
            game.GameRender += Background;
            game.GameRender += RenderPlayer;
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
            playertex = SDL_CreateTextureFromSurface(e.renderer, temp);
            SDL_FreeSurface(temp);
        }
        static void RenderPlayer(object? sender, GameEventArgs e)
        {
            SDL_RenderCopy(e.renderer, playertex, SDL_Rect, null);
        }
    }
}