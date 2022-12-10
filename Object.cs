using Raylib_cs;
using System.Numerics;

class Object {
    public Texture2D Texture;
    public Vector2 Position = new Vector2(0, 0);
    public Vector2 Size = new Vector2(0, 0);
    public Vector2 Velocity = new Vector2(0, 0);
    public Vector2 Acceleration = new Vector2(0, (float)1.25);
    public Vector2 MaxVelocity = new Vector2(0, 0);
    public int Health = 1;
    public string ObjectType = "Object";
    
    public virtual void Move () {
        Velocity += Acceleration;
        Velocity = new Vector2(Math.Clamp(Velocity.X, MaxVelocity.X * -1, MaxVelocity.X), Math.Clamp(Velocity.Y, MaxVelocity.Y * -1, MaxVelocity.Y));
        Position += Velocity;
        if (Position.Y > 1000) {
            Health = 0;
        }
    }
    public void MoveTo (float x, float y) {
        Position = new Vector2(x, y);
        Velocity *= 0;
        Acceleration.X = 0;
    }
    public virtual void Draw () {
        Raylib.DrawRectangleV(Position, Size, Color.BROWN);
    }
    public virtual void CheckCollisionSolid (Object other) {
        // this detects the collision against the y directions
        if (this.Position.X < other.Position.X + other.Size.X && this.Position.X + this.Size.X > other.Position.X) {
            if (this.Position.Y + this.Size.Y > other.Position.Y && this.Position.Y + this.Size.Y < other.Position.Y + this.Velocity.Y - other.Velocity.Y + 1) {
                this.Position.Y = other.Position.Y - this.Size.Y;
                this.Velocity.Y = 0;
                other.Velocity.Y = 0;
            }
            if (this.Position.Y < other.Position.Y + other.Size.Y && this.Position.Y > other.Position.Y + other.Size.Y + this.Velocity.Y - other.Velocity.Y - 1) {
                this.Position.Y = other.Position.Y + other.Size.Y;
                this.Velocity.Y = 0;
                other.Velocity.Y = 0;
            }
        }
        // this detects the collision against the x directions
        if (this.Position.Y < other.Position.Y + other.Size.Y && this.Position.Y + this.Size.Y > other.Position.Y) {
            if (this.Position.X + this.Size.X > other.Position.X && this.Position.X + this.Size.X < other.Position.X + this.Velocity.X - other.Velocity.X + 1) {
                this.Position.X = other.Position.X - this.Size.X;
                this.Velocity.X = 0;
                other.Velocity.X = 0;
            }
            if (this.Position.X < other.Position.X + other.Size.X && this.Position.X > other.Position.X + other.Size.X + this.Velocity.X - other.Velocity.X - 1) {
                this.Position.X = other.Position.X + other.Size.X;
                this.Velocity.X = 0;
                other.Velocity.X = 0;
            }
        }
    }
    public bool CheckCollisionHitbox (Object other) {
        return this.Position.Y < other.Position.Y + other.Size.Y && this.Position.Y + this.Size.Y > other.Position.Y && this.Position.X < other.Position.X + other.Size.X && this.Position.X + this.Size.X > other.Position.X;
    }

}