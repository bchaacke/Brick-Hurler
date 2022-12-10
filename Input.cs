using Raylib_cs;
using System.Numerics;

class Input {
    int mouseX = 0;
    int mouseY = 0;
    int direction = 0;
    bool jump = false;
    bool grab = false;
    bool restart = false;

    public void Refresh () {
        jump = Raylib.IsKeyDown(KeyboardKey.KEY_W) || Raylib.IsKeyDown(KeyboardKey.KEY_SPACE) || Raylib.IsKeyDown(KeyboardKey.KEY_UP);
        grab = Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON);
        restart = Raylib.IsKeyPressed(KeyboardKey.KEY_R);
        if (Raylib.IsKeyDown(KeyboardKey.KEY_D) || Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT)) {
            direction = 1;
        } else if (Raylib.IsKeyDown(KeyboardKey.KEY_A) || Raylib.IsKeyDown(KeyboardKey.KEY_LEFT)) {
            direction = -1;
        } else {
            direction = 0;
        }
        mouseX = Raylib.GetMouseX();
        mouseY = Raylib.GetMouseY();
    }
    public int Direction () {
        return direction;
    }
    public bool Jump () {
        return jump;
    }
    public bool Grab () {
        return grab;
    }
    public int MouseX () {
        return mouseX;
    }
    public int MouseY () {
        return mouseY;
    }
    public bool Restart () {
        return restart;
    }
    public void DrawCursor () {
        Raylib.SetMousePosition(Math.Clamp(mouseX, 0, 1600), Math.Clamp(mouseY, 0, 800));
        Raylib.DrawCircleLines(mouseX, mouseY, 10, Color.RED);
        Raylib.DrawCircleLines(mouseX, mouseY, 11, Color.RED);
        Raylib.DrawLine(mouseX, mouseY - 16, mouseX, mouseY + 16, Color.RED);
        Raylib.DrawLine(mouseX - 16, mouseY, mouseX + 16, mouseY, Color.RED);
    }
}