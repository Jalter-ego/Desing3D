using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;
using System.Collections.Generic;

public class Poligono
{
    private List<Vector3> listaDeVertices = new List<Vector3>();
    private Vector3 centro;
    private Vector3 dimensiones;
    private Color4 color;
    private float desplazamientoX;
    private float desplazamientoY;
    private float desplazamientoZ;

    public Poligono(Vector3 centro, Vector3 dimensiones, Color4 color, float desplazamientoX, 
        float desplazamientoY, float desplazamientoZ)
    {
        this.centro = centro;
        this.dimensiones = dimensiones;
        this.color = color;
        this.desplazamientoX = desplazamientoX;
        this.desplazamientoY = desplazamientoY;
        this.desplazamientoZ = desplazamientoZ;
    }

    public void Dibujar()
    {
        GL.PushMatrix();
        GL.Translate(centro + new Vector3(desplazamientoX, desplazamientoY, desplazamientoZ));

        GL.Begin(PrimitiveType.Quads);
        GL.Color4(color);

        float halfX = dimensiones.X / 2.0f;
        float halfY = dimensiones.Y / 2.0f;
        float halfZ = dimensiones.Z / 2.0f;

        // Cara frontal
        DibujarCara(halfX, halfY, halfZ, -halfX, halfY, halfZ, -halfX, -halfY, halfZ, halfX, -halfY, halfZ);
        // Cara trasera
        DibujarCara(halfX, halfY, -halfZ, -halfX, halfY, -halfZ, -halfX, -halfY, -halfZ, halfX, -halfY, -halfZ);
        // Otras caras (laterales)
        DibujarCara(halfX, halfY, halfZ, halfX, halfY, -halfZ, halfX, -halfY, -halfZ, halfX, -halfY, halfZ);
        DibujarCara(-halfX, halfY, halfZ, -halfX, halfY, -halfZ, -halfX, -halfY, -halfZ, -halfX, -halfY, halfZ);
        DibujarCara(halfX, halfY, halfZ, -halfX, halfY, halfZ, -halfX, halfY, -halfZ, halfX, halfY, -halfZ);
        DibujarCara(halfX, -halfY, halfZ, -halfX, -halfY, halfZ, -halfX, -halfY, -halfZ, halfX, -halfY, -halfZ);

        GL.End();
        GL.PopMatrix();
    }

    private void DibujarCara(float x1, float y1, float z1, float x2, float y2, float z2, 
        float x3, float y3, float z3, float x4, float y4, float z4)
    {
        GL.Vertex3(x1, y1, z1);
        GL.Vertex3(x2, y2, z2);
        GL.Vertex3(x3, y3, z3);
        GL.Vertex3(x4, y4, z4);
    }
}

