<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>Salary Report</title>    
    <script src="../../Scripts/jquery-1.8.2.js"></script>
    <style>
        #Pde2ee3bf92c041c8a4e66550ffe87cb7_1_oReportDiv * {
    box-sizing:content-box;
}
    </style>
    
    <script runat="server">        
        void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {  
                using ( HRAndPayroll.Entities db = new HRAndPayroll.Entities())
                {
                    var ReportData = (from EM in db.Employee_Masters
                                      join SS in db.Salary_Sheets
                                          on EM.Employee_Id equals SS.Employee_Id
                                      join DM in db.Designation_Masters.Distinct()
                                          on EM.Designation_Id equals DM.Designation_Id
                                      select new HRAndPayroll.Models.clsSalarySheet
                                      {
                                          Employee_Code = EM.Employee_Code,
                                          Employee_Id = EM.Employee_Id,
                                          Employee_Name = EM.First_Name + " " + EM.Last_Name,
                                          Designation = DM.Designation_Name,
                                          //Salary_Month = (Int32)SS.Salary_Month,
                                          //Salary_Year = (Int32)SS.Salary_Year,
                                          Gross_Salary = (Int32)SS.Gross_Salary,
                                          Days_Payable = (Double)SS.Days_Payable,
                                          Incentive = (Double)SS.Incentive,
                                          TDS = (Double)SS.TDS,
                                          Net_Payable = (Double)SS.Net_Payable,
                                          ESI_EE_Share = (Double)SS.ESI_EE_Share,
                                          ESI_EY_Share = (Double)SS.ESI_EY_Share,
                                          EPF_EE_Share = (Double)SS.EPF_EE_Share,
                                          EPF_EY_Share = (Double)SS.EPF_EY_Share,
                                          DatePaid = (DateTime)SS.DatePaid,
                                          DateTimeLastUpdated = (DateTime)SS.DateTimeLastUpdated
                                      }).Distinct().ToList();
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/Salary_Sheet.rdlc");
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportDataSource reportDataSource = new ReportDataSource("DataSet1", ReportData);;
                    ReportViewer1.LocalReport.DataSources.Add(reportDataSource);
                    ReportViewer1.LocalReport.Refresh();
                }
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width:100%; overflow:scroll; overflow-x:scroll; overflow-y:hidden;">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" AsyncRendering="false" SizeToReportContent="true"></rsweb:ReportViewer>            
    </div>
    </form>
</body>
</html>