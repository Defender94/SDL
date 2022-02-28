using SDL2;
using static SDL2.SDL;
namespace Runtime
{
    class Runtime
    {
        static void Main(string[] args)
        {
            var game = new Game();
            game.GameRender += Background;
            game.GameHandle += QuitEvent;
            game.Start("Test", SDL_WINDOWPOS_CENTERED, SDL_WINDOWPOS_CENTERED, 600, 400, true);
        }
        static void Background(object? sender, GameRenderEventArgs e)
        {
            SDL_SetRenderDrawColor(e.renderer, 255, 255, 255, 255);
        }
        static void QuitEvent(object? sender, GameHandleEventArgs e)
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
    }
}