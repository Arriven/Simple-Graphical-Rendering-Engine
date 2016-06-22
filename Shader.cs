using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinearMath;
using System.Drawing;
using System.Diagnostics;

namespace MyGl
{
    public class Shader
    {
        Vector[] clip_positions;

        Vector[] varying_Positions;
        Vector[] varying_TextureCoords;
        Vector[] varying_Normals;

        objModel model;
        ViewPoint viewP;

        public Matrix projection;
        public Matrix View;
        public Matrix ViewPort;
        public Matrix modelPort;

        public Shader(ViewPoint cam, objModel mod)
        {
            clip_positions = new Vector[3];
            varying_Positions = new Vector[3];
            varying_TextureCoords = new Vector[3];
            varying_Normals = new Vector[3];
            this.viewP = cam;
            this.model = mod;
            this.projection = cam.projection;
            this.View = cam.View;
            this.ViewPort = cam.ViewPort;
            this.modelPort = mod.coords.Sys_World_Trans();
        }

        public Vector[] TopsShader(Vector[] positions, Vector[] textures, Vector[] normals)
        {
            for (int i = 0; i < positions.Length; i++)
            {
                clip_positions[i] = ViewPort * projection * View * modelPort * positions[i];
                varying_Positions[i] = clip_positions[i].RemoveDimension();
                varying_TextureCoords[i] = textures[i];
                varying_Normals[i] = (modelPort * normals[i].AddDimension()).RemoveDimension();
                varying_Normals[i].Normalize();
            }
            return varying_Positions;
        }
        public void Fragment(Vector pos, Vector LightDir, Matrix transitionMatrix, ViewPoint lightSource = null, bool zBufferOnly = false)
        {
            Vec3 barycentric = GL.barycentric(varying_Positions, pos);
            if (barycentric.x < 0 || barycentric.y < 0 || barycentric.z < 0) return; 
            Vec3 bc_clip = GL.bc_clip(clip_positions, barycentric);
            
            //bc_clip = barycentric;
            Vector position = new Matrix(clip_positions) * bc_clip;
            position = position.RemoveDimension();
            Vector texture = new Matrix(varying_TextureCoords) * bc_clip;
            Vector normal = new Matrix(varying_Normals) * bc_clip;
            normal.Normalize();
            if (viewP.zBuffer[(int)pos[0], (int)pos[1]] <= position[2])
            {
                viewP.zBuffer[(int)pos[0], (int)pos[1]] = position[2];
                if (!zBufferOnly)
                {
                    bool Lightened = checkLighting(lightSource, position, transitionMatrix);
                    float intensity = GetIntensity(LightDir, normal, Lightened);
                    Color pixelColor = GetColor(texture, intensity);
                    viewP.image[(int)pos[0], (int)pos[1]] = pixelColor;
                }
            }
        }
        private bool checkLighting(ViewPoint lightSource, Vector position, Matrix transitionMatrix)
        {
            if (lightSource == null)
                return true;
            else
            {
                return checkIfIsLightened(lightSource, position, transitionMatrix);
            }
        }
        private float GetIntensity(Vector LightDirection, Vector normal, bool isLightened = false)
        {
            float intensity = 1 * this.model.ambient_koef;
            if (isLightened)
            {
                float diffuse_intensity = normal * LightDirection * model.diffuse_koef;
                diffuse_intensity = (diffuse_intensity >= 0) ? diffuse_intensity : 0;
                intensity += diffuse_intensity;
                intensity += GL.GetSpecularIntensity(normal.ToVec3(), viewP.coord.basis[2].ToVec3(), LightDirection.ToVec3(), 8) * model.specular_koef;
            }
            return intensity;
        }

        private bool checkIfIsLightened(ViewPoint lightSource, Vector position, Matrix transitionMatrix)
        {
            if (lightSource == null)
                return true;
            else
            {
                Vector pos = (transitionMatrix * position.AddDimension()).RemoveDimension();
                int i = (int)pos[0];
                int j = (int)pos[1];
                if (i < 0 || j < 0 || i >= lightSource.zBuffer.width - 1 || j >= lightSource.zBuffer.height - 1) return false;
                Vector[] arr = new Vector[3];
                arr[0] = new Vec3(i, j, lightSource.zBuffer[i, j]);
                arr[1] = new Vec3(i + 1, j, lightSource.zBuffer[i + 1, j]);
                arr[2] = new Vec3(i + 1, j + 1, lightSource.zBuffer[i + 1, j + 1]);
                Vec3 bc = GL.barycentric(arr, pos);
                if (bc.x < 0 || bc.y < 0 || bc.z < 0)
                {
                    arr[1] = new Vec3(i, j + 1, lightSource.zBuffer[i, j + 1]);
                    bc = GL.barycentric(arr, pos);
                    float depth = (new Matrix(arr) * bc)[2];
                    if (depth < pos[2] + lightSource.zBufferDepth / 100) return true;
                }
                else
                {
                    float depth = (new Matrix(arr) * bc)[2];
                    if (depth <= pos[2] + (lightSource.zBufferDepth / 100)) return true;
                }
            }
            return false;
        }
        private Color GetColor(Vector textureCoords, float intensity)
        {
            Color textureColor = model.texture[(int)(textureCoords[0] * model.texture.width) % model.texture.width,
                (int)(textureCoords[1] * model.texture.height) % model.texture.height];
            int R = textureColor.R;
            int G = textureColor.G;
            int B = textureColor.B;
            R = (int)(R * intensity);
            G = (int)(G * intensity);
            B = (int)(B * intensity);
            R = (R < 256) ? R : 255;
            G = (G < 256) ? G : 255;
            B = (B < 256) ? B : 255;
            R = (R >= 0) ? R : 0;
            G = (G >= 0) ? G : 0;
            B = (B >= 0) ? B : 0;
            return Color.FromArgb(textureColor.A, R, G, B);
        }
    }
}
