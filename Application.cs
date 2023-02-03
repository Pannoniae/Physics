// See https://aka.ms/new-console-template for more information

using Physics;
using SDL2;
using SDL2.Bindings;


public class Application {
    public static int Main(string[] args) {
        Console.WriteLine("Hello, World!");
        Application app = new Application();

        app.Setup();

        while (app.IsRunning()) {
            app.Input();
            app.Update();
            app.Render();
        }

        app.Destroy();

        return 0;
    }

    private bool running = false;

    bool IsRunning() {
        return running;
    }

///////////////////////////////////////////////////////////////////////////////
// Setup function (executed once in the beginning of the simulation)
///////////////////////////////////////////////////////////////////////////////
    void Setup() {
        running = Graphics.OpenWindow();

        // TODO: setup objects in the scene
    }

///////////////////////////////////////////////////////////////////////////////
// Input processing
///////////////////////////////////////////////////////////////////////////////
    void Input() {
        SDL.SDL_Event e;
        while (SDL.SDL_PollEvent(out e) != 0) {
            switch (e.type) {
                case SDL.SDL_EventType.SDL_QUIT:
                    running = false;
                    break;
                case SDL.SDL_EventType.SDL_KEYDOWN:
                    if (e.key.keysym.sym == SDL.SDL_Keycode.SDLK_ESCAPE)
                        running = false;
                    break;
            }
        }
    }

///////////////////////////////////////////////////////////////////////////////
// Update function (called several times per second to update objects)
///////////////////////////////////////////////////////////////////////////////
    void Update() {
        // TODO: update all objects in the scene
    }

///////////////////////////////////////////////////////////////////////////////
// Render function (called several times per second to draw objects)
///////////////////////////////////////////////////////////////////////////////
    void Render() {
        Graphics.ClearScreen(0xFF056263);
        Graphics.DrawFillCircle(200, 200, 40, 0xFFFFFFFF);
        Graphics.RenderFrame();
    }

///////////////////////////////////////////////////////////////////////////////
// Destroy function to delete objects and close the window
///////////////////////////////////////////////////////////////////////////////
    void Destroy() {
        // TODO: destroy all objects in the scene

        Graphics.CloseWindow();
    }
}