using System;
using SDL2;
//using static SDL2.SDL;

namespace Runtime
{
    public abstract class Program
    {
        public abstract void Start(string title, int xpos, int ypos, int width, int heigth, bool fullscreen);
        public abstract void Update();
        public abstract void Render();
        public abstract void Quit();
        public abstract void HandleEvents();
        public virtual bool Alive() => isAlive;
        protected bool isAlive = false;
        protected IntPtr window;
        public IntPtr renderer;
    }
}