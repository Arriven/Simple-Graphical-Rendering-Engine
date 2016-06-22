using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinearMath;

namespace MyGl
{
    public static class Parser
    {
        public static objModel objParse(string directory)
        {
            string[] file = System.IO.File.ReadAllLines(directory);
            objModel model = new objModel();
            for(int i = 0;i<file.Length; i++)
            {
                if (file[i].Length == 0 || file[i][0] == '#') continue;
                file[i] = file[i].Replace('.', ',');
                if (file[i][0] == 'v')
                {
                    string[] line = file[i].Split(new char[] { ' ' },StringSplitOptions.RemoveEmptyEntries);
                    if (line[0] == "v")
                    {
                        model.positions.Add(new Vector(new float[] { float.Parse(line[1]), float.Parse(line[2]), float.Parse(line[3]) }));
                    }
                    else if (line[0] == "vt")
                    {
                        model.textureCoords.Add(new Vector(new float[] { float.Parse(line[1]), float.Parse(line[2]) }));
                    }
                    else if (line[0] == "vn")
                    {
                        model.normals.Add(new Vector(new float[] { float.Parse(line[1]), float.Parse(line[2]), float.Parse(line[3]) }));
                    }
                }
                else if(file[i][0] == 'f')
                {
                    string[] line = file[i].Split(' ');
                    string[] pt1 = line[1].Split('/');
                    string[] pt2 = line[2].Split('/');
                    string[] pt3 = line[3].Split('/');
                    float[] point1 = new float[pt1.Length];
                    float[] point2 = new float[pt2.Length];
                    float[] point3 = new float[pt3.Length];
                    for(int j = 0; j < pt1.Length; j++)
                    {
                        point1[j] = float.Parse(pt1[j]) - 1;
                        point2[j] = float.Parse(pt2[j]) - 1;
                        point3[j] = float.Parse(pt3[j]) - 1;
                    }
                    Vector[] result = new Vector[pt1.Length];
                    for(int j = 0; j < result.Length; j++)
                    {
                        result[j] = new Vec3(point1[j], point2[j], point3[j]);
                    }
                    Vector[] pts = new Vector[3];
                    pts[0] = new Vector(point1);
                    pts[1] = new Vector(point2);
                    pts[2] = new Vector(point3);
                    model.triangles.Add(result);
                }
            }
            return model;
        }
        public static objModel objParse(string directory, objModel model)
        {
            string[] file = System.IO.File.ReadAllLines(directory);
            for (int i = 0; i < file.Length; i++)
            {
                if (file[i].Length == 0 || file[i][0] == '#') continue;
                file[i] = file[i].Replace('.', ',');
                if (file[i][0] == 'v')
                {
                    string[] line = file[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (line[0] == "v")
                    {
                        model.positions.Add(new Vector(new float[] { float.Parse(line[1]), float.Parse(line[2]), float.Parse(line[3]) }));
                    }
                    else if (line[0] == "vt")
                    {
                        model.textureCoords.Add(new Vector(new float[] { float.Parse(line[1]), float.Parse(line[2]), float.Parse(line[3]) }));
                    }
                    else if (line[0] == "vn")
                    {
                        model.normals.Add(new Vector(new float[] { float.Parse(line[1]), float.Parse(line[2]), float.Parse(line[3]) }));
                    }
                }
                else if (file[i][0] == 'f')
                {
                    string[] line = file[i].Split(' ');
                    string[] pt1 = line[1].Split('/');
                    string[] pt2 = line[2].Split('/');
                    string[] pt3 = line[3].Split('/');
                    float[] point1 = new float[pt1.Length];
                    float[] point2 = new float[pt2.Length];
                    float[] point3 = new float[pt3.Length];
                    for (int j = 0; j < pt1.Length; j++)
                    {
                        point1[j] = float.Parse(pt1[j]) - 1;
                        point2[j] = float.Parse(pt2[j]) - 1;
                        point3[j] = float.Parse(pt3[j]) - 1;
                    }
                    Vector[] result = new Vector[pt1.Length];
                    for (int j = 0; j < result.Length; j++)
                    {
                        result[j] = new Vec3(point1[j], point2[j], point3[j]);
                    }
                    Vector[] pts = new Vector[3];
                    pts[0] = new Vector(point1);
                    pts[1] = new Vector(point2);
                    pts[2] = new Vector(point3);
                    model.triangles.Add(result);
                }
            }
            return model;
        }
    }
}
