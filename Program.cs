using Raylib_cs;
using System.Numerics;

class Program {
    static void Main () {
        // Sets up and initializes all variables   v
        Raylib.InitWindow(1600, 800, "Brick Hurler");
        Raylib.SetTargetFPS(60);
        Raylib.DisableCursor();
        List<Object> level = SetupLevel();
        ObjectSpawner objectSpawner = new ObjectSpawner();
        Player player = new Player();
        Input input = new Input();
        bool tutorial = true;
        // Sets up and initializes all variables   ^
        // The game loop!   v
        while (!Raylib.WindowShouldClose()) {
            if (!tutorial) {
                if (objectSpawner.AreEnemiesDead()) {
                    player.Health += 40;
                    player.Health = Math.Clamp(player.Health, 0, 400);
                }
            }
            objectSpawner.RespawnBricks();
            // Gets all the input for the player, moves the player, and grabs bricks   v
            input.Refresh();
            if (input.Restart()) {
                objectSpawner = new ObjectSpawner();
                player = new Player();
            }
            if (player.Health > 0) {
                player.Accelerate(input.Direction());
                player.Jump(input.Jump());
                if (input.Grab()) {
                    if (player.BrickHolding != 2) {
                        objectSpawner.Bricks[player.BrickHolding].ThrowAt(input.MouseX(), input.MouseY());
                        player.BrickHolding = 2;
                    } else {
                        if (player.CheckCollisionHitbox(objectSpawner.Bricks[0])) player.BrickHolding = 0;
                        else if (player.CheckCollisionHitbox(objectSpawner.Bricks[1])) player.BrickHolding = 1;
                        else player.BrickHolding = 2;
                    }
                }
                if (player.BrickHolding != 2) {
                    objectSpawner.Bricks[player.BrickHolding].Position = player.Position + (player.Size - objectSpawner.Bricks[player.BrickHolding].Size) / 2;
                    objectSpawner.Bricks[player.BrickHolding].Velocity *= 0;
                }
            }
            player.Move();
            // Gets all the input for the player, moves the player, and grabs bricks   ^
            // Moves all enemies and detects collision against the player   v
            for (int i = 0; i < objectSpawner.Enemies.Count(); i++) {
                if (objectSpawner.Enemies[i].Health > 0) {
                    objectSpawner.Enemies[i].AccelerateTowards(player.Position + player.Size / 2);
                    objectSpawner.Enemies[i].Move();
                    if (objectSpawner.Enemies[i].CheckCollisionHitbox(player) && player.Hit == 0) {
                        player.Health -= 20;
                        player.Hit = 18;
                        player.Velocity.X = player.MaxVelocity.X / 2 * objectSpawner.Enemies[i].Acceleration.X / Math.Abs(objectSpawner.Enemies[i].Acceleration.X);
                        player.Velocity.Y = -12;
                        objectSpawner.Enemies[i].Velocity *= -1;
                    }
                }
            }
            // Moves all enemies and detects collision against the player   ^
            // Moves both bricks and detects their collision against the enemies   v
            for (int i = 0; i < 2; i++) {
                objectSpawner.Bricks[i].Move();
                if (objectSpawner.Bricks[i].Velocity.X != 0) {
                    for (int j = 0; j < objectSpawner.Enemies.Count(); j++) {
                        if (objectSpawner.Bricks[i].CheckCollisionHitbox(objectSpawner.Enemies[j])) {
                            objectSpawner.Enemies[j].Velocity = objectSpawner.Bricks[i].Velocity;
                            objectSpawner.Enemies[j].Health--;
                            if (objectSpawner.Enemies[j].Health == 0) {
                                objectSpawner.Enemies[j].Position.Y = 1000;
                            }
                            objectSpawner.Bricks[i].Velocity /= -2;
                            objectSpawner.Bricks[i].Position += objectSpawner.Bricks[i].Velocity;
                        }
                    }
                }
            }
            // Moves both bricks and detects their collision against the enemies   ^
            // detects collision of all objects against the platforms in the level   v
            foreach (Object platform in level) {
                player.CheckCollisionSolid(platform);
                for (int i = 0; i < objectSpawner.Enemies.Count(); i++) {
                    objectSpawner.Enemies[i].CheckCollisionSolid(platform);
                }
                objectSpawner.Bricks[0].CheckCollisionSolid(platform);
                objectSpawner.Bricks[1].CheckCollisionSolid(platform);
            }
            // detects collision of all objects against the platforms in the level   ^
            // Draws everything   v
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.WHITE);
            player.Draw();
            foreach (Object platform in level) {
                platform.Draw();
            }
            objectSpawner.Draw();
            objectSpawner.Bricks[0].Draw();
            objectSpawner.Bricks[1].Draw();
            if (tutorial) tutorial = Tutorial();
            input.DrawCursor();
            Raylib.EndDrawing();
            // Draws everything   ^
        }
    }

    static public List<Object> SetupLevel () {
        List<Object> ground = new List<Object>();
        for (int i = 0; i < 2; i++) {
            ground.Add(new Object());
            ground[i].Acceleration.Y = 0;
        }
        ground[0].Position.Y = 700;
        ground[0].Size = new Vector2(725, 500);
        ground[1].Position = new Vector2(875, 650);
        ground[1].Size = new Vector2(725, 500);        
        return ground;
    }

    static public bool Tutorial () {
        Raylib.DrawText("This is Claymore.\n\nClaymore loves spending time with his beloved bricks\nBut these stupid purple monsters keep inturrupting his quality time!\n\nUse either the arrow keys or WASD to move around\nClick anywhere on the screen while standing on a brick to pick it up\nWhile holding a brick, aim and click with the mouse to throw it!\n\nDodge the enemies and throw bricks at them to kill them\n\nPress SPACE to begin", 10, 90, 30, Color.BLACK);
        return !Raylib.IsKeyDown(KeyboardKey.KEY_SPACE);
    }

}