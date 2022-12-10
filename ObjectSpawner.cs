using Raylib_cs;
using System.Numerics;

class ObjectSpawner {
    public int Level = 1;
    public List<Enemy> Enemies = new List<Enemy>();
    public List<Brick> Bricks = new List<Brick>();
    
    public ObjectSpawner () {
        Bricks.Add(new Brick());
        Bricks.Add(new Brick());
        Bricks[0].Position = new Vector2(400, 680); 
        Bricks[1].Position = new Vector2(1200, 630);
    }
    public bool AreEnemiesDead () {
        foreach (Enemy enemy in Enemies) {
            if (enemy.Health > 0) {
                return false;
            }
        }
        Level++;
        LoadEnemies();
        return true;
    }
    public void RespawnBricks () {
        Random random = new Random();
        foreach (Brick brick in Bricks) {
            if (brick.Position.Y > 1500 * Level) {
                brick.Position = new Vector2(random.Next(100, 600) + 900 * random.Next(0, 2), 200);
                brick.Velocity *= 0;
            }
        }
    }
    void LoadEnemies () {
        Random random = new Random();
        Enemies = new List<Enemy>();
        Texture2D texture = Raylib.LoadTextureFromImage(Raylib.LoadImage("monster ball.png"));
        for (int i = 0; i < (int)Math.Ceiling((double)Level / 2); i++) {
            Enemies.Add(new Enemy(texture));
            Enemies[i].Position = new Vector2(800 + (1600 + 100 * i * Level) * (1 - random.Next(0, 2) * 2), 600);
            Enemies[i].MaxVelocity += new Vector2(Math.Clamp(4 + i + Level / 2, 0, 36), Math.Clamp(4 + i + Level / 2, 0, 36)) / 2;
            Enemies[i].Health = Level / 2;
        }
    }
    public void Draw () {
        if (Level != 1) Raylib.DrawText("Wave: " + (Level - 1).ToString(), 5, 0, 50, Color.BLACK);
        foreach (Enemy enemy in Enemies) {
            if (enemy.Health > 0) enemy.Draw();
            Raylib.DrawRectangle((int)enemy.Position.X, (int)(enemy.Position.Y - 4), 40 * enemy.Health / (Level / 2), 3, Color.GREEN);
        }
    }
}