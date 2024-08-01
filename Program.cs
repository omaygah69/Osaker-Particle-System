namespace ParticleSystem;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System.Numerics;

public class Program
{

    static void Main()
    {
        const int ScreenWidth = 900;
        const int ScreenHeight = 600;
        const int ParticleCount = 80000;
        Particle[] Particles = new Particle[ParticleCount];

        for(int i = 0; i < ParticleCount; i++){
            Particles[i] = new Particle(ScreenWidth, ScreenHeight);
        }

        InitWindow(ScreenWidth, ScreenHeight, "Bruh");
        SetTargetFPS(60);

        while(!WindowShouldClose()){

            Vector2 MousePosition = new(GetMouseX(), GetMouseY());
            for (int i = 0; i < ParticleCount; i++)
            {
                Particles[i].Attract(MousePosition);
                Particles[i].ApplyFriction(0.99f);
                Particles[i].Move(ScreenWidth, ScreenHeight);
            }
            // Draw Something
            BeginDrawing();
            ClearBackground(RayWhite);

            for(int i = 0; i < ParticleCount; i++){
                Particles[i].DrawPixel();
            }
            DrawFPS(10, 10);
            
            EndDrawing();

        }

        CloseWindow();
    }
}
