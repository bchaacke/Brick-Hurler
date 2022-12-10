using Raylib_cs;
using System.Numerics;

class Enemy: Object {
    public Enemy (Texture2D texture) {
        this.Texture = texture;
        ObjectType = "Enemy";
        Acceleration.Y = 0;
        Size = new Vector2(40, 40);
    }
    public void AccelerateTowards (Vector2 position) {
        double angle = Math.Abs(Math.Atan((position.Y - this.Position.Y - this.Size.Y / 2)/(position.X - this.Position.X - this.Size.X / 2)));
        int xDirection = 1;
        int yDirection = 1;
        if (position.X - this.Position.X < 0) xDirection = -1;
        if (position.Y - this.Position.Y < 0) yDirection = -1;
        Acceleration = new Vector2((float)Math.Cos(angle) * xDirection, (float)Math.Sin(angle) * yDirection) / 5;
        if (Math.Abs(Position.X - 800) < 1200 && Math.Abs(Position.X - 800) > 1000) Position.Y = -400;
    }
    public override void Draw () {
        Raylib.DrawTextureV(Texture, Position, Color.WHITE);
    }
}