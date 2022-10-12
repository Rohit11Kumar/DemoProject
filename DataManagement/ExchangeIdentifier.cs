using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace DemoProject
{
    public class ExchangeIdentifier : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the ExchangeIdentifier class.
        /// </summary>
        public ExchangeIdentifier()
          : base("ExchangeIdentifier", "ExchangeIdentifier",
              "Description",
              "DataExchange", "DataManagement")
        {

        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddBoxParameter("Input Exchange Data ", "Identify the Exchange Data", "Enter Exchange Data Details", GH_ParamAccess.item);
            
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddBoxParameter("View ExchangeIdentifierData", "View Excahnge Data", "Exchange Data Details", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon => Properties.Resources.ExchangeIdentifier;
        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("0381FF9C-818B-4568-8630-87685632DBD8"); }
        }
    }
}