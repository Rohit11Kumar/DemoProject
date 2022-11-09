using System;
using System.Collections.Generic;
using System.IO;
using DemoProject.Properties;
using Grasshopper.Kernel;
using NHibernate.Mapping.ByCode;
using Rhino.FileIO;
using Rhino.Geometry;
using System.Linq;
using Rhino.DocObjects;
using Rhino;

namespace DemoProject.DataManagement
{
    public class ReadStep : GH_Component
    {
       

        /// <summary>
        /// Initializes a new instance of the ReadStep class.
        /// </summary>
        public ReadStep()
          : base("ReadStep", "Stp File",
              "This is for Reading Step File",
              "DataExchange", "DataManagement")
        {

        }
        
        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        /// 

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("file","Step","Read Step File",GH_ParamAccess.list);
            pManager.AddBooleanParameter("import", "ImportStep", "Read Step File", GH_ParamAccess.list);
            
           // pManager.AddTextParameter("reset", "resetButton", "It is used to reset the model", GH_ParamAccess.list);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGeometryParameter("Geometry", "Read Stp File", "View the Stp Details", GH_ParamAccess.list);
        }


 
        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<string> input = new List<string>();
            var check = DA.GetDataList(0, input);
            List<GeometryBase> geometryBases = new List<GeometryBase>();
            foreach (string inputFile in input)
            {
                geometryBases = RunScript(inputFile);
            }

            DA.SetDataList(0, geometryBases);
               // private List<GeometryBase> geos = new List<GeometryBase>();
        }
        List<GeometryBase> RunScript(string file)
        {
            //if (import)
            //{
            //RhinoDoc.ActiveDoc.Objects.UnselectAll();
            //string cmd = "!_-Import" + "\" " + file + "\" " + "Enter";
            //Rhino.RhinoApp.RunScript(cmd, false);
            RhinoApp.RunScript($@"-Import {file} _Enter", false);
            //RhinoDoc.ActiveDoc.Objects.UnselectAll();
            //string cmd = "!_-Import " + "\"" + file + "\"" + " _Enter";
            //Rhino.RhinoApp.RunScript(cmd, false);
            //RhinoDoc selectedObjs = RhinoDoc.ActiveDoc;
            //var rhinoObject = selectedObjs;
            var selectedObjs = RhinoDoc.ActiveDoc.Objects.GetSelectedObjects(false, false).ToList();
            List<RhinoObject> explodedRhinoObjList = new List<RhinoObject>();
            foreach (RhinoObject selectedObj in selectedObjs)
            {                
                if (selectedObj.ObjectType == ObjectType.InstanceReference)
                {
                    InstanceObject instanceObject = (InstanceObject)selectedObj;
                    instanceObject.Explode(true, out RhinoObject[] objectListFromInstance, out ObjectAttributes[] pieceAttributes, out Transform[] pieceTransforms);
                    foreach (RhinoObject explodedRhinoObj in objectListFromInstance)
                    {
                        explodedRhinoObjList.Add(explodedRhinoObj);
                    }
                }
                else
                {
                    explodedRhinoObjList.Add(selectedObj);
                }
            }
            List<GeometryBase> geos = new List<GeometryBase>();
            foreach (RhinoObject selectedExplodedRhinoObj in explodedRhinoObjList)
            {
                var geo = selectedExplodedRhinoObj.Geometry;
                geos.Add(geo);
                RhinoDoc.ActiveDoc.Objects.Delete(selectedExplodedRhinoObj, true);
            }
            return geos;
        }
        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                //You can add image files to your project resources and access them like this:
                // return Resources.IconForThisComponent;
                return null;
            }
        }
        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("3C9A1626-B296-438D-938C-6315F8886C32"); }
        }
    }
}