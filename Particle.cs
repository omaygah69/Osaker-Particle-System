namespace ParticleSystem;
using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;

class Particle
{
    Vector2 Position;
    Vector2 Velocity;
    Color color;
    public static Random random = new();

    public Particle(int screenWidth, int screenHeight){
        Position.X = GetRandomValue(0, screenWidth - 1);
        Position.Y = GetRandomValue(0, screenHeight - 1);
        Velocity.X = GetRandomFloat(-1.0f, 1.0f);
        Velocity.Y = GetRandomFloat(-1.0f, 1.0f);
        // color = new Color(0, 0, 0, 100);
        color = GetRandomColor();
    }
    
    public Particle(Vector2 pos, Vector2 vel, Color _color)
    {
        Position = pos;
        Velocity = vel;
        color = _color;
    }

    float GetDistance(Vector2 OtherPos)
    {
        float dx = Position.X - OtherPos.X;
        float dy = Position.Y - OtherPos.Y;
        return MathF.Sqrt((dx * dx) + (dy * dy));
    }

    Vector2 GetNormal(Vector2 OtherPos){
        float distance = GetDistance(OtherPos);
        if(distance == 0.0f)
            distance = 1;
        float dx = Position.X - OtherPos.X;
        float dy = Position.Y - OtherPos.Y;
        Vector2 Normal = new Vector2(dx * (1/distance), dy * (1/distance)); // Sus
        return Normal; // Sus
    }

    public void Attract(Vector2 PosToAttract){
        float distance = MathF.Max(GetDistance(PosToAttract), 0.5f);
        Vector2 Normal = GetNormal(PosToAttract);
        Velocity.X -= Normal.X / distance;
        Velocity.Y -= Normal.Y / distance;
    }

    public void ApplyFriction(float amount){
        Velocity.X *= amount;
        Velocity.Y *= amount;
    }

    public void Move(int ScreenWidth, int ScreenHeight){
        Position.X += Velocity.X;
        Position.Y += Velocity.Y;
        if(Position.X < 0)  Position.X += ScreenWidth;
        if(Position.X >= ScreenWidth) Position.X -= ScreenWidth;
        if(Position.Y < 0) Position.Y += ScreenHeight;
        if(Position.Y >= ScreenHeight) Position.Y -= ScreenHeight;
    }

    public void DrawPixel(){
        DrawPixelV(Position, color);
    }

    private static float GetRandomFloat(float min, float max){
        return (float)(random.NextDouble() * (max - min) + min);
    }

    private static int GetRandomValue(int min, int max){
        return random.Next(min, max + 1);
    }

    public static Color GetRandomColor(){
        byte r = (byte)random.Next(256);
        byte g = (byte)random.Next(256);
        byte b = (byte)random.Next(256);
        byte a = 255; // Fully opaque color
        return new Color(r, g, b, a);
    }
}
