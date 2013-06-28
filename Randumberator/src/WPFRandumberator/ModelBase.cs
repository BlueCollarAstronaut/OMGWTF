using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media.Media3D;
using Petzold.MeshGeometries;

namespace WPFRandumberator
{
    public class ModelBase
        : ModelVisual3D
    {
                // Private fields
        MeshGeometry3D meshGeometry;
        GeometryModel3D modelContent;

        // Public parameterless constructor
        public ModelBase()
        {
            meshGeometry = new MeshGeometry3D();
            modelContent = new GeometryModel3D();
            Geometry = meshGeometry;
            Content = modelContent;
        }

        // Material and BackMaterial dependency properties
        public static readonly DependencyProperty GeometryProperty =
            GeometryModel3D.GeometryProperty.AddOwner(typeof(ModelBase),
            new PropertyMetadata(null, OnPropertyChanged));

        public static readonly DependencyProperty MaterialProperty =
            GeometryModel3D.MaterialProperty.AddOwner(typeof(ModelBase),
            new PropertyMetadata(null, OnPropertyChanged));

        public static readonly DependencyProperty BackMaterialProperty =
            GeometryModel3D.BackMaterialProperty.AddOwner(typeof(ModelBase),
            new PropertyMetadata(null, OnPropertyChanged));

        // Material and BackMaterial CLR properties
        public Geometry3D Geometry
        {
            get { return (Geometry3D)GetValue(GeometryProperty); }
            set { SetValue(GeometryProperty, value); }
        }
        public Material Material
        {
            get { return (Material)GetValue(MaterialProperty); }
            set { SetValue(MaterialProperty, value); }
        }
        public Material BackMaterial
        {
            get { return (Material)GetValue(BackMaterialProperty); }
            set { SetValue(BackMaterialProperty, value); }
        }

        // Material and BackMaterial OnChanged handler.
        static void OnPropertyChanged(DependencyObject obj,
                            DependencyPropertyChangedEventArgs args)
        {
            var modbase = (ModelBase)obj;

            if (args.Property == GeometryProperty)
                modbase.modelContent.Geometry = (Geometry3D)args.NewValue;

            else if (args.Property == MaterialProperty)
                modbase.modelContent.Material = (Material)args.NewValue;

            else
                modbase.modelContent.BackMaterial = (Material)args.NewValue;
        }
    }
}
