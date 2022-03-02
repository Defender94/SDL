using SDL2;
using static SDL2.SDL;
namespace Runtime
{
    public class GameObject
    {
        public float velocity;
        public IntPtr texture;
        public SDL_Rect srcRect = new SDL_Rect(0, 0, 64, 64);
        public SDL_Rect destRect = new SDL_Rect(0, 0, 64, 64);
        public GameObject(IntPtr texture, SDL_Rect srcRect, SDL_Rect destRect)
        {
            this.texture = texture;
            this.srcRect = srcRect;
            this.destRect = destRect;
        }
    }
}