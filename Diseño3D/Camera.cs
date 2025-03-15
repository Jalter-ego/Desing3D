using OpenTK;
using OpenTK.Graphics.OpenGL;

public class Camera
{
    public static void LookAt(Vector3 eye, Vector3 target, Vector3 up)
    {
        Matrix4 view = Matrix4.LookAt(eye, target, up);
        GL.LoadMatrix(ref view);  // Aplica la matriz de vista a la pila de matrices de OpenGL
    }
}
