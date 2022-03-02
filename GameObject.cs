using SDL2;
using System;
using System.Runtime.InteropServices;
using static SDL2.SDL;
namespace Runtime
{
    public class GameObject
    {
        public float velocity = 0.0f;
        public float acceleration;
        public IntPtr texture;
        public SDL_Rect srcRect = new SDL_Rect(0, 0, 64, 64);
        public SDL_Rect destRect = new SDL_Rect(0, 0, 64, 64);
        public Game game;
        public GameObject(IntPtr texture, Game game)//, SDL_Rect srcRect, SDL_Rect destRect)
        {
            this.texture = texture;
            this.game = game;
            game.GameHandle += HandleMovement;
            //this.srcRect = srcRect;
            //this.destRect = destRect;
        }
        public unsafe void HandleMovement(object? sender, GameEventArgs e)
        {
            int num = 0;
            var keys = SDL_GetKeyboardState(out num);
            int[] keystate = new int[num];
            Marshal.Copy(keys, keystate, 0, num);
            for(int i = 0; i < num; i++)
                Console.WriteLine(keystate[i]);
            if(keystate[26] != 0)
            {
                this.velocity = 1.0f;
            }
            if(keystate[(int)SDL_Scancode.SDL_SCANCODE_S] == 1)
            {
                this.velocity = -1.0f;
            }
            /*
            switch(e.game.ev.key.
            {
                case 
                    switch(e.game.ev.key.keysym.sym)
                    {
                        case SDL_Keycode.SDLK_w:
                            this.velocity = 10.0f;
                            break;
                        case SDL_Keycode.SDLK_s:
                            this.velocity = -10.0f;
                            break;
                    }
                    break;
                default:
                    //this.velocity = 0.0f;
                    break;
            }
            if(e.game.ev.key.keysym.sym == SDL_Keycode.SDLK_w)
                this.velocity = 1.0f;
            if(e.game.ev.key.keysym.sym == SDL_Keycode.SDLK_s)
                this.velocity = -1.0f;
                */
        }
    }
}