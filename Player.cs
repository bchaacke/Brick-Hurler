using Raylib_cs;
using System.Numerics;

class Player: Object {
    public int OnGround = 0;
    public int Hit = 0;
    public int BrickHolding = 2;

    public Player () {
        this.Texture = Raylib.LoadTextureFromImage(Raylib.LoadImage("buff dude.png"));
        Health = 400;
        ObjectType = "Player";
        Size = new Vector2(50, 80);
        MaxVelocity = new Vector2(10, 40);
        Position = new Vector2 (200, 620);
    }
    public void Jump (bool jump) {
        if (jump) {
            if (OnGround > 0) Velocity.Y = -21; 
        } else if (Velocity.Y < 0) {
            Velocity.Y /= 2;
        }
    }
    public void Accelerate (int direction) {
        Acceleration.X = 2 * direction;
    }
    public override void Move () {
        Acceleration.X -= Velocity.X / 5;
        base.Move();
        if (Hit > 0) {
            Velocity.X -= Acceleration.X;
            Hit--;
        }
        if (Math.Abs(Velocity.X) < 0.25) Velocity.X = 0;
        if (OnGround > 0) OnGround--;
        if (Position.X < 0) Position.X = 0;
        if (Position.X + Size.X > 1600) Position.X = 1600 - Size.X;
    }
    public override void CheckCollisionSolid (Object other) {
        bool groundCheck = Velocity.Y > 0;
        base.CheckCollisionSolid(other);
        if (Velocity.Y == 0 && groundCheck) {
            OnGround = 8;
        }
    }
    public override void Draw () {
        if (Health > 0) Raylib.DrawTextureV(Texture, Position, Color.WHITE);
        else {
            string text = "You died :(. Press 'R' to restart!";
            Raylib.DrawText(text, 800 - Raylib.MeasureText(text, 50) / 2, 300, 50, Color.BLACK);
            Position = new Vector2(0, -400);
        }
        Raylib.DrawText("HEALTH", 694, 1, 50, Color.BLACK);
        Raylib.DrawRectangleLines(398, 48, 804, 24, Color.BLACK);
        Raylib.DrawRectangle(800 - Health, 50, Health * 2, 20, Color.GREEN);
    }
}