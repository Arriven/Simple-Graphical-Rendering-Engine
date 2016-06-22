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
    public static class GL
    {
        public static Vec3 barycentric(Vector[] pts, Vector Point)
        {
            Vec3 u = (new Vec3(pts[2][0] - pts[0][0], pts[1][0] - pts[0][0], pts[0][0] - Point[0])) ^ (new Vec3(pts[2][1] - pts[0][1], pts[1][1] - pts[0][1], pts[0][1] - Point[1]));
            if (Math.Abs(u[2]) < 1) return new Vec3(-1, 1, 1);
            return new Vec3(1 - (u.x + u.y) / u.z, u.y / u.z, u.x / u.z);
        }

        public static Vec3 bc_clip(Vector[] pts, Vec3 bar)
        {
            Vec3 bc_clip = new Vec3(bar[0] / pts[0][3], bar[1] / pts[1][3], bar[2] / pts[2][3]);
            bc_clip = (bc_clip * (1 / (bc_clip[0] + bc_clip[1] + bc_clip[2]))).ToVec3();
            return bc_clip;
        }

        public static Matrix projection(float koef)
        {
            Matrix proj = Matrix.E(4);
            proj[3, 2] = koef;
            return proj;
        }

        public static Matrix ViewPort(float x, float y, float w, float h, float d)
        {
            Matrix Viewport = Matrix.E(4);
            Viewport[0, 3] = x + w / 2;
            Viewport[1, 3] = y + h / 2;
            Viewport[2, 3] = d / 2;
            Viewport[0, 0] = w / 2;
            Viewport[1, 1] = h / 2;
            Viewport[2, 2] = d / 2;
            return Viewport;
        }

        public static float GetSpecularIntensity(Vec3 surfNormal, Vec3 viewVector, Vec3 LightDir, float n)
        {
            Vec3 Reflected = (surfNormal + LightDir).ToVec3();
            Reflected = (Reflected * (-1)).ToVec3();
            Reflected.Normalize();
            float intensity = (float)Math.Cos(Reflected * viewVector);
            intensity = (intensity > 0) ? intensity : 0;
            intensity = (float)Math.Pow(intensity, n);
            return intensity;
        }

        public static Color GetColor(Color color, float intensity)
        {
            float R = color.R;
            float G = color.G;
            float B = color.B;
            R = Math.Min(255, R * intensity);
            G = Math.Min(255, G * intensity);
            B = Math.Min(255, B * intensity);
            return Color.FromArgb((int)R, (int)G, (int)B);
        }
    }
}
