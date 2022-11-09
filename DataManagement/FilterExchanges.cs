using System;
using System.Collections.Generic;
using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.GUI;
using Rhino.Display;
namespace DemoProject
{
    public class FilterExchanges : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the FilterExchanges class.
        /// </summary>
        public FilterExchanges()
          : base("FilterExchanges", "FilterExchange",
              "Description",
              "DataExchange", "DataManagement")
        {
        }

        private bool m_enabledState = true;

        protected override void AppendAdditionalComponentMenuItems(System.Windows.Forms.ToolStripDropDown menu)
        {
            base.AppendAdditionalComponentMenuItems(menu);
            Menu_AppendItem(menu, "Custom Enabled", Menu_EnabledClicked, true, m_enabledState);
        }
        private void Menu_EnabledClicked(object sender, EventArgs e)
        {
            RecordUndoEvent("Enabled Changed");
            m_enabledState = !m_enabledState;
            ExpireSolution(true);
        }

        public override bool Write(GH_IO.Serialization.GH_IWriter writer)
        {
            writer.SetBoolean("EnabledState", m_enabledState);
            return base.Write(writer);
        }
        public override bool Read(GH_IO.Serialization.GH_IReader reader)
        {
            m_enabledState = true;
            reader.TryGetBoolean("EnabledState", ref m_enabledState);
            return base.Read(reader);
        }
        public override void CreateAttributes()
        {
            m_attributes = new CustomUI.ButtonUIAttributes(this, "Click", FunctionToRunOnClick, "Opt description");
           // m_attributes = new CustomUI.SliderUIAttributes(this, SetVal, SetMaxMin, Value, MinValue, MaxValue, noDigits, "Opt description");
        }
        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
       

        public void FunctionToRunOnClick()
        {
            System.Windows.Forms.MessageBox.Show("Button was clicked");
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddBoxParameter("Account", "Accountdetails", "Enter Account Details", GH_ParamAccess.item);
            pManager.AddBoxParameter("Excahnge", "Exchange Details", "Provide Exchange Data", GH_ParamAccess.item);
          
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddBoxParameter("Result", "View Excahnge Data", "Exchange Data Details", GH_ParamAccess.item);
           
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
        protected override System.Drawing.Bitmap Icon => Properties.Resources.FilterExchanges;
       
        
        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("F84CF780-8345-4F71-A66E-2C6F60630092"); }
        }
    }
}