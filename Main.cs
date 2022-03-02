using SDL2;
using static SDL2.SDL;
namespace Runtime
{
    class Runtime
    {
        
        static Game game = new Game();
        static void Main(string[] args)
        {
            game.GameInit += LoadPlayer; 
            game.Start("Test", SDL_WINDOWPOS_CENTERED, SDL_WINDOWPOS_CENTERED, 600, 400, true);
        }
        static void LoadPlayer(object? sender, GameEventArgs e)
        {       
            var temp = SDL_image.IMG_Load("assets/sprite.png");
            GameObject player = new GameObject(SDL_CreateTextureFromSurface(e.game.renderer, temp), game);
            SDL_FreeSurface(temp);

            e.game.objectsToRender.Add(player);
        }
    }
}