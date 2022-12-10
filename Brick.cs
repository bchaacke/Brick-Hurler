using Raylib_cs;
using System.Numerics;

class Brick: Object {
    public Brick () {
        Size = new Vector2(20, 20);
        ObjectType = "Brick";
        MaxVelocity = new Vector2(60, 60);
        var image = Raylib.LoadImage("brick.png");
        Texture = Raylib.LoadTextureFromImage(image);
        Raylib.UnloadImage(image);
    }
    public void ThrowAt (int x, int y) {
        double angle = Math.Abs(Math.Atan((y - Position.Y) / (x - Position.X)));
        int xDirection = 1;
        int yDirection = 1;
        if (y - Position.Y < 0) yDirection = -1;
        if (x - Position.X < 0) xDirection = -1;
        Velocity = new Vector2((float)Math.Cos(angle) * xDirection, (float)Math.Sin(angle) * yDirection - (float)(Math.Sqrt((y - Position.Y) * (y - Position.Y) + (x - Position.X) * (x - Position.X)) / 3200)) * 40;
    }
    public override void Move () {
        if (Velocity.Y == 0) Acceleration.X = -Velocity.X / 2;
        else Acceleration.X = 0;
        base.Move();
        if (Position.X + Size.X > 1600) {
            Velocity.X /= -3;
            Position.X = 1600 - Size.X;
        }
        if (Position.X < 0) {
            Velocity.X /= -3;
            Position.X = 0;
        }
        if (Math.Abs(Velocity.X) < 0.25) Velocity.X = 0;
    }
    public override void Draw () {
        Raylib.DrawTextureV(Texture, Position, Color.WHITE);
    }
}