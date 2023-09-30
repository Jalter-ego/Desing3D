using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;
using System.Collections.Generic;

public class Poligono
{
    private List<Vector3> listaDeVertices;
    private Vector3 centro;
    private Vector3 dimensiones;
    private Color4 color;
    private float desplazamientoX;
    private float desplazamientoY;
    private float desplazamientoZ;

    public Poligono(Vector3 centro, Vector3 dimensiones, Color4 color, float desplazamientoX, 
        float desplazamientoY, float desplazamientoZ)
    {
        this.listaDeVertices = new List<Vector3>();
        this.centro = centro;
        this.dimensiones = dimensiones;
        this.color = color;
        this.desplazamientoX = desplazamientoX;
        this.desplazamientoY = desplazamientoY;
        this.desplazamientoZ = desplazamientoZ;
    }

    public void addVertice(float x, float y, float z)
    {
        this.listaDeVertices.Add(new Vector3(x,y,z));
    }

    public void deleteVertice(float x, float y, float z)
    {
        this.listaDeVertices.Remove(new Vector3(x, y, z));
    }

    public void Dibujar()
    {
        GL.PushMatrix();
        GL.Translate(centro + new Vector3(desplazamientoX, desplazamientoY, desplazamientoZ));

        GL.Begin(PrimitiveType.Polygon);
        GL.Color4(color);

        float halfX = dimensiones.X / 2.0f;
        float halfY = dimensiones.Y / 2.0f;
        float halfZ = dimensiones.Z / 2.0f;

        // Cara frontal

        this.addVertice(halfX, halfY, halfZ);
        this.addVertice(-halfX, halfY, halfZ);
        this.addVertice(-halfX, -halfY, halfZ);
        this.addVertice(halfX, -halfY, halfZ);
        this.DibujarCara(0);

        // Cara trasera

        this.addVertice(halfX, halfY, -halfZ);
        this.addVertice(-halfX, halfY, -halfZ);
        this.addVertice(-halfX, -halfY, -halfZ);
        this.addVertice(halfX, -halfY, -halfZ);
        this.DibujarCara(4);

        // Otras caras (laterales)

        this.addVertice(halfX, halfY, halfZ);
        this.addVertice(halfX, halfY, -halfZ);
        this.addVertice(halfX, -halfY, -halfZ);
        this.addVertice(halfX, -halfY, halfZ);
        this.DibujarCara(8);

        this.addVertice(-halfX, halfY, halfZ);
        this.addVertice(-halfX, halfY, -halfZ);
        this.addVertice(-halfX, -halfY, -halfZ);
        this.addVertice(-halfX, -halfY, halfZ);
        this.DibujarCara(12);


        this.addVertice(halfX, halfY, halfZ);
        this.addVertice(-halfX, halfY, halfZ);
        this.addVertice(-halfX, halfY, -halfZ);
        this.addVertice(halfX, halfY, -halfZ);
        this.DibujarCara(16);


        this.addVertice(halfX, -halfY, halfZ);
        this.addVertice(-halfX, -halfY, halfZ);
        this.addVertice(-halfX, -halfY, -halfZ);
        this.addVertice(halfX, -halfY, -halfZ);
        this.DibujarCara(20);

        GL.End();
        GL.PopMatrix();
    }

    private void DibujarCara(int i)
    {
        GL.Vertex3(this.listaDeVertices[i]);
        GL.Vertex3(this.listaDeVertices[i+1]);
        GL.Vertex3(this.listaDeVertices[i+2]);
        GL.Vertex3(this.listaDeVertices[i+3]);
    }
}

