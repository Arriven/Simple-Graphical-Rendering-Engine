using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearMath
{
    public class CoordSystem
    {
        public Vec3 Centre;
        public Matrix basis;
        public CoordSystem(Vec3 cent, Matrix bas)
        {
            basis = new Matrix(bas.GetContainer(), bas.width, bas.height);
            Centre = new Vec3(cent.GetContainer());
        }

        public Matrix World_Sys_Trans()
        {
            Matrix trans = this.basis.AddDimension().Transponated();
            Matrix move = (-1 * this.Centre).GetMoveMatrix();
            return trans * move;
        }
        public Matrix Sys_World_Trans()
        {
            return World_Sys_Trans().Inversed();
        }
        public Matrix GetBasisChangeMatrixTo(CoordSystem Destination)
        {
            return this.basis.Inversed() * Destination.basis;
        }
        public Matrix GetTransitionMatrixTo(CoordSystem Destination)
        {
            Vec3 DestinationCentreInThisSystem = (this.basis * (this.Centre - Destination.Centre)).ToVec3();
            Matrix moveMatrix = DestinationCentreInThisSystem.GetMoveMatrix();
            return this.GetBasisChangeMatrixTo(Destination).AddDimension() * moveMatrix;
        }
        public Matrix GetBasisChangeMatrixFrom(CoordSystem Source)
        {
            return Source.basis.Inversed() * this.basis;
        }
        public Matrix GetTransitionMatrixFrom(CoordSystem Source)
        {
            return Source.GetTransitionMatrixTo(this);
        }


        public static readonly Vec3 i = new Vec3(1, 0, 0);
        public static readonly Vec3 j = new Vec3(0, 1, 0);
        public static readonly Vec3 k = new Vec3(0, 0, 1);
        public static readonly CoordSystem WorldCoordSys = new CoordSystem(new Vec3(0, 0, 0), new Matrix(new Vec3[] { i, j, k }));
    }
}
