using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinearMath;
using BmpEditor;

namespace MyGl
{
    public class ViewPoint
    {
        public float zBufferDepth = 255;
        public bool isLightPoint = false;
        public CoordSystem coord;
        public readonly int width;
        public readonly int height;
        public Img image;
        public Buffer<float> zBuffer;
        public Buffer<float> intensityMap;

        public Matrix projection;
        public Matrix View;
        public Matrix ViewPort;

        public ViewPoint(int wdth, int hght, CoordSystem sys)
        {
            this.width = wdth;
            this.height = hght;
            this.coord = new CoordSystem(sys.Centre, sys.basis);
            Refresh();
        }

        public void Refresh()
        {
            image = new Img(this.width, this.height);
            zBuffer = new Buffer<float>(this.width, this.height);
            intensityMap = new Buffer<float>(this.width, this.height);
        }

        public void ProjectionMatrix(float koef)
        {
            this.projection = GL.projection(koef);
        }

        public void ViewMatrix()
        {
            this.View = this.coord.World_Sys_Trans();
        }

        public void ViewPortMatrix()
        {
            if (isLightPoint)
                this.ViewPort = GL.ViewPort(this.width / 4, this.height / 4, this.width / 2, this.height / 2, zBufferDepth);
            else
                this.ViewPort = GL.ViewPort(0, 0, this.width, this.height, zBufferDepth);
        }

        public void DrawModel(objModel model, Vector lightDir, ViewPoint light = null, bool zBufferOnly = false)
        {
            lightDir.Normalize();
            Matrix transitionMatrix = new Matrix();
            if (!zBufferOnly && light != null)
                transitionMatrix = light.ViewPort * light.projection * light.View *
                    this.View.Inversed() * this.projection.Inversed() * this.ViewPort.Inversed();
            Shader shader = new Shader(this, model);
            for (int i = 0; i < model.triangles.Count; i++)
            {
                Vector[] coords = new Vector[3];
                Vector[] textures = new Vector[3];
                Vector[] normals = new Vector[3];
                for (int j = 0; j < 3; j++)
                {
                    coords[j] = model.positions[(int)model.triangles[i][0][j]].AddDimension();
                    textures[j] = model.textureCoords[(int)model.triangles[i][1][j]];
                    normals[j] = model.normals[(int)model.triangles[i][2][j]];
                }
                coords = shader.TopsShader(coords, textures, normals);
                DrawTriangle(shader, coords, lightDir, transitionMatrix, light, zBufferOnly);
            }
        }
        public void DrawTriangle(Shader shad, Vector[] coords, Vector lightDir, Matrix transitionMatrix, ViewPoint light = null, bool zBufferOnly = false)
        {
            Vector imgMin = new Vector(new float[] { this.image.width - 1, this.image.height - 1 });
            Vector imgMax = new Vector(new float[] { 0, 0 });
            Vector clamp = new Vector(new float[] { this.image.width - 1, this.image.height - 1 });
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 2; j++)
                {
                    imgMin[j] = Math.Max(0, Math.Min(imgMin[j], coords[i][j]));
                    imgMax[j] = Math.Min(clamp[j], Math.Max(imgMax[j], coords[i][j]));
                }
            Vector P = new Vector(imgMin.GetContainer());
            for (P[0] = (float)Math.Ceiling(imgMin[0]); P[0] <= imgMax[0]; P[0]++)
                for (P[1] = (float)Math.Ceiling(imgMin[1]); P[1] <= imgMax[1]; P[1]++)
                {
                    shad.Fragment(P, lightDir, transitionMatrix, light, zBufferOnly);
                }
        }
    }
}
