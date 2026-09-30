using Raylib_cs;
namespace ColorLab;

internal static class Programa
{
    public static void Main()
    {
        const int anchoVentana = 800;
        const int altoVentana = 600;

        Raylib.InitWindow(anchoVentana, altoVentana, "Pixel Lab 16x16");
        Raylib.SetTargetFPS(60);

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);
            //aqui se dibuja equisde
            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}