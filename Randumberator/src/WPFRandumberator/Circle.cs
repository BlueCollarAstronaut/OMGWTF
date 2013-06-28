using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media.Media3D;
using Petzold.MeshGeometries;

namespace WPFRandumberator
{
    public class Circle
        : ModelBase
    {
        #region Properties

        #region Slices
        public static readonly DependencyProperty SlicesProperty =
            DependencyProperty.Register("Slices", typeof(int), typeof(Circle),
                new PropertyMetadata(12, GeometryInvalidated),
                    ValidateSlices);
        public int Slices
        {
            get { return (int)GetValue(SlicesProperty); }
            set { SetValue(SlicesProperty, value); }
        }
        #endregion

        #region Offset
        public static readonly DependencyProperty OffsetProperty =
            DependencyProperty.Register("Offset", typeof(int), typeof(Circle),
                new PropertyMetadata(1, GeometryInvalidated),
                    ValidateOffset);



        public int Offset
        {
            get { return (int)GetValue(OffsetProperty); }
            set { SetValue(OffsetProperty, value); }
        }
        #endregion
        
        #endregion

        private static bool ValidateSlices(object value)
        {
            return (int) value > 5;
        }

        private static void GeometryInvalidated(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var circle = d as Circle;
            if (circle != null)
            {
                circle.Render();
            }
        }

        private void Render()
        {
            Geometry = GenerateGeometry();
        }


        private MeshGeometry3D GenerateGeometry()
        {
            MeshGeometry3D mesh = new MeshGeometry3D();

            mesh.Positions.Add(new Point3D(0, Offset, 0));
            for (int i = 0; i <= Slices; i++)
            {
                double x = Math.Sin(2*i*Math.PI/Slices);
                double z = Math.Cos(2*i*Math.PI/Slices);

                mesh.Positions.Add(new Point3D(x, Offset, z));

                if (i < Slices)
                {
                    mesh.TriangleIndices.Add(0);
                    mesh.TriangleIndices.Add(i);
                    mesh.TriangleIndices.Add(i + 1);
                }
            }
            return mesh;
        }


        public Circle()
        {
            
        }

        private static bool ValidateOffset(object value)
        {
            return true;
        }        
    }
}
