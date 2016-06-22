using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinearMath;
using System.Drawing;
using BmpEditor;

namespace MyGl
{
    public class objModel
    {
        public CoordSystem coords;
        public List<Vector> positions;
        public List<Vector> textureCoords;
        public List<Vector> normals;
        public Img texture;
        public List<Vector[]> triangles;

        public float ambient_koef = 0.2f;
        public float diffuse_koef = 0.4f;
        public float specular_koef = 0.4f;

        public objModel()
        {
            positions = new List<Vector>();
            textureCoords = new List<Vector>();
            normals = new List<Vector>();
            triangles = new List<Vector[]>();
            coords = new CoordSystem(CoordSystem.WorldCoordSys.Centre, CoordSystem.WorldCoordSys.basis);
        }
        public void AddTexture(Bitmap img)
        {
            texture = new Img(img);
        }
    }
}
