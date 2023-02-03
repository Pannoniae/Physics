using SDL2.Bindings;

namespace Physics;

public class Graphics {

    public static IntPtr window;
    public static IntPtr renderer;
    public static int windowWidth = 800;
    public static int windowHeight = 600;

    public static int offset = 20;

    public static int Width() {
        return windowWidth;
    }

    public static int Height() {
        return windowHeight;
    }

    public static bool OpenWindow() {
        if (SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING) != 0) {
            Console.Out.WriteLine("Error initializing SDL");
            return false;
        }

        //SDL.SDL_GetCurrentDisplayMode(0, out var display_mode);
        //windowWidth = display_mode.w;
        //windowHeight = display_mode.h;
        window = SDL.SDL_CreateWindow("My window!", 0 + offset, 0 + offset, windowWidth, windowHeight, SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE | SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);
        if (window == 0) {
            Console.Out.WriteLine("Error creating SDL window");
            return false;
        }

        renderer = SDL.SDL_CreateRenderer(window, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED | SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);
        if (renderer == 0) {
            Console.Out.WriteLine("Error creating SDL renderer");
            return false;
        }

        return true;
    }

    public static void ClearScreen(uint color) {
        SDL.SDL_SetRenderDrawColor(renderer, (byte)(color >> 16), (byte)(color >> 8), (byte)color, 255);
        SDL.SDL_RenderClear(renderer);
    }

    public static void RenderFrame() {
        SDL.SDL_RenderPresent(renderer);
    }

    public static void DrawLine(short x0, short y0, short x1, short y1, uint color) {
        SDL_gfx.lineColor(renderer, x0, y0, x1, y1, color);
    }

    public static void DrawCircle(short x, short y, short radius, double angle, uint color) {
        SDL_gfx.circleColor(renderer, x, y, radius, color);
        SDL_gfx.lineColor(renderer, x, y, (short)(x + Math.Cos(angle) * radius), (short)(y + Math.Sin(angle) * radius), color);
    }

    public static void DrawFillCircle(short x, short y, short radius, uint color) {
        SDL_gfx.filledCircleColor(renderer, x, y, radius, color);
    }

    public static void DrawRect(short x, short y, short width, short height, uint color) {
        SDL_gfx.lineColor(renderer, (short)(x - width / 2.0), (short)(y - height / 2.0), (short)(x + width / 2.0), (short)(y - height / 2.0), color);
        SDL_gfx.lineColor(renderer, (short)(x + width / 2.0), (short)(y - height / 2.0), (short)(x + width / 2.0), (short)(y + height / 2.0), color);
        SDL_gfx.lineColor(renderer, (short)(x + width / 2.0), (short)(y + height / 2.0), (short)(x - width / 2.0), (short)(y + height / 2.0), color);
        SDL_gfx.lineColor(renderer, (short)(x - width / 2.0), (short)(y + height / 2.0), (short)(x - width / 2.0), (short)(y - height / 2.0), color);
    }

    public static void DrawFillRect(short x, short y, short width, short height, uint color) {
        SDL_gfx.boxColor(renderer, (short)(x - width / 2.0), (short)(y - height / 2.0), (short)(x + width / 2.0), (short)(y + height / 2.0), color);
    }

    public static void DrawPolygon(short x, short y,  List<Vec2> vertices, uint color) {
        for (int i = 0; i < vertices.Count; i++) {
            int currIndex = i;
            int nextIndex = (i + 1) % vertices.Count;
            SDL_gfx.lineColor(renderer, (short)vertices[currIndex].x, (short)vertices[currIndex].y, (short)vertices[nextIndex].x,
                (short)vertices[nextIndex].y, color);
        }

        SDL_gfx.filledCircleColor(renderer, x, y, 1, color);
    }

    public static void DrawFillPolygon(short x, short y,  List<Vec2> vertices, uint color) {
        List<short> vx = new List<short>();
        List<short> vy = new List<short>();
        for (int i = 0; i < vertices.Count; i++) {
            vx.Add((short)vertices[i].x);
        }

        for (int i = 0; i < vertices.Count; i++) {
            vy.Add((short)vertices[i].y);
        }

        SDL_gfx.filledPolygonColor(renderer, vx.ToArray(), vy.ToArray(), vertices.Count, color);
        SDL_gfx.filledCircleColor(renderer, x, y, 1, 0xFF000000);
    }

    public static unsafe void DrawTexture(int x, int y, int width, int height, float rotation, IntPtr texture) {
        var dstRect = new SDL.SDL_Rect {
            x = x - width / 2,
            y = y - height / 2,
            w = width,
            h = height
        };
        var rotationDeg = rotation * 57.2958f;
        // do something with ref struct here
            SDL.SDL_RenderCopyEx(renderer, texture, nint.Zero, ref dstRect, rotationDeg, nint.Zero, SDL.SDL_RendererFlip.SDL_FLIP_NONE);

    }

    public static void CloseWindow() {
        SDL.SDL_DestroyRenderer(renderer);
        SDL.SDL_DestroyWindow(window);
        SDL.SDL_Quit();
    }
}